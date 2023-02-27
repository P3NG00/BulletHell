using BulletHell.Scenes;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities.Enemies
{
    public sealed class ProjectileEnemy : AbstractBasicEnemy
    {
        private static DrawData EnemyDrawData => new(Textures.Circle, Colors.EnemyProjectile);

        private static readonly int ProjectileTicks = GameManager.SecondsToTicks(5f);

        private int _nextProjectileTicks = ProjectileTicks;

        public ProjectileEnemy(Vector2 position, float enemyLife) : base(position, enemyLife, EnemyDrawData, Colors.EnemyProjectileHealth) {}

        public sealed override void Tick()
        {
            // tick projectile timer
            if (Alive && --_nextProjectileTicks <= 0)
            {
                _nextProjectileTicks = ProjectileTicks;
                // spawn projectile
                var direction = GameScene.Player.Position - Position;
                Projectile.FireFromEntity(this, direction);
            }
            base.Tick();
        }
    }
}
