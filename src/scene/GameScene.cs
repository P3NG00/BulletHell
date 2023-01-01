using System.Collections.Generic;
using BulletHell.Entities;
using BulletHell.Input;
using BulletHell.Utils;
using BulletHell.Weapon;
using Microsoft.Xna.Framework;

namespace BulletHell.Scenes
{
    public sealed class GameScene : AbstractSimpleScene
    {
        private static GameScene _instance;

        public static Player Player => _instance._player;

        private readonly Button _buttonResume = new(new(0.5f, 0.6f), new(250, 100), "resume", Colors.ThemeDefault, ResumeGame, 3);
        private readonly Button _buttonExit = CreateExitButton(BackToMainMenu);
        private readonly List<AbstractEntity> _entities = new();
        private readonly Player _player = new();

        private bool _paused = false;
        private int _lastTilesDrawn;

        public sealed override string[] ExtraDebugInfo => new[] {
            $"paused: {_paused}",
            $"last_tiles_drawn: {_lastTilesDrawn}",
            $"entities: {_entities.Count}",
            $"next_shot_ticks: {WeaponManager.NextShotTicks}",
            $"player_vel_x: {_player.Velocity.X:0.000}",
            $"player_vel_y: {_player.Velocity.Y:0.000}",
            $"player_x: {_player.Position.X:0.000}",
            $"player_y: {_player.Position.Y:0.000}"};

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
            // paused
            if (!_paused)
                return;
            _buttonResume.Update();
            _buttonExit.Update();
        }

        public sealed override void Tick()
        {
            // check pause
            if (_paused)
                return;
            // tick player
            _player.Tick();
            // tick entities
            _entities.RemoveAll(TickEntityAndCheckAlive);
            // tick weapon
            WeaponManager.Tick();
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
            _player.Draw();
            // draw entities
            _entities.ForEach(entity => entity.Draw());
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

        private bool TickEntityAndCheckAlive(AbstractEntity entity)
        {
            entity.Tick();
            return !entity.Alive;
        }

        private static void BackToMainMenu()
        {
            SceneManager.SetScene(new MainMenuScene());
            // nullify singleton
            _instance = null;
        }

        private static void ResumeGame() => _instance._paused = false;

        public static void AddEntity(AbstractEntity entity) => _instance._entities.Add(entity);
    }
}
