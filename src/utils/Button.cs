using System;
using BulletHell.Input;
using Microsoft.Xna.Framework;

namespace BulletHell.Utils
{
    public sealed class Button
    {
        private readonly Vector2 _relativeCenter;
        private readonly Point _size;
        private readonly ColorTheme _colorTheme;
        private readonly Action _action;
        private readonly string _text;
        private readonly Vector2 _textScale;

        private Rectangle _lastRectangle;
        private bool _highlighted;

        private bool Clicked => Keybinds.MouseLeft.ReleasedThisFrame && _highlighted;

        // (0f, 0f) = top-left of window.
        // (1f, 1f) = bottom-right of window.
        public Button(Vector2 relativeCenter, Point size, string text, ColorTheme colorTheme, Action action, int textScale = 1)
        {
            _relativeCenter = relativeCenter;
            _size = size;
            _text = text;
            _colorTheme = colorTheme;
            _action = action;
            _textScale = new(textScale);
        }

        public void Update()
        {
            _lastRectangle = new Rectangle(((Display.WindowSize.ToVector2() * _relativeCenter) - (_size.ToVector2() / 2f)).ToPoint(), _size);
            // TODO make only highlightable while not holding left mouse
            _highlighted = _lastRectangle.Contains(InputManager.MousePosition);
            // check if mouse was released over button
            if (Clicked)
                _action();
        }

        public void Draw()
        {
            // draw box
            Display.Draw(_lastRectangle.Location.ToVector2(), _size.ToVector2(), new(_highlighted ? _colorTheme.MainHighlight : _colorTheme.Main));
            // draw text centered in box
            var color = _highlighted ? _colorTheme.TextHighlight : _colorTheme.Text;
            FontType.VeniceClassic.DrawCenteredString(_relativeCenter, _text, color, _textScale, drawStringFunc: Fonts.DrawStringWithShadow);
        }
    }
}
