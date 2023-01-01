using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BulletHell.Utils
{
    public static class Util
    {
        public const int UI_SPACER = 5;

        public static Vector2 UISpacerVec => new(UI_SPACER);

        public static readonly Random Random = new Random();

        public static void SingletonCheck<T>(this T instance, ref T singleton)
        {
            if (singleton != null)
                throw new System.Exception("Singleton already instantiated.");
            singleton = instance;
        }

        public static void Toggle(ref bool b) => b = !b;

        public static void ForEach<T>(this T[] array, Action<T> action) => Array.ForEach(array, action);

        public static void ForEach<T>(this T[,] array, Action<T> action)
        {
            foreach (T t in array)
                action(t);
        }

        public static bool IsInteger(this float f) => f % 1f == 0f;

        public static int Floor(this float f) => (int)Math.Floor((double)f);

        public static T GetRandom<T>(this T[] t) => t[Random.Next(t.Length)];

        public static bool TestChance(this float chance)
        {
            if (chance >= 1.0f)
                return true;
            if (chance <= 0.0f)
                return false;
            return Random.NextDouble() < chance;
        }

        public static bool TestChance(this decimal chance)
        {
            if (chance >= 1.0m)
                return true;
            if (chance <= 0.0m)
                return false;
            return (decimal)Random.NextDouble() < chance;
        }

        public static double Average(this IEnumerable<ulong> source)
        {
            ulong sum = 0;
            ulong count = 0;
            foreach (ulong i in source)
            {
                sum += i;
                count++;
            }
            return (double)sum / (double)count;
        }

        public static bool NextBool(this Random random) => (0.5f).TestChance();

        public static Point NextPoint(this Random random, Point max) => new Point(random.Next(max.X), random.Next(max.Y));

        public static Vector2 NextUnitVector(this Random random)
        {
            float angle = (float)random.NextDouble() * MathHelper.TwoPi;
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

        public delegate void ActionRef<T>(ref T t);
    }
}
