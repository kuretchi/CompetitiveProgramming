using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using CompetitiveProgramming.Math;

namespace CompetitiveProgramming.Others
{
    public class Scanner
    {
        private readonly Stream _stream;
        private const int _bufferSize = 1024;
        private readonly byte[] _buf = new byte[_bufferSize];
        private int _len, _ptr;

        public Scanner(Stream stream)
        {
            _stream = stream;
        }

        public byte ReadByte()
        {
            if (_ptr >= _len)
            {
                _len = _stream.Read(_buf, 0, _bufferSize);
                _ptr = 0;
            }
            return _buf[_ptr++];
        }

        public char ReadChar() => (char)ReadByte();

        public string ReadLine()
        {
            var r = new StringBuilder();
            if (_ptr == 0) r.Append(ReadChar());
            for (; _ptr < _len; _ptr++) r.Append((char)_buf[_ptr]);
            return r.ToString();
        }

        public char NextChar() => char.Parse(NextString());

        public string NextString()
        {
            var r = new StringBuilder();
            var b = ReadChar();
            while (b != ' ' && b != '\n')
            {
                r.Append(b);
                b = ReadChar();
            }
            return r.ToString();
        }

        public int NextInt() => (int)NextLong();

        public long NextLong()
        {
            var r = 0L;
            var b = ReadByte();
            var n = b == '-';
            if (n) b = ReadByte();
            while (b != ' ' && b != '\n')
            {
                r = r * 10 + b - '0';
                b = ReadByte();
            }
            return n ? -r : r;
        }

        public BigInteger NextBigInteger()
        {
            var r = new BigInteger();
            var b = ReadByte();
            var n = b == '-';
            if (n) b = ReadByte();
            while (b != ' ' && b != '\n')
            {
                r = r * 10 + b - '0';
                b = ReadByte();
            }
            return n ? -r : r;
        }

        public double NextDouble()
        {
            var i = 0L;
            var b = ReadByte();
            var n = b == '-';
            if (n) b = ReadByte();
            while (b != '.' && b != ' ' && b != '\n')
            {
                i = i * 10 + b - '0';
                b = ReadByte();
            }
            if (b != '.') return n ? -i : i;
            b = ReadByte();
            var f = 0L;
            var p = 0;
            while (b != ' ' && b != '\n')
            {
                f = f * 10 + b - '0';
                b = ReadByte();
                p++;
            }
            var r = i + (double)f / MathExtensions.Pow(10, p);
            return n ? -r : r;
        }

        public decimal NextDecimal() => decimal.Parse(NextString());

        public T Next<T>(Converter<string, T> parser) => parser(NextString());
    }
}
