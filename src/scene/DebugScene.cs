using System;
using BulletHell.Input;
using BulletHell.Utils;
using Microsoft.Xna.Framework;
using static BulletHell.Utils.Fonts;

namespace BulletHell.Scenes
{
    public sealed class DebugScene : AbstractScene
    {
        private const float TEXT_SCALE = 4f; // TODO change into adjustable with buttons/keybinds

        public sealed override void Tick()
        {
            // check exit keybind
            if (Keybinds.Pause.PressedThisFrame)
                SceneManager.Scene = new MainMenuScene();
        }

        public sealed override void Draw()
        {
            // draw font examples
            var drawPos = Util.UISpacerVector;
            foreach (var fontType in Enum.GetValues<FontType>())
            {
                DrawFont(fontType, Fonts.DrawStringWithBackground);
                DrawFont(fontType, Fonts.DrawStringWithShadow);
                DrawFont(fontType, Fonts.DrawString);
            }
            // local func
            void DrawFont(FontType fontType, DrawStringFunc drawStringFunc)
            {
                drawStringFunc(fontType, drawPos, fontType.ToString(), Colors.Text, new Vector2(TEXT_SCALE));
                drawPos.Y += fontType.GetFont().LineSpacing * TEXT_SCALE;
            }
        }
    }
}
