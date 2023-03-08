using BulletHell.Utils;

namespace BulletHell.Game.Waves
{
    public sealed class Wave : GameObject
    {
        public readonly EnemyInfo[] EnemyInfoArray;
        public readonly int WaveLengthTicks;

        public Wave(float waveLengthSeconds, int id, params EnemyInfo[] enemyInfoArray) : base(id)
        {
            WaveLengthTicks = GameManager.SecondsToTicks(waveLengthSeconds);
            EnemyInfoArray = enemyInfoArray;
        }
    }
}
