using System;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities
{
    public abstract class AbstractEntity
    {
        public bool Alive => _life > 0f;
        public Vector2 Velocity => RawVelocity * MoveSpeed;

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

        protected Vector2 DrawPosition => Position * new Vector2(1, -1);
        protected virtual DrawData DrawData => _drawData;
        protected virtual float MoveSpeed => _moveSpeed;

        private Vector2 DrawSize => new Vector2(Radius * 2f);

        public readonly float Radius;
        public Vector2 Position;

        protected virtual Vector2 RawVelocity
        {
            get => _rawVelocity;
            set => _rawVelocity = value;
        }

        private readonly DrawData _drawData;
        private readonly float _moveSpeed;
        private Vector2 _rawVelocity;
        private float _life;

        public AbstractEntity(
            Vector2 position,
            float radius,
            float moveSpeed,
            float life,
            DrawData drawData,
            Vector2? velocity = null)
        {
            Position = position;
            Radius = radius;
            _moveSpeed = moveSpeed;
            _life = life;
            _drawData = drawData;
            _rawVelocity = velocity ?? Vector2.Zero;
        }

        public virtual void Tick()
        {
            if (!Alive)
                return;
            // normalize velocity
            if (RawVelocity.Length() > 1f)
                RawVelocity = Vector2.Normalize(RawVelocity);
            // update position
            Position += Velocity;
        }

        public virtual void Draw()
        {
            if (!Alive)
                return;
            // draw to surface
            Display.DrawOffsetCentered(DrawPosition, DrawSize, DrawData);
        }

        public virtual void Damage(float amount = 1f) => Life -= amount;

        protected virtual void OnDeath() {}

        public bool CollidesWith(AbstractEntity other)
        {
            var distance = Vector2.Distance(Position, other.Position);
            var radiusSum = Radius + other.Radius;
            return distance <= radiusSum;
        }

        public void Kill() => Life = 0f;
    }
}
