using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Entities
{
    public sealed class Projectile : AbstractEntity
    {
        private const float PROJECTILE_SPEED = 5f;

        private static DrawData ProjectileDrawData => new(color: new Color(255, 0, 0));

        public static Vector2 ProjectileSize => new(4);

        public Projectile(Vector2 position, Vector2 direction) : base(position, ProjectileSize, PROJECTILE_SPEED, ProjectileDrawData, direction) {}
    }
}
