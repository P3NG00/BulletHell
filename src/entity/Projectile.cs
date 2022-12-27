using Microsoft.Xna.Framework;

namespace BulletHell.Entities
{
    public sealed class Projectile : AbstractEntity
    {
        private static Vector2 ProjectileSize => new(4);

        public Projectile(Vector2 position, Vector2 direction) : base(position - new Vector2(0, ProjectileSize.Y / 2f), ProjectileSize, 5, new(color: new Color(255, 0, 0)), direction) {}
    }
}
