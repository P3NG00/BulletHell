using System.Collections.Immutable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.Utils
{
    public static class Fonts
    {
        private static ImmutableArray<SpriteFont> s_typeWriterFont;

        public static void LoadContent(ContentManager content)
        {
            // load font
            s_typeWriterFont = ImmutableArray.Create(
                Load("alagard"),
                Load("VeniceClassic"),
                Load("type_writer"));

            // local func
            SpriteFont Load(string name) => content.Load<SpriteFont>(name);
        }

        public static SpriteFont GetFont(this FontType fontSize) => s_typeWriterFont[(int)fontSize];

        public static Vector2 MeasureString(this FontType fontSize, string text) => GetFont(fontSize).MeasureString(text);

        public static void DrawString(this FontType fontSize, Vector2 position, string text, Color color, Vector2? scale = null, float rotation = 0f)
        {
            FixScale(ref scale);
            var origin = fontSize.MeasureString(text) / 2f;
            Display.SpriteBatch.DrawString(fontSize.GetFont(), text, position + origin, color, rotation, origin, scale.Value, SpriteEffects.None, 0f);
        }

        public static void DrawStringWithBackground(this FontType fontSize, Vector2 position, string text, Color color, Vector2? scale = null, float rotation = 0f)
        {
            FixScale(ref scale);
            // draw text background
            var textSize = fontSize.MeasureString(text);
            var uiSpacerVec = new Vector2(Util.UI_SPACER);
            Display.Draw(position - (uiSpacerVec / 2f), (textSize + uiSpacerVec) * scale.Value, new(color: Colors.TextBackground), rotation);
            // draw text
            DrawString(fontSize, position, text, color, scale, rotation);
        }

        public static void DrawStringWithShadow(this FontType fontSize, Vector2 position, string text, Color color, Vector2? scale = null, float rotation = 0f)
        {
            // draw shadowed text
            var shadowOffset = new Vector2(2 * ((int)fontSize + 1));
            DrawString(fontSize, position + shadowOffset, text, Colors.TextShadow, scale, rotation);
            // draw regular text
            DrawString(fontSize, position, text, color, scale, rotation);
        }

        // (0f, 0f) = top-left of window.
        // (1f, 1f) = bottom-right of window.
        public static void DrawCenteredString(this FontType fontSize, Vector2 relativeScreenPosition, string text, Color color, Vector2? scale = null, float rotation = 0f, DrawStringFunc drawStringFunc = null)
        {
            FixDrawStringFunc(ref drawStringFunc);
            var textSize = fontSize.MeasureString(text);
            var screenPosition = relativeScreenPosition * Display.WindowSize.ToVector2();
            var drawPos = screenPosition - (textSize / 2f);
            drawStringFunc(fontSize, drawPos, text, color, scale, rotation);
        }

        public static void DrawOffsetString(this FontType fontSize, Vector2 position, string text, Color color, Vector2? scale = null, float rotation = 0f, DrawStringFunc drawStringFunc = null)
        {
            FixDrawStringFunc(ref drawStringFunc);
            var drawPos = position - Display.CameraOffset;
            drawStringFunc(fontSize, drawPos, text, color, scale, rotation);
        }

        public delegate void DrawStringFunc(FontType fontSize, Vector2 position, string text, Color color, Vector2? scale, float rotation);

        private static void FixScale(ref Vector2? scale) => scale ??= Vector2.One;

        private static void FixDrawStringFunc(ref DrawStringFunc drawStringFunc) => drawStringFunc ??= DrawString;
    }
}
