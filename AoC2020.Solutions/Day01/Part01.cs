namespace AoC2020.Solutions.Day01
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            var numbers = input
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x))
                .ToList();

            (int first, int second) = this.FindTwoNumbersBySum(numbers, 2020);

            return (first * second).ToString();
        }

        private (int First, int Second) FindTwoNumbersBySum(List<int> numbers, int target)
        {
            return numbers.Select(x => (First: x, Second: target - x)).FirstOrDefault(x => numbers.Any(n => n != x.First && n == x.Second));
        }
    }
}
