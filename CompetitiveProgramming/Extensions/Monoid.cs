using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace CompetitiveProgramming.Extensions
{
    public class Monoid<T>
    {
        public Monoid(T unit, Func<T, T, T> append)
        {
            this.Unit = unit;
            this.Append = append;
        }

        public T Unit { get; }

        public Func<T, T, T> Append { get; }
    }

    public static class Monoid
    {
        public static Monoid<T> Create<T>(T unit, Func<T, T, T> append) => new Monoid<T>(unit, append);

        public static readonly Monoid<int> Min_Int32 = new Monoid<int>(int.MaxValue, Min);
        public static readonly Monoid<int> Max_Int32 = new Monoid<int>(int.MinValue, Max);

        public static readonly Monoid<long> Min_Int64 = new Monoid<long>(long.MaxValue, Min);
        public static readonly Monoid<long> Max_Int64 = new Monoid<long>(long.MinValue, Max);
    }
}
