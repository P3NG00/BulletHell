using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities.Enemies
{
    public sealed class DashingEnemy : AbstractBasicEnemy
    {
        private const float ENEMY_DASH_MULTPLIER = 3f;
        private const float ENEMY_DASH_SECONDS = 0.5f;
        private const float ENEMY_DASH_COOLDOWN_SECONDS = 5f;

        private static readonly DrawData EnemyDrawData = new(Textures.Circle, Colors.EnemyDashing);

        public DashingEnemy(Vector2 position, float enemyLife, float enemyDamage) :
            base(
                position: position,
                enemyLife: enemyLife,
                enemyDamage: enemyDamage,
                pointReward: enemyLife,
                drawData: EnemyDrawData,
                dashMultiplier: ENEMY_DASH_MULTPLIER,
                dashSeconds: ENEMY_DASH_SECONDS,
                dashCooldownSeconds: ENEMY_DASH_COOLDOWN_SECONDS
            )
        {}

        public sealed override void Tick()
        {
            if (Alive)
                Dash();
            base.Tick();
        }
    }
}
