using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompetitiveProgramming.Extensions;

namespace CompetitiveProgramming.RangeQuery
{
    public class Imos2D<T>
    {
        private readonly T[][] _array;

        public Imos2D(int height, int width, Monoid<T> monoid)
        {
            _array = new T[height][];
            for (var i = 0; i < height; i++) _array[i] = Enumerable.Repeat(monoid.Unit, width).ToArray();
            this.Monoid = monoid;
        }

        public Imos2D(int height, int width, Group<T> group) : this(height, width, (Monoid<T>)group)
        {
            this.Group = group;
        }

        public Monoid<T> Monoid { get; }

        public Group<T> Group { get; }

        public int Degree { get; private set; } = -1;

        public int Height => _array.Length;

        public int Width => _array[0].Length;

        public T this[int h, int w]
            => this.Degree > 0 ? _array[h][w] : throw new InvalidOperationException();

        public void Append(int h1, int w1, int h2, int w2, T value)
        {
            _array[h1][w1] = this.Group.Append(_array[h1][w1], value);
            var inv = this.Group.Invert(value);
            if (++w2 < this.Width)
                _array[h1][w2] = this.Group.Append(_array[h1][w2], inv);
            if (++h2 < this.Height)
                _array[h2][w1] = this.Group.Append(_array[h2][w1], inv);
            if (w2 < this.Width && h2 < this.Height)
                _array[h2][w2] = this.Group.Append(_array[h2][w2], value);
        }

        public void Append(int h1, int w1, T value)
        {
            _array[h1][w1] = this.Monoid.Append(_array[h1][w1], value);
        }

        public void Integrate()
        {
            for (var h = 0; h < this.Height; h++)
                for (var w = 1; w < this.Width; w++)
                    _array[h][w] = this.Monoid.Append(_array[h][w - 1], _array[h][w]);
            for (var w = 0; w < this.Width; w++)
                for (var h = 1; h < this.Height; h++)
                    _array[h][w] = this.Monoid.Append(_array[h - 1][w], _array[h][w]);
            this.Degree++;
        }

        public void Differentiate()
        {
            if (this.Degree < 0) throw new InvalidOperationException();
            for (var w = this.Width - 1; w >= 0; w--)
                for (var h = this.Height - 1; h > 0; h--)
                    _array[h][w] = this.Group.Append(_array[h][w], this.Group.Invert(_array[h - 1][w]));
            for (var h = this.Height - 1; h >= 0; h--)
                for (var w = this.Width - 1; w > 0; w--)
                    _array[h][w] = this.Group.Append(_array[h][w], this.Group.Invert(_array[h][w - 1]));
        }
    }
}
