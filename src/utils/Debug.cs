using BulletHell.Input;
using BulletHell.Scenes;
using Microsoft.Xna.Framework;

namespace BulletHell.Utils
{
    public static class Debug
    {
        public static bool _enabled = false;

        public static bool Enabled => _enabled;

        public static void Update()
        {
            // toggle debug
            if (Keybinds.Debug.PressedThisFrame)
                Util.Toggle(ref _enabled);
        }

        public static void Draw()
        {
            if (!Enabled)
                return;
            // draw center point
            Display.DrawScreenRelativeCentered(new Vector2(0.5f), new Vector2(6), new(new Color(0, 0, 255)));
            // draw ui info
            var drawPos = Util.UISpacerVec;
            var spacer = Util.UI_SPACER + FontType.type_writer.GetFont().LineSpacing;
            var debugInfo = new[] {
                $"window_size: {Display.WindowSize.X} x {Display.WindowSize.Y}",
                $"time_scale: {GameManager.TimeScale:0.00}",
                $"seconds: {(GameManager.Ticks / (float)GameManager.TICKS_PER_SECOND):0.000}",
                $"ticks: {GameManager.Ticks}",
                $"tps: {GameManager.TICKS_PER_SECOND}",
                $"tps_scaled: {GameManager.TICKS_PER_SECOND * GameManager.TimeScale:0.000}",
                $"frames_per_second: {GameManager.AverageFramesPerSecond:0.000}",
                $"ticks_per_frame: {GameManager.AverageTicksPerFrame:0.000}",
                $"camera_offset_x: {Display.CameraOffset.X:0.000}",
                $"camera_offset_y: {Display.CameraOffset.Y:0.000}",
                $"offset_mouse_x: {InputManager.MousePositionOffset.X:0.000}",
                $"offset_mouse_y: {InputManager.MousePositionOffset.Y:0.000}"};
            foreach (var d in debugInfo)
                DrawDebugInfo(d);
            // draw extra info
            var extraInfo = SceneManager.Scene.ExtraDebugInfo;
            if (extraInfo == null)
                return;
            foreach (var e in extraInfo)
            {
                AddSpacer();
                DrawDebugInfo($"category_{e.Title}");
                e.Lines.ForEach(line => DrawDebugInfo(line));
            }
            // local func
            void DrawDebugInfo(string debugLine)
            {
                FontType.type_writer.DrawStringWithBackground(drawPos, debugLine, Colors.UI_Text);
                AddSpacer();
            }
            void AddSpacer() => drawPos.Y += spacer;
        }
    }
}
