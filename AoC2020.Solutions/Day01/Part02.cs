namespace AoC2020.Solutions.Day01
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            var numbers = input
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x))
                .ToList();

            foreach (int current in numbers)
            {
                if (this.TryFindTwoNumbersBySum(numbers, 2020 - current, out (int First, int Second)? result))
                {
                    return (current * result.Value.First * result.Value.Second).ToString();
                }
            }

            return string.Empty;
        }

        private bool TryFindTwoNumbersBySum(List<int> numbers, int target, [NotNullWhen(true)] out (int First, int Second)? result)
        {
            result = numbers.Select(x => (First: x, Second: target - x)).FirstOrDefault(x => numbers.Any(n => n != x.First && n == x.Second));
            return result != default((int First, int Second));
        }
    }
}
