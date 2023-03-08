using BulletHell.Utils;

namespace BulletHell.Game.Waves
{
    public sealed class Wave : GameObject
    {
        public readonly EnemyInfo[] EnemyInfoArray;
        public readonly int WaveLengthTicks;

        public Wave(float waveLengthSeconds, int id, params EnemyInfo[] enemyInfos) : base(id)
        {
            WaveLengthTicks = GameManager.SecondsToTicks(waveLengthSeconds);
            EnemyInfoArray = enemyInfos;
        }
    }
}
