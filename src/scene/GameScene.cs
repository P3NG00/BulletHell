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

        public static GameScene Instance => _instance;

        private readonly Button _buttonResume = new(new(0.5f, 0.6f), new(250, 100), "resume", Colors.ThemeDefault, ResumeGame, 3);
        private readonly Button _buttonExit = CreateExitButton(BackToMainMenu);
        private readonly List<AbstractEntity> _entities = new();
        private readonly Player _player = new();

        private static bool s_paused;

        public sealed override string[] ExtraDebugInfo => new[] {
            $"paused: {s_paused}",
            $"entities: {_entities.Count}",
            $"velocity_x: {_player.Velocity.X:0.000}",
            $"velocity_y: {_player.Velocity.Y:0.000}",
            $"x: {_player.Position.X:0.000}",
            $"y: {_player.Position.Y:0.000}"};

        public GameScene()
        {
            this.SingletonCheck(ref _instance);
            s_paused = false;
            Display.UpdateCameraOffset(Vector2.Zero);
        }

        public sealed override void Update()
        {
            // toggle pause
            if (Keybinds.Pause.PressedThisFrame)
                Util.Toggle(ref s_paused);
            // paused
            if (s_paused)
            {
                _buttonResume.Update();
                _buttonExit.Update();
            }
        }

        public sealed override void Tick()
        {
            if (s_paused)
                return;
            _player.Tick();
            _entities.ForEach(entity => entity.Tick());
        }

        public sealed override void Draw()
        {
            // draw player
            _player.Draw();
            // draw entities
            _entities.ForEach(entity => entity.Draw());
            // check pause
            if (!s_paused)
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

        private static void ResumeGame() => s_paused = false;

        public static void AddEntity(AbstractEntity entity) => _instance._entities.Add(entity);
    }
}
