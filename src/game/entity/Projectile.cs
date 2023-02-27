using BulletHell.Scenes;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities
{
    public sealed class Projectile : AbstractEntity
    {
        private const float PROJECTILE_SPEED = 6.25f;

        public const float PROJECTILE_RADIUS = 8f;

        private static readonly float ProjectileLife = GameManager.SecondsToTicks(1f);

        private static DrawData ProjectileDrawData => new(Textures.Circle, Colors.Projectile);

        public readonly AbstractEntity SourceEntity;

        public Projectile(Vector2 position, Vector2 direction, AbstractEntity source) :
            base(position, PROJECTILE_RADIUS, PROJECTILE_SPEED, ProjectileLife, ProjectileDrawData, direction) =>
                SourceEntity = source;

        public sealed override void Tick()
        {
            Damage();
            // base call
            base.Tick();
        }

        public static void FireFromEntity(AbstractEntity entity, Vector2 direction)
        {
            // fix direction
            if (direction == Vector2.Zero)
                direction = Vector2.UnitX;
            else
                direction.Normalize();
            // spawn projectile
            var distance = Projectile.PROJECTILE_RADIUS + entity.Radius;
            var spawnOffset = direction * distance;
            var spawnPos = entity.Position + spawnOffset;
            var projectile = new Projectile(spawnPos, direction, entity);
            GameScene.AddProjectile(projectile);
        }
    }
}
