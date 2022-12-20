using Microsoft.Xna.Framework.Input;

namespace BulletHell.Input
{
    public sealed class KeyInput : AbstractInput<Keys>
    {
        public KeyInput(Keys key) : base(key) {}

        public sealed override bool PressedThisFrame => InputManager.KeyPressedThisFrame(InputType);

        public sealed override bool ReleasedThisFrame => InputManager.KeyReleasedThisFrame(InputType);

        public sealed override bool Held => InputManager.KeyHeld(InputType);
    }
}
