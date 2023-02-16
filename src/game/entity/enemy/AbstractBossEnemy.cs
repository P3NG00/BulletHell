using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities.Enemies
{
    public sealed class AbstractBossEnemy : AbstractEnemy
    {
        public AbstractBossEnemy(Vector2 position, float radius, float moveSpeed, float enemyLife, DrawData drawData, Color healthColor) :
            base(position, radius, moveSpeed, enemyLife, drawData, healthColor) =>
                throw new System.NotImplementedException("Boss Entities not yet implemented"); // TODO implement boss enemies
    }
}
