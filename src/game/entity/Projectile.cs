using BulletHell.Game.Weapon;
using BulletHell.Scenes;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities
{
    public sealed class Projectile : AbstractEntity
    {
        private static readonly DrawData ProjectileDrawData = new(Textures.Circle, Colors.Projectile);

        public readonly ProjectileInfo ProjectileInfo;
        public readonly AbstractEntity SourceEntity;

        public Projectile(ProjectileInfo projectileInfo, AbstractEntity source, Vector2 position, Vector2 direction) :
            base(position, projectileInfo.Radius, projectileInfo.Speed, projectileInfo.LifeTicks, ProjectileDrawData, direction)
        {
            ProjectileInfo = projectileInfo;
            SourceEntity = source;
        }

        public sealed override void Tick()
        {
            Damage();
            // base call
            base.Tick();
        }

        public static void FireFromEntity(ProjectileInfo projectileInfo, AbstractEntity entity, Vector2 direction)
        {
            // fix direction
            if (direction == Vector2.Zero)
                direction = Vector2.UnitX;
            else
                direction.Normalize();
            // spawn projectile
            var distance = projectileInfo.Radius + entity.Radius;
            var spawnOffset = direction * distance;
            var spawnPos = entity.Position + spawnOffset;
            var projectile = new Projectile(projectileInfo, entity, spawnPos, direction);
            GameScene.AddProjectile(projectile);
        }
    }
}
