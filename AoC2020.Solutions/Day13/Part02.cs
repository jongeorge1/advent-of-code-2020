namespace AoC2020.Solutions.Day13
{
    using System;
    using System.Linq;
    using AoC2020.Solutions.Helpers;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            // Get rid of the newlines
            string[] data = input
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            (long serviceNumber, long offset)[] servicesWithOffsets = data[1]
                .Split(',')
                .Select((x, i) => (x, (long)i))
                .Where(x => x.x != "x")
                .Select(x => (long.Parse(x.x), x.Item2))
                .OrderBy(x => x.Item1)
                .ToArray();

            // We now have a list of service numbers and the offset from the time 't' the first bus departs that we're
            // looking for. Essentially these numbers are the remainders we'd expect if we divide the numbers by 't'.
            // So, the example given is:
            // 7,13,x,x,59,x,31,19
            // In this example we're looking for a time t such that:
            // t % 7 == 0
            // t % 13 == 1
            // t % 59 == 4
            // t % 31 == 6
            // t % 19 == 7
            // It turns out there's a thing called the Chinese Remainder Theorem which works for this case. It's
            // nicely described here: https://brilliant.org/wiki/chinese-remainder-theorem/
            // Essentially it says that given a set of pairwise coprime positive integers n1, n2,...,nk, and a further
            // set of arbitrary integers a1,a2,...,ak, then the system of "simultaneous congruences"...
            // x === a1 (mod n1)
            // x === a2 (mod n2)
            // ...
            // x === a3 (mod n3)
            // can be solved for N.
            // In our case the pairwise coprime positive integers n1...nk are our service numbers (by some stroke of
            // luck it turns out that all of our service numbers are primes... almost as if it were by intention) and
            // arbitrary integers a1...ak are the time offsets.
            long[] divisors = servicesWithOffsets.Select(x => x.serviceNumber).ToArray();
            long[] remainders = servicesWithOffsets.Select(x => x.offset).ToArray();

            return ChineseRemainderTheorem.Solve(divisors, remainders).ToString();
        }
    }
}
