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

        public static int Score;

        private readonly Button _buttonResume = CreateMainButton("resume", Colors.ThemeDefault, ResumeGame);
        private readonly Button _buttonExit = CreateExitButton(BackToMainMenu);
        private readonly List<Projectile> _projectiles = new();
        private readonly List<AbstractEnemy> _enemies = new();
        private readonly Player _player = new();

        private Vector2 _mouseOffset = Vector2.Zero;
        private bool _paused = false;
        private int _cleanupTicks = CleanupInterval;
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
            }),
            ("wave",
            new[] {
                $"wave: {WaveManager.CurrentWave}",
                // $"enemy_health: {WaveManager.EnemyHealth}", // TODO remove or fix with new entity system
                $"wave_ticks: {WaveManager.CurrentWaveTicks}",
                $"spawn_ticks: {WaveManager.NextSpawnTicks}",
                $"spawn_distance: {WaveManager.SpawnDistance:0.000}",
            }),
        };

        public GameScene() // TODO take DeathReason as parameter to display accurate death message. make DeathReason enum
        {
            this.SingletonCheck(ref _instance);
            WeaponManager.Reset();
            WaveManager.Reset();
            Score = 0;
        }

        // TODO implement scroll wheel for zooming in and out
        public sealed override void Update()
        {
            // toggle pause
            if (Keybinds.Pause.PressedThisFrame)
            {
                Util.Toggle(ref _paused);
                if (_paused)
                {
                    _buttonResume.ResetMouseLock();
                    _buttonExit.ResetMouseLock();
                }
            }
            // paused
            if (_paused)
            {
                _buttonResume.Update();
                _buttonExit.Update();
                return;
            }
            // update weapon
            WeaponManager.Update();
            // update wave manager
            WaveManager.Update();
            // update player
            _player.Update();
        }

        public sealed override void Tick()
        {
            // check pause
            if (_paused || _instance == null)
                return;
            // tick player
            _player.Tick();
            // tick weapon
            WeaponManager.Tick();
            // tick entities
            _enemies.ForEach(enemy => enemy.Tick());
            _projectiles.ForEach(projectile => projectile.Tick());
            // tick wave
            WaveManager.Tick();
            // check collisions
            HandleCollisions();
            // tick cleanup
            if (--_cleanupTicks <= 0)
                Cleanup();
            // update camera offset
            var mouseOffset = InputManager.MousePositionOffset - _player.Position;
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

        private void DrawGame()
        {
            // draw background
            DrawBackground();
            // draw player
            _player.Draw(); // TODO draw line to show where player is aiming
            // draw entities
            _enemies.ForEach(enemy => enemy.Draw());
            _projectiles.ForEach(projectile => projectile.Draw());
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
            Display.DrawPauseOverlay();
            // draw paused text
            FontType.VeniceClassic.DrawCenteredString(new(0.5f, 0.4f), "paused", Colors.UI_Text, new(3), drawStringFunc: Fonts.DrawStringWithShadow);
            // draw buttons
            _buttonResume.Draw();
            _buttonExit.Draw();
        }

        // TODO optimize collision checking
        private void HandleCollisions()
        {
            // handle projectiles
            for (int i = 0; i < _projectiles.Count; i++)
            {
                var projectile = _projectiles[i];
                if (!projectile.Alive)
                    continue;
                for (int j = 0; j < _enemies.Count; j++)
                {
                    var enemy = _enemies[j];
                    if (!enemy.Alive)
                        continue;
                    if (projectile.CollidesWith(enemy))
                    {
                        enemy.Damage(); // TODO pass damage amount from weapon type
                        projectile.Kill();
                        break;
                    }
                }
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
                    _player.Damage(); // TODO pass enemy damage
                }
            }
        }

        private void Cleanup()
        {
            _cleanupTicks = CleanupInterval;
            _enemies.RemoveAll(enemy => !enemy.Alive);
            _projectiles.RemoveAll(projectile => !projectile.Alive);
        }

        private static void BackToMainMenu() => SceneManager.Scene = new MainMenuScene();

        private static void ResumeGame() => _instance._paused = false;

        public static void NullifySingleton() => _instance = null;

        public static void AddEnemy(AbstractEnemy enemy) => _instance._enemies.Add(enemy);

        public static void AddProjectile(Projectile projectile) => _instance._projectiles.Add(projectile);
    }
}
