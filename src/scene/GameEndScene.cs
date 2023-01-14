using BulletHell.Utils;

namespace BulletHell.Scenes
{
    public sealed class GameEndScene : AbstractScene
    {
        private readonly Button _restartButton = CreateMainButton("restart", Colors.ThemeGreen, RestartGame);
        private readonly Button _mainMenuButton = CreateExitButton(BackToMainMenu);
        private readonly int _score;

        public sealed override (string, string[])[] ExtraDebugInfo => new[] { ("game_end", new[] {$"score: {_score}",}) };

        public GameEndScene(int score) => _score = score;

        public sealed override void Update()
        {
            _restartButton.Update();
            _mainMenuButton.Update();
        }

        public sealed override void Draw()
        {
            // draw score
            FontType.VeniceClassic.DrawCenteredString(new(0.5f, 0.4f), $"score: {_score}", Colors.UI_Text, new(4), drawStringFunc: Fonts.DrawStringWithShadow);
            // draw buttons
            _restartButton.Draw();
            _mainMenuButton.Draw();
        }

        private static void RestartGame() => SceneManager.Scene = new GameScene();

        private static void BackToMainMenu() => SceneManager.Scene = new MainMenuScene();
    }
}
