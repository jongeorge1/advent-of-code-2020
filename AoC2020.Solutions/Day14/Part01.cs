namespace AoC2020.Solutions.Day14
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            // Get rid of the newlines
            string[][] data = input
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(new char[] { ' ', '=', '[', ']' }, StringSplitOptions.RemoveEmptyEntries))
                .ToArray();

            long currentAndMask = long.MaxValue;
            long currentOrMask = 0L;

            var memory = new Dictionary<long, long>();

            foreach (string[] current in data)
            {
                if (current[0] == "mask")
                {
                    currentOrMask = Convert.ToInt64(current[1].Replace('X', '0'), 2);
                    currentAndMask = Convert.ToInt64(current[1].Replace('X', '1'), 2);
                }
                else
                {
                    long location = long.Parse(current[1]);
                    long value = long.Parse(current[2]);

                    memory[location] = (value & currentAndMask) | currentOrMask;
                }
            }

            return memory.Values.Sum().ToString();
        }
    }
}
