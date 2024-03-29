using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities.Enemies
{
    public sealed class FollowerEnemy : AbstractBasicFollowingEnemy
    {
        private static readonly DrawData EnemyDrawData = new(Textures.Circle, Colors.EnemyFollower);

        public FollowerEnemy(Vector2 position, float enemyLife, float enemyDamage) :
            base(
                position: position,
                enemyLife: enemyLife,
                enemyDamage: enemyDamage,
                pointReward: enemyLife,
                drawData: EnemyDrawData
            )
        {}
    }
}
