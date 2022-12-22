using System;
using System.Linq;
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
        public const int FRAMES_PER_SECOND = 60;
        public const int TICKS_PER_SECOND = 60;
        public const float TICK_STEP = 1f / TICKS_PER_SECOND;

        public static BulletHell Instance => _instance;

        private static BulletHell _instance;

        // tick & frame handling variables
        public static ulong Ticks => s_ticks[0];
        public static double AverageFramesPerSecond => s_lastFps.Average();
        public static double AverageTicksPerFrame => s_lastTickDifferences.Average();
        private static ulong[] s_ticks = new ulong[] {0, 0};
        private static ulong[] s_lastTickDifferences = new ulong[TICKS_PER_SECOND];
        private static double[] s_lastFps = new double[FRAMES_PER_SECOND];
        private static double s_tickDelta = 0f;

        public BulletHell()
        {
            this.SingletonCheck(ref _instance);
            // initialize scene after instance is set
            SceneManager.SetScene(new MainMenuScene());
            Display.CreateGraphicsManager(this);
            Content.RootDirectory = "Content";
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(1000f / FRAMES_PER_SECOND);
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
            UpdateTicks(gameTime.ElapsedGameTime.TotalSeconds * Debug.TimeScale);
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
            UpdateFramesPerSecond(gameTime.ElapsedGameTime.TotalMilliseconds);
            // begin drawing
            Display.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            // draw scene
            SceneManager.Draw();
            // end drawing
            Display.SpriteBatch.End();
            // base call
            base.Draw(gameTime);
        }

        private static void UpdateTicks(double timeThisUpdate) => s_tickDelta += timeThisUpdate;

        private static void UpdateFramesPerSecond(double timeThisFrame)
        {
            // move values down
            for (int i = s_lastFps.Length - 2; i >= 0; i--)
                s_lastFps[i + 1] = s_lastFps[i];
            // store fps value
            s_lastFps[0] = 1000f / timeThisFrame;
            // move last tick count down
            for (int i = s_lastTickDifferences.Length - 2; i >= 0; i--)
                s_lastTickDifferences[i + 1] = s_lastTickDifferences[i];
            // set last tick difference
            s_lastTickDifferences[0] = s_ticks[0] - s_ticks[1];
            // update last tick count
            s_ticks[1] = s_ticks[0];
        }

        public static bool WillTick()
        {
            if (s_tickDelta < TICK_STEP)
                return false;
            // decrement delta time by tick step
            s_tickDelta -= TICK_STEP;
            // increment tick counter
            s_ticks[0]++;
            // return success
            return true;
        }

        public static void AddTick() => s_tickDelta += TICK_STEP;
    }
}
