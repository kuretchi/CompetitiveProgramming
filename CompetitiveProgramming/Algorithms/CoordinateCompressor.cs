﻿using System;
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
            if (!arr.Any()) return;
            T prevItem;
            var index = 0;
            _compress[prevItem = arr.First()] = index++;
            for (var i = 1; i < arr.Length; i++)
                if (comparer.Compare(arr[i], prevItem) != 0)
                    _compress[prevItem = arr[i]] = index++;
        }

        public int Count => _decompress.Length - 1;

        public int Compress(T coordinate) => _compress[coordinate];

        public T Decompress(int index) => _decompress[index];
    }
}
