using BulletHell.Scenes;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities
{
    public sealed class Enemy : AbstractEntity
    {
        private const float ENEMY_SPEED = 3f;
        private const float ENEMY_RADIUS = 16f;

        private static DrawData EnemyDrawData => new(Textures.Circle, new(255, 0, 0));

        public Enemy(Vector2 position, float enemyLife) :
            base(position, ENEMY_RADIUS, ENEMY_SPEED, enemyLife, EnemyDrawData) =>
            GameScene.AddEnemy(this);

        public sealed override void Tick()
        {
            // set velocity towards player
            RawVelocity = GameScene.Player.Position - Position;
            // base call
            base.Tick();
        }

        // TODO draw health bar horizontally across enemy body

        protected sealed override void OnDeath() => GameScene.Score++; // TODO increase score based on enemy difficulty
    }
}
