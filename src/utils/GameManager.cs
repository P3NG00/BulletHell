using System;
using System.Linq;
using BulletHell.Input;
using BulletHell.Scenes;

namespace BulletHell.Utils
{
    public static class GameManager
    {
        public const int FRAMES_PER_SECOND = 60;
        public const int TICKS_PER_SECOND = 64;
        public const double TICK_STEP = 1f / TICKS_PER_SECOND;

        private const double TIME_SCALE_MIN = 0;
        private const double TIME_SCALE_MAX = 5;
        private const double TIME_SCALE_STEP = 0.1f;

        public static ulong Ticks => s_ticks[0];
        public static double TimeScale => s_timeScale;
        public static double AverageFramesPerSecond => s_lastFps.Average();
        public static double AverageTicksPerFrame => s_lastTickDifferences.Average();

        private static readonly FixedSizeLinkedList<ulong> s_lastTickDifferences = new(TICKS_PER_SECOND);
        private static readonly ulong[] s_ticks = new ulong[] {0, 0};
        private static readonly double[] s_lastFps = new double[FRAMES_PER_SECOND];
        private static double s_tickDelta = 0f;
        private static double s_timeScale = 1f;

        public static void HandleInput(double timeThisUpdate)
        {
            if (Keybinds.TimeScaleDecrement.PressedThisFrame)
                s_timeScale = Math.Max(TIME_SCALE_MIN, s_timeScale - TIME_SCALE_STEP);
            if (Keybinds.TimeScaleIncrement.PressedThisFrame)
                s_timeScale = Math.Min(TIME_SCALE_MAX, s_timeScale + TIME_SCALE_STEP);
            if (Keybinds.TimeTickStep.PressedThisFrame)
                s_tickDelta += TICK_STEP;
            s_tickDelta += timeThisUpdate * TimeScale;
        }

        public static void UpdateFramesPerSecond(double timeThisFrame)
        {
            // move values down
            for (int i = s_lastFps.Length - 2; i >= 0; i--)
                s_lastFps[i + 1] = s_lastFps[i];
            // store fps value
            s_lastFps[0] = 1000f / timeThisFrame;
            // add tick difference
            s_lastTickDifferences.Add(s_ticks[0] - s_ticks[1]);
            // update last tick count
            s_ticks[1] = s_ticks[0];
        }

        public static void UpdateTicks(bool tickScene)
        {
            while (s_tickDelta >= TICK_STEP)
            {
                // decrement delta time by tick step
                s_tickDelta -= TICK_STEP;
                // increment tick counter
                s_ticks[0]++;
                // update scene tick
                if (tickScene)
                    SceneManager.Scene.Tick();
            }
        }

        public static int SecondsToTicks(float seconds) => (int)(TICKS_PER_SECOND * seconds);
    }
}
