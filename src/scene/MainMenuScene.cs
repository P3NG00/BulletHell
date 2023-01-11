using System;
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

        public sealed override void Update()
        {
            // update buttons
            _buttonStart.Update();
            _buttonExit.Update();
        }

        public sealed override void Draw()
        {
            // draw title
            var rotation = MathF.Sin(GameManager.Ticks * TITLE_ROT_FREQ) * TITLE_ROT_AMP;
            FontType.alagard.DrawCenteredString(new(0.5f, 0.35f), BulletHell.TITLE, Colors.UI_Title, new(6), rotation, Fonts.DrawStringWithShadow);
            // draw buttons
            _buttonStart.Draw();
            _buttonExit.Draw();
        }

        private static void StartNewGame() => SceneManager.Scene = new GameScene();
    }
}
