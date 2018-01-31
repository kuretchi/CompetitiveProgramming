using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompetitiveProgramming.Extensions;

namespace CompetitiveProgramming.Graphs
{
    public class AdjacencyMatrix<T, TMinMonoid, TSumMonoid>
        where TMinMonoid : struct, IMonoid<T>
        where TSumMonoid : struct, IMonoid<T>
    {
        private static readonly TMinMonoid _min = default(TMinMonoid);
        private static readonly TSumMonoid _sum = default(TSumMonoid);
        private readonly T[][] _matrix;

        public AdjacencyMatrix(int length)
        {
            _matrix = new T[length][];
            for (var i = 0; i < length; i++) _matrix[i] = Enumerable.Repeat(_min.Unit, length).ToArray();
            for (var i = 0; i < length; i++) _matrix[i][i] = _sum.Unit;
            this.Length = length;
        }

        public int Length { get; }

        public T this[int source, int target]
        {
            get { return _matrix[source][target]; }
            set { _matrix[source][target] = _min.Append(_matrix[source][target], value); }
        }

        public T[][] ToArray()
        {
            var array = new T[this.Length][];
            _matrix.CopyTo(array, 0);
            for (var i = 0; i < this.Length; i++) _matrix[i].CopyTo(array[i], 0);
            return array;
        }
    }
}
