namespace AoC2020.Solutions.Day06
{
    using System;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            return input
                .Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Sum(CountAnswers)
                .ToString();
        }

        private static int CountAnswers(string input)
        {
            return input
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.ToCharArray())
                .Aggregate((total, next) => total.Intersect(next).ToArray())
                .Length;
        }
    }
}
