using System;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities
{
    public abstract class AbstractEntity
    {
        public bool Alive => _life > 0f;
        public Vector2 Velocity => RawVelocity * _moveSpeed;

        private Vector2 DrawSize => new Vector2(Radius * 2f);
        private Vector2 HealthBarSize => new Vector2(Radius * 2f, Radius / 4f);

        public float Radius { get; private set; }
        public float Life
        {
            get => _life;
            private set
            {
                _life = Math.Max(0f, value);
                if (!Alive)
                    OnDeath();
            }
        }

        protected virtual DrawData DrawData => _drawData;

        public Vector2 Position;

        private readonly DrawData? _healthBarDrawData;
        private readonly DrawData _drawData;
        private readonly float _moveSpeed;
        private readonly float _maxLife;

        private float _life;

        protected Vector2 RawVelocity;

        public AbstractEntity(Vector2 position, float radius, float moveSpeed, float maxLife, DrawData drawData, Vector2? velocity = null, Color? healthColor = null)
        {
            Position = position;
            Radius = radius;
            _moveSpeed = moveSpeed;
            _maxLife = maxLife;
            Life = maxLife;
            _drawData = drawData;
            RawVelocity = velocity ?? Vector2.Zero;
            _healthBarDrawData = healthColor.HasValue ? new DrawData(Textures.Circle, healthColor.Value) : null;
        }

        public virtual void Tick()
        {
            if (!Alive)
                return;
            // normalize velocity
            if (RawVelocity.Length() != 0f)
                RawVelocity.Normalize();
            // update position
            Position += Velocity;
        }

        public void Draw()
        {
            if (!Alive)
                return;
            // get relative screen position
            var drawPos = Position * new Vector2(1, -1);
            // draw to surface
            Display.DrawOffsetCentered(drawPos, DrawSize, DrawData);
            // draw health bar horizontally across enemy body
            if (!_healthBarDrawData.HasValue)
                return;
            var healthPercentage = Life / _maxLife;
            var healthBarDrawSize = DrawSize * healthPercentage;
            Display.DrawOffsetCentered(drawPos, healthBarDrawSize, _healthBarDrawData.Value);
        }

        protected virtual void OnDeath() {}

        public bool CollidesWith(AbstractEntity other)
        {
            var distance = Vector2.Distance(Position, other.Position);
            var radiusSum = Radius + other.Radius;
            return distance < radiusSum;
        }

        public virtual void Damage(float amount = 1f) => Life -= amount;

        public void Kill() => Life = 0f;
    }
}
