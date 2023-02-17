using BulletHell.Utils;

namespace BulletHell.Game.Waves
{
    public sealed class Wave : GameObject
    {
        public readonly WaveInfo[] WaveInfoArray;
        public readonly int WaveLengthTicks;

        public Wave(float waveLengthSeconds, int id, params WaveInfo[] waveInfoArray) : base(id)
        {
            WaveLengthTicks = GameManager.SecondsToTicks(waveLengthSeconds);
            WaveInfoArray = waveInfoArray;
        }
    }
}
