using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompetitiveProgramming.Math.Algebraic;

namespace CompetitiveProgramming.Collections.RangeQuery
{
    public class FenwickTreeOnMonoid<T, TMonoid> :
        IPrefixRangeConcatable<T>, IPointAppendable<T>
        where TMonoid : struct, ICommutativeMonoid<T>
    {
        private static readonly TMonoid _monoid = default(TMonoid);
        protected readonly T[] _t;

        public FenwickTreeOnMonoid(int length)
        {
            _t = Enumerable.Repeat(_monoid.Identity, length + 1).ToArray();
        }

        public FenwickTreeOnMonoid(IReadOnlyList<T> collection)
        {
            var count = collection.Count;
            _t = new T[count + 1];
            for (var i = 0; i < count; i++)
                _t[i + 1] = collection[i];
            for (var i = 1; i < count; i++)
            {
                var j = i + (i & -i);
                if (j < count + 1) _t[j] = _monoid.Append(_t[j], _t[i]);
            }
        }

        public int Length => _t.Length - 1;

        public void AppendAt(int i, T value)
        {
            for (i++; i <= this.Length; i += i & -i)
                _t[i] = _monoid.Append(_t[i], value);
        }

        // [0, r)
        public T Concat(int r)
        {
            var acc = _monoid.Identity;
            for (; r > 0; r -= r & -r)
                acc = _monoid.Append(acc, _t[r]);
            return acc;
        }
    }

    public partial class FenwickTree<T, TGroup> : FenwickTreeOnMonoid<T, TGroup>,
        IRangeConcatable<T>, IPointGettable<T>, IPointSettable<T>
        where TGroup : struct, ICommutativeGroup<T>
    {
        private static readonly TGroup _group = default(TGroup);

        public FenwickTree(int length) : base(length) { }

        public FenwickTree(IReadOnlyList<T> collection) : base(collection) { }

        public T GetAt(int i)
            => Concat(i, i + 1);

        public void SetAt(int i, T value)
            => this.AppendAt(i, _group.Append(value, _group.Invert(this.GetAt(i))));

        // [l, r)
        public T Concat(int l, int r)
        {
            var acc = _group.Identity;
            for (; r > l; r -= r & -r)
                acc = _group.Append(acc, _t[r]);
            for (; l > r; l -= l & -l)
                acc = _group.Append(acc, _group.Invert(_t[l]));
            return acc;
        }

        // for debug
        internal IEnumerable<T> Values
        {
            get { for (var i = 0; i < this.Length; i++) yield return this.GetAt(i); }
        }
    }
}
