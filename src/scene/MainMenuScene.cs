using System;
using BulletHell.Utils;

namespace BulletHell.Scenes
{
    public sealed class MainMenuScene : AbstractSimpleScene
    {
        // frequency of title rotation
        private const float TITLE_ROT_FREQ = 0.025f;
        // amplitude of title rotation
        private const float TITLE_ROT_AMP = 0.1f;

        public MainMenuScene()
        {
            var buttonStart = new Button(new(0.5f, 0.6f), new(250, 100), "start", Colors.ThemeGreen, StartNewGame, 3);
            var buttonExit = CreateExitButton(BulletHell.Instance.Exit);
            // TODO
            // Button buttonWorldContinue = null;
            // if (Data.SaveExists)
            //     buttonWorldContinue = new Button(new(0.5f, 0.7f), new(250, 50), "continue world", Colors.ThemeBlue, LoadSavedGame);
            // set scene objects
            SetSceneObjects(buttonStart, buttonExit /* , buttonWorldContinue */ );
        }

        public sealed override void Draw()
        {
            // draw title
            var rotation = MathF.Sin(BulletHell.Ticks * TITLE_ROT_FREQ) * TITLE_ROT_AMP;
            Display.DrawCenteredString(FontType.alagard, new(0.5f, 0.35f), BulletHell.TITLE, Colors.UI_Title, new(6), rotation, Display.DrawStringWithShadow);
            // base call
            base.Draw();
        }

        private void StartNewGame() => BulletHell.SetScene(new GameScene());

        // TODO
        // private void LoadSavedGame()
        // {
        //     Data.Load();
        //     BulletHell.SetScene(new GameScene());
        // }
    }
}
