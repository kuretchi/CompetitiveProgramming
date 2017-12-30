using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using CompetitiveProgramming.Extensions;
using static CompetitiveProgramming.Math.MathExtensions;

namespace CompetitiveProgramming.Math
{
    public struct BigRational : IComparable<BigRational>, IEquatable<BigRational>
    {
        public BigRational(BigInteger numer, BigInteger denom)
        {
            if (denom == 0) throw new ArgumentException();
            this.Numer = numer;
            this.Denom = denom;
            this.Reduce();
        }

        public BigRational(BigRational numer, BigRational denom)
        {
            this = numer / denom;
        }

        public BigInteger Numer { get; private set; }

        public BigInteger Denom { get; private set; }

        private void Reduce()
        {
            if (this.Numer == 0) { this.Denom = 1; return; }
            var gcd = Gcd(this.Numer, this.Denom);
            this.Numer /= gcd; this.Denom /= gcd;
            if (this.Denom < 0) { this.Numer *= -1; this.Denom *= -1; }
        }

        public BigRational Invert()
        {
            if (this.Numer == 0) throw new InvalidOperationException();
            return new BigRational(this.Denom, this.Numer);
        }

        public static BigRational operator -(BigRational value)
            => new BigRational(-value.Numer, value.Denom);

        public static BigRational operator +(BigRational left, BigRational right)
        {
            var d = Lcm(left.Denom, right.Denom);
            var n = left.Numer * (d / left.Denom) + right.Numer * (d / right.Denom);
            return new BigRational(n, d);
        }

        public static BigRational operator *(BigRational left, BigRational right)
        {
            var n = left.Numer * right.Numer;
            var d = left.Denom * right.Denom;
            return new BigRational(n, d);
        }

        public static BigRational operator -(BigRational left, BigRational right)
            => left + -right;

        public static BigRational operator /(BigRational left, BigRational right)
            => left * right.Invert();

        public static BigRational operator %(BigRational left, BigRational right)
            => left - (BigInteger)(left / right) * right;

        public static bool operator ==(BigRational left, BigRational right)
            => left.Equals(right);

        public static bool operator !=(BigRational left, BigRational right)
            => !left.Equals(right);

        public static bool operator <(BigRational left, BigRational right)
            => left.CompareTo(right) < 0;

        public static bool operator >(BigRational left, BigRational right)
            => left.CompareTo(right) > 0;

        public static bool operator <=(BigRational left, BigRational right)
            => left.CompareTo(right) <= 0;

        public static bool operator >=(BigRational left, BigRational right)
            => left.CompareTo(right) >= 0;

        public static implicit operator BigRational(int value)
            => new BigRational(value, 1);

        public static implicit operator BigRational(long value)
            => new BigRational(value, 1);

        public static implicit operator BigRational(BigInteger value)
            => new BigRational(value, 1);

        public static explicit operator BigInteger(BigRational value)
            => value.Numer / value.Denom;

        public static explicit operator double(BigRational value)
            => (double)value.Numer / (double)value.Denom;

        public int CompareTo(BigRational other)
            => (this - other).Numer.CompareTo(0);

        public bool Equals(BigRational other)
        {
            var r = this / other;
            return r.Numer == r.Denom;
        }

        public override bool Equals(object obj)
            => obj is BigRational other ? this.Equals(other) : false;

        public override int GetHashCode()
            => this.Numer.GetHashCode() ^ this.Denom.GetHashCode();

        public override string ToString()
            => $"{this.Numer}/{this.Denom}";
    }
}
