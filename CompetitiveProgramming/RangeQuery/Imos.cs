using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompetitiveProgramming.Extensions;

namespace CompetitiveProgramming.RangeQuery
{
    public class Imos<T> : IReadOnlyList<T>
    {
        private readonly T[] _array;

        public Imos(int length, Monoid<T> monoid)
        {
            _array = Enumerable.Repeat(monoid.Unit, length).ToArray();
            this.Monoid = monoid;
        }

        public Imos(int length, Group<T> group) : this(length, (Monoid<T>)group)
        {
            this.Group = group;
        }

        public Monoid<T> Monoid { get; }

        public Group<T> Group { get; }

        public int Degree { get; private set; } = -1;

        public int Length => _array.Length;

        int IReadOnlyCollection<T>.Count => this.Length;

        public T this[int i] => this.Degree < 0 ? throw new InvalidOperationException() : _array[i];

        public IEnumerator<T> GetEnumerator()
        {
            if (this.Degree < 0) throw new InvalidOperationException();
            foreach (var value in _array) yield return value;
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public void Append(int l, int r, T value)
        {
            _array[l] = this.Group.Append(_array[l], value);
            if (r == _array.Length - 1) return;
            _array[r + 1] = this.Group.Append(_array[r + 1], this.Group.Invert(value));
        }

        public void Append(int l, T value)
        {
            _array[l] = this.Monoid.Append(_array[l], value);
        }

        public void Integrate()
        {
            for (var i = 1; i < _array.Length; i++)
                _array[i] = this.Monoid.Append(_array[i - 1], _array[i]);
            this.Degree++;
        }

        public void Differentiate()
        {
            if (this.Degree < 0) throw new InvalidOperationException();
            for (var i = _array.Length - 1; i > 0; i--)
                _array[i] = this.Group.Append(_array[i], this.Group.Invert(_array[i - 1]));
            this.Degree--;
        }
    }
}
