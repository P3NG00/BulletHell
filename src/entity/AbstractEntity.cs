using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Entities
{
    public abstract class AbstractEntity
    {
        private readonly Vector2 _dimensions;
        private readonly DrawData _drawData;
        private readonly float _moveSpeed;

        protected Vector2 RawVelocity;

        public Vector2 Velocity => RawVelocity * _moveSpeed;
        public Vector2 Center => Position + new Vector2(0, _dimensions.Y / 2f);

        public Vector2 Position;

        public AbstractEntity(Vector2 position, Vector2 dimensions, float moveSpeed, DrawData drawData, Vector2? velocity = null)
        {
            Position = position;
            _dimensions = dimensions;
            _moveSpeed = moveSpeed;
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
            var drawPos = Center * new Vector2(1, -1);
            // draw to surface
            Display.DrawOffsetCentered(drawPos, _dimensions, _drawData);
        }
    }
}
