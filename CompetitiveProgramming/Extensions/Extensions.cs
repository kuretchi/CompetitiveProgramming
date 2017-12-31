using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CompetitiveProgramming.Extensions
{
    public static class Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Assert(bool condition)
        {
            if (!condition) throw new Exception("Assertion failed");
        }

        public static string AsString(this IEnumerable<char> source) => new string(source.ToArray());

        public static void Exit(int exitCode)
        {
            Console.Out.Flush();
            Environment.Exit(exitCode);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source) action(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForEach<T, _>(this IEnumerable<T> source, Func<T, _> func)
        {
            foreach (var item in source) func(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            var i = 0;
            foreach (var item in source) action(item, i++);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForEach<T, _>(this IEnumerable<T> source, Func<T, int, _> func)
        {
            var i = 0;
            foreach (var item in source) func(item, i++);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Iterate<T>(int count, T seed, Func<T, T> func)
        {
            var r = seed;
            for (var i = 0; i < count; i++) r = func(r);
            return r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Repeat(int count, Action action)
        {
            for (var i = 0; i < count; i++) action();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Repeat(int count, Action<int> action)
        {
            for (var i = 0; i < count; i++) action(i);
        }

        public static IEnumerable<T> Repeat<T>(Func<T> func)
        {
            for (var i = 0; ; i++) yield return func();
        }

        public static IEnumerable<T> Repeat<T>(int count, Func<T> func)
        {
            for (var i = 0; i < count; i++) yield return func();
        }

        public static IEnumerable<T> Repeat<T>(Func<int, T> func)
        {
            for (var i = 0; ; i++) yield return func(i);
        }

        public static IEnumerable<T> Repeat<T>(int count, Func<int, T> func)
        {
            for (var i = 0; i < count; i++) yield return func(i);
        }

        public static void Swap<T>(ref T x, ref T y)
        {
            var tmp = x; x = y; y = tmp;
        }

        public static (T1[], T2[]) Unzip<T1, T2>(this ICollection<(T1, T2)> source)
        {
            var ts1 = new T1[source.Count];
            var ts2 = new T2[source.Count];
            var i = 0;
            foreach (var (t1, t2) in source) { ts1[i] = t1; ts2[i] = t2; i++; }
            return (ts1, ts2);
        }

        public static (T1[], T2[], T3[]) Unzip<T1, T2, T3>(this ICollection<(T1, T2, T3)> source)
        {
            var ts1 = new T1[source.Count];
            var ts2 = new T2[source.Count];
            var ts3 = new T3[source.Count];
            var i = 0;
            foreach (var (t1, t2, t3) in source) { ts1[i] = t1; ts2[i] = t2; ts3[i] = t3; i++; }
            return (ts1, ts2, ts3);
        }

        public static (T1[], T2[], T3[], T4[]) Unzip<T1, T2, T3, T4>(this ICollection<(T1, T2, T3, T4)> source)
        {
            var ts1 = new T1[source.Count];
            var ts2 = new T2[source.Count];
            var ts3 = new T3[source.Count];
            var ts4 = new T4[source.Count];
            var i = 0;
            foreach (var (t1, t2, t3, t4) in source) { ts1[i] = t1; ts2[i] = t2; ts3[i] = t3; ts4[i] = t4; i++; }
            return (ts1, ts2, ts3, ts4);
        }

        public static IEnumerable<T> Zip<T1, T2, T3, T>(this IEnumerable<T1> first, IEnumerable<T2> second, IEnumerable<T3> thrid, Func<T1, T2, T3, T> resultSelector)
        {
            using (var e1 = first.GetEnumerator())
            using (var e2 = second.GetEnumerator())
            using (var e3 = thrid.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext())
                    yield return resultSelector(e1.Current, e2.Current, e3.Current);
            }
        }

        public static IEnumerable<T> Zip<T1, T2, T3, T4, T>(this IEnumerable<T1> first, IEnumerable<T2> second, IEnumerable<T3> thrid, IEnumerable<T4> fourth, Func<T1, T2, T3, T4, T> resultSelector)
        {
            using (var e1 = first.GetEnumerator())
            using (var e2 = second.GetEnumerator())
            using (var e3 = thrid.GetEnumerator())
            using (var e4 = fourth.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext() && e4.MoveNext())
                    yield return resultSelector(e1.Current, e2.Current, e3.Current, e4.Current);
            }
        }
    }
}
