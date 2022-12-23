using Microsoft.Xna.Framework;

namespace BulletHell.Entities
{
    public sealed class Projectile : AbstractEntity
    {
        // TODO add direction for Projectiles to travel in
        public Projectile(Vector2 position) : base(position, new(4), 5, new(color: new Color(255, 0, 0))) {}
    }
}
