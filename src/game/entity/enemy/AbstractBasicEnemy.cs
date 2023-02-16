using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities.Enemies
{
    public class AbstractBasicEnemy : AbstractEnemy
    {
        private const float ENEMY_SPEED = 3f;
        private const float ENEMY_RADIUS = 16f;
        private const float ENEMY_LERP_VALUE = 0.6f;

        public AbstractBasicEnemy(Vector2 position, float enemyLife, DrawData drawData, Color healthColor) : base(position, ENEMY_RADIUS, ENEMY_SPEED, enemyLife, drawData, healthColor) {}

        public override void Tick()
        {
            UpdateVelocityTowardsPlayer(ENEMY_LERP_VALUE);
            base.Tick();
        }
    }
}
