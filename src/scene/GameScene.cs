using BulletHell.Entities;
using BulletHell.Input;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Scenes
{
    public sealed class GameScene : AbstractScene
    {
        private readonly Button _buttonExit = CreateExitButton(BackToMainMenu);
        private readonly Player _player = new();

        private bool _paused = false;

        public GameScene() => Display.UpdateCameraOffset(Vector2.Zero);

        public sealed override void Update()
        {
            // toggle debug
            if (Keybinds.Debug.PressedThisFrame)
                Util.Toggle(ref Debug.Enabled);
            // toggle pause
            if (Keybinds.Pause.PressedThisFrame)
                Util.Toggle(ref _paused);
            // paused
            if (_paused)
                _buttonExit.Update();
        }

        public sealed override void Tick()
        {
            if (!_paused)
                _player.Tick();
        }

        public sealed override void Draw()
        {
            // draw player
            _player.Draw();
            // paused
            if (_paused)
            {
                // draw overlay
                Display.DrawFadedOverlay();
                // draw paused text
                Display.DrawCenteredString(FontType.VeniceClassic, new(0.5f), "paused", Colors.UI_Text, new(2), drawStringFunc: Display.DrawStringWithShadow);
                // draw exit button
                _buttonExit.Draw();
            }
            // draw debug
            if (Debug.Enabled)
            {
                // draw center point
                Display.DrawScreenRelative(new Vector2(0.5f), new Vector2(6), new(color: new(0, 255, 0)));
                // draw ui info
                var drawPos = new Vector2(Util.UI_SPACER);
                foreach (var debugInfo in new[] {
                    $"window_size: {Display.WindowSize.X}x{Display.WindowSize.Y}",
                    $"paused: {_paused}",
                    $"time_scale: {Debug.TimeScale:0.00}",
                    $"time: {(GameManager.Ticks / (float)GameManager.TICKS_PER_SECOND):0.000}",
                    $"ticks: {GameManager.Ticks} ({GameManager.TICKS_PER_SECOND} tps)",
                    $"frames_per_second: {GameManager.AverageFramesPerSecond:0.000}",
                    $"ticks_per_frame: {GameManager.AverageTicksPerFrame:0.000}",
                    $"camera_offset_x: {Display.CameraOffset.X:0.000}",
                    $"camera_offset_y: {Display.CameraOffset.Y:0.000}",
                    $"x: {_player.Position.X:0.000}",
                    $"y: {_player.Position.Y:0.000}"})
                {
                    Display.DrawStringWithBackground(FontType.type_writer, drawPos, debugInfo, Colors.UI_Text);
                    drawPos.Y += Util.UI_SPACER + FontType.type_writer.GetFont().LineSpacing;
                }
            }
        }

        private static void BackToMainMenu() => SceneManager.SetScene(new MainMenuScene());
    }
}
