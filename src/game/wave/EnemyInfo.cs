using System;
using BulletHell.Utils;

namespace BulletHell.Game.Waves
{
    public struct EnemyInfo
    {
        public readonly Type EnemyType;
        public readonly float EnemyHealth;
        public readonly float EnemyDamage;
        public readonly int SpawnTicks;

        public EnemyInfo(Type enemyType, float enemyHealth, float enemyDamage, float spawnSeconds)
        {
            EnemyType = enemyType;
            EnemyHealth = enemyHealth;
            EnemyDamage = enemyDamage;
            SpawnTicks = GameManager.SecondsToTicks(spawnSeconds);
        }
    }
}
