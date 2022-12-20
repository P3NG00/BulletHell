using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell
{
    public abstract class AbstractScene
    {
        public readonly Color BackgroundColor;

        public AbstractScene(Color? backgroundColor = null) => BackgroundColor = backgroundColor ?? Colors.Background;

        public virtual void Update() {}

        public virtual void Tick() {}

        public virtual void Draw() {}
    }
}
