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

    // requires Append(x, y) == Append(y, x)
    public interface ICommutativeGroup<T> : IGroup<T>, ICommutativeMonoid<T> { }

    public struct SumGroup_Int32 : ICommutativeGroup<int>
    {
        public int Identity => 0;
        public int Append(int left, int right) => left + right;
        public int Invert(int value) => -value;
    }

    public struct SumGroup_Int64 : ICommutativeGroup<long>
    {
        public long Identity => 0L;
        public long Append(long left, long right) => left + right;
        public long Invert(long value) => -value;
    }
}
