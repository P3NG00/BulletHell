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
        public const int TICKS_PER_SECOND = 60;
        public const float TICK_STEP = 1f / TICKS_PER_SECOND;

        public static BulletHell Instance => _instance;

        private static BulletHell _instance;

        private AbstractScene _scene;
        private AbstractScene _nextScene = null;

        // tick & frame handling variables
        public static ulong Ticks => _ticks[0];
        public static double AverageFramesPerSecond => _lastFps.Average();
        public static double AverageTicksPerFrame => _lastTickDifferences.Average();
        private static ulong[] _ticks = new ulong[] {0, 0};
        private static ulong[] _lastTickDifferences = new ulong[TICKS_PER_SECOND];
        private static double[] _lastFps = new double[Display.FRAMES_PER_SECOND];
        private static double _tickDelta = 0f;

        public BulletHell()
        {
            this.SingletonCheck(ref _instance);
            // initialize scene after instance is set
            _scene = new MainMenuScene();
            Display.CreateGraphicsManager(this);
            Content.RootDirectory = "Content";
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(1000f / Display.FRAMES_PER_SECOND);
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += new EventHandler<EventArgs>((sender, eventArgs) => Display.UpdateSize(Window.ClientBounds.Width, Window.ClientBounds.Height));
        }

        protected sealed override void Initialize()
        {
            // set window title
            Window.Title = TITLE;
            // set window properties
            Window.AllowAltF4 = false;
            // initialize display
            Display.Initialize();
            // base call
            base.Initialize();
        }

        protected sealed override void LoadContent()
        {
            Textures.Initialize();
            Fonts.Initialize(Content);
            Display.LoadContent();
            // initialize items after textures
            // TODO new Items().Initialize();
            // base call
            base.LoadContent();
        }

        protected sealed override void Update(GameTime gameTime)
        {
            // update scene if set
            if (_nextScene != null)
            {
                _scene = _nextScene;
                _nextScene = null;
            }
            // update input
            InputManager.Update();
            // togglable fullscreen
            if (Keybinds.Fullscreen.PressedThisFrame)
                Display.ToggleFullscreen();
            // update ticks
            UpdateTicks(gameTime.ElapsedGameTime.TotalSeconds * Debug.TimeScale);
            // update scene
            _scene.Update();
            // tick scene
            while (CanTick())
                _scene.Tick();
            // base call
            base.Update(gameTime);
        }

        protected sealed override void Draw(GameTime gameTime)
        {
            // fill background
            GraphicsDevice.Clear(_scene.BackgroundColor);
            // update frames per second
            UpdateFramesPerSecond(gameTime.ElapsedGameTime.TotalMilliseconds);
            // begin drawing
            Display.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            // draw scene
            _scene.Draw();
            // end drawing
            Display.SpriteBatch.End();
            // base call
            base.Draw(gameTime);
        }

        private bool CanTick()
        {
            if (_tickDelta < TICK_STEP)
                return false;
            // decrement delta time by tick step
            _tickDelta -= TICK_STEP;
            // increment tick counter
            _ticks[0]++;
            // return success
            return true;
        }

        private void UpdateTicks(double timeThisUpdate) => _tickDelta += timeThisUpdate;

        private void UpdateFramesPerSecond(double timeThisFrame)
        {
            // move values down
            for (int i = _lastFps.Length - 2; i >= 0; i--)
                _lastFps[i + 1] = _lastFps[i];
            // store fps value
            _lastFps[0] = 1000f / timeThisFrame;
            // move last tick count down
            for (int i = _lastTickDifferences.Length - 2; i >= 0; i--)
                _lastTickDifferences[i + 1] = _lastTickDifferences[i];
            // set last tick difference
            _lastTickDifferences[0] = _ticks[0] - _ticks[1];
            // update last tick count
            _ticks[1] = _ticks[0];
        }

        public static void AddTick() => _tickDelta += TICK_STEP;

        public static void SetScene(AbstractScene scene) => Instance._nextScene = scene;
    }
}
