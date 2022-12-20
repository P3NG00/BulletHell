using System;

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
    }
}
