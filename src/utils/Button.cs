using System;
using Microsoft.Xna.Framework;

namespace BulletHell.Utils
{
    public sealed class Button : AbstractHighlightable, ISceneObject
    {
        private readonly ColorTheme _colorTheme;
        private readonly Action _action;
        private readonly string _text;
        private readonly Vector2 _textScale;

        protected sealed override Rectangle GetRectangle => new Rectangle(((Display.WindowSize.ToVector2() * RelativeCenter) - (Size.ToVector2() / 2f)).ToPoint(), Size);

        // (0f, 0f) = top-left of window.
        // (1f, 1f) = bottom-right of window.
        public Button(Vector2 relativeCenter, Point size, string text, ColorTheme colorTheme, Action action, int? textScale = null) : base(relativeCenter, size)
        {
            _text = text;
            _colorTheme = colorTheme;
            _action = action;
            _textScale = new(textScale ?? 1);
        }

        public sealed override void Update()
        {
            // base call
            base.Update();
            // check if mouse was released over button
            if (Clicked)
                _action();
        }

        public void Draw()
        {
            // draw box
            Display.Draw(LastRectangle.Location.ToVector2(), Size.ToVector2(), new(color: Highlighted ? _colorTheme.MainHighlight : _colorTheme.Main));
            // draw text centered in box
            var color = Highlighted ? _colorTheme.TextHighlight : _colorTheme.Text;
            FontType.VeniceClassic.DrawCenteredString(RelativeCenter, _text, color, _textScale, drawStringFunc: Fonts.DrawStringWithShadow);
        }
    }
}
