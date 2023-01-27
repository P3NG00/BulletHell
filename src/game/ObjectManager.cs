using System.Collections.Immutable;
using BulletHell.Utils;

namespace BulletHell.Game
{
    public abstract class ObjectManager<T> where T : GameObject
    {
        private readonly ImmutableArray<T> _objects;

        protected abstract T[] ObjectArray { get; }

        public int ObjectAmount => _objects.Length;

        public ObjectManager(ref ObjectManager<T> instance)
        {
            this.SingletonCheck(ref instance);
            _objects = ImmutableArray.Create(ObjectArray);
            // id check
            for (int i = 0; i < ObjectAmount; i++)
            {
                var gameObject = ObjectFromID(i);
                if (gameObject.ID != i)
                    throw new System.Exception($"Object ID mismatch: expected {i} but got {gameObject.ID}");
            }
        }

        public T ObjectFromID(int id) => _objects[id];
    }
}
