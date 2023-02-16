using System;
using BulletHell.Utils;

namespace BulletHell.Game.Waves
{
    public sealed class Wave : GameObject
    {
        public readonly (Type EnemyType, float EnemyHealth)[] EnemyTypes;
        public readonly int WaveLengthTicks;
        public readonly int SpawnRateTicks;

        public Wave(float waveLengthSeconds, float spawnRatePerSecond, (Type, float)[] enemyTypes, int id) : base(id)
        {
            WaveLengthTicks = GameManager.SecondsToTicks(waveLengthSeconds);
            SpawnRateTicks = GameManager.SecondsToTicks(1f / spawnRatePerSecond);
            EnemyTypes = enemyTypes;
        }
    }
}
