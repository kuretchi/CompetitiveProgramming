using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompetitiveProgramming.Extensions;

namespace CompetitiveProgramming.RangeQuery
{
    public class FenwickTree<T> : IReadOnlyList<T>
    {
        private readonly T[] _tree;

        public FenwickTree(int length, Monoid<T> monoid)
        {
            _tree = Enumerable.Repeat(monoid.Unit, length + 1).ToArray();
            this.Monoid = monoid;
        }

        public FenwickTree(int length, Group<T> group) : this(length, (Monoid<T>)group)
        {
            this.Group = group;
        }

        public FenwickTree(IEnumerable<T> collection, Monoid<T> monoid)
        {
            var count = collection.Count();
            this.Monoid = monoid;
            _tree = new T[count + 1];
            collection.ForEach((x, i) => { _tree[i + 1] = x; });
            for (var i = 1; i < count; i++)
            {
                var j = i + (i & -i);
                _tree[j] = monoid.Append(_tree[j], _tree[i]);
            }
        }

        public FenwickTree(IEnumerable<T> collection, Group<T> group) : this(collection, (Monoid<T>)group)
        {
            this.Group = group;
        }

        public Monoid<T> Monoid { get; } // commutative

        public Group<T> Group { get; } // commutative

        public int Length => _tree.Length - 1;

        int IReadOnlyCollection<T>.Count => this.Length;

        public T this[int i]
        {
            get => this[i, i];
            set => this.Append(i, this.Group.Append(value, this.Group.Invert(this[i])));
        }

        public T this[int l, int r]
        {
            get
            {
                var acc = this.Group.Unit;
                r++;
                for (; r > l; r -= r & -r) acc = this.Group.Append(acc, _tree[r]);
                for (; l > r; l -= l & -l) acc = this.Group.Append(acc, this.Group.Invert(_tree[l]));
                return acc;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < this.Length; i++) yield return Concat(i);
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public T Concat(int l)
        {
            var acc = this.Monoid.Unit;
            for (l++; l > 0; l -= l & -l) acc = this.Monoid.Append(acc, _tree[l]);
            return acc;
        }

        public void Append(int r, T value)
        {
            for (r++; r <= this.Length; r += r & -r) _tree[r] = this.Monoid.Append(_tree[r], value);
        }
    }
}
