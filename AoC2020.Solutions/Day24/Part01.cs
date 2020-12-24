namespace AoC2020.Solutions.Day24
{
    using System;
    using System.Collections.Generic;

    public class Part01 : ISolution
    {
        private static readonly Dictionary<string, Func<(int, int, int), (int, int, int)>> Mutators = new Dictionary<string, Func<(int, int, int), (int, int, int)>>
        {
            { "ne", ((int x, int y, int z) current) => (current.x + 1, current.y, current.z - 1) },
            { "nw", ((int x, int y, int z) current) => (current.x, current.y + 1, current.z - 1) },
            { "se", ((int x, int y, int z) current) => (current.x, current.y - 1, current.z + 1) },
            { "sw", ((int x, int y, int z) current) => (current.x - 1, current.y, current.z + 1) },
            { "e", ((int x, int y, int z) current) => (current.x + 1, current.y - 1, current.z) },
            { "w", ((int x, int y, int z) current) => (current.x - 1, current.y + 1, current.z) },
        };

        public string Solve(string input)
        {
            string[] directions = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // Represent the hex grid using cube coordinate system; see https://www.redblobgames.com/grids/hexagons/
            // for more info.
            var flippedTiles = new Dictionary<(int, int, int), bool>();

            foreach (string direction in directions)
            {
                var location = GetLocation(direction);
                if (flippedTiles.ContainsKey(location))
                {
                    flippedTiles.Remove(location);
                }
                else
                {
                    flippedTiles.Add(location, true);
                }
            }

            return flippedTiles.Count.ToString();
        }

        private static (int, int, int) GetLocation(string directions)
        {
            ReadOnlySpan<char> remainingDirections = directions.AsSpan();

            (int, int, int) location = (0, 0, 0);

            while (remainingDirections.Length > 0)
            {
                foreach (KeyValuePair<string, Func<(int, int, int), (int, int, int)>> mutator in Mutators)
                {
                    if (remainingDirections.StartsWith(mutator.Key))
                    {
                        location = mutator.Value(location);
                        remainingDirections = remainingDirections[mutator.Key.Length..];
                    }
                }
            }

            return location;
        }
    }
}
