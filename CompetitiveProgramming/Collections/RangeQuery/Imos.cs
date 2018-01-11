using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompetitiveProgramming.Extensions;

namespace CompetitiveProgramming.Collections.RangeQuery
{
    public class ImosOnMonoid<T, TMonoid> : IReadOnlyList<T> where TMonoid : struct, IMonoid<T>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly TMonoid _monoid = default(TMonoid);
        protected readonly T[] _array;

        public ImosOnMonoid(int length)
        {
            _array = Enumerable.Repeat(_monoid.Unit, length).ToArray();
        }

        public int Degree { get; protected set; } = -1;

        public int Length => _array.Length;

        int IReadOnlyCollection<T>.Count => this.Length;

        public T this[int i] => this.Degree < 0 ? throw new InvalidOperationException() : _array[i];

        public IEnumerator<T> GetEnumerator()
        {
            if (this.Degree < 0) throw new InvalidOperationException();
            foreach (var value in _array) yield return value;
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public void Append(int l, T value)
        {
            _array[l] = _monoid.Append(_array[l], value);
        }

        public void Integrate()
        {
            for (var i = 1; i < _array.Length; i++)
                _array[i] = _monoid.Append(_array[i - 1], _array[i]);
            this.Degree++;
        }
    }

    public class Imos<T, TGroup> : ImosOnMonoid<T, TGroup>, IReadOnlyList<T> where TGroup : struct, IGroup<T>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly TGroup _group = default(TGroup);

        public Imos(int length) : base(length) { }

        public void Append(int l, int r, T value)
        {
            _array[l] = _group.Append(_array[l], value);
            if (r == _array.Length - 1) return;
            _array[r + 1] = _group.Append(_array[r + 1], _group.Invert(value));
        }

        public void Differentiate()
        {
            if (this.Degree < 0) throw new InvalidOperationException();
            for (var i = _array.Length - 1; i > 0; i--)
                _array[i] = _group.Append(_array[i], _group.Invert(_array[i - 1]));
            this.Degree--;
        }
    }
}
