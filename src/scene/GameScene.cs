using BulletHell.Utils;

namespace BulletHell.Scenes
{
    public sealed class GameScene : AbstractSimpleScene
    {
        public GameScene()
        {
            var buttonBack = new Button(new(0.5f, 0.9f), new(125, 50), "back", Colors.ThemeExit, BackToMainMenu);
            SetSceneObjects(buttonBack);
        }

        private void BackToMainMenu() => BulletHell.SetScene(new MainMenuScene());

        // TODO add controllable player that can move around

        public sealed override void Draw()
        {
            // print work in progress text
            Display.DrawCenteredString(FontType.VeniceClassic, new(0.5f), "Work in Progress", Colors.UI_Text, new(2), drawStringFunc: Display.DrawStringWithShadow);
            // base call
            base.Draw();
        }
    }
}
