using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CompetitiveProgramming.Others
{
    public class StreamScanner
    {
        private readonly Stream _stream;
        private const int _bufferSize = 1024;
        private readonly byte[] _buf = new byte[_bufferSize];
        private int _len, _ptr;

        public StreamScanner(Stream stream)
        {
            _stream = stream;
        }

        public byte ReadByte()
        {
            if (_ptr >= _len) _len = _stream.Read(_buf, _ptr = 0, _bufferSize);
            return _len > 0 ? _buf[_ptr++] : (byte)'\n';
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
            var c = ReadChar();
            while (!char.IsWhiteSpace(c)) { r.Append(c); c = ReadChar(); }
            return r.ToString();
        }

        public int NextInt32()
        {
            var r = 0;
            var c = ReadChar();
            var n = c == '-';
            if (n) c = ReadChar();
            while (!char.IsWhiteSpace(c)) { r = r * 10 + c - '0'; c = ReadChar(); }
            return n ? -r : r;
        }

        public long NextInt64()
        {
            var r = 0L;
            var c = ReadChar();
            var n = c == '-';
            if (n) c = ReadChar();
            while (!char.IsWhiteSpace(c)) { r = r * 10 + c - '0'; c = ReadChar(); }
            return n ? -r : r;
        }

        public BigInteger NextBigInteger()
        {
            var r = BigInteger.Zero;
            var c = ReadChar();
            var n = c == '-';
            if (n) c = ReadChar();
            while (!char.IsWhiteSpace(c)) { r = r * 10 + c - '0'; c = ReadChar(); }
            return n ? -r : r;
        }

        public double NextDouble() => double.Parse(NextString());

        public decimal NextDecimal() => decimal.Parse(NextString());
    }
}
