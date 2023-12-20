using System;
using BulletHell.Input;
using BulletHell.Utils;

namespace BulletHell.Scenes
{
    public sealed class MainMenuScene : AbstractScene
    {
        // frequency of title rotation
        private const float TITLE_ROT_FREQ = 0.025f;
        // amplitude of title rotation
        private const float TITLE_ROT_AMP = 0.1f;

        private readonly Button _buttonStart = CreateMainButton("start", Colors.ThemeGreen, StartNewGame);
        private readonly Button _buttonExit = CreateExitButton(BulletHell.ExitGame);

        private float _rotation;

        public sealed override (string, string[])[] ExtraDebugInfo => new[] { ("main_menu", new[] {$"rotation: {_rotation}"}) };

        public sealed override void HandleInput()
        {
            // update buttons
            _buttonStart.HandleInput();
            _buttonExit.HandleInput();
            // check debug screen input
            if (Keybinds.Pause.Held && Keybinds.Debug.Held)
                SceneManager.Scene = new DebugScene();
        }

        public sealed override void Draw()
        {
            // draw title
            _rotation = MathF.Sin(GameManager.Ticks * TITLE_ROT_FREQ) * TITLE_ROT_AMP;
            FontType.alagard.DrawCenteredString(new(0.5f, 0.35f), BulletHell.TITLE, Colors.Title, new(6), _rotation, Fonts.DrawStringWithShadow);
            // draw buttons
            _buttonStart.Draw();
            _buttonExit.Draw();
        }

        private static void StartNewGame() => SceneManager.Scene = new GameScene();
    }
}
