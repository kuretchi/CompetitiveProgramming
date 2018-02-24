using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitiveProgramming.Algorithms
{
    public class CoordinateCompressor<T>
    {
        private readonly Dictionary<T, int> _compress;
        private readonly T[] _decompress;

        public CoordinateCompressor(IEnumerable<T> coordinates) : this(coordinates, Comparer<T>.Default) { }

        public CoordinateCompressor(IEnumerable<T> coordinates, IComparer<T> comparer)
        {
            var arr = coordinates.ToArray();
            Array.Sort(arr, comparer);
            _compress = new Dictionary<T, int>(arr.Length);
            _decompress = arr;
            var pi = default(int);
            var pa = default(T);
            if (arr.Any()) _compress.Add(arr[0], 0);

            for (var i = 0; i < arr.Length; i++)
                if (i > 0 && comparer.Compare(arr[i], pa) != 0) _compress[pa = arr[i]] = pi = i;
        }

        public int Count => _decompress.Length - 1;

        public int Compress(T coordinate) => _compress[coordinate];

        public T Decompress(int index) => _decompress[index];
    }
}
