using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitiveProgramming.Collections
{
    public class BinaryHeap<T> : IPriorityQueue<T>
    {
        private readonly IComparer<T> _comparer;
        private readonly List<T> _t;

        public BinaryHeap() : this(Comparer<T>.Default) { }

        public BinaryHeap(IComparer<T> comparer)
        {
            _t = new List<T>();
            _comparer = comparer;
        }

        public BinaryHeap(int capacity) : this(capacity, Comparer<T>.Default) { }

        public BinaryHeap(int capacity, IComparer<T> comparer)
        {
            _t = new List<T>(capacity);
            _comparer = comparer;
        }

        public int Count { get; private set; }

        public int Capacity => _t.Capacity;

        public bool Any() => this.Count > 0;

        public void Clear()
        {
            _t.Clear();
            this.Count = 0;
        }

        public void TrimExcess() => _t.TrimExcess();

        public void Enqueue(T item)
        {
            var i = this.Count++;
            while (i > 0)
            {
                var p = (i - 1) >> 1;
                if (_comparer.Compare(_t[p], item) <= 0) break;
                Set(i, _t[p]);
                i = p;
            }
            Set(i, item);
        }

        public T Dequeue()
        {
            if (this.Count == 0) throw new InvalidOperationException();
            var ret = _t[0];
            var x = _t[--this.Count];
            var i = 0;
            while ((i << 1) + 1 < this.Count)
            {
                var a = (i << 1) + 1;
                var b = (i << 1) + 2;
                if (b < this.Count && _comparer.Compare(_t[b], _t[a]) < 0) a = b;
                if (_comparer.Compare(_t[a], x) >= 0) break;
                _t[i] = _t[a];
                i = a;
            }
            _t[i] = x;
            return ret;
        }

        public T Peek()
        {
            if (this.Count == 0) throw new InvalidOperationException();
            return _t[0];
        }

        private void Set(int i, T value)
        {
            if (i < _t.Count) _t[i] = value;
            else if (i == _t.Count) _t.Add(value);
            else throw new InvalidOperationException();
        }
    }
}
