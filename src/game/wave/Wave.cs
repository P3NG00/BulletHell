using BulletHell.Utils;

namespace BulletHell.Game.Waves
{
    public sealed class Wave : GameObject
    {
        public readonly int WaveLengthTicks;
        public readonly int SpawnRateTicks;

        // TODO implement enemy health scaling

        public Wave(float waveLengthSeconds, float spawnRatePerSecond, int id) : base(id)
        {
            WaveLengthTicks = GameManager.SecondsToTicks(waveLengthSeconds);
            SpawnRateTicks = GameManager.SecondsToTicks(1f / spawnRatePerSecond);
        }
    }
}
