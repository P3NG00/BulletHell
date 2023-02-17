using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities
{
    public sealed class Projectile : AbstractEntity
    {
        private const float PROJECTILE_SPEED = 6.25f;

        public const float PROJECTILE_RADIUS = 8f;

        private static readonly float ProjectileLife = GameManager.SecondsToTicks(1f);

        private static DrawData ProjectileDrawData => new(Textures.Circle, new Color(255, 0, 255));

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
    }
}
