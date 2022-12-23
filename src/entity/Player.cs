using BulletHell.Input;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Entities
{
    public class Player
    {
        public Vector2 Position => _position;

        private readonly DrawData _drawData = new(color: new(255, 0, 0));
        private readonly Vector2 _size = new(8);

        private Vector2 _position = Vector2.Zero;

        public void Tick()
        {
            // handle input
            int x = 0;
            int y = 0;
            if (Keybinds.MoveLeft.Held)
                x--;
            if (Keybinds.MoveRight.Held)
                x++;
            if (Keybinds.MoveUp.Held)
                y++;
            if (Keybinds.MoveDown.Held)
                y--;
            _position += new Vector2(x, y);
        }

        public void Draw()
        {
            // get relative screen position
            var drawPos = _position * new Vector2(1, -1);
            // draw to surface
            Display.DrawOffset(drawPos, _size, _drawData);
        }
    }
}