using BulletHell.Input;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Entities
{
    public sealed class Player : AbstractEntity
    {
        private const float PLAYER_LIFE = 50f;
        private const float PLAYER_SPEED = 3.5f;
        private const float PLAYER_RADIUS = 16f;

        private static DrawData PlayerDrawData => new(Textures.Circle, new(0, 255, 0));

        public Player() :
            base(Vector2.Zero,
                 PLAYER_RADIUS,
                 PLAYER_SPEED,
                 PLAYER_LIFE,
                 PlayerDrawData) {}

        public sealed override void Tick()
        {
            // reset velocity
            RawVelocity = Vector2.Zero;
            // handle movement input
            if (Keybinds.MoveLeft.Held)
                RawVelocity.X--;
            if (Keybinds.MoveRight.Held)
                RawVelocity.X++;
            if (Keybinds.MoveUp.Held)
                RawVelocity.Y++;
            if (Keybinds.MoveDown.Held)
                RawVelocity.Y--;
            // base call
            base.Tick();
        }
    }
}
