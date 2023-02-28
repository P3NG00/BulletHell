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

        public static void LoadContent(GraphicsDevice graphicsDevice) => SpriteBatch = new SpriteBatch(graphicsDevice);

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
            var textureScale = size / textureSize;
            var origin = textureSize / 2f;
            var drawOffset = position + (size / 2f);
            SpriteBatch.Draw(drawData.Texture, drawOffset, null, drawData.Color, rotation, origin, textureScale, SpriteEffects.None, 0f);
        }

        public static void DrawOffset(Vector2 position, Vector2 size, DrawData drawData, float rotation = 0f)
        {
            var drawPos = position - CameraOffset;
            Draw(drawPos, size, drawData, rotation);
        }

        public static void DrawOffsetCentered(Vector2 position, Vector2 size, DrawData drawData, float rotation = 0f)
        {
            var drawPos = position - CameraOffset - (size / 2f);
            Draw(drawPos, size, drawData, rotation);
        }

        public static void DrawScreenRelativeCentered(Vector2 screenPosition, Vector2 size, DrawData drawData, float rotation = 0f)
        {
            var drawPos = (screenPosition * WindowSize.ToVector2()) - (size / 2f);
            Draw(drawPos, size, drawData, rotation);
        }

        // draws faded overlay over entire window
        public static void DrawFadedOverlay() => Display.Draw(Vector2.Zero, WindowSize.ToVector2(), new(Colors.FadedOverlay));
    }
}
