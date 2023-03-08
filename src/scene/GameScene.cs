using System.Collections.Generic;
using BulletHell.Game.Entities;
using BulletHell.Game.Entities.Enemies;
using BulletHell.Game.Waves;
using BulletHell.Game.Weapon;
using BulletHell.Input;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Scenes
{
    public sealed class GameScene : AbstractScene
    {
        private const float MAX_MOUSE_OFFSET = 50f;
        private const int TILE_SIZE = 64;

        private static readonly int CleanupInterval = GameManager.SecondsToTicks(5f);
        private static GameScene _instance;

        public static Player Player => _instance._player;

        public static float Score;

        private readonly Button _buttonResume = CreateMainButton("resume", Colors.ThemeDefault, ResumeGame);
        private readonly Button _buttonRestart = CreateMainButton("restart", Colors.ThemeGreen, NewGame);
        private readonly Button _buttonMainMenu = CreateExitButton(BackToMainMenu);
        private readonly List<Projectile> _projectiles = new();
        private readonly List<AbstractEnemy> _enemies = new();

        private Player _player;
        private Vector2 _mouseOffset;
        private bool _paused;
        private int _cleanupTicks;
        private int _lastTilesDrawn;

        public sealed override (string, string[])[] ExtraDebugInfo => new[] {
            ("game",
            new[] {
                $"paused: {_paused}",
                $"score: {Score}",
                $"last_tiles_drawn: {_lastTilesDrawn}",
                $"cleanup_ticks: {_cleanupTicks}",
                $"projectiles: {_projectiles.Count}",
                $"enemies: {_enemies.Count}",
            }),
            ("weapon",
            new[] {
                $"weapon: {WeaponManager.Weapon.Name}",
                $"clip_amount: {WeaponManager.AmmoAmount}",
                $"switch_ticks: {WeaponManager.SwitchTicks}",
                $"reload_ticks: {WeaponManager.ReloadTicks}",
                $"next_shot_ticks: {WeaponManager.NextShotTicks}",
            }),
            ("player",
            new[] {
                $"velocity_x: {_player.Velocity.X:0.000}",
                $"velocity_y: {_player.Velocity.Y:0.000}",
                $"pos_x: {_player.Position.X:0.000}",
                $"pos_y: {_player.Position.Y:0.000}",
                $"life: {_player.Life:0.000}",
                $"invincible_ticks: {_player.InvincibilityTicks}",
                $"dash_ticks: {_player.DashTicks}",
                $"dash_cooldown_ticks: {_player.DashCooldownTicks}",
            }),
            ("wave",
            new[] {
                $"wave: {WaveManager.CurrentWave}",
                $"wave_ticks: {WaveManager.CurrentWaveTicks}",
                $"spawn_distance: {WaveManager.SpawnDistance:0.000}",
            }),
        };

        // TODO add health pickups that spawn randomly and rarely

        public GameScene()
        {
            this.SingletonCheck(ref _instance);
            NewGame();
        }

        // TODO implement scroll wheel for zooming in and out
        public sealed override void HandleInput()
        {
            // toggle pause
            if (Keybinds.Pause.PressedThisFrame)
            {
                Util.Toggle(ref _paused);
                if (_paused)
                {
                    _buttonResume.ResetMouseLock();
                    _buttonMainMenu.ResetMouseLock();
                }
            }
            // paused
            if (_paused)
            {
                _buttonResume.HandleInput();
                _buttonMainMenu.HandleInput();
                return;
            }
            // if dead
            if (!_player.Alive)
            {
                _buttonRestart.HandleInput();
                _buttonMainMenu.HandleInput();
                return;
            }
            // update weapon
            WeaponManager.HandleInput();
            // update wave manager
            WaveManager.HandleInput();
            // update player
            _player.HandleInput();
        }

        public sealed override void Tick()
        {
            // check pause. check instance because it's referenced in following function calls
            if (_paused || _instance == null)
                return;
            // if alive
            var playerAlive = _player.Alive;
            if (playerAlive)
            {
                // tick player
                _player.Tick();
                // tick weapon
                WeaponManager.Tick();
                // tick wave
                WaveManager.Tick();
            }
            // tick entities
            _enemies.ForEach(enemy => enemy.Tick());
            _projectiles.ForEach(projectile => projectile.Tick());
            // check collisions
            HandleCollisions();
            // tick cleanup
            if (--_cleanupTicks <= 0)
                Cleanup();
            // update camera offset
            var mouseOffset = playerAlive ? InputManager.MousePositionOffset - _player.Position : Vector2.Zero;
            if (mouseOffset.Length() > MAX_MOUSE_OFFSET)
                mouseOffset = Vector2.Normalize(mouseOffset) * MAX_MOUSE_OFFSET;
            _mouseOffset = Vector2.Lerp(_mouseOffset, mouseOffset, 0.1f);
            Display.UpdateCameraOffset(_player.Position + _mouseOffset);
        }

        public sealed override void Draw()
        {
            // draw game
            DrawGame();
            // TODO DrawUI() (draw score, health, weapon, life, etc.)
            // draw pause
            DrawPause();
        }

        public sealed override void OnLostFocus()
        {
            if (_player.Alive)
                _paused = true;
        }

        private void DrawGame()
        {
            // draw background
            DrawBackground();
            // draw player
            _player.Draw(); // TODO draw line to show where player is aiming
            // draw entities
            _enemies.ForEach(enemy => enemy.Draw());
            _projectiles.ForEach(projectile => projectile.Draw());
            // check alive
            if (_player.Alive)
                return;
            Display.DrawFadedOverlay();
            // TODO display game over reason: death from projectile, death from enemy, game end success, etc.
            // draw score
            FontType.VeniceClassic.DrawCenteredString(new(0.5f, 0.4f), $"score: {Score}", Colors.Text, new(4), drawStringFunc: Fonts.DrawStringWithShadow);
            // draw buttons
            _buttonRestart.Draw();
            _buttonMainMenu.Draw();
        }

        private void DrawBackground()
        {
            // TODO needs a little further fine tuning
            var startX = (-Display.CameraOffset.X % TILE_SIZE) - TILE_SIZE;
            var startY = (-Display.CameraOffset.Y % TILE_SIZE) - TILE_SIZE;
            var endX = Display.WindowSize.X + TILE_SIZE;
            var endY = Display.WindowSize.Y + TILE_SIZE;
            var drawPos = new Vector2(startX, startY);
            var drawSize = new Vector2(TILE_SIZE);
            var drawData = new DrawData(Textures.SquareShaded, Colors.Background);
            _lastTilesDrawn = 0;
            for (float y = startX; y < endY; y += TILE_SIZE)
            {
                for (float x = startY; x < endX; x += TILE_SIZE)
                {
                    Display.Draw(drawPos, drawSize, drawData);
                    _lastTilesDrawn++;
                    drawPos.X += TILE_SIZE;
                }
                drawPos.X = startX;
                drawPos.Y += TILE_SIZE;
            }
        }

        private void DrawPause()
        {
            if (!_paused)
                return;
            // draw overlay
            Display.DrawFadedOverlay();
            // draw paused text
            FontType.VeniceClassic.DrawCenteredString(new(0.5f, 0.4f), "paused", Colors.Text, new(3), drawStringFunc: Fonts.DrawStringWithShadow);
            // draw buttons
            _buttonResume.Draw();
            _buttonMainMenu.Draw();
        }

        // TODO optimize collision checking
        private void HandleCollisions()
        {
            // handle projectiles
            for (int i = 0; i < _projectiles.Count; i++)
            {
                var projectile = _projectiles[i];
                var projectileDamage = projectile.ProjectileDamage;
                if (!projectile.Alive)
                    continue;
                // check against enemies
                for (int j = 0; j < _enemies.Count; j++)
                {
                    var enemy = _enemies[j];
                    if (!enemy.Alive || projectile.SourceEntity is AbstractEnemy || !projectile.CollidesWith(enemy))
                        continue;
                    enemy.Damage(projectileDamage);
                    projectile.Kill();
                    break;
                }
                // check against player
                if (projectile.SourceEntity is not AbstractEnemy || !projectile.CollidesWith(_player))
                    continue;
                _player.Damage(projectileDamage);
                projectile.Kill();
            }
            // handle entity collisions
            for (int i = 0; i < _enemies.Count; i++)
            {
                var enemy = _enemies[i];
                if (!enemy.Alive)
                    continue;
                // check against other enemies
                for (int j = i + 1; j < _enemies.Count; j++)
                {
                    var enemy0 = _enemies[j];
                    if (!enemy0.Alive)
                        continue;
                    if (enemy.CollidesWith(enemy0))
                    {
                        var direction = Vector2.Normalize(enemy0.Position - enemy.Position);
                        var newPos = enemy.Position + (direction * (enemy.Radius + enemy0.Radius));
                        enemy0.Position = newPos;
                    }
                }
                // check against player
                if (enemy.CollidesWith(_player))
                {
                    var direction = Vector2.Normalize(enemy.Position - _player.Position);
                    var newPos = _player.Position + (direction * (enemy.Radius + _player.Radius));
                    enemy.Position = newPos;
                    _player.Damage(enemy.EnemyDamage);
                }
            }
        }

        private void Cleanup()
        {
            _cleanupTicks = CleanupInterval;
            _enemies.RemoveAll(enemy => !enemy.Alive);
            _projectiles.RemoveAll(projectile => !projectile.Alive);
        }

        private static void NewGame()
        {
            var inst = _instance;
            inst._player = new();
            inst._projectiles.Clear();
            inst._enemies.Clear();
            inst._mouseOffset = Vector2.Zero;
            inst._cleanupTicks = CleanupInterval;
            inst._paused = false;
            inst._buttonMainMenu.ResetMouseLock();
            inst._buttonRestart.ResetMouseLock();
            WeaponManager.Reset();
            WaveManager.SetWave(0);
            Score = 0f;
        }

        private static void BackToMainMenu() => SceneManager.Scene = new MainMenuScene();

        private static void ResumeGame() => _instance._paused = false;

        public static void NullifySingleton() => _instance = null;

        public static void AddEnemy(AbstractEnemy enemy) => _instance._enemies.Add(enemy);

        public static void AddProjectile(Projectile projectile) => _instance._projectiles.Add(projectile);
    }
}
