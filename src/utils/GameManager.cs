using System.Linq;

namespace BulletHell.Utils
{
    public static class GameManager
    {
        public const int FRAMES_PER_SECOND = 60;
        public const int TICKS_PER_SECOND = 60;
        public const float TICK_STEP = 1f / TICKS_PER_SECOND;

        public static ulong Ticks => s_ticks[0];
        public static double AverageFramesPerSecond => s_lastFps.Average();
        public static double AverageTicksPerFrame => s_lastTickDifferences.Average();

        private static ulong[] s_ticks = new ulong[] {0, 0};
        private static ulong[] s_lastTickDifferences = new ulong[TICKS_PER_SECOND];
        private static double[] s_lastFps = new double[FRAMES_PER_SECOND];
        private static double s_tickDelta = 0f;

        public static void UpdateTicks(double timeThisUpdate) => s_tickDelta += timeThisUpdate;

        public static void UpdateFramesPerSecond(double timeThisFrame)
        {
            // move values down
            for (int i = s_lastFps.Length - 2; i >= 0; i--)
                s_lastFps[i + 1] = s_lastFps[i];
            // store fps value
            s_lastFps[0] = 1000f / timeThisFrame;
            // move last tick count down
            for (int i = s_lastTickDifferences.Length - 2; i >= 0; i--)
                s_lastTickDifferences[i + 1] = s_lastTickDifferences[i];
            // set last tick difference
            s_lastTickDifferences[0] = s_ticks[0] - s_ticks[1];
            // update last tick count
            s_ticks[1] = s_ticks[0];
        }

        public static bool WillTick()
        {
            if (s_tickDelta < TICK_STEP)
                return false;
            // decrement delta time by tick step
            s_tickDelta -= TICK_STEP;
            // increment tick counter
            s_ticks[0]++;
            // return success
            return true;
        }

        public static void AddTick() => s_tickDelta += TICK_STEP;
    }
}
