using System.Collections.Generic;
using BulletHell.Entities;
using BulletHell.Input;
using BulletHell.Utils;

namespace BulletHell.Scenes
{
    public sealed class GameScene : AbstractSimpleScene
    {
        private static GameScene _instance;

        public static GameScene Instance => _instance;

        private readonly Button _buttonResume = new(new(0.5f, 0.6f), new(250, 100), "resume", Colors.ThemeDefault, ResumeGame, 3);
        private readonly Button _buttonExit = CreateExitButton(BackToMainMenu);
        private readonly List<AbstractEntity> _entities = new();
        private readonly Player _player = new();

        private bool _paused;

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
            _paused = false;
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
            if (_paused)
                return;
            _player.Tick();
            _entities.ForEach(entity => entity.Tick());
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

        private static void ResumeGame() => Instance._paused = false;

        public static void AddEntity(AbstractEntity entity) => Instance._entities.Add(entity);
    }
}
