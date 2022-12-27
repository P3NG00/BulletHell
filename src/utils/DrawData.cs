using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.Utils
{
    public struct DrawData
    {
        public readonly Texture2D Texture;
        public readonly Color Color;

        public DrawData(Texture2D texture = null, Color? color = null)
        {
            Texture = texture ?? Textures.Square;
            Color = color ?? Colors.Default;
        }
    }
}
