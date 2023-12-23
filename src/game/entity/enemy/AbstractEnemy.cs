using BulletHell.Scenes;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities.Enemies
{
    public abstract class AbstractEnemy : AbstractCreatureEntity
    {
        public readonly float EnemyDamage;
        public readonly float PointReward;

        protected AbstractEnemy(
            Vector2 position,
            float radius,
            float moveSpeed,
            float enemyLife,
            float enemyDamage,
            float pointReward,
            DrawData drawData,
            float dashMultiplier = 0f,
            float dashSeconds = 0f,
            float dashCooldownSeconds = 0f) :
        base(
            position: position,
            radius: radius,
            moveSpeed: moveSpeed,
            life: enemyLife,
            drawData: drawData,
            dashMultiplier: dashMultiplier,
            dashSeconds: dashSeconds,
            dashCooldownSeconds: dashCooldownSeconds)
        {
            EnemyDamage = enemyDamage;
            PointReward = pointReward;
            SetVelocityTowardsPlayer();
        }

        protected void UpdateVelocityTowardsPlayer(float lerpValue)
        {
            var playerDirection = GameScene.Player.Position - Position;
            if (playerDirection.Length() != 0f)
                playerDirection.Normalize();
            RawVelocity = Vector2.Lerp(RawVelocity, playerDirection, lerpValue);
        }

        protected void SetVelocityTowardsPlayer() => RawVelocity = GameScene.Player.Position - Position;

        protected sealed override void OnDeath() => GameScene.Score += PointReward;
    }
}
