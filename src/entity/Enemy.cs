using BulletHell.Scenes;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Entities
{
    public sealed class Enemy : AbstractEntity
    {
        private const float ENEMY_LIFE = 2f;
        private const float ENEMY_SPEED = 2f;
        private const float ENEMY_RADIUS = 16f;

        private static DrawData EnemyDrawData => new(Textures.Circle, new(255, 0, 0));

        public Enemy(Vector2 position) :
            base(position,
                 ENEMY_RADIUS,
                 ENEMY_SPEED,
                 ENEMY_LIFE,
                 EnemyDrawData) {}

        public sealed override void Tick()
        {
            // set velocity towards player
            RawVelocity = GameScene.Player.Position - Position;
            // base call
            base.Tick();
        }

        // TODO draw health bar horizontally across enemy body
    }
}
