using Microsoft.Xna.Framework;

namespace BulletHell.Utils
{
    public static class Colors
    {
        public static Color Default => new(255, 255, 255);
        public static Color Background => new(39, 33, 40);
        public static Color TextBackground => new(0, 0, 0, 128);
        public static Color TextShadow => new(0, 0, 0, 64);
        public static Color Overlay => new(0, 0, 0, 128);
        public static Color UI_Title => new(32, 192, 255);
        public static Color UI_Text => new(255, 255, 255);

        public static ColorTheme ThemeDefault => new(new(0, 0, 0), new(255, 255, 255), new(255, 255, 255), new(0, 0, 0));
        public static ColorTheme ThemeExit => new(new(64, 0, 0), new(255, 0, 0), new(224, 0, 0), new(0, 0, 0));
        public static ColorTheme ThemeGreen => new(new(0, 0, 0), new (0, 128, 96), new(255, 255, 255), new(32, 255, 192));
    }
}
