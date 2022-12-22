using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.Utils
{
    public static class Textures
    {
        public const int SIZE = 8;
        private const int EDGE = SIZE - 1;

        public static Texture2D Blank { get; private set; }
        public static Texture2D Shaded { get; private set; }

        public static void LoadContent()
        {
            Blank = CreateTexture(FilledTexture);
            Shaded = CreateTexture(ShadedTexture);

            // local funcs
            Texture2D CreateTexture(ColorFunc colorFunc)
            {
                var data = new Color[SIZE * SIZE];
                for (int y = 0; y < SIZE; y++)
                    for (int x = 0; x < SIZE; x++)
                        data[(y * SIZE) + x] = colorFunc(x, y);
                var texture = new Texture2D(BulletHell.Instance.GraphicsDevice, SIZE, SIZE);
                texture.SetData(data);
                return texture;
            }

            Color FilledTexture(int x, int y) => new Color(255, 255, 255);

            Color ShadedTexture(int x, int y)
            {
                var isRightEdge = x == EDGE;
                var isBottomEdge = y == EDGE;
                return isRightEdge || isBottomEdge ? new Color(192, 192, 192) : new Color(255, 255, 255);
            }
        }

        private delegate Color ColorFunc(int x, int y);
    }
}
