using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitiveProgramming.Extensions
{
    public class Group<T> : Monoid<T>
    {
        public Group(T unit, Func<T, T, T> append, Func<T, T> invert) : base(unit, append)
        {
            this.Invert = invert;
        }

        public Func<T, T> Invert { get; }
    }

    public static class Group
    {
        public static Group<T> Create<T>(T unit, Func<T, T, T> append, Func<T, T> invert)
            => new Group<T>(unit, append, invert);

        public static readonly Group<int> Sum_Int32 = new Group<int>(0, (x, y) => x + y, x => -x);
        public static readonly Group<long> Sum_Int64 = new Group<long>(0L, (x, y) => x + y, x => -x);
    }
}
