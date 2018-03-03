using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompetitiveProgramming.Math.Algebraic;

namespace CompetitiveProgramming.Collections.RangeQuery
{
    public class SegmentTree<T, TMonoid> :
        IPointGettable<T>, IPointSettable<T>, IRangeConcatable<T>
        where TMonoid : struct, IMonoid<T>
    {
        private static readonly TMonoid _monoid = default(TMonoid);
        private const int _maxLength = (int.MaxValue >> 1) + 1;
        private readonly T[] _t;
        private readonly int _size;

        public SegmentTree(int length)
        {
            if (length < 0 || _maxLength < length) throw new ArgumentOutOfRangeException();
            _size = 1;
            while (_size < length) _size <<= 1;
            _t = Enumerable.Repeat(_monoid.Identity, _size << 1).ToArray();
            this.Length = length;
        }

        public SegmentTree(IReadOnlyList<T> collection)
        {
            if (collection.Count > _maxLength) throw new ArgumentException();
            _size = 1;
            while (_size < collection.Count) _size <<= 1;
            _t = new T[_size << 1];
            for (var i = 0; i < _size; i++)
                _t[i + _size] = i < collection.Count ? collection[i] : _monoid.Identity;
            for (var i = _size - 1; i > 0; i--)
                _t[i] = _monoid.Append(_t[i << 1], _t[(i << 1) + 1]);
            this.Length = collection.Count;
        }

        public int Length { get; }

        public T this[int i] => GetAt(i);

        public T GetAt(int i)
        {
            if (i < 0 || this.Length <= i) throw new IndexOutOfRangeException();
            return _t[i + _size];
        }

        public void SetAt(int i, T value)
        {
            if (i < 0 || this.Length <= i) throw new IndexOutOfRangeException();
            _t[i += _size] = value;
            for (i >>= 1; i > 0; i >>= 1)
                _t[i] = _monoid.Append(_t[i << 1], _t[(i << 1) + 1]);
        }

        // [l, r)
        public T Concat(int l, int r)
        {
            if (l < 0 || r < l || this.Length < r) throw new IndexOutOfRangeException();
            var lacc = _monoid.Identity;
            var racc = _monoid.Identity;
            for (l += _size, r += _size; l < r; l >>= 1, r >>= 1)
            {
                if ((l & 1) != 0) lacc = _monoid.Append(lacc, _t[l++]);
                if ((r & 1) != 0) racc = _monoid.Append(_t[--r], racc);
            }
            return _monoid.Append(lacc, racc);
        }

        // for debug
        internal IEnumerable<T> Values
        {
            get { for (var i = 0; i < this.Length; i++) yield return GetAt(i); }
        }
    }
}
