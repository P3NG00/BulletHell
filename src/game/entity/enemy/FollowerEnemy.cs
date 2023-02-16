using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities.Enemies
{
    public sealed class FollowerEnemy : AbstractBasicEnemy
    {
        private static DrawData EnemyDrawData => new(Textures.Circle, new(0, 0, 255));
        private static Color EnemyHealthColor => new(255, 0, 0);

        public FollowerEnemy(Vector2 position, float enemyLife) : base(position, enemyLife, EnemyDrawData, EnemyHealthColor) {}
    }
}
