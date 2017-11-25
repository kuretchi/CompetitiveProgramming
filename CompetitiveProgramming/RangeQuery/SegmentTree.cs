using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompetitiveProgramming.Extensions;

namespace CompetitiveProgramming.RangeQuery
{
    public class SegmentTree<T> : IReadOnlyList<T>
    {
        private readonly T[] _tree;
        private readonly int _size;

        public SegmentTree(int length, Monoid<T> monoid)
        {
            if (length > (int.MaxValue >> 1) + 1) throw new ArgumentException();
            _size = 1;
            while (_size < length) _size <<= 1;
            _tree = Enumerable.Repeat(monoid.Unit, _size << 1).ToArray();
            this.Monoid = monoid;
            this.Length = length;
        }

        public SegmentTree(IList<T> collection, Monoid<T> monoid)
        {
            if (collection.Count > (int.MaxValue >> 1) + 1) throw new ArgumentException();
            _size = 1;
            while (_size < collection.Count) _size <<= 1;
            _tree = new T[_size << 1];
            for (var i = 0; i < _size; i++)
                _tree[i + _size] = i < collection.Count ? collection[i] : monoid.Unit;
            for (var i = _size - 1; i > 0; i--)
                _tree[i] = monoid.Append(_tree[i << 1], _tree[(i << 1) + 1]);
            this.Monoid = monoid;
            this.Length = collection.Count;
        }

        public Monoid<T> Monoid { get; }

        public int Length { get; }

        int IReadOnlyCollection<T>.Count => this.Length;

        public T this[int i]
        {
            get => _tree[i + _size];
            set
            {
                _tree[i += _size] = value;
                for (i >>= 1; i > 0; i >>= 1)
                    _tree[i] = this.Monoid.Append(_tree[i << 1], _tree[(i << 1) + 1]);
            }
        }

        public T this[int l, int r]
        {
            get
            {
                var lacc = this.Monoid.Unit;
                var racc = this.Monoid.Unit;
                for (l += _size, r += _size + 1; l < r; l >>= 1, r >>= 1)
                {
                    if ((l & 1) != 0) lacc = this.Monoid.Append(lacc, _tree[l++]);
                    if ((r & 1) != 0) racc = this.Monoid.Append(_tree[--r], racc);
                }
                return this.Monoid.Append(lacc, racc);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < this.Length; i++) yield return this[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
