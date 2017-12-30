using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitiveProgramming.Algorithms
{
    public class CoordinateCompressor<T> where T : IComparable<T>, IEquatable<T>
    {
        private readonly Dictionary<T, int> _compress;
        private readonly T[] _decompress;

        public CoordinateCompressor(IEnumerable<T> coordinates)
        {
            var arr = coordinates.ToArray();
            Array.Sort(arr);
            _compress = new Dictionary<T, int>(arr.Length);
            _decompress = arr;
            var pi = default(int);
            var pa = default(T);
            if (arr.Any()) _compress.Add(arr[0], 0);

            for (var i = 0; i < arr.Length; i++)
                if (i > 0 && !arr[i].Equals(pa)) _compress[pa = arr[i]] = pi = i;
        }

        public int Compress(T coordinate) => _compress[coordinate];

        public T Decompress(int index) => _decompress[index];
    }
}
