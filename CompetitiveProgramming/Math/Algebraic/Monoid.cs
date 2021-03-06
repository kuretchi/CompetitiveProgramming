﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace CompetitiveProgramming.Math.Algebraic
{
    public interface IMonoid<T>
    {
        T Identity { get; }
        T Append(T left, T right);
    }

    // requires Append(x, y) == Append(y, x)
    public interface ICommutativeMonoid<T> : IMonoid<T> { }

    public struct MinMonoid_Int32 : ICommutativeMonoid<int>
    {
        public int Identity => int.MaxValue;
        public int Append(int left, int right) => Min(left, right);
    }

    public struct MaxMonoid_Int32 : ICommutativeMonoid<int>
    {
        public int Identity => int.MinValue;
        public int Append(int left, int right) => Max(left, right);
    }

    public struct MinMonoid_Int64 : ICommutativeMonoid<long>
    {
        public long Identity => long.MaxValue;
        public long Append(long left, long right) => Min(left, right);
    }

    public struct MaxMonoid_Int64 : ICommutativeMonoid<long>
    {
        public long Identity => long.MinValue;
        public long Append(long left, long right) => Max(left, right);
    }
}
