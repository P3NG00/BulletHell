using BulletHell.Scenes;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities.Enemies
{
    public sealed class TeleportEnemy : AbstractBasicEnemy
    {
        private static readonly DrawData EnemyDrawData = new(Textures.Circle, Colors.EnemyTeleport);
        private static readonly int TeleportIntervalTicks = GameManager.SecondsToTicks(6f);

        private int _teleportTicks = TeleportIntervalTicks;

        public TeleportEnemy(Vector2 position, float enemyLife, float enemyDamage) :
            base(
                position: position,
                enemyLife: enemyLife,
                enemyDamage: enemyDamage,
                drawData: EnemyDrawData,
                healthColor: Colors.EnemyTeleportHealth
            )
        {}

        private void TeleportAroundPlayer()
        {
            var playerPos = GameScene.Player.Position;
            var distance = Vector2.Distance(playerPos, Position);
            var newRelativePos = Util.Random.NextUnitVector() * distance;
            Position = playerPos + newRelativePos;
        }

        public sealed override void Tick()
        {
            if (Alive && --_teleportTicks <= 0)
            {
                _teleportTicks = TeleportIntervalTicks;
                TeleportAroundPlayer();
            }
            base.Tick();
        }
    }
}
