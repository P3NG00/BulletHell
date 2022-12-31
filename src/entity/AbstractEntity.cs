using System;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Entities
{
    public abstract class AbstractEntity
    {
        public bool Alive => _life > 0f;
        public Vector2 Velocity => RawVelocity * _moveSpeed;

        private readonly Vector2 _dimensions;
        private readonly DrawData _drawData;
        private readonly float _moveSpeed;

        private float _life;

        protected Vector2 RawVelocity;

        public Vector2 Position;

        public AbstractEntity(Vector2 position, Vector2 dimensions, float moveSpeed, float maxLife, DrawData drawData, Vector2? velocity = null)
        {
            Position = position;
            _dimensions = dimensions;
            _moveSpeed = moveSpeed;
            _life = maxLife;
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
            Display.DrawOffsetCentered(drawPos, _dimensions, _drawData);
        }

        public void Damage(float amount = 1f) => _life = Math.Max(0f, _life - amount);

        public void Kill() => _life = 0f;
    }
}
