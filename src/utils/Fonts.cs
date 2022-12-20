using System.Collections.Immutable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.Utils
{
    public static class Fonts
    {
        private static ImmutableArray<SpriteFont> s_typeWriterFont;

        public static void Initialize(ContentManager content)
        {
            // load font
            s_typeWriterFont = ImmutableArray.Create(
                // TODO
                // Load("acknowtt"),
                Load("alagard"),
                // Load("ArcadeAlternate"),
                // Load("MotorolaScreentype"),
                Load("VeniceClassic"));

            // local func
            SpriteFont Load(string name) => content.Load<SpriteFont>(name);
        }

        public static SpriteFont GetFont(this FontType fontSize) => s_typeWriterFont[(int)fontSize];

        public static Vector2 MeasureString(this FontType fontSize, string text) => GetFont(fontSize).MeasureString(text);
    }
}
