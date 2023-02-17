using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities.Enemies
{
    public sealed class DashingEnemy : AbstractBasicEnemy
    {
        private static DrawData EnemyDrawData => new(Textures.Circle, new(255, 0, 255));
        private static Color EnemyHealthColor => new(255, 128, 128);

        public DashingEnemy(Vector2 position, float enemyLife) : base(position, enemyLife, EnemyDrawData, EnemyHealthColor) {}

        // TODO make dash towards player at intervals
    }
}
