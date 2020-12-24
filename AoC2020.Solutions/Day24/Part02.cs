namespace AoC2020.Solutions.Day24
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
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
            var flippedTiles = Setup(input);

            for (int generation = 0; generation < 100; generation++)
            {
                var tilesToCheck = flippedTiles.Keys.SelectMany(t => GetNeighbours(t)).Distinct();
                var newGrid = new Dictionary<(int, int, int), bool>();

                foreach (var current in tilesToCheck)
                {
                    IEnumerable<(int, int, int)> neighbours = GetNeighbours(current);
                    int adjacentBlackTiles = neighbours.Count(n => flippedTiles.ContainsKey(n));
                    bool currentIsBlack = flippedTiles.ContainsKey(current);

                    if (currentIsBlack && (adjacentBlackTiles == 0 || adjacentBlackTiles > 2))
                    {
                        // Current location is black and needs to go white; we don't add it to the new grid
                        // (so this is a noop)
                    }
                    else if (!currentIsBlack && adjacentBlackTiles == 2)
                    {
                        // Current is white and needs to go black.
                        newGrid.Add(current, true);
                    }
                    else if (currentIsBlack)
                    {
                        // No change...
                        newGrid.Add(current, true);
                    }
                }

                flippedTiles = newGrid;
            }

            return flippedTiles.Count.ToString();
        }

        private static IEnumerable<(int, int, int)> GetNeighbours((int x, int y, int z) location)
        {
            return Mutators.Select(m => m.Value(location));
        }

        private static Dictionary<(int x, int y, int z), bool> Setup(string input)
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

            return flippedTiles;
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
