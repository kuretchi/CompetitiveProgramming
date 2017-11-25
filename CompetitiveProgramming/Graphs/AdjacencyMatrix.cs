using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompetitiveProgramming.Extensions;

namespace CompetitiveProgramming.Graphs
{
    public class AdjacencyMatrix<T>
    {
        private readonly T[][] _matrix;

        public AdjacencyMatrix(int length, Monoid<T> min, Monoid<T> sum)
        {
            _matrix = new T[length][];
            for (var i = 0; i < length; i++) _matrix[i] = Enumerable.Repeat(min.Unit, length).ToArray();
            for (var i = 0; i < length; i++) _matrix[i][i] = sum.Unit;
            this.Length = length;
            this.Min = min;
            this.Sum = sum;
        }

        public int Length { get; }

        public Monoid<T> Min { get; }

        public Monoid<T> Sum { get; }

        public T this[int source, int target]
        {
            get => _matrix[source][target];
            set => _matrix[source][target] = this.Min.Append(_matrix[source][target], value);
        }

        public T[][] ToArray() => _matrix;
    }
}
