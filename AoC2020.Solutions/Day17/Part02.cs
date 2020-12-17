namespace AoC2020.Solutions.Day17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            IDictionary<(int x, int y, int z, int w), char> grid = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.ToCharArray())
                .SelectMany((entry, row) => entry.Select((item, col) => (Location: (x: col, y: row, z: 0, w: 0), Entry: item)))
                .Where(entry => entry.Entry == '#')
                .ToDictionary(item => item.Location, item => item.Entry);

            for (int cycle = 0; cycle < 6; cycle++)
            {
                // Start each cycle by finding the bounds of the current space. For the next cycle, we extend that
                // space out by 1 in each direction then determine the state of each location in that space.
                int xMin = grid.Keys.Min(el => el.x) - 1;
                int xMax = grid.Keys.Max(el => el.x) + 1;
                int yMin = grid.Keys.Min(el => el.y) - 1;
                int yMax = grid.Keys.Max(el => el.y) + 1;
                int zMin = grid.Keys.Min(el => el.z) - 1;
                int zMax = grid.Keys.Max(el => el.z) + 1;
                int wMin = grid.Keys.Min(el => el.w) - 1;
                int wMax = grid.Keys.Max(el => el.w) + 1;

                var newSpace = new Dictionary<(int x, int y, int z, int w), char>();

                // Now iterate through the space and determine the new state of each cube.
                for (int x = xMin; x <= xMax; x++)
                {
                    for (int y = yMin; y <= yMax; y++)
                    {
                        for (int z = zMin; z <= zMax; z++)
                        {
                            for (int w = wMin; w <= wMax; w++)
                            {
                                IEnumerable<(int, int, int, int)> neighbours = GetNeighbours((x, y, z, w));
                                int activeNeighbours = neighbours.Count(location => grid.TryGetValue(location, out char state) && state == '#');
                                if (grid.TryGetValue((x, y, z, w), out char currentLocationState) && currentLocationState == '#')
                                {
                                    // It's active...
                                    if (activeNeighbours == 2 || activeNeighbours == 3)
                                    {
                                        newSpace[(x, y, z, w)] = '#';
                                    }
                                }
                                else
                                {
                                    // It's inactive
                                    if (activeNeighbours == 3)
                                    {
                                        newSpace[(x, y, z, w)] = '#';
                                    }
                                }
                            }
                        }
                    }
                }

                grid = newSpace;
            }

            return grid.Count.ToString();
        }

        public static IEnumerable<(int, int, int, int)> GetNeighbours((int x, int y, int z, int w) location)
        {
            for (int x1 = location.x - 1; x1 <= location.x + 1; x1++)
            {
                for (int y1 = location.y - 1; y1 <= location.y + 1; y1++)
                {
                    for (int z1 = location.z - 1; z1 <= location.z + 1; z1++)
                    {
                        for (int w1 = location.w - 1; w1 <= location.w + 1; w1++)
                        {
                            if ((x1, y1, z1, w1) != location)
                            {
                                yield return (x1, y1, z1, w1);
                            }
                        }
                    }
                }
            }
        }
    }
}
