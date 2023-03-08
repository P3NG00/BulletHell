using System;
using BulletHell.Utils;

namespace BulletHell.Game.Waves
{
    public struct WaveInfo
    {
        public readonly Type EnemyType;
        public readonly float EnemyHealth;
        public readonly int SpawnRateTicks;

        // TODO change 'spawnRatePerSecond' to 'spawnSeconds' and adjust accordingly
        public WaveInfo(Type enemyType, float enemyHealth, float spawnRatePerSecond)
        {
            EnemyType = enemyType;
            EnemyHealth = enemyHealth;
            SpawnRateTicks = GameManager.SecondsToTicks(1f / spawnRatePerSecond);
        }
    }
}
