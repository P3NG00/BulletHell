using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.Utils
{
    public static class Textures
    {
        public static Texture2D Tile { get; private set; }
        public static Texture2D Heart { get; private set; } // TODO
        public static Texture2D Bullet { get; private set; } // TODO

        public static Texture2D Square { get; private set; }
        public static Texture2D SquareShaded { get; private set; }
        public static Texture2D Circle { get; private set; }

        public static void LoadContent(ContentManager content)
        {
            Tile = Load("tile");
            Heart = Load("heart");
            Bullet = Load("bullet");

            Square = Load("square");
            SquareShaded = Load("square_shaded");
            Circle = Load("circle");
            // load func
            Texture2D Load(string name) => content.Load<Texture2D>($"textures/{name}");
        }
    }
}
