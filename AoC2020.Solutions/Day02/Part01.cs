namespace AoC2020.Solutions.Day02
{
    using System;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            return input
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(new[] { ' ', '-', ':' }, StringSplitOptions.RemoveEmptyEntries))
                .Select(x => (x[0], x[1], x[3].Count(c => c == x[2][0])))
                .Count(x => x.Item3 <= int.Parse(x.Item2) && x.Item3 >= int.Parse(x.Item1))
                .ToString();
        }
    }
}
