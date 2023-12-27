using BulletHell.Scenes;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities.Enemies
{
    public sealed class MoverEnemy : AbstractBasicEnemy
    {
        private static readonly DrawData EnemyDrawData = new(Textures.Circle, Colors.EnemyMover);
        private static readonly int MoverTicksMin = GameManager.SecondsToTicks(0.5f);
        private static readonly int MoverTicksMax = GameManager.SecondsToTicks(2.5f);
        private static readonly float LeadDistanceMinimum = 20f;
        private static readonly float LeadDistanceRandom = 80f;

        private int _nextMoverTicks;

        public MoverEnemy(Vector2 position, float enemyLife, float enemyDamage) :
            base(
                position: position,
                enemyLife: enemyLife,
                enemyDamage: enemyDamage,
                pointReward: enemyLife,
                drawData: EnemyDrawData
            )
        {
            ResetMoverTicks();
        }

        public override void Tick()
        {
            if (Alive && --_nextMoverTicks <= 0)
            {
                ResetMoverTicks();
                RawVelocity = GameScene.Player.LeadInCurrentDirection(LeadDistanceMinimum, LeadDistanceRandom) - Position;
            }
            base.Tick();
        }

        private void ResetMoverTicks() => _nextMoverTicks = Util.Random.Next(MoverTicksMin, MoverTicksMax + 1);
    }
}
