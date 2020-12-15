#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
namespace AoC2020.Solutions.Day14
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            // Get rid of the newlines
            string[][] data = input
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(new char[] { ' ', '=', '[', ']' }, StringSplitOptions.RemoveEmptyEntries))
                .ToArray();

            var memory = new Dictionary<long, long>();
            string currentMask = string.Empty;

            foreach (string[] current in data)
            {
                if (current[0] == "mask")
                {
                    currentMask = current[1];
                }
                else
                {
                    long[] locations = this.BuildLocations(currentMask, current[1]);
                    long value = long.Parse(current[2]);

                    foreach (long location in locations)
                    {
                        memory[location] = value;
                    }
                }
            }

            return memory.Values.Sum().ToString();
        }

        private long[] BuildLocations(string mask, string locationValue)
        {
            // Transform the location value to binary
            string binaryLocationValue = Convert.ToString(long.Parse(locationValue), 2).PadLeft(mask.Length, '0');

            // First build the location including floating bits.
            if (mask.Length != binaryLocationValue.Length)
            {
                throw new Exception();
            }

            char[] maskedLocation = mask.Select(
                (x, i) =>
                x switch
                {
                    '0' => binaryLocationValue[i],
                    '1' => '1',
                    _ => 'X',
                }).ToArray();

            return this.GenerateMaskPermutations(maskedLocation);
        }

        private long[] GenerateMaskPermutations(char[] input)
        {
            int firstX = Array.IndexOf(input, 'X');

            if (firstX == -1)
            {
                // No more 'X' - return the input parsed as a long.
                return new[] { Convert.ToInt64(string.Join(string.Empty, input), 2) };
            }

            // There's at least one X.
            var results = new List<long>();

            char[]? option1 = (char[])input.Clone();
            option1[firstX] = '0';

            results.AddRange(this.GenerateMaskPermutations(option1));

            char[]? option2 = (char[])input.Clone();
            option2[firstX] = '1';
            results.AddRange(this.GenerateMaskPermutations(option2));

            return results.ToArray();
        }
    }
}
