namespace AoC2020.Solutions.Day04
{
    using System;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            return input.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new Passport(x))
                .Count(x => x.IsValid())
                .ToString();
        }
    }
}
