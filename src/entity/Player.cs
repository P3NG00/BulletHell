using BulletHell.Input;
using BulletHell.Scenes;
using Microsoft.Xna.Framework;

namespace BulletHell.Entities
{
    public sealed class Player : AbstractEntity
    {
        public Player() : base(Vector2.Zero, new(8), 3.5f, new(color: new(0, 255, 0))) {}

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
            // normalize velocity
            if (RawVelocity.Length() != 0)
                RawVelocity.Normalize();
            // check projectile
            if (Keybinds.MouseLeft.PressedThisFrame)
                GameScene.AddEntity(new Projectile(Position));
            // base call
            base.Tick();
        }
    }
}
