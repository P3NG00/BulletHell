using BulletHell.Utils;

namespace BulletHell.Game.Waves
{
    public sealed class Wave : GameObject
    {
        public readonly float EnemyHealth;
        public readonly int WaveLengthTicks;
        public readonly int SpawnRateTicks;

        public Wave(float waveLengthSeconds, float spawnRatePerSecond, float enemyHealth, int id) : base(id)
        {
            EnemyHealth = enemyHealth;
            WaveLengthTicks = GameManager.SecondsToTicks(waveLengthSeconds);
            SpawnRateTicks = GameManager.SecondsToTicks(1f / spawnRatePerSecond);
        }
    }
}
