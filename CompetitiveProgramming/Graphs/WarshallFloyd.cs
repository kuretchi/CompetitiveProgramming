using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitiveProgramming.Graphs
{
    public class WarshallFloyd<T>
    {
        private readonly T[][] _d;

        public WarshallFloyd(AdjacencyMatrix<T> graph)
        {
            _d = new T[graph.Length][];
            var a = graph.ToArray();
            a.CopyTo(_d, 0);
            for (var i = 0; i < graph.Length; i++) a[i].CopyTo(_d[i], 0);

            for (var k = 0; k < graph.Length; k++)
                for (var i = 0; i < graph.Length; i++)
                    for (var j = 0; j < graph.Length; j++)
                        _d[i][j] = graph.Min.Append(_d[i][j], graph.Sum.Append(_d[i][k], _d[k][j]));
        }

        public T this[int source, int target] => _d[source][target];
    }
}
