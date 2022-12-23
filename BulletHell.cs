﻿using System;
using BulletHell.Input;
using BulletHell.Scenes;
using BulletHell.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell
{
    public sealed class BulletHell : Game
    {
        public const string TITLE = "Bullet Hell";

        public static BulletHell Instance => _instance;

        private static BulletHell _instance;

        public BulletHell()
        {
            this.SingletonCheck(ref _instance);
            // initialize scene after instance is set
            SceneManager.SetScene(new MainMenuScene());
            Display.CreateGraphicsManager(this);
            Content.RootDirectory = "Content";
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(1000f / GameManager.FRAMES_PER_SECOND);
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += new EventHandler<EventArgs>((sender, eventArgs) => Display.UpdateSize(Window.ClientBounds.Width, Window.ClientBounds.Height));
        }

        protected sealed override void Initialize()
        {
            Window.Title = TITLE;
            Window.AllowAltF4 = false;
            Display.Initialize();
            // base call
            base.Initialize();
        }

        protected sealed override void LoadContent()
        {
            Textures.LoadContent();
            Fonts.LoadContent(Content);
            Display.LoadContent();
            // TODO initialize items after textures
            // base call
            base.LoadContent();
        }

        protected sealed override void Update(GameTime gameTime)
        {
            // update input
            InputManager.Update();
            // togglable fullscreen
            if (Keybinds.Fullscreen.PressedThisFrame)
                Display.ToggleFullscreen();
            // update ticks
            GameManager.UpdateTicks(gameTime.ElapsedGameTime.TotalSeconds * Debug.TimeScale);
            // toggle debug
            if (Keybinds.Debug.PressedThisFrame)
                Util.Toggle(ref Debug.Enabled);
            // update scene
            SceneManager.Update();
            // base call
            base.Update(gameTime);
        }

        protected sealed override void Draw(GameTime gameTime)
        {
            // fill background
            GraphicsDevice.Clear(SceneManager.Scene.BackgroundColor);
            // update frames per second
            GameManager.UpdateFramesPerSecond(gameTime.ElapsedGameTime.TotalMilliseconds);
            // begin drawing
            Display.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            // draw scene
            SceneManager.Draw();
            // draw debug
            Debug.Draw(SceneManager.Scene.ExtraDebugInfo);
            // end drawing
            Display.SpriteBatch.End();
            // base call
            base.Draw(gameTime);
        }
    }
}
