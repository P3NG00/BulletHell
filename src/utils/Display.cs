using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.Utils
{
    public static class Display
    {
        public static SpriteBatch SpriteBatch { get; private set; }
        public static Point WindowSize => new(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

        public static Vector2 CameraOffset;

        private static GraphicsDeviceManager _graphics;
        private static Point _lastWindowSize;

        public static void CreateGraphicsManager(Microsoft.Xna.Framework.Game game) => _graphics = new GraphicsDeviceManager(game);

        public static void LoadContent() => SpriteBatch = new SpriteBatch(BulletHell.Instance.GraphicsDevice);

        public static void Initialize() => SetSize(1280, 720, false);

        // Focuses camera around given position
        public static void UpdateCameraOffset(Vector2 position)
        {
            var centeredScreen = -(WindowSize.ToVector2() / 2f);
            CameraOffset = new Vector2(centeredScreen.X + position.X,
                                       centeredScreen.Y - position.Y);
        }

        public static void ToggleFullscreen()
        {
            if (_graphics.IsFullScreen)
                SetSize(_lastWindowSize.X, _lastWindowSize.Y, false);
            else
            {
                _lastWindowSize = WindowSize;
                SetSize(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                        GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height, true);
            }
        }

        public static void SetSize(int width, int height, bool fullscreen)
        {
            _graphics.IsFullScreen = fullscreen;
            UpdateSize(width, height);
            _graphics.ApplyChanges();
        }

        public static void UpdateSize(int width, int height)
        {
            _graphics.PreferredBackBufferWidth = width;
            _graphics.PreferredBackBufferHeight = height;
        }

        public static void Draw(Vector2 position, Vector2 size, DrawData drawData, float rotation = 0f)
        {
            var textureSize = drawData.Texture.Bounds.Size.ToVector2();
            var textureScale = (size / textureSize);
            var origin = textureSize / 2f;
            var drawOffset = position + (size / 2f);
            SpriteBatch.Draw(drawData.Texture, drawOffset, null, drawData.Color, rotation, origin, textureScale, SpriteEffects.None, 0f);
        }

        public static void DrawOffset(Vector2 position, Vector2 size, DrawData drawData, float rotation = 0f)
        {
            var drawPos = position - CameraOffset - (size / 2f);
            Draw(drawPos, size, drawData, rotation);
        }

        public static void DrawScreenRelative(Vector2 screenPosition, Vector2 size, DrawData drawData, float rotation = 0f)
        {
            var drawPos = (screenPosition * WindowSize.ToVector2()) - (size / 2f);
            Draw(drawPos, size, drawData, rotation);
        }

        // draws faded overlay over entire window
        public static void DrawFadedOverlay() => Display.Draw(Vector2.Zero, WindowSize.ToVector2(), new(color: Colors.Overlay));

        public static void DrawString(FontType fontSize, Vector2 position, string text, Color color, Vector2? scale = null, float rotation = 0f)
        {
            FixScale(ref scale);
            var origin = fontSize.MeasureString(text) / 2f;
            SpriteBatch.DrawString(fontSize.GetFont(), text, position + origin, color, rotation, origin, scale.Value, SpriteEffects.None, 0f);
        }

        public static void DrawStringWithBackground(FontType fontSize, Vector2 position, string text, Color color, Vector2? scale = null, float rotation = 0f)
        {
            FixScale(ref scale);
            // draw text background
            var textSize = fontSize.MeasureString(text);
            var uiSpacerVec = new Vector2(Util.UI_SPACER);
            Draw(position - (uiSpacerVec / 2f), (textSize + uiSpacerVec) * scale.Value, new(color: Colors.TextBackground), rotation);
            // draw text
            DrawString(fontSize, position, text, color, scale, rotation);
        }

        public static void DrawStringWithShadow(FontType fontSize, Vector2 position, string text, Color color, Vector2? scale = null, float rotation = 0f)
        {
            // draw shadowed text
            var shadowOffset = new Vector2(2 * ((int)fontSize + 1));
            DrawString(fontSize, position + shadowOffset, text, Colors.TextShadow, scale, rotation);
            // draw regular text
            DrawString(fontSize, position, text, color, scale, rotation);
        }

        // (0f, 0f) = top-left of window.
        // (1f, 1f) = bottom-right of window.
        public static void DrawCenteredString(FontType fontSize, Vector2 relativeScreenPosition, string text, Color color, Vector2? scale = null, float rotation = 0f, DrawStringFunc drawStringFunc = null)
        {
            FixDrawStringFunc(ref drawStringFunc);
            var textSize = fontSize.MeasureString(text);
            var screenPosition = relativeScreenPosition * WindowSize.ToVector2();
            var drawPos = screenPosition - (textSize / 2f);
            drawStringFunc(fontSize, drawPos, text, color, scale, rotation);
        }

        public static void DrawOffsetString(FontType fontSize, Vector2 position, string text, Color color, Vector2? scale = null, float rotation = 0f, DrawStringFunc drawStringFunc = null)
        {
            FixDrawStringFunc(ref drawStringFunc);
            var drawPos = position - CameraOffset;
            drawStringFunc(fontSize, drawPos, text, color, scale, rotation);
        }

        public delegate void DrawStringFunc(FontType fontSize, Vector2 position, string text, Color color, Vector2? scale, float rotation);

        private static void FixScale(ref Vector2? scale) => scale ??= Vector2.One;

        private static void FixDrawStringFunc(ref DrawStringFunc drawStringFunc) => drawStringFunc ??= DrawString;
    }
}
