using System;
using BulletHell.Utils;

namespace BulletHell.Game.Waves
{
    public struct WaveInfo
    {
        public readonly Type EnemyType;
        public readonly float EnemyHealth;
        public readonly int SpawnTicks;

        public WaveInfo(Type enemyType, float enemyHealth, float spawnSeconds)
        {
            EnemyType = enemyType;
            EnemyHealth = enemyHealth;
            SpawnTicks = GameManager.SecondsToTicks(spawnSeconds);
        }
    }
}
