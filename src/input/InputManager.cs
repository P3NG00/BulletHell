using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BulletHell.Input
{
    public static class InputManager
    {
        private static readonly KeyboardState[] _keyStates = new KeyboardState[2];
        private static readonly MouseState[] _mouseStates = new MouseState[2];

        public static void Update()
        {
            _keyStates[1] = _keyStates[0];
            _keyStates[0] = Keyboard.GetState();
            _mouseStates[1] = _mouseStates[0];
            _mouseStates[0] = Microsoft.Xna.Framework.Input.Mouse.GetState();
        }

        public static bool KeyPressedThisFrame(Keys key) => _keyStates[0].IsKeyDown(key) && _keyStates[1].IsKeyUp(key);

        public static bool KeyReleasedThisFrame(Keys key) => _keyStates[0].IsKeyUp(key) && _keyStates[1].IsKeyDown(key);

        public static bool KeyHeld(Keys key) => _keyStates[0].IsKeyDown(key);

        public static bool MousePressedThisFrame(Mouse mouse)
        {
            var states = GetMouseButtonStates(mouse);
            return states.previous == ButtonState.Released && states.current == ButtonState.Pressed;
        }

        public static bool MouseReleasedThisFrame(Mouse mouse)
        {
            var states = GetMouseButtonStates(mouse);
            return states.previous == ButtonState.Pressed && states.current == ButtonState.Released;
        }

        public static bool MouseHeld(Mouse mouse)
        {
            var states = GetMouseButtonStates(mouse);
            return states.current == ButtonState.Pressed;
        }

        private static (ButtonState previous, ButtonState current) GetMouseButtonStates(Mouse mouse) => mouse switch
        {
            Mouse.Left => (_mouseStates[1].LeftButton, _mouseStates[0].LeftButton),
            Mouse.Middle => (_mouseStates[1].MiddleButton, _mouseStates[0].MiddleButton),
            Mouse.Right => (_mouseStates[1].RightButton, _mouseStates[0].RightButton),
            _ => throw new System.NotImplementedException($"{nameof(GetMouseButtonStates)} not implemented for {mouse}")
        };

        public static Point MousePosition => _mouseStates[0].Position;

        public static int ScrollWheelDelta
        {
            get
            {
                if (_mouseStates[0].ScrollWheelValue > _mouseStates[1].ScrollWheelValue)
                    return 1;
                else if (_mouseStates[0].ScrollWheelValue < _mouseStates[1].ScrollWheelValue)
                    return -1;
                else
                    return 0;
            }
        }
    }
}
