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
        private static bool _lastHandledInput = false;

        public static void Update(bool handleInput)
        {
            // check next scene
            if (_nextScene != null)
            {
                _scene = _nextScene;
                _nextScene = null;
            }
            // test lost focus
            if (_lastHandledInput && !handleInput)
                _scene.OnLostFocus();
            _lastHandledInput = handleInput;
            // update scene
            if (handleInput)
                _scene.HandleInput();
            // update ticks
            GameManager.UpdateTicks(handleInput);
            // nullify singleton if leaving GameScene
            if (_scene is GameScene && _nextScene != null)
                GameScene.NullifySingleton();
        }
    }
}
