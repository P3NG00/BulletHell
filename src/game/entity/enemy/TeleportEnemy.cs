using BulletHell.Scenes;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities.Enemies
{
    public sealed class TeleportEnemy : AbstractBasicEnemy
    {
        private static readonly int TeleportIntervalTicks = GameManager.SecondsToTicks(6f);

        private static DrawData EnemyDrawData => new(Textures.Circle, new(0, 255, 255));
        private static Color EnemyHealthColor => new(255, 255, 0);

        private int _teleportTicks = 0;

        public TeleportEnemy(Vector2 position, float enemyLife) : base(position, enemyLife, EnemyDrawData, EnemyHealthColor) {}

        private void TeleportAroundPlayer()
        {
            var playerPos = GameScene.Player.Position;
            var distance = Vector2.Distance(playerPos, Position);
            var newRelativePos = Util.Random.NextUnitVector() * distance;
            Position = playerPos + newRelativePos;
        }

        public sealed override void Tick()
        {
            if (--_teleportTicks <= 0)
            {
                _teleportTicks = TeleportIntervalTicks;
                TeleportAroundPlayer();
            }
            base.Tick();
        }
    }
}
