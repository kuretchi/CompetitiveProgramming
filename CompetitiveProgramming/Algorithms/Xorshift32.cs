using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitiveProgramming.Algorithms
{
    public class Xorshift32
    {
        private uint _y;

        public Xorshift32() : this(unchecked((uint)DateTime.Now.Ticks)) { }

        public Xorshift32(int seed) : this(unchecked((uint)seed)) { }

        public Xorshift32(uint seed)
        {
            _y = seed > 0 ? seed : 2463534242;
        }

        public int Next() => (int)(Sample() >> 1);

        public int Next(int maxValue) => this.Next() % maxValue;

        public int Next(int minValue, int maxValue) => this.Next() % (maxValue - minValue) + minValue;

        public double NextDouble() => this.Sample() / (double)uint.MaxValue;

        private uint Sample()
        {
            _y = _y ^ (_y << 13);
            _y = _y ^ (_y >> 17);
            return _y = _y ^ (_y << 5);
        }
    }
}
