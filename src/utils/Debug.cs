using BulletHell.Input;
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
                $"time_scale: {GameManager.TimeScale:0.00}",
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
