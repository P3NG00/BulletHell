using System;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Entities
{
    public abstract class AbstractEntity
    {
        public bool Alive => Life > 0f;
        public Vector2 Velocity => RawVelocity * _moveSpeed;

        private Vector2 DrawSize => new Vector2(Radius * 2f);

        private readonly DrawData _drawData;
        private readonly float _moveSpeed;

        protected Vector2 RawVelocity;

        public float Radius { get; private set; }
        public float Life { get; private set; }
        public Vector2 Position;

        public AbstractEntity(Vector2 position, float radius, float moveSpeed, float maxLife, DrawData drawData, Vector2? velocity = null)
        {
            Position = position;
            Radius = radius;
            _moveSpeed = moveSpeed;
            Life = maxLife;
            _drawData = drawData;
            RawVelocity = velocity ?? Vector2.Zero;
        }

        public virtual void Tick()
        {
            // normalize velocity
            if (RawVelocity.Length() != 0f)
                RawVelocity.Normalize();
            // update position
            Position += Velocity;
        }

        public void Draw()
        {
            // get relative screen position
            var drawPos = Position * new Vector2(1, -1);
            // draw to surface
            Display.DrawOffsetCentered(drawPos, DrawSize, _drawData);
        }

        public void Damage(float amount = 1f) => Life = Math.Max(0f, Life - amount);

        public void Kill() => Life = 0f;
    }
}
