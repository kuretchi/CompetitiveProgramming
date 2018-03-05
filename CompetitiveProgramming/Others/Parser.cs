using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitiveProgramming.Others
{
    public delegate Parser.Result<T> Parser<T>(Parser.Input input);

    public static class Parser
    {
        public class Input
        {
            public Input(string text, int index)
            {
                this.Text = text;
                this.Index = index;
            }

            public string Text { get; }

            public int Index { get; }
        }

        public class Result<T>
        {
            public static Result<T> Success(T value, Input rest)
                => new Result<T> { Succeeded = true, Value = value, Rest = rest };

            public static Result<T> Failure(Input input)
                => new Result<T> { Rest = input };

            public bool Succeeded { get; private set; }

            public T Value { get; private set; }

            public Input Rest { get; private set; }
        }

        public static T Parse<T>(this Parser<T> parser, string s)
        {
            var result = parser(new Input(s, 0));
            if (result.Succeeded) return result.Value;
            else throw new InvalidOperationException("Parse failed.");
        }

        public static Parser<T2> Select<T1, T2>(this Parser<T1> parser, Func<T1, T2> selector) => input =>
        {
            var result = parser(input);
            return result.Succeeded
                ? Result<T2>.Success(selector(result.Value), result.Rest)
                : Result<T2>.Failure(result.Rest);
        };

        public static Parser<T3> SelectMany<T1, T2, T3>(this Parser<T1> parser, Func<T1, Parser<T2>> selector1, Func<T1, T2, T3> selector2) => input =>
        {
            var result = parser(input);
            if (!result.Succeeded) return Result<T3>.Failure(result.Rest);

            var parser2 = selector1(result.Value);
            var result2 = parser2(result.Rest);

            return result2.Succeeded
                ? Result<T3>.Success(selector2(result.Value, result2.Value), result2.Rest)
                : Result<T3>.Failure(result.Rest);
        };

        public static Parser<IEnumerable<T>> Many<T>(this Parser<T> parser) => input =>
        {
            var result = parser(input);
            var values = new List<T>();

            while (result.Succeeded)
            {
                values.Add(result.Value);
                result = parser(result.Rest);
            }

            return Result<IEnumerable<T>>.Success(values, result.Rest);
        };

        public static Parser<IEnumerable<T>> AtLeastOnce<T>(this Parser<T> parser) => input =>
        {
            var result = parser(input);
            if (!result.Succeeded) return Result<IEnumerable<T>>.Failure(input);

            var values = new List<T>();
            do
            {
                values.Add(result.Value);
                result = parser(result.Rest);
            }
            while (result.Succeeded);

            return Result<IEnumerable<T>>.Success(values, result.Rest);
        };

        public static Parser<T> Option<T>(this Parser<T> parser, T @default) => input =>
        {
            var result = parser(input);
            var values = result.Succeeded ? result.Value : @default;
            return Result<T>.Success(values, result.Rest);
        };

        public static Parser<IEnumerable<T>> Repeat<T>(this Parser<T> parser, int count) => input =>
        {
            var result = parser(input);
            if (!result.Succeeded) return Result<IEnumerable<T>>.Failure(input);
            var values = new List<T> { result.Value };

            for (var i = 1; i < count && result.Succeeded; i++)
            {
                result = parser(result.Rest);
                values.Add(result.Value);
            }

            return result.Succeeded
                ? Result<IEnumerable<T>>.Success(values, result.Rest)
                : Result<IEnumerable<T>>.Failure(input);
        };

        public static Parser<T2> Then<T1, T2>(this Parser<T1> first, Parser<T2> second)
            => from f in first
               from s in second
               select s;

        public static Parser<T> Or<T>(this Parser<T> first, Parser<T> second) => input =>
        {
            var result = first(input);
            return result.Succeeded ? result : second(input);
        };

        public static Parser<IEnumerable<T>> Concat<T>(
            this Parser<IEnumerable<T>> first, Parser<IEnumerable<T>> second)
            => from f in first
               from s in second
               select f.Concat(s);

        public static Parser<T3> Concat<T1, T2, T3>(
            this Parser<T1> first, Parser<T2> second, Func<T1, T2, T3> selector)
            => from f in first
               from s in second
               select selector(f, s);

        public static Parser<IEnumerable<T>> Concat<T>(this IEnumerable<Parser<T>> parsers)
            => parsers.Aggregate(Return(Enumerable.Empty<T>()), (x, y) => x.Concat(y.Repeat(1)));

        public static Parser<string> AsString(this Parser<IEnumerable<char>> parser)
            => parser.Select(cs => new string(cs.ToArray()));

        public static Parser<T> Return<T>(T value) => input => Result<T>.Success(value, input);

        public static Parser<char> Char(Func<char, bool> predicate) => input =>
        {
            if (input.Index >= input.Text.Length) return Result<char>.Failure(input);
            var c = input.Text[input.Index];
            return predicate(c)
                ? Result<char>.Success(c, new Input(input.Text, input.Index + 1))
                : Result<char>.Failure(input);
        };

        public static Parser<char> Char(char c) => Char(c_ => c_ == c);

        public static Parser<string> String(string s)
            => s.Select(Char).Concat().Select(x => new string(x.ToArray()));

        public static Parser<T> Integer<T>(Func<string, T> converter)
            => from minus in String("-").Option("")
               from digits in Char(char.IsDigit).Many().AsString()
               select converter(minus + digits);
    }
}
