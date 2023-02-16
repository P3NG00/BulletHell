using BulletHell.Utils;

namespace BulletHell.Scenes
{
    public static class SceneManager
    {
        public static AbstractScene Scene
        {
            get => _scene;
            set
            {
                _nextScene = value;
                if (_scene is GameScene)
                    GameScene.NullifySingleton();
            }
        }

        private static AbstractScene _nextScene = null;
        private static AbstractScene _scene;

        public static void Update()
        {
            // check next scene
            if (_nextScene != null)
            {
                _scene = _nextScene;
                _nextScene = null;
            }
            // update scene
            _scene.Update();
            // update ticks
            GameManager.UpdateTicks();
        }
    }
}
