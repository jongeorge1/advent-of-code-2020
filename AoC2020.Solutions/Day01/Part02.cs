namespace AoC2020.Solutions.Day01
{
    using System;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            var numbers = input
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x))
                .ToList();

            foreach (int first in numbers)
            {
                foreach (int second in numbers)
                {
                    int target = 2020 - first - second;
                    if (numbers.Contains(target))
                    {
                        return (first * second * target).ToString();
                    }
                }
            }

            return string.Empty;
        }
    }
}
