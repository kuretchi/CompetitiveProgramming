using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitiveProgramming.Algorithms
{
    public static class BinarySearch
    {
        // source[source.LowerBound(value)] <= value
        // Example:
        //   var source = new[] { 10, 30, 30, 50 };
        //   source.LowerBound(20) -> 1
        //   source.LowerBound(30) -> 1

        public static int LowerBound<T>(this IReadOnlyList<T> source, T value) where T : IComparable<T>
            => source.LowerBound(value, Comparer<T>.Default);

        public static int LowerBound<T>(this IReadOnlyList<T> source, T value, IComparer<T> comparer)
            => LowerBound(i => source[i], 0, source.Count - 1, value, comparer);

        public static int LowerBound<T>(
            Func<int, T> func, int minValue, int maxValue, T value) where T : IComparable<T>
            => LowerBound(func, minValue, maxValue, value, Comparer<T>.Default);

        public static int LowerBound<T>(
            Func<int, T> func, int minValue, int maxValue, T value, IComparer<T> comparer)
        {
            var i = LowerBound(x => comparer.Compare(func(x), value) >= 0, minValue, maxValue);
            return comparer.Compare(func(i), value) < 0 ? i + 1 : i;
        }

        public static int LowerBound(Predicate<int> predicate, int minValue, int maxValue)
        {
            int i;
            return Search(predicate, maxValue, minValue - 1, out i) ? i : i + 1;
        }

        // value < source[source.UpperBound(value)]
        // Example:
        //   var source = new[] { 10, 30, 30, 50 };
        //   source.UpperBound(20) -> 1
        //   source.UpperBound(30) -> 3
        //   source.UpperBound(50) -> 4

        public static int UpperBound<T>(this IReadOnlyList<T> source, T value) where T : IComparable<T>
            => source.UpperBound(value, Comparer<T>.Default);

        public static int UpperBound<T>(
            Func<int, T> func, int minValue, int maxValue, T value) where T : IComparable<T>
            => UpperBound(func, minValue, maxValue, value, Comparer<T>.Default);

        public static int UpperBound<T>(this IReadOnlyList<T> source, T value, IComparer<T> comparer)
            => UpperBound(i => source[i], 0, source.Count - 1, value, comparer);

        public static int UpperBound<T>(
            Func<int, T> func, int minValue, int maxValue, T value, IComparer<T> comparer)
        {
            var i = LowerBound(x => comparer.Compare(func(x), value) > 0, minValue, maxValue);
            return comparer.Compare(func(i), value) <= 0 ? i + 1 : i;
        }

        public static int UpperBound(Predicate<int> predicate, int minValue, int maxValue)
        {
            int i;
            return Search(predicate, minValue, maxValue + 1, out i) ? i : i - 1;
        }

        private static bool Search(Predicate<int> predicate, int ok, int ng, out int i)
        {
            while (System.Math.Abs(ok - ng) > 1)
            {
                var mid = ng + (ok - ng) / 2;
                if (predicate(mid)) ok = mid;
                else ng = mid;
            }

            i = ok;
            return true;
        }
    }
}
