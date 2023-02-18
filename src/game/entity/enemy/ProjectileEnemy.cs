using BulletHell.Scenes;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities.Enemies
{
    public sealed class ProjectileEnemy : AbstractBasicEnemy
    {
        private static DrawData EnemyDrawData => new(Textures.Circle, new(255, 0, 255));
        private static Color EnemyHealthColor => new(255, 128, 128);

        private static readonly int ProjectileTicks = GameManager.SecondsToTicks(5f);

        private int _nextProjectileTicks = ProjectileTicks;

        public ProjectileEnemy(Vector2 position, float enemyLife) : base(position, enemyLife, EnemyDrawData, EnemyHealthColor) {}

        public sealed override void Tick()
        {
            // tick projectile timer
            if (Alive && --_nextProjectileTicks <= 0)
            {
                _nextProjectileTicks = ProjectileTicks;
                // spawn projectile
                // TODO spawn projectile in front of enemy like in WeaponManager
                var projectileDirection = GameScene.Player.Position - Position;
                var projectile = new Projectile(Position, projectileDirection, this);
                GameScene.AddProjectile(projectile);
            }
            base.Tick();
        }
    }
}
