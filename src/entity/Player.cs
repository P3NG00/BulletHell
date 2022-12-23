using BulletHell.Input;
using Microsoft.Xna.Framework;

namespace BulletHell.Entities
{
    public sealed class Player : AbstractEntity
    {
        public Player() : base(Vector2.Zero, new(8), 3.5f, new(color: new(255, 0, 0))) {}

        public sealed override void Tick()
        {
            // reset velocity
            RawVelocity = Vector2.Zero;
            // handle input
            if (Keybinds.MoveLeft.Held)
                RawVelocity.X--;
            if (Keybinds.MoveRight.Held)
                RawVelocity.X++;
            if (Keybinds.MoveUp.Held)
                RawVelocity.Y++;
            if (Keybinds.MoveDown.Held)
                RawVelocity.Y--;
            // normalize velocity
            if (RawVelocity.Length() != 0)
                RawVelocity.Normalize();
            // base call
            base.Tick();
        }
    }
}