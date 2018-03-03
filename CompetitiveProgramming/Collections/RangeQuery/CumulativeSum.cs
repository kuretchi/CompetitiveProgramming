using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompetitiveProgramming.Math.Algebraic;

namespace CompetitiveProgramming.Collections.RangeQuery
{
    public class CumulativeSumOnMonoid<T, TMonoid> :
        IPrefixRangeConcatable<T>
        where TMonoid : struct, IMonoid<T>
    {
        private static readonly TMonoid _monoid = default(TMonoid);
        protected readonly T[] _t;

        public CumulativeSumOnMonoid(IEnumerable<T> collection)
        {
            _t = collection.ToArray();
            for (var i = 1; i < _t.Length; i++)
                _t[i] = _monoid.Append(_t[i - 1], _t[i]);
        }

        public int Length => _t.Length;

        public T Concat(int r)
            => r == 0 ? _monoid.Unit : _t[r - 1];
    }

    public class CumulativeSum<T, TGroup> : CumulativeSumOnMonoid<T, TGroup>,
        IRangeConcatable<T>
        where TGroup : struct, IGroup<T>
    {
        private static readonly TGroup _group = default(TGroup);

        public CumulativeSum(IEnumerable<T> collection) : base(collection) { }

        public T Concat(int l, int r)
        {
            if (l < 0 || r < l || this.Length < r) throw new IndexOutOfRangeException();
            return _group.Append(this.Concat(r), _group.Invert(this.Concat(l)));
        }
    }
}
