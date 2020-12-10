namespace AoC2020.Solutions.Day09
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
                .Select(long.Parse)
                .ToList();

            return this.FindWeakness(data).ToString();
        }

        private long FindWeakness(List<long> data)
        {
            int preambleSize = data.Count < 100 ? 5 : 25;

            for (int currentNumberIndex = preambleSize; currentNumberIndex < data.Count; currentNumberIndex++)
            {
                if (!this.IsValidAt(currentNumberIndex, data, preambleSize))
                {
                    return data[currentNumberIndex];
                }
            }

            return 0;
        }

        private bool IsValidAt(int pointer, List<long> data, int preambleSize)
        {
            Range checkRange = (pointer - preambleSize)..pointer;
            for (int number1Index = checkRange.Start.Value; number1Index < checkRange.End.Value; number1Index++)
            {
                for (int number2Index = checkRange.Start.Value; number2Index < checkRange.End.Value; number2Index++)
                {
                    if (number1Index == number2Index)
                    {
                        continue;
                    }

                    if (data[number1Index] + data[number2Index] == data[pointer])
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
