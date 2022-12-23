using System;
using Microsoft.Xna.Framework;

namespace BulletHell.Utils
{
    public static class Debug
    {
        private const double TIME_SCALE_MIN = 0;
        private const double TIME_SCALE_MAX = 5;
        private const double TIME_SCALE_STEP = 0.1f;

        public static bool Enabled = false;

        private static double _timeScale = 1f;

        public static double TimeScale => _timeScale;

        public static void IncreaseTimeScale() => _timeScale = Math.Min(TIME_SCALE_MAX, _timeScale + TIME_SCALE_STEP);

        public static void DecreaseTimeScale() => _timeScale = Math.Max(TIME_SCALE_MIN, _timeScale - TIME_SCALE_STEP);

        public static void Draw(params string[] extraInfo)
        {
            if (!Enabled)
                return;
            // draw center point
            Display.DrawScreenRelative(new Vector2(0.5f), new Vector2(6), new(color: new(0, 255, 0)));
            // draw ui info
            var drawPos = new Vector2(Util.UI_SPACER);
            var spacer = Util.UI_SPACER + FontType.type_writer.GetFont().LineSpacing;
            var debugInfo = new[] {
                $"window_size: {Display.WindowSize.X}x{Display.WindowSize.Y}",
                $"time_scale: {Debug.TimeScale:0.00}",
                $"time: {(GameManager.Ticks / (float)GameManager.TICKS_PER_SECOND):0.000}",
                $"ticks: {GameManager.Ticks} ({GameManager.TICKS_PER_SECOND} tps)",
                $"frames_per_second: {GameManager.AverageFramesPerSecond:0.000}",
                $"ticks_per_frame: {GameManager.AverageTicksPerFrame:0.000}",
                $"camera_offset_x: {Display.CameraOffset.X:0.000}",
                $"camera_offset_y: {Display.CameraOffset.Y:0.000}"};
            foreach (var d in debugInfo)
                DrawDebugInfo(d);
            // draw extra info
            if (extraInfo.IsEmpty())
                return;
            AddSpacer();
            foreach (var d in extraInfo)
                DrawDebugInfo(d);
            // local func
            void DrawDebugInfo(string debugLine)
            {
                Display.DrawStringWithBackground(FontType.type_writer, drawPos, debugLine, Colors.UI_Text);
                AddSpacer();
            }
            void AddSpacer() => drawPos.Y += spacer;
        }
    }
}
