using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitiveProgramming.Math.Algebraic
{
    public interface IGroup<T> : IMonoid<T>
    {
        T Invert(T value);
    }

    public struct SumGroup_Int32 : IGroup<int>
    {
        public int Unit => 0;
        public int Append(int left, int right) => left + right;
        public int Invert(int value) => -value;
    }

    public struct SumGroup_Int64 : IGroup<long>
    {
        public long Unit => 0L;
        public long Append(long left, long right) => left + right;
        public long Invert(long value) => -value;
    }
}
