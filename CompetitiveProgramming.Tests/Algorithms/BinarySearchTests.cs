using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CompetitiveProgramming.Algorithms;

namespace CompetitiveProgramming.Tests.Algorithms
{
    public class BinarySearchTests
    {
        public static IEnumerable<object[]> CreateTestCases(int testCaseCount, int count, int minValue, int maxValue, int value)
        {
            var rand = new Random(Seed: 0);

            for (var i = 0; i < testCaseCount; i++)
            {
                var array = new int[count];

                for (var j = 0; j < count; j++)
                    array[j] = rand.Next(minValue, maxValue);

                Array.Sort(array);

                yield return new object[] { array, value };
            }
        }

        [Theory(DisplayName = nameof(BinarySearchTest))]
        [MemberData(nameof(CreateTestCases), 10, 10, -10000, 10000, 0)]
        [MemberData(nameof(CreateTestCases), 10, 10000, -10, 10, 0)]
        [MemberData(nameof(CreateTestCases), 10, 100, -100, 100, 200)]
        [MemberData(nameof(CreateTestCases), 10, 100, -100, 100, -200)]
        [MemberData(nameof(CreateTestCases), 1, 1, 0, 1, 0)]
        [MemberData(nameof(CreateTestCases), 1, 1, 0, 1, -100)]
        [MemberData(nameof(CreateTestCases), 1, 1, 0, 1, 100)]
        public void BinarySearchTest(IReadOnlyList<int> source, int value)
        {
            var lowerBoundIndex = source.Count;
            var upperBoundIndex = source.Count;

            for (var i = 0; i < source.Count; i++)
            {
                if (source[i] >= value)
                {
                    lowerBoundIndex = i;
                    break;
                }
            }

            for (var i = 0; i < source.Count; i++)
            {
                if (source[i] > value)
                {
                    upperBoundIndex = i;
                    break;
                }
            }

            source.LowerBound(value).Is(lowerBoundIndex);
            BinarySearch.LowerBound(i => source[i], 0, source.Count, value).Is(lowerBoundIndex);

            source.UpperBound(value).Is(upperBoundIndex);
            BinarySearch.UpperBound(i => source[i], 0, source.Count, value).Is(upperBoundIndex);
        }
    }
}
