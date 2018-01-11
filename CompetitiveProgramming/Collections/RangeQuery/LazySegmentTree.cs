using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompetitiveProgramming.Extensions;

namespace CompetitiveProgramming.Collections.RangeQuery
{
    public class LazySegmentTree<T, TMonoid, TOperand, TOperandMonoid, TOperator>
        where TMonoid : struct, IMonoid<T>
        where TOperandMonoid : struct, IMonoid<TOperand>
        where TOperator : struct, IBinaryOperator<T, TOperand, T>
    {
        public class Segment
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private readonly LazySegmentTree<T, TMonoid, TOperand, TOperandMonoid, TOperator> _outer;

            public Segment(LazySegmentTree<T, TMonoid, TOperand, TOperandMonoid, TOperator> outer)
            {
                _outer = outer;
            }

            public int L { get; set; }
            public int R { get; set; }

            public void Apply(TOperand x) => Apply(0, 0, _outer._size, this.L, this.R, x);
            public T Concat() => Concat(0, 0, _outer._size, this.L, this.R);

            private void Apply(int i, int il, int ir, int l, int r, TOperand x)
            {
                if (ir <= l || r <= il) return;
                else if (l <= il && ir <= r)
                {
                    _outer._t[i] = _operator.Operate(_outer._t[i], x);
                    _outer._ope[i] = _opeMonoid.Append(x, _outer._ope[i]);
                }
                else
                {
                    Apply((i << 1) + 1, il, (il + ir) >> 1, 0, _outer._size, _outer._ope[i]);
                    Apply((i << 1) + 1, il, (il + ir) >> 1, l, r, x);
                    Apply((i << 1) + 2, (il + ir) >> 1, ir, 0, _outer._size, _outer._ope[i]);
                    Apply((i << 1) + 2, (il + ir) >> 1, ir, l, r, x);
                    _outer._t[i] = _monoid.Append(_outer._t[(i << 1) + 1], _outer._t[(i << 1) + 2]);
                    _outer._ope[i] = _opeMonoid.Unit;
                }
            }

            private T Concat(int i, int il, int ir, int l, int r)
            {
                if (ir <= l || r <= il) return _monoid.Unit;
                else if (l <= il && ir <= r) return _outer._t[i];
                else return _operator.Operate(_monoid.Append(
                    Concat((i << 1) + 1, il, (il + ir) >> 1, l, r),
                    Concat((i << 1) + 2, (il + ir) >> 1, ir, l, r)), _outer._ope[i]);
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly TMonoid _monoid = default(TMonoid);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly TOperandMonoid _opeMonoid = default(TOperandMonoid);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly TOperator _operator = default(TOperator);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Segment _segment;

        private readonly T[] _t;
        private readonly TOperand[] _ope;
        private readonly int _size;

        public LazySegmentTree(int length) : this()
        {
            if (length > (int.MaxValue >> 1) + 1) throw new ArgumentException();
            _size = 1;
            while (_size < length) _size <<= 1;
            _t = Enumerable.Repeat(_monoid.Unit, _size << 1).ToArray();
            _ope = Enumerable.Repeat(_opeMonoid.Unit, _size << 1).ToArray();
            this.Length = length;
        }

        public LazySegmentTree(IReadOnlyList<T> collection) : this()
        {
            if (collection.Count > (int.MaxValue >> 1) + 1) throw new ArgumentException();
            _size = 1;
            while (_size < collection.Count) _size <<= 1;
            _t = new T[_size << 1];
            for (var i = 0; i < _size; i++)
                _t[i + _size - 1] = i < collection.Count ? collection[i] : _monoid.Unit;
            for (var i = _size - 2; i >= 0; i--)
                _t[i] = _monoid.Append(_t[(i << 1) + 1], _t[(i << 1) + 2]);
            _ope = Enumerable.Repeat(_opeMonoid.Unit, _size << 1).ToArray();
            this.Length = collection.Count;
        }

        private LazySegmentTree()
        {
            _segment = new Segment(this);
        }

        public int Length { get; }
        public Segment this[int i] => this[i, i];
        public Segment this[int l, int r]
        {
            get { _segment.L = l; _segment.R = r + 1; return _segment; }
        }

        // for debug
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        internal IEnumerable<T> Values
        {
            get { for (var i = 0; i < this.Length; i++) yield return this[i].Concat(); }
        }
    }
}
