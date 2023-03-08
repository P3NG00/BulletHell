using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities.Enemies
{
    public class AbstractBasicEnemy : AbstractEnemy
    {
        private const float ENEMY_SPEED = 3f;
        private const float ENEMY_RADIUS = 16f;
        private const float ENEMY_LERP_VALUE = 0.085f;

        public AbstractBasicEnemy(Vector2 position, float enemyLife, float enemyDamage, DrawData drawData, Color healthColor, float dashMultiplier = 0f, float dashSeconds = 0f, float dashCooldownSeconds = 0f) :
            base(
                position: position,
                radius: ENEMY_RADIUS,
                moveSpeed: ENEMY_SPEED,
                enemyLife: enemyLife,
                enemyDamage: enemyDamage,
                drawData: drawData,
                healthColor: healthColor,
                dashMultiplier: dashMultiplier,
                dashSeconds: dashSeconds,
                dashCooldownSeconds: dashCooldownSeconds
            )
        {}

        public override void Tick()
        {
            if (Alive)
                UpdateVelocityTowardsPlayer(ENEMY_LERP_VALUE);
            base.Tick();
        }
    }
}
