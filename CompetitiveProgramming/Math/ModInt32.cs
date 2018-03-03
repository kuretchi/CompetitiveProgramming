using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitiveProgramming.Math
{
    public struct ModInt32 : IEquatable<ModInt32>
    {
        private long _value;

        public const int Mod = (int)1e9 + 7;

        public static readonly ModInt32 Zero = new ModInt32(0);

        public static readonly ModInt32 One = new ModInt32(1);

        public ModInt32(long value) { _value = value % Mod; }

        private ModInt32(int value) { _value = value; }

        public int Value => (int)_value;

        public ModInt32 Invert() => ModPow(this, Mod - 2); // Mod must be prime

        public static ModInt32 operator -(ModInt32 value)
        {
            value._value = Mod - value._value;
            return value;
        }

        public static ModInt32 operator +(ModInt32 left, ModInt32 right)
        {
            left._value += right._value;
            if (left._value >= Mod) left._value -= Mod;
            return left;
        }

        public static ModInt32 operator -(ModInt32 left, ModInt32 right)
        {
            left._value -= right._value;
            if (left._value < 0) left._value += Mod;
            return left;
        }

        public static ModInt32 operator *(ModInt32 left, ModInt32 right)
        {
            left._value = left._value * right._value % Mod;
            return left;
        }

        public static ModInt32 operator /(ModInt32 left, ModInt32 right) => left * right.Invert();

        public static ModInt32 operator ++(ModInt32 value)
        {
            if (value._value == Mod - 1) value._value = 0;
            else value._value++;
            return value;
        }

        public static ModInt32 operator --(ModInt32 value)
        {
            if (value._value == 0) value._value = Mod - 1;
            else value._value--;
            return value;
        }

        public static bool operator ==(ModInt32 left, ModInt32 right) => left.Equals(right);

        public static bool operator !=(ModInt32 left, ModInt32 right) => !left.Equals(right);

        public static implicit operator ModInt32(int value) => new ModInt32(value);

        public static implicit operator ModInt32(long value) => new ModInt32(value);

        public static ModInt32 ModPow(ModInt32 value, long exponent)
        {
            var r = new ModInt32(1);
            for (; exponent > 0; value *= value, exponent >>= 1)
                if ((exponent & 1) == 1) r *= value;
            return r;
        }

        public static ModInt32 ModFact(int value)
        {
            var r = new ModInt32(1);
            for (var i = 2; i <= value; i++) r *= value;
            return r;
        }

        public bool Equals(ModInt32 other) => _value == other._value;

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return this.Equals((ModInt32)obj);
        }

        public override int GetHashCode() => _value.GetHashCode();

        public override string ToString() => _value.ToString();
    }
}
