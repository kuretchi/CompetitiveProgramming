using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitiveProgramming.Extensions
{
    public interface IBinaryOperator<in TLeft, in TRight, out TResult>
    {
        TResult Operate(TLeft left, TRight right);
    }

    public struct AddOperator_Int32 : IBinaryOperator<int, int, int>
    {
        public int Operate(int left, int right) => left + right;
    }

    public struct AddOperator_Int64 : IBinaryOperator<long, long, long>
    {
        public long Operate(long left, long right) => left + right;
    }
}
