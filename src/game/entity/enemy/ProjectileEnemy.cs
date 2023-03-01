using BulletHell.Game.Weapon;
using BulletHell.Scenes;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities.Enemies
{
    public sealed class ProjectileEnemy : AbstractBasicEnemy
    {
        private static readonly ProjectileInfo ProjectileInfo = new(1f, 6f, 8f, 1.25f);
        private static readonly DrawData EnemyDrawData = new(Textures.Circle, Colors.EnemyProjectile);
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
                Projectile.FireFromEntity(ProjectileInfo, this, direction);
            }
            base.Tick();
        }
    }
}
