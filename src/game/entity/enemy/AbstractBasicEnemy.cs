using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities.Enemies
{
    public abstract class AbstractBasicEnemy : AbstractEnemy
    {
        private const float ENEMY_SPEED = 3f;
        private const float ENEMY_RADIUS = 16f;

        protected AbstractBasicEnemy(
            Vector2 position,
            float enemyLife,
            float enemyDamage,
            float pointReward,
            DrawData drawData,
            float dashMultiplier = 0f,
            float dashSeconds = 0f,
            float dashCooldownSeconds = 0f) :
        base(
            position: position,
            radius: ENEMY_RADIUS,
            moveSpeed: ENEMY_SPEED,
            enemyLife: enemyLife,
            enemyDamage: enemyDamage,
            pointReward: pointReward,
            drawData: drawData,
            dashMultiplier: dashMultiplier,
            dashSeconds: dashSeconds,
            dashCooldownSeconds: dashCooldownSeconds)
        {}
    }
}
