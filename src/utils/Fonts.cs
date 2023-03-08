using System.Collections.Immutable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.Utils
{
    public static class Fonts
    {
        private static ImmutableArray<SpriteFont>? s_fonts = null;

        public static void LoadContent(ContentManager content)
        {
            // create font array
            Util.SingletonCheck(
                instance: ImmutableArray.Create(
                    Load("alagard"),
                    Load("VeniceClassic"),
                    Load("type_writer")
                ),
                singleton: ref s_fonts
            );
            // load func
            SpriteFont Load(string name) => content.Load<SpriteFont>($"fonts/{name}");
        }

        public static SpriteFont GetFont(this FontType fontType) => s_fonts.Value[(int)fontType];

        public static Vector2 MeasureString(this FontType fontType, string text) => GetFont(fontType).MeasureString(text);

        public static void DrawString(this FontType fontType, Vector2 position, string text, Color color, Vector2? scale = null, float rotation = 0f)
        {
            FixScale(ref scale);
            var origin = fontType.MeasureString(text) / 2f;
            Display.SpriteBatch.DrawString(fontType.GetFont(), text, position + (origin * scale.Value), color, rotation, origin, scale.Value, SpriteEffects.None, 0f);
        }

        public static void DrawStringWithBackground(this FontType fontType, Vector2 position, string text, Color color, Vector2? scale = null, float rotation = 0f)
        {
            FixScale(ref scale);
            // draw text background
            var textSize = fontType.MeasureString(text);
            var uiSpacerVec = Util.UISpacerVector;
            var drawPos = position - ((uiSpacerVec / 2f) * scale.Value);
            Display.Draw(drawPos, (textSize + uiSpacerVec) * scale.Value, new(Colors.TextBackground), rotation);
            // draw text
            DrawString(fontType, position, text, color, scale, rotation);
        }

        public static void DrawStringWithShadow(this FontType fontType, Vector2 position, string text, Color color, Vector2? scale = null, float rotation = 0f)
        {
            FixScale(ref scale);
            // draw shadowed text
            DrawString(fontType, position + scale.Value, text, Colors.TextShadow, scale, rotation);
            // draw regular text
            DrawString(fontType, position, text, color, scale, rotation);
        }

        // (0f, 0f) = top-left of window.
        // (1f, 1f) = bottom-right of window.
        public static void DrawCenteredString(this FontType fontType, Vector2 relativeScreenPosition, string text, Color color, Vector2? scale = null, float rotation = 0f, DrawStringFunc drawStringFunc = null)
        {
            FixDrawStringFunc(ref drawStringFunc);
            var textSize = fontType.MeasureString(text) * scale.Value;
            var screenPosition = relativeScreenPosition * Display.WindowSize.ToVector2();
            var drawPos = screenPosition - (textSize / 2f);
            drawStringFunc(fontType, drawPos, text, color, scale, rotation);
        }

        public static void DrawOffsetString(this FontType fontType, Vector2 position, string text, Color color, Vector2? scale = null, float rotation = 0f, DrawStringFunc drawStringFunc = null)
        {
            FixDrawStringFunc(ref drawStringFunc);
            var drawPos = position - Display.CameraOffset;
            drawStringFunc(fontType, drawPos, text, color, scale, rotation);
        }

        public delegate void DrawStringFunc(FontType fontType, Vector2 position, string text, Color color, Vector2? scale = null, float rotation = 0f);

        private static void FixScale(ref Vector2? scale) => scale ??= Vector2.One;

        private static void FixDrawStringFunc(ref DrawStringFunc drawStringFunc) => drawStringFunc ??= DrawString;
    }
}
