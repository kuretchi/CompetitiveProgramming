using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompetitiveProgramming.Extensions;

namespace CompetitiveProgramming.Graphs
{
    public class WarshallFloyd<T, TMinMonoid, TSumMonoid>
        where TMinMonoid : struct, IMonoid<T>
        where TSumMonoid : struct, IMonoid<T>
    {
        private static readonly TMinMonoid _min = default(TMinMonoid);
        private static readonly TSumMonoid _sum = default(TSumMonoid);
        private readonly T[][] _d;

        public WarshallFloyd(AdjacencyMatrix<T, TMinMonoid, TSumMonoid> graph)
        {
            _d = graph.ToArray();
            for (var k = 0; k < graph.Length; k++)
                for (var i = 0; i < graph.Length; i++)
                    for (var j = 0; j < graph.Length; j++)
                        _d[i][j] = _min.Append(_d[i][j], _sum.Append(_d[i][k], _d[k][j]));
        }

        public T GetMinCost(int source, int target) => _d[source][target];
    }

    public static class WarshallFloyd
    {
        public static WarshallFloyd<T, TMinMonoid, TSumMonoid> Create<T, TMinMonoid, TSumMonoid>(AdjacencyMatrix<T, TMinMonoid, TSumMonoid> graph)
            where TMinMonoid : struct, IMonoid<T>
            where TSumMonoid : struct, IMonoid<T>
            => new WarshallFloyd<T, TMinMonoid, TSumMonoid>(graph);
    }
}
