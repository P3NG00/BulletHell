using Microsoft.Xna.Framework;

namespace BulletHell.Utils
{
    public static class Colors
    {
        public static Color Default => new Color(255, 255, 255);
        public static Color Background => new Color(39, 33, 40);
        public static Color TextBackground => new Color(0, 0, 0, 128);
        public static Color TextShadow => new Color(0, 0, 0, 64);
        public static Color UI_Title => new Color(32, 192, 255);
        public static Color UI_Text => new Color(255, 255, 255);

        public static ColorTheme ThemeDefault => new ColorTheme(new Color(0, 0, 0), new Color(255, 255, 255), new Color(255, 255, 255), new Color(0, 0, 0));
        public static ColorTheme ThemeExit => new ColorTheme(new Color(64, 0, 0), new Color(255, 0, 0), new Color(224, 0, 0), new Color(0, 0, 0));
        public static ColorTheme ThemeBlue => new ColorTheme(new Color(0, 0, 0), new Color(0, 96, 128), new Color(255, 255, 255), UI_Title);
    }
}
