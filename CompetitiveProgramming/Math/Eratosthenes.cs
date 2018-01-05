using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace CompetitiveProgramming.Math
{
    public static class Eratosthenes
    {
        public static IEnumerable<int> Sieve(int maxValue)
        {
            if (maxValue < 2) yield break;
            yield return 2;

            var flgs = new bool[maxValue + 1];
            var i = 3;
            for (i = 3; i * i < maxValue; i += 2)
                if (!flgs[i])
                {
                    yield return i;
                    for (var j = i * (i | 1); j <= maxValue; j += i << 1)
                        flgs[j] = true;
                }

            for (; i <= maxValue; i += 2)
                if (!flgs[i])
                    yield return i;
        }
    }
}
