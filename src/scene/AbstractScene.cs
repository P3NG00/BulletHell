using System;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Scenes
{
    public abstract class AbstractScene
    {
        public readonly Color BackgroundColor;

        public virtual (string Title, string[] Lines)[] ExtraDebugInfo => Array.Empty<(string, string[])>();

        protected AbstractScene(Color? backgroundColor = null) => BackgroundColor = backgroundColor ?? Colors.Background;

        public virtual void HandleInput() {}

        public virtual void Tick() {}

        public virtual void Draw() {}

        public virtual void OnLostFocus() {}

        protected static Button CreateMainButton(string text, ColorTheme colorTheme, Action action) => new(new(0.5f, 0.6f), new(250, 100), text, colorTheme, action, 3);

        protected static Button CreateExitButton(Action action) => new(new(0.5f, 0.75f), new(125, 50), "exit", Colors.ThemeExit, action, 2);
    }
}
