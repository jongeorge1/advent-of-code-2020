namespace AoC2020.Solutions.Day12
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using AoC2020.Solutions.Helpers;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            // Get rid of the newlines
            string[] data = input
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            (int x, int y) waypointOffset = (10, 1);
            (int x, int y) position = (0, 0);

            foreach (string current in data)
            {
                int quantifier = int.Parse(current[1..]);

                switch (current[0])
                {
                    case 'N':
                        waypointOffset = (waypointOffset.x, waypointOffset.y + quantifier);
                        break;

                    case 'E':
                        waypointOffset = (waypointOffset.x + quantifier, waypointOffset.y);
                        break;

                    case 'S':
                        waypointOffset = (waypointOffset.x, waypointOffset.y - quantifier);
                        break;

                    case 'W':
                        waypointOffset = (waypointOffset.x - quantifier, waypointOffset.y);
                        break;

                    case 'F':
                        position = Move(position, waypointOffset, quantifier);
                        break;

                    case 'L':
                        waypointOffset = Transform(waypointOffset, 360 - quantifier);
                        break;

                    case 'R':
                        waypointOffset = Transform(waypointOffset, quantifier);
                        break;
                }
            }

            return Distance.Manhattan(position.x, position.y).ToString();
        }

        private static (int, int) Move((int, int) position, (int, int) waypointOffset, int quantifier)
        {
            for (int i = 0; i < quantifier; i++)
            {
                position = (position.Item1 + waypointOffset.Item1, position.Item2 + waypointOffset.Item2);
            }

            return position;
        }

        private static (int, int) Transform((int, int) offset, int quantifier)
        {
            return quantifier switch
            {
                0 => offset,
                90 => (offset.Item2, -offset.Item1),
                180 => (-offset.Item1, -offset.Item2),
                270 => (-offset.Item2, offset.Item1),
                _ => throw new Exception(),
            };
        }
    }
}
