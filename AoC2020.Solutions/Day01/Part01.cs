namespace AoC2020.Solutions.Day01
{
    using System;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            var numbers = input
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x))
                .ToList();

            foreach (int number in numbers)
            {
                int target = 2020 - number;
                if (numbers.Contains(target))
                {
                    return (number * target).ToString();
                }
            }

            return string.Empty;
        }
    }
}
