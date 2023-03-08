using BulletHell.Game.Weapon;
using BulletHell.Scenes;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities.Enemies
{
    public sealed class ProjectileEnemy : AbstractBasicEnemy
    {
        private static readonly DrawData EnemyDrawData = new(Textures.Circle, Colors.EnemyProjectile);
        private static readonly int ProjectileTicks = GameManager.SecondsToTicks(5f);

        private ProjectileInfo ProjectileInfo => new(EnemyDamage, 6f, 8f, 1.25f);

        private int _nextProjectileTicks = ProjectileTicks;

        public ProjectileEnemy(Vector2 position, float enemyLife, float enemyDamage) :
            base(
                position: position,
                enemyLife: enemyLife,
                enemyDamage: enemyDamage,
                drawData: EnemyDrawData,
                healthColor: Colors.EnemyProjectileHealth
            )
        {}

        // TODO 2 types of directional shots: one in direction of player with random aspect to it, and one exactly where the player will be if they continue moving in the same direction
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
