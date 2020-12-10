namespace AoC2020.Solutions.Day10
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            var data = input
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            data.Add(0);
            data.Add(data.Max() + 3);

            data.Sort();

            IEnumerable<IGrouping<int, int>>? differences = data.Skip(1).Select((x, i) => x - data[i]).GroupBy(x => x);

            int jumpsOfOne = differences.First(x => x.Key == 1).Count();
            int jumpsOfThree = differences.First(x => x.Key == 3).Count();

            return (jumpsOfOne * jumpsOfThree).ToString();
        }
    }
}
