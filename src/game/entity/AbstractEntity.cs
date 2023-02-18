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

        protected virtual DrawData DrawData => _drawData;
        protected virtual float MoveSpeed => _moveSpeed;

        private Vector2 DrawSize => new Vector2(Radius * 2f);

        public float Radius { get; private set; }
        public readonly float MaxLife;
        public Vector2 Position;

        protected virtual Vector2 RawVelocity
        {
            get => _rawVelocity;
            set => _rawVelocity = value;
        }

        private readonly DrawData? _healthBarDrawData = null;
        private readonly DrawData _drawData;
        private readonly float _moveSpeed;
        private Vector2 _rawVelocity;
        private float _life;

        public AbstractEntity(Vector2 position, float radius, float moveSpeed, float maxLife, DrawData drawData, Vector2? velocity = null, Color? healthColor = null)
        {
            Position = position;
            Radius = radius;
            _moveSpeed = moveSpeed;
            MaxLife = maxLife;
            Life = maxLife;
            _drawData = drawData;
            RawVelocity = velocity ?? Vector2.Zero;
            if (healthColor.HasValue)
                _healthBarDrawData = new DrawData(Textures.Circle, healthColor.Value);
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
            // draw health
            if (!_healthBarDrawData.HasValue)
                return;
            var healthPercentage = Life / MaxLife;
            var healthBarDrawSize = DrawSize * healthPercentage;
            Display.DrawOffsetCentered(drawPos, healthBarDrawSize, _healthBarDrawData.Value);
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
