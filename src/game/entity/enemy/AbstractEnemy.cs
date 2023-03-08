using BulletHell.Scenes;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities.Enemies
{
    public abstract class AbstractEnemy : AbstractCreatureEntity
    {
        public readonly float EnemyDamage;

        public AbstractEnemy(Vector2 position, float radius, float moveSpeed, float enemyLife, float enemyDamage, DrawData drawData, Color healthColor, float dashMultiplier = 0f, float dashSeconds = 0f, float dashCooldownSeconds = 0f) :
            base(
                position: position,
                radius: radius,
                moveSpeed: moveSpeed,
                maxLife: enemyLife,
                drawData: drawData,
                healthColor: healthColor,
                dashMultiplier: dashMultiplier,
                dashSeconds: dashSeconds,
                dashCooldownSeconds: dashCooldownSeconds
            )
        {
            EnemyDamage = enemyDamage;
            UpdateVelocityTowardsPlayer(1f);
        }

        protected void UpdateVelocityTowardsPlayer(float lerpValue)
        {
            var playerDirection = GameScene.Player.Position - Position;
            if (playerDirection.Length() != 0f)
                playerDirection.Normalize();
            RawVelocity = Vector2.Lerp(RawVelocity, playerDirection, lerpValue);
        }

        // TODO make 'float Reward' a property when MaxLife is removed
        protected sealed override void OnDeath() => GameScene.Score += MaxLife;
    }
}
