using Microsoft.Xna.Framework.Input;

namespace BulletHell.Input
{
    public static class Keybinds
    {
        // mouse
        public static readonly MouseInput MouseLeft = new(Mouse.Left);
        public static readonly MouseInput MouseMiddle = new(Mouse.Middle);
        public static readonly MouseInput MouseRight = new(Mouse.Right);

        // debug
        public static readonly KeyInput Debug = new(Keys.F12);

        // game
        public static readonly KeyInput Pause = new(Keys.Escape);
        public static readonly KeyInput Fullscreen = new(Keys.F11);
        public static readonly KeyInput MoveLeft = new(Keys.A);
        public static readonly KeyInput MoveRight = new(Keys.D);
        public static readonly KeyInput MoveUp = new(Keys.W);
        public static readonly KeyInput MoveDown = new(Keys.S);

        // time
        public static readonly KeyInput TimeScaleDecrement = new(Keys.F1);
        public static readonly KeyInput TimeScaleIncrement = new(Keys.F2);
        public static readonly KeyInput TimeTickStep = new(Keys.F3);
    }
}
