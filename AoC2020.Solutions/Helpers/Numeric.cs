namespace AoC2020.Solutions.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Numeric
    {
        public static long LeastCommonMultiple(IEnumerable<long> numbers)
        {
            return numbers.Aggregate(LeastCommonMultiple);
        }

        public static long LeastCommonMultiple(long a, long b)
        {
            return Math.Abs(a * b) / GreatestCommonDivisor(a, b);
        }

        public static long GreatestCommonDivisor(long a, long b)
        {
            return b == 0 ? a : GreatestCommonDivisor(b, a % b);
        }
    }
}
