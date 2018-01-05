using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompetitiveProgramming.Extensions;

namespace CompetitiveProgramming.Collections.RangeQuery
{
    public class FenwickTreeOnMonoid<T, TMonoid> : IEnumerable<T>
        where TMonoid : struct, IMonoid<T> // commutative
    {
        private static readonly TMonoid _monoid = default(TMonoid);
        protected readonly T[] _tree;

        public FenwickTreeOnMonoid(int length)
        {
            _tree = Enumerable.Repeat(_monoid.Unit, length + 1).ToArray();
        }

        public FenwickTreeOnMonoid(IEnumerable<T> collection)
        {
            var count = collection.Count();
            _tree = new T[count + 1];
            collection.ForEach((x, i) => { _tree[i + 1] = x; });
            for (var i = 1; i < count; i++)
            {
                var j = i + (i & -i);
                _tree[j] = _monoid.Append(_tree[j], _tree[i]);
            }
        }

        public int Length => _tree.Length - 1;

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < this.Length; i++) yield return Concat(i);
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

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

    public class FenwickTree<T, TGroup> : FenwickTreeOnMonoid<T, TGroup>, IReadOnlyList<T>
        where TGroup : struct, IGroup<T> // commutative
    {
        private static readonly TGroup _group = default(TGroup);

        public FenwickTree(int length) : base(length) { }

        public FenwickTree(IEnumerable<T> collection) : base(collection) { }

        int IReadOnlyCollection<T>.Count => this.Length;

        public T this[int i]
        {
            get => this[i, i];
            set => this.Append(i, _group.Append(value, _group.Invert(this[i])));
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
    }
}
