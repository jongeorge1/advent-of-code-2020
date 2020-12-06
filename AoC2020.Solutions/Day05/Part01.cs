namespace AoC2020.Solutions.Day05
{
    using System;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            return input
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Max(CalculateSeatId)
                .ToString();
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
