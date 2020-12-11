#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
namespace AoC2020.Solutions.Day11
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    public class Part01 : ISolution
    {
        private int totalRows = 0;

        private int totalColumns = 0;

        public string Solve(string input)
        {
            // Get rid of the newlines
            char[][] grid = input
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.ToCharArray())
                .ToArray();

            this.totalRows = grid.Length;
            this.totalColumns = grid[0].Length;

            while (true)
            {
                if (!this.TryGetNextGeneration(grid, out char[][]? next))
                {
                    return grid.Sum(x => x.Count(y => y == '#')).ToString();
                }

                grid = next;
            }
        }

        private bool TryGetNextGeneration(char[][] current, [NotNullWhen(true)] out char[][]? next)
        {
            next = new char[this.totalRows][];
            bool changed = false;

            for (int row = 0; row < this.totalRows; row++)
            {
                next[row] = new char[this.totalColumns];

                for (int column = 0; column < this.totalColumns; column++)
                {
                    int surroundingSeatOccupancy = this.GetSurroundingSeatOccupancy(current, row, column);
                    if (current[row][column] == 'L' && surroundingSeatOccupancy == 0)
                    {
                        changed = true;
                        next[row][column] = '#';
                    }
                    else if (current[row][column] == '#' && surroundingSeatOccupancy > 3)
                    {
                        changed = true;
                        next[row][column] = 'L';
                    }
                    else
                    {
                        next[row][column] = current[row][column];
                    }
                }
            }

            return changed;
        }

        private int GetSurroundingSeatOccupancy(char[][] data, int row, int column)
        {
            int result = 0;

            bool firstCol = column == 0;
            bool lastCol = column == this.totalColumns - 1;
            bool firstRow = row == 0;
            bool lastRow = row == this.totalRows - 1;

            if (!firstRow && !firstCol && data[row - 1][column - 1] == '#')
            {
                result++;
            }

            if (!firstRow && data[row - 1][column] == '#')
            {
                result++;
            }

            if (!firstRow && !lastCol && data[row - 1][column + 1] == '#')
            {
                result++;
            }

            if (!firstCol && data[row][column - 1] == '#')
            {
                result++;
            }

            if (!lastCol && data[row][column + 1] == '#')
            {
                result++;
            }

            if (!lastRow && !firstCol && data[row + 1][column - 1] == '#')
            {
                result++;
            }

            if (!lastRow && data[row + 1][column] == '#')
            {
                result++;
            }

            if (!lastRow && !lastCol && data[row + 1][column + 1] == '#')
            {
                result++;
            }

            return result;
        }
    }
}
