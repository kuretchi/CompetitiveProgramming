using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitiveProgramming.Extensions
{
    public static class Extensions
    {
        public static void Answer(object value)
        {
            Console.WriteLine(value);
            Exit(0);
        }

        public static void Assert(bool condition)
        {
            if (!condition) throw new Exception("Assertion failed");
        }

        public static string AsString(this IEnumerable<char> source) => new string(source.ToArray());

        public static Dictionary<T, int> Bucket<T>(this IEnumerable<T> source) where T : IEquatable<T>
        {
            var dict = new Dictionary<T, int>();
            foreach (var item in source) if (dict.ContainsKey(item)) dict[item]++; else dict[item] = 1;
            return dict;
        }

        public static int[] Bucket<T>(this IEnumerable<T> source, int maxValue, Func<T, int> selector)
        {
            var arr = new int[maxValue + 1];
            foreach (var item in source) arr[selector(item)]++;
            return arr;
        }

        public static IComparer<T> CreateDescendingComparer<T>()
            where T : IComparable<T>
            => Comparer<T>.Create((x, y) => y.CompareTo(x));

        public static IEnumerable<int> CumSum(this IEnumerable<int> source)
        {
            var sum = 0;
            foreach (var item in source) yield return sum += item;
        }

        public static IEnumerable<long> CumSum(this IEnumerable<long> source)
        {
            var sum = 0L;
            foreach (var item in source) yield return sum += item;
        }

        public static void Exit(int exitCode)
        {
            Console.Out.Flush();
            Environment.Exit(exitCode);
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source) action(item);
        }

        public static void ForEach<T, _>(this IEnumerable<T> source, Func<T, _> func)
        {
            foreach (var item in source) func(item);
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            var i = 0;
            foreach (var item in source) action(item, i++);
        }

        public static void ForEach<T, _>(this IEnumerable<T> source, Func<T, int, _> func)
        {
            var i = 0;
            foreach (var item in source) func(item, i++);
        }

        // [l, r)
        public static bool IsIn<T>(this T value, T l, T r)
            where T : IComparable<T>
        {
            if (l.CompareTo(r) > 0) throw new ArgumentException();
            return l.CompareTo(value) <= 0 && value.CompareTo(r) < 0;
        }

        public static IEnumerable<int> Range(int start, int end, int step = 1)
        {
            for (var i = start; i < end; i += step) yield return i;
        }

        public static IEnumerable<int> Range(int end) => Range(0, end);

        public static void Repeat(int count, Action action)
        {
            for (var i = 0; i < count; i++) action();
        }

        public static void Repeat(int count, Action<int> action)
        {
            for (var i = 0; i < count; i++) action(i);
        }

        public static IEnumerable<T> Repeat<T>(int count, Func<T> func)
        {
            for (var i = 0; i < count; i++) yield return func();
        }

        public static IEnumerable<T> Repeat<T>(int count, Func<int, T> func)
        {
            for (var i = 0; i < count; i++) yield return func(i);
        }

        public static void Swap<T>(ref T x, ref T y)
        {
            var tmp = x; x = y; y = tmp;
        }
    }
}
