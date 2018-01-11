using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompetitiveProgramming.Extensions;

namespace CompetitiveProgramming.Collections.RangeQuery
{
    public class FenwickTreeOnMonoid<T, TMonoid>
        where TMonoid : struct, IMonoid<T> // commutative
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly TMonoid _monoid = default(TMonoid);
        protected readonly T[] _tree;

        public FenwickTreeOnMonoid(int length)
        {
            _tree = Enumerable.Repeat(_monoid.Unit, length + 1).ToArray();
        }

        public FenwickTreeOnMonoid(IReadOnlyList<T> collection)
        {
            var count = collection.Count;
            _tree = new T[count + 1];
            for (var i = 0; i < count; i++) _tree[i + 1] = collection[i];
            for (var i = 1; i < count; i++)
            {
                var j = i + (i & -i);
                if (j < count + 1) _tree[j] = _monoid.Append(_tree[j], _tree[i]);
            }
        }

        public int Length => _tree.Length - 1;

        public T Concat(int l)
        {
            var acc = _monoid.Unit;
            for (l++; l > 0; l -= l & -l) acc = _monoid.Append(acc, _tree[l]);
            return acc;
        }

        public void Append(int r, T value)
        {
            for (r++; r <= this.Length; r += r & -r) _tree[r] = _monoid.Append(_tree[r], value);
        }
    }

    public class FenwickTree<T, TGroup> : FenwickTreeOnMonoid<T, TGroup>
        where TGroup : struct, IGroup<T> // commutative
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly TGroup _group = default(TGroup);

        public FenwickTree(int length) : base(length) { }

        public FenwickTree(IReadOnlyList<T> collection) : base(collection) { }

        public T this[int i]
        {
            get { return this[i, i]; }
            set { this.Append(i, _group.Append(value, _group.Invert(this[i]))); }
        }

        public T this[int l, int r]
        {
            get
            {
                var acc = _group.Unit;
                r++;
                for (; r > l; r -= r & -r) acc = _group.Append(acc, _tree[r]);
                for (; l > r; l -= l & -l) acc = _group.Append(acc, _group.Invert(_tree[l]));
                return acc;
            }
        }

        // for debug
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        internal IEnumerable<T> Values
        {
            get { for (var i = 0; i < this.Length; i++) yield return this[i]; }
        }
    }
}
