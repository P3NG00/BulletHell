using BulletHell.Utils;

namespace BulletHell.Scenes
{
    public sealed class GameEndScene : AbstractScene
    {
        private readonly Button _restartButton = CreateMainButton("restart", Colors.ThemeGreen, RestartGame);
        private readonly Button _mainMenuButton = CreateExitButton(BackToMainMenu);
        private readonly int _score;

        public GameEndScene(int score) => _score = score;

        public sealed override void Update()
        {
            _restartButton.Update();
            _mainMenuButton.Update();
        }

        public sealed override void Draw()
        {
            FontType.VeniceClassic.DrawCenteredString(new(0.5f, 0.4f), $"score: {_score}", Colors.UI_Text, new(4f), drawStringFunc: Fonts.DrawStringWithShadow);
            // TODO draw score on screen
            _restartButton.Draw();
            _mainMenuButton.Draw();
        }

        private static void RestartGame() => SceneManager.Scene = new GameScene();

        private static void BackToMainMenu() => SceneManager.Scene = new MainMenuScene();
    }
}
