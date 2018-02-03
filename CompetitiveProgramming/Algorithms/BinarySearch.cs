using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitiveProgramming.Algorithms
{
    public static class BinarySearch
    {
        public static int LowerBound<T>(this IReadOnlyList<T> source, T value)
            where T : IComparable<T>
            => source.LowerBound(value, Comparer<T>.Default);

        public static int LowerBound<T>(this IReadOnlyList<T> source, T value, IComparer<T> comparer)
            => LowerBound(i => source[i], 0, source.Count - 1, value, comparer);

        public static int LowerBound<T>(Func<int, T> func, int minValue, int maxValue, T value)
            where T : IComparable<T>
            => LowerBound(func, minValue, maxValue, value, Comparer<T>.Default);

        public static int LowerBound<T>(Func<int, T> func, int minValue, int maxValue, T value, IComparer<T> comparer)
        {
            var i = Search(x => comparer.Compare(func(x), value) >= 0, minValue, maxValue);
            return comparer.Compare(func(i), value) < 0 ? i + 1 : i;
        }

        public static int UpperBound<T>(this IReadOnlyList<T> source, T value)
            where T : IComparable<T>
            => source.UpperBound(value, Comparer<T>.Default);

        public static int UpperBound<T>(Func<int, T> func, int minValue, int maxValue, T value)
            where T : IComparable<T>
            => UpperBound(func, minValue, maxValue, value, Comparer<T>.Default);

        public static int UpperBound<T>(this IReadOnlyList<T> source, T value, IComparer<T> comparer)
            => UpperBound(i => source[i], 0, source.Count - 1, value, comparer);

        public static int UpperBound<T>(Func<int, T> func, int minValue, int maxValue, T value, IComparer<T> comparer)
        {
            var i = Search(x => comparer.Compare(func(x), value) > 0, minValue, maxValue);
            return comparer.Compare(func(i), value) <= 0 ? i + 1 : i;
        }

        public static int Search(Predicate<int> predicate, int minValue, int maxValue)
        {
            var ok = maxValue;
            var ng = minValue - 1;

            while (System.Math.Abs(ok - ng) > 1)
            {
                var mid = (ok + ng) / 2;
                if (predicate(mid)) ok = mid;
                else ng = mid;
            }

            return ok;
        }
    }
}
