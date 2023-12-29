using System.Collections.Generic;

namespace BulletHell.Utils
{
    /*
     * While this could extend LinkedList<T>, it's only necessary
     * right now to use the only Add method available instead of
     * overriding inherited methods to ensure that the list is
     * always the correct size.
     */

    public sealed class FixedSizeLinkedList<T>
    {
        private readonly LinkedList<T> _list = new();
        private readonly int _maxSize;

        public int Count => _list.Count;

        public FixedSizeLinkedList(int maxSize)
        {
            if (maxSize < 1)
                throw new System.ArgumentOutOfRangeException(nameof(maxSize), "Max size must be greater than 0");
            _maxSize = maxSize;
        }

        public void Add(T value)
        {
            // remove last element if list is full
            if (_list.Count >= _maxSize)
                _list.RemoveLast();
            // add new element to front of list
            _list.AddFirst(value);
        }

        // override GetEnumerator() to allow foreach
        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();
    }
}
