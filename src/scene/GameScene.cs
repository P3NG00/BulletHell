using System.Collections.Generic;
using BulletHell.Entities;
using BulletHell.Input;
using BulletHell.Utils;
using BulletHell.Weapon;
using Microsoft.Xna.Framework;

namespace BulletHell.Scenes
{
    public sealed class GameScene : AbstractScene
    {
        private static readonly int CleanupInterval = GameManager.SecondsToTicks(5f);
        private static GameScene _instance;

        public static Player Player => _instance._player;

        private readonly Button _buttonResume = CreateMainButton("resume", Colors.ThemeDefault, ResumeGame);
        private readonly Button _buttonExit = CreateExitButton(BackToMainMenu);
        private readonly List<Projectile> _projectiles = new();
        private readonly List<Enemy> _enemies = new();
        private readonly Player _player = new();

        private bool _paused = false;
        private int _cleanupTicks = CleanupInterval;
        private int _lastTilesDrawn;

        public sealed override string[] ExtraDebugInfo => new[] {
            $"paused: {_paused}",
            $"last_tiles_drawn: {_lastTilesDrawn}",
            $"cleanup_ticks: {_cleanupTicks}",
            $"projectiles: {_projectiles.Count}",
            $"enemies: {_enemies.Count}",
            $"weapon: {WeaponManager.Weapon.Name}",
            $"next_shot_ticks: {WeaponManager.NextShotTicks}",
            $"reload_ticks: {WeaponManager.ReloadTicks}",
            $"clip_amount: {WeaponManager.AmmoAmount}",
            $"player_vel_x: {_player.Velocity.X:0.000}",
            $"player_vel_y: {_player.Velocity.Y:0.000}",
            $"player_x: {_player.Position.X:0.000}",
            $"player_y: {_player.Position.Y:0.000}",
            $"player_life: {_player.Life:0.000}"};

        public GameScene()
        {
            this.SingletonCheck(ref _instance);
            WeaponManager.Reset();
            // TODO add method to spawn Enemies at random positions around player
        }

        public sealed override void Update()
        {
            // toggle pause
            if (Keybinds.Pause.PressedThisFrame)
                Util.Toggle(ref _paused);
            // update weapon
            WeaponManager.Update();
            // paused
            if (_paused)
            {
                _buttonResume.Update();
                _buttonExit.Update();
                return;
            }
            // spawn enemy with right click // TODO remove, only for testing
            if (Keybinds.MouseRight.PressedThisFrame)
                _enemies.Add(new Enemy(InputManager.MousePositionOffset));
        }

        public sealed override void Tick()
        {
            // check pause
            if (_paused)
                return;
            // tick player
            _player.Tick();
            // tick weapon
            WeaponManager.Tick();
            // tick entities
            _enemies.ForEach(enemy => enemy.Tick());
            _projectiles.ForEach(projectile => projectile.Tick());
            // tick cleanup
            if (--_cleanupTicks <= 0)
            {
                _cleanupTicks = CleanupInterval;
                Cleanup();
            }
            // update camera offset
            Display.UpdateCameraOffset(_player.Position);
        }

        public sealed override void Draw()
        {
            // draw game
            DrawGame();
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
            const int TILE_SIZE = 64;
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

        private void Cleanup()
        {
            _enemies.RemoveAll(enemy => !enemy.Alive);
            _projectiles.RemoveAll(projectile => !projectile.Alive);
        }

        private static void BackToMainMenu()
        {
            SceneManager.Scene = new MainMenuScene();
            // nullify singleton
            _instance = null;
        }

        private static void ResumeGame() => _instance._paused = false;

        public static void AddEnemy(Enemy enemy) => _instance._enemies.Add(enemy);

        public static void AddProjectile(Projectile projectile) => _instance._projectiles.Add(projectile);
    }
}
