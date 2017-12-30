using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompetitiveProgramming.Extensions;

namespace CompetitiveProgramming.RangeQuery
{
    public class SegmentTree<T, TMonoid> : IReadOnlyList<T> where TMonoid : struct, IMonoid<T>
    {
        private readonly TMonoid _monoid;
        private readonly T[] _tree;
        private readonly int _size;

        public SegmentTree(int length)
        {
            _monoid = default(TMonoid);
            if (length > (int.MaxValue >> 1) + 1) throw new ArgumentException();
            _size = 1;
            while (_size < length) _size <<= 1;
            _tree = Enumerable.Repeat(_monoid.Unit, _size << 1).ToArray();
            this.Length = length;
        }

        public SegmentTree(IList<T> collection)
        {
            _monoid = default(TMonoid);
            if (collection.Count > (int.MaxValue >> 1) + 1) throw new ArgumentException();
            _size = 1;
            while (_size < collection.Count) _size <<= 1;
            _tree = new T[_size << 1];
            for (var i = 0; i < _size; i++)
                _tree[i + _size] = i < collection.Count ? collection[i] : _monoid.Unit;
            for (var i = _size - 1; i > 0; i--)
                _tree[i] = _monoid.Append(_tree[i << 1], _tree[(i << 1) + 1]);
            this.Length = collection.Count;
        }

        public int Length { get; }

        int IReadOnlyCollection<T>.Count => this.Length;

        public T this[int i]
        {
            get => _tree[i + _size];
            set
            {
                _tree[i += _size] = value;
                for (i >>= 1; i > 0; i >>= 1)
                    _tree[i] = _monoid.Append(_tree[i << 1], _tree[(i << 1) + 1]);
            }
        }

        public T this[int l, int r]
        {
            get
            {
                var lacc = _monoid.Unit;
                var racc = _monoid.Unit;
                for (l += _size, r += _size + 1; l < r; l >>= 1, r >>= 1)
                {
                    if ((l & 1) != 0) lacc = _monoid.Append(lacc, _tree[l++]);
                    if ((r & 1) != 0) racc = _monoid.Append(_tree[--r], racc);
                }
                return _monoid.Append(lacc, racc);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < this.Length; i++) yield return this[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
