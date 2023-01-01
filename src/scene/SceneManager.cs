using BulletHell.Utils;

namespace BulletHell.Scenes
{
    public static class SceneManager
    {
        public static AbstractScene Scene { get; private set; } = null;

        private static AbstractScene _nextScene = null;

        public static void Update()
        {
            // check next scene
            if (_nextScene != null)
            {
                Scene = _nextScene;
                _nextScene = null;
            }
            // update scene
            Scene.Update();
            // tick scene
            while (GameManager.WillTick())
                Scene.Tick();
        }

        public static void SetScene(AbstractScene scene) => _nextScene = scene;
    }
}
