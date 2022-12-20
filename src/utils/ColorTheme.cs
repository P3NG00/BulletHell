using Microsoft.Xna.Framework;

namespace BulletHell.Utils
{
    public struct ColorTheme
    {
        public readonly Color Main;
        public readonly Color MainHighlight;
        public readonly Color Text;
        public readonly Color TextHighlight;

        public ColorTheme(Color main, Color mainHighlight, Color text, Color textHighlight)
        {
            Main = main;
            MainHighlight = mainHighlight;
            Text = text;
            TextHighlight = textHighlight;
        }
    }
}
