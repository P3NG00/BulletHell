using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Entities
{
    public sealed class Projectile : AbstractEntity
    {
        private const float PROJECTILE_LIFE = GameManager.TICKS_PER_SECOND;
        private const float PROJECTILE_SPEED = 5f;
        private const float PROJECTILE_RADIUS = 8f;

        private static DrawData ProjectileDrawData => new(Textures.Circle, new Color(255, 0, 0));

        public Projectile(Vector2 position, Vector2 direction) :
            base(position,
                 PROJECTILE_RADIUS,
                 PROJECTILE_SPEED,
                 PROJECTILE_LIFE,
                 ProjectileDrawData,
                 direction) {}

        public sealed override void Tick()
        {
            Damage();
            // base call
            base.Tick();
        }
    }
}
