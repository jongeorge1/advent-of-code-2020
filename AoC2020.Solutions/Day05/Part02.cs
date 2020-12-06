namespace AoC2020.Solutions.Day05
{
    using System;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            var ids = input
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(CalculateSeatId)
                .ToList();

            ids.Sort();

            for (int i = 0; i < ids.Count - 1; i++)
            {
                if (ids[i + 1] - ids[i] == 2)
                {
                    return (ids[i] + 1).ToString();
                }
            }

            return string.Empty;
        }

        private static int CalculateSeatId(string input)
        {
            // The seat Id can be identified by treating input as a binary number
            // with the 1s being B/R and the 0s being F/L.
            int id = 0;
            for (int pos = 0; pos < 7; pos++)
            {
                if (input[pos] == 'B')
                {
                    id += (int)Math.Pow(2, 9 - pos);
                }
            }

            for (int pos = 7; pos < 10; pos++)
            {
                if (input[pos] == 'R')
                {
                    id += (int)Math.Pow(2, 9 - pos);
                }
            }

            return id;
        }
    }
}
