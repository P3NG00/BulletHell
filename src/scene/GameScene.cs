using System.Collections.Generic;
using BulletHell.Entities;
using BulletHell.Input;
using BulletHell.Utils;
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

        public sealed override string[] ExtraDebugInfo => new[] {
            $"paused: {_paused}",
            $"entities: {_entities.Count}",
            $"player_vel_x: {_player.Velocity.X:0.000}",
            $"player_vel_y: {_player.Velocity.Y:0.000}",
            $"player_x: {_player.Position.X:0.000}",
            $"player_y: {_player.Position.Y:0.000}"};

        public GameScene()
        {
            this.SingletonCheck(ref _instance);
            // TODO remove code below, only here to test enemies movement
            var startPos = Display.WindowSize.ToVector2() / 2f;
            AddEntity(new Enemy(startPos * new Vector2(-1)));
            AddEntity(new Enemy(startPos * new Vector2(-1, 1)));
            AddEntity(new Enemy(startPos * new Vector2(1, -1)));
            AddEntity(new Enemy(startPos));
        }

        public sealed override void Update()
        {
            // toggle pause
            if (Keybinds.Pause.PressedThisFrame)
                Util.Toggle(ref _paused);
            // paused
            if (_paused)
            {
                _buttonResume.Update();
                _buttonExit.Update();
            }
        }

        public sealed override void Tick()
        {
            // check pause
            if (_paused)
                return;
            // tick player
            _player.Tick();
            // tick entities
            _entities.ForEach(entity => entity.Tick());
            // check projectile
            if (Keybinds.MouseLeft.PressedThisFrame)
            {
                var direction = InputManager.MousePosition.ToVector2() + Display.CameraOffset;
                direction.Y *= -1f;
                direction -= _player.Center;
                GameScene.AddEntity(new Projectile(_player.Center, direction));
            }
            // update camera offset
            Display.UpdateCameraOffset(_player.Center);
        }

        public sealed override void Draw()
        {
            // draw player
            _player.Draw();
            // draw entities
            _entities.ForEach(entity => entity.Draw());
            // check pause
            if (!_paused)
                return;
            // draw overlay
            Display.DrawFadedOverlay();
            // draw paused text
            FontType.VeniceClassic.DrawCenteredString(new(0.5f, 0.4f), "paused", Colors.UI_Text, new(3), drawStringFunc: Fonts.DrawStringWithShadow);
            // draw buttons
            _buttonResume.Draw();
            _buttonExit.Draw();
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
