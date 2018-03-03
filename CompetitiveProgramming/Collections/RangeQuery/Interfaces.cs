using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitiveProgramming.Collections.RangeQuery
{
    public interface IPointGettable<T>
    {
        T GetAt(int i);
    }

    public interface IPointSettable<T>
    {
        void SetAt(int i, T value);
    }

    public interface IPrefixRangeConcatable<T>
    {
        T Concat(int r);
    }

    public interface IRangeConcatable<T>
    {
        T Concat(int l, int r);
    }

    public interface IPointAppendable<T>
    {
        void AppendAt(int i, T value);
    }

    public interface IRangeAppendable<T>
    {
        void Append(int l, int r, T value);
    }
}
