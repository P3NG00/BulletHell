using BulletHell.Scenes;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities
{
    public sealed class Enemy : AbstractEntity
    {
        private const float ENEMY_SPEED = 3f;
        private const float ENEMY_RADIUS = 16f;

        private static DrawData EnemyDrawData => new(Textures.Circle, new(0, 0, 255));

        public Enemy(Vector2 position, float enemyLife) :
            base(position, ENEMY_RADIUS, ENEMY_SPEED, enemyLife, EnemyDrawData) =>
                GameScene.AddEnemy(this);

        public sealed override void Tick()
        {
            // set velocity towards player
            var playerDirection = GameScene.Player.Position - Position;
            if (playerDirection.Length() != 0f)
                playerDirection.Normalize();
            RawVelocity = Vector2.Lerp(RawVelocity, playerDirection, 0.8f);
            // base call
            base.Tick();
        }

        protected sealed override void OnDeath() => GameScene.Score++; // TODO increase score based on enemy difficulty
    }
}
