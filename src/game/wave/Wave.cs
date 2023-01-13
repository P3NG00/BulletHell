using BulletHell.Utils;

namespace BulletHell.Game.Waves
{
    public struct Wave
    {
        public readonly int WaveLengthTicks;
        public readonly int SpawnRateTicks;
        public readonly int ID;

        // TODO implement enemy health scaling

        public Wave(float waveLengthSeconds, float spawnRatePerSecond, int id)
        {
            ID = id;
            WaveLengthTicks = GameManager.SecondsToTicks(waveLengthSeconds);
            SpawnRateTicks = GameManager.SecondsToTicks(1f / spawnRatePerSecond);
        }
    }
}
