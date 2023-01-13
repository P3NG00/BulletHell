using BulletHell.Utils;

namespace BulletHell.Game
{
    public abstract class ObjectManager<T> where T : GameObject
    {
        private T[] _objects;

        protected abstract T[] ObjectArray { get; }

        public int ObjectAmount => _objects.Length;

        public ObjectManager(ref ObjectManager<T> instance) => this.SingletonCheck(ref instance);

        public void Initialize()
        {
            _objects = ObjectArray;
            CheckIDs();
        }

        private void CheckIDs()
        {
            for (int i = 0; i < ObjectAmount; i++)
                if (_objects[i].ID != i)
                    throw new System.Exception($"Object ID mismatch: expected {i} but got {_objects[i].ID}");
        }

        public T ObjectFromID(int id) => _objects[id];
    }
}
