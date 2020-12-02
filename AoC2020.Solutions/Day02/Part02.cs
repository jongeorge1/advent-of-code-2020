namespace AoC2020.Solutions.Day02
{
    using System;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            return input
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(new[] { ' ', '-', ':' }, StringSplitOptions.RemoveEmptyEntries))
                .Select(x => (int.Parse(x[0]), int.Parse(x[1]), x[2][0], x[3].ToCharArray()))
                .Count(x => x.Item4[x.Item1 - 1] == x.Item3 ^ x.Item4[x.Item2 - 1] == x.Item3)
                .ToString();
        }
    }
}
