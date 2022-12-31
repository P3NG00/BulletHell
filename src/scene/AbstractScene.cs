using System;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell
{
    public abstract class AbstractScene
    {
        public readonly Color BackgroundColor;

        public virtual string[] ExtraDebugInfo => null;

        public AbstractScene(Color? backgroundColor = null) => BackgroundColor = backgroundColor ?? Colors.Background;

        public virtual void Update() {}

        public virtual void Tick() {}

        public virtual void Draw() {}

        protected static Button CreateExitButton(Action action) => new(new(0.5f, 0.75f), new(125, 50), "exit", Colors.ThemeExit, action, 2);
    }
}
