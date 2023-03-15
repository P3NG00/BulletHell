using BulletHell.Utils;

namespace BulletHell.Game.Waves
{
    public sealed class Wave : GameObject
    {
        public readonly EnemyInfo[] EnemyInfoArray;
        public readonly int WaveLengthTicks;

        public Wave(float waveLengthSeconds, EnemyInfo[] enemyInfos, int id) : base(id)
        {
            WaveLengthTicks = GameManager.SecondsToTicks(waveLengthSeconds);
            EnemyInfoArray = enemyInfos;
        }
    }
}
