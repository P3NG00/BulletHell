using BulletHell.Scenes;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Entities
{
    public sealed class Enemy : AbstractEntity
    {
        private const float ENEMY_SPEED = 2f;

        private static DrawData EnemyDrawData => new(Textures.Circle, new(255, 0, 0));
        private static Vector2 EnemySize => new(32);

        public Enemy(Vector2 position) :
            base(position,
                 EnemySize,
                 ENEMY_SPEED,
                 EnemyDrawData) {}

        public sealed override void Tick()
        {
            // set velocity towards player
            var direction = GameScene.Player.Position - Position;
            RawVelocity = Vector2.Normalize(direction);
            // base call
            base.Tick();
        }
    }
}
