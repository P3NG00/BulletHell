using BulletHell.Utils;

namespace BulletHell.Scenes
{
    public sealed class GameEndScene : AbstractScene
    {
        // TODO remove GameEndScene. make end screen an overlay on the GameScene while it's still unpaused and stop spawning new enemies
        // TODO take DeathReason as parameter to display accurate death message. make DeathReason enum

        private readonly Button _restartButton = CreateMainButton("restart", Colors.ThemeGreen, RestartGame);
        private readonly Button _mainMenuButton = CreateExitButton(BackToMainMenu);
        private readonly int _score = GameScene.Score;

        public sealed override (string, string[])[] ExtraDebugInfo => new[] { ("game_end", new[] {$"score: {_score}",}) };

        public sealed override void HandleInput()
        {
            _restartButton.HandleInput();
            _mainMenuButton.HandleInput();
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
