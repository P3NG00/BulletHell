using BulletHell.Game.Weapon;
using BulletHell.Scenes;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities
{
    public sealed class Projectile : AbstractEntity
    {
        private static readonly DrawData ProjectileDrawData = new(Textures.Circle, Colors.Projectile);

        public readonly AbstractEntity SourceEntity;
        public readonly float ProjectileDamage;

        public Projectile(ProjectileInfo projectileInfo, AbstractEntity source, Vector2 position, Vector2 direction) :
            base(
                position: position,
                radius: projectileInfo.Radius,
                moveSpeed: projectileInfo.Speed,
                life: projectileInfo.LifeTicks,
                drawData: ProjectileDrawData,
                velocity: direction
            )
        {
            ProjectileDamage = projectileInfo.Damage;
            SourceEntity = source;
        }

        public sealed override void Tick()
        {
            Damage();
            // base call
            base.Tick();
        }

        public static void FireFromEntity(ProjectileInfo projectileInfo, AbstractEntity sourceEntity, Vector2 towardsPoint, float angleRadians = 0f)
        {
            var direction = towardsPoint - sourceEntity.Position;
            // fix direction
            if (direction == Vector2.Zero)
                direction = Vector2.UnitX;
            else
                direction.Normalize();
            // rotate direction
            if (angleRadians != 0f)
                direction = Vector2.Transform(direction, Matrix.CreateRotationZ(angleRadians));
            // spawn projectile
            var distance = projectileInfo.Radius + sourceEntity.Radius;
            var spawnOffset = direction * distance;
            var spawnPos = sourceEntity.Position + spawnOffset;
            var projectile = new Projectile(projectileInfo, sourceEntity, spawnPos, direction);
            GameScene.AddProjectile(projectile);
        }
    }
}
