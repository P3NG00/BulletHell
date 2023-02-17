using BulletHell.Utils;

namespace BulletHell.Scenes
{
    public static class SceneManager
    {
        public static AbstractScene Scene
        {
            get => _scene;
            set => _nextScene = value;
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
            // nullify singleton if leaving GameScene
            if (_scene is GameScene && _nextScene != null)
                GameScene.NullifySingleton();
        }
    }
}
