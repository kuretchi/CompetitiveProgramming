using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitiveProgramming.Collections
{
    public partial class Deque<T> : IEnumerable<T>, IReadOnlyCollection<T>
    {
        private const int _defaultCapacity = 4;
        private T[] _array;
        private int _first, _last;

        public Deque()
        {
            _array = Array.Empty<T>();
        }

        public Deque(int capacity)
        {
            _array = new T[capacity];
        }

        public Deque(IEnumerable<T> collection)
        {
            _array = collection.ToArray();
            _last = _array.Length - 1;
            this.Count = _array.Length;
        }

        public int Count { get; private set; }

        public int Capacity => _array.Length;

        public void Clear()
        {
            this.Count = 0;
            _first = _last = 0;
        }

        public void EnqueueFirst(T item)
        {
            EnsureCapacity(this.Count + 1);
            if (this.Count++ > 0) Decrement(ref _first);
            _array[_first] = item;
        }

        public void EnqueueLast(T item)
        {
            EnsureCapacity(this.Count + 1);
            if (this.Count++ > 0) Increment(ref _last);
            _array[_last] = item;
        }

        public T DequeueFirst()
        {
            if (this.Count == 0) throw new InvalidOperationException();
            var item = _array[_first];
            if (--this.Count > 0) Increment(ref _first);
            return item;
        }

        public T DequeueLast()
        {
            if (this.Count == 0) throw new InvalidOperationException();
            var item = _array[_last];
            if (--this.Count > 0) Decrement(ref _last);
            return item;
        }

        public T PeekFirst()
        {
            if (this.Count == 0) throw new InvalidOperationException();
            return _array[_first];
        }

        public T PeekLast()
        {
            if (this.Count == 0) throw new InvalidOperationException();
            return _array[_last];
        }

        public IEnumerator<T> GetEnumerator()
        {
            var i = _first;
            for (var j = 0; j < this.Count; j++)
            {
                yield return _array[i];
                Increment(ref i);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private void EnsureCapacity(int capacity)
        {
            if (this.Capacity >= capacity) return;

            var newCapacity = System.Math.Max(this.Capacity == 0 ? _defaultCapacity : this.Capacity << 1, capacity);
            var newArray = new T[newCapacity];

            if (this.Count == 0)
            {
                _array = newArray;
                return;
            }

            if (_first < _last)
                Array.Copy(_array, _first, newArray, 0, this.Count);
            else
            {
                Array.Copy(_array, _first, newArray, 0, _array.Length - _first);
                Array.Copy(_array, 0, newArray, _array.Length - _first, _last + 1);
            }

            _first = 0;
            _last = this.Count - 1;
            _array = newArray;
        }

        private void Increment(ref int index)
        {
            if (++index == _array.Length) index = 0;
        }

        private void Decrement(ref int index)
        {
            if (--index == -1) index = _array.Length - 1;
        }
    }

    public partial class Deque<T> : IReadOnlyList<T>
    {
        public T this[int index]
        {
            get { return _array[GetIndex(index)]; }
            set { _array[GetIndex(index)] = value; }
        }

        private int GetIndex(int index)
        {
            if (index < 0 || this.Count <= index) throw new IndexOutOfRangeException();
            index += _first;
            return index >= _array.Length ? index - _array.Length : index;
        }
    }
}
