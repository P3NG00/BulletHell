using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities.Enemies
{
    public sealed class MoverEnemy : AbstractBasicEnemy
    {
        private static readonly DrawData EnemyDrawData = new(Textures.Circle, Colors.EnemyMover);
        private static readonly int MoverTicks = GameManager.SecondsToTicks(5f);

        private int _nextMoverTicks = MoverTicks;

        public MoverEnemy(Vector2 position, float enemyLife, float enemyDamage) :
            base(
                position: position,
                enemyLife: enemyLife,
                enemyDamage: enemyDamage,
                pointReward: enemyLife,
                drawData: EnemyDrawData
            )
        {}

        public override void Tick()
        {
            if (Alive && --_nextMoverTicks <= 0)
            {
                _nextMoverTicks = MoverTicks;
                SetVelocityTowardsPlayer();
            }
            base.Tick();
        }
    }
}
