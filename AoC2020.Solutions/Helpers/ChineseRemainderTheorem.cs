namespace AoC2020.Solutions.Helpers
{
    using System.Linq;

    public static class ChineseRemainderTheorem
    {
        public static long Solve(long[] divisors, long[] remainders)
        {
            long prod = divisors.Aggregate(1L, (i, j) => i * j);
            long result = 0;
            for (long i = 0; i < divisors.Length; i++)
            {
                long p = prod / divisors[i];
                result += remainders[i] * ModularMultiplicativeInverse(p, divisors[i]) * p;
            }

            return result % prod;
        }

        private static long ModularMultiplicativeInverse(long a, long mod)
        {
            long m0 = mod;
            long t;
            long q;
            long x0 = 0;
            long x1 = 1;

            if (mod == 1)
            {
                return 0;
            }

            // Apply extended Euclid Algorithm
            while (a > 1)
            {
                // q is quotient
                q = a / mod;

                t = mod;

                // m is remainder now, process same as euclid's algo.
                mod = a % mod;
                a = t;

                t = x0;

                x0 = x1 - (q * x0);

                x1 = t;
            }

            // Make x1 positive
            if (x1 < 0)
            {
                x1 += m0;
            }

            return x1;
        }
    }
}
