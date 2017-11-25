using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CompetitiveProgramming.Math
{
    public static class MathExtensions
    {
        public static int Gcd(int left, int right)
        {
            int r;
            while ((r = left % right) != 0) { left = right; right = r; }
            return right;
        }

        public static long Gcd(long left, long right)
        {
            long r;
            while ((r = left % right) != 0L) { left = right; right = r; }
            return right;
        }

        public static BigInteger Gcd(BigInteger left, BigInteger right)
            => BigInteger.GreatestCommonDivisor(left, right);

        public static int HighestOneBit(int x)
        {
            x |= x >> 01;
            x |= x >> 02;
            x |= x >> 04;
            x |= x >> 08;
            x |= x >> 16;
            return x - (x >> 1);
        }

        public static long HighestOneBit(long x)
        {
            x |= x >> 01;
            x |= x >> 02;
            x |= x >> 04;
            x |= x >> 08;
            x |= x >> 16;
            x |= x >> 32;
            return x - (x >> 1);
        }

        public static int Lcm(int left, int right) => left / Gcd(left, right) * right;

        public static long Lcm(long left, long right) => left / Gcd(left, right) * right;

        public static BigInteger Lcm(BigInteger left, BigInteger right)
            => left / Gcd(left, right) * right;

        public static int Pow(int value, int exponent)
        {
            var r = 1;
            while (exponent > 0)
            {
                if ((exponent & 1) == 1) r *= value;
                value *= value;
                exponent >>= 1;
            }
            return r;
        }

        public static long Pow(long value, int exponent)
        {
            var r = 1L;
            while (exponent > 0)
            {
                if ((exponent & 1) == 1) r *= value;
                value *= value;
                exponent >>= 1;
            }
            return r;
        }

        public static long Fact(int value)
        {
            var r = 1L;
            for (var i = 2; i <= value; i++) r *= i;
            return r;
        }
    }
}
