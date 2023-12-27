using BulletHell.Game.Weapon;
using BulletHell.Scenes;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities.Enemies
{
    public sealed class ProjectileEnemy : AbstractBasicFollowingEnemy
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
                pointReward: enemyLife,
                drawData: EnemyDrawData
            )
        {}

        public sealed override void Tick()
        {
            // tick projectile timer
            if (Alive && --_nextProjectileTicks <= 0)
            {
                _nextProjectileTicks = ProjectileTicks;
                // spawn projectile
                var shot_leading = Util.Random.Next(40 + 1) + 10;
                Projectile.FireFromEntity(ProjectileInfo, this, GameScene.Player.Position + (GameScene.Player.Velocity * shot_leading));
            }
            base.Tick();
        }
    }
}
