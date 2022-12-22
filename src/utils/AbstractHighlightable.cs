using BulletHell.Input;
using Microsoft.Xna.Framework;

namespace BulletHell.Utils
{
    public abstract class AbstractHighlightable
    {
        protected Rectangle LastRectangle { get; private set; }
        protected Vector2 RelativeCenter { get; private set; }
        protected bool Highlighted { get; private set; }

        protected abstract Rectangle GetRectangle { get; }

        protected bool Clicked => Keybinds.MouseLeft.ReleasedThisFrame && Highlighted;

        protected virtual Point Size => _size ?? throw new System.Exception("If size is not specified, override Size property.");

        private readonly Point? _size;

        public AbstractHighlightable(Vector2 relativeCenter, Point? size = null)
        {
            RelativeCenter = relativeCenter;
            _size = size;
        }

        public virtual void Update()
        {
            LastRectangle = GetRectangle;
            Highlighted = LastRectangle.Contains(InputManager.MousePosition);
        }
    }
}
