using BulletHell.Scenes;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities.Enemies
{
    public abstract class AbstractEnemy : AbstractEntity
    {
        public AbstractEnemy(Vector2 position, float radius, float moveSpeed, float enemyLife, DrawData drawData, Color healthColor) :
            base(position, radius, moveSpeed, enemyLife, drawData, healthColor: healthColor) =>
                UpdateVelocityTowardsPlayer(1f);

        protected void UpdateVelocityTowardsPlayer(float lerpValue)
        {
            var playerDirection = GameScene.Player.Position - Position;
            if (playerDirection.Length() != 0f)
                playerDirection.Normalize();
            RawVelocity = Vector2.Lerp(RawVelocity, playerDirection, lerpValue);
        }

        protected sealed override void OnDeath() => GameScene.Score += (int)MaxLife;
    }
}
