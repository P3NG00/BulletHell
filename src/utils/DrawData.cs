using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.Utils
{
    public readonly struct DrawData
    {
        public readonly Texture2D Texture;
        public readonly Color Color;

        public DrawData(Texture2D texture = null, Color? color = null)
        {
            Texture = texture ?? Textures.Square;
            Color = color ?? Colors.Default;
        }

        public DrawData(Color color) : this(null, color) {}
    }
}
