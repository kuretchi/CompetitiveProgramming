using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompetitiveProgramming.Math.Algebraic;

namespace CompetitiveProgramming.Collections.RangeQuery
{
    public class Imos<T, TGroup> : IReadOnlyCollection<T>,
        IRangeAppendable<T>, IPointGettable<T>
        where TGroup : struct, ICommutativeGroup<T>
    {
        private static readonly TGroup _group = default(TGroup);
        private readonly Action<int, int, T, Action<int, T>> _append;
        private readonly int _degree;
        private readonly T[] _t;

        public Imos(int length, int degree, Action<int, int, T, Action<int, T>> append)
        {
            _degree = degree;
            _append = append;
            _t = Enumerable.Repeat(_group.Identity, length).ToArray();
            this.Length = length;
        }

        public int Length { get; }

        public bool IsIntegrated { get; protected set; }

        int IReadOnlyCollection<T>.Count => this.Length;

        public T this[int i] => GetAt(i);

        // [l, r) += value
        public void Append(int l, int r, T value)
        {
            if (this.IsIntegrated) throw new InvalidOperationException();
            _append(l, r, value, (i, v) => _t[i] = _group.Append(_t[i], v));
        }

        public T GetAt(int i)
        {
            if (!this.IsIntegrated) throw new InvalidOperationException();
            return _t[i];
        }

        public void Integrate()
        {
            if (this.IsIntegrated) throw new InvalidOperationException();
            for (var i = 0; i <= _degree; i++)
                for (var j = 1; j < _t.Length; j++)
                    _t[j] = _group.Append(_t[j - 1], _t[j]);
            this.IsIntegrated = true;
        }

        public void Differentiate()
        {
            if (!this.IsIntegrated) throw new InvalidOperationException();
            for (var i = 0; i <= _degree; i++)
                for (var j = _t.Length - 1; j > 0; j--)
                    _t[j] = _group.Append(_t[j], _group.Invert(_t[j - 1]));
            this.IsIntegrated = false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (!this.IsIntegrated) throw new InvalidOperationException();
            for (var i = 0; i < _t.Length; i++)
                yield return _t[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();
    }
}
