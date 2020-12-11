namespace AoC2020.Solutions.Day11
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        private int totalRows = 0;

        private int totalColumns = 0;

        public string Solve(string input)
        {
            this.totalColumns = input.IndexOf(Environment.NewLine);

            // Get rid of the newlines
            char[] grid = input.Replace(Environment.NewLine, string.Empty).ToCharArray();

            this.totalRows = grid.Length / this.totalColumns;

            // Check....
            if (grid.Length % this.totalColumns != 0)
            {
                throw new Exception("Something's wrong with either the code or the input. It's probably the code.");
            }

            string last = string.Concat(grid);

            while (true)
            {
                char[] next = this.NextGeneration(grid);
                string nextString = string.Concat(next);

                if (nextString == last)
                {
                    return next.Count(x => x == '#').ToString();
                }

                last = nextString;
                grid = next;
            }
        }

        private char[] NextGeneration(char[] current)
        {
            char[] result = new char[current.Length];

            for (int index = 0; index < current.Length; index++)
            {
                List<int> surroundingSeats = this.GetSurroundingSeatReferences(index);

                if (current[index] == 'L' && !surroundingSeats.Any(i => current[i] == '#'))
                {
                    result[index] = '#';
                }
                else if (current[index] == '#' && surroundingSeats.Count(i => current[i] == '#') >= 4)
                {
                    result[index] = 'L';
                }
                else
                {
                    result[index] = current[index];
                }
            }

            return result;
        }

        private List<int> GetSurroundingSeatReferences(int index)
        {
            return this.GetSurroundingSeatReferences(
                index % this.totalColumns,
                index / this.totalColumns);
        }

        private List<int> GetSurroundingSeatReferences(int currentColumn, int currentRow)
        {
            var result = new List<int>();

            bool firstCol = currentColumn == 0;
            bool lastCol = currentColumn == this.totalColumns - 1;
            bool firstRow = currentRow == 0;
            bool lastRow = currentRow == this.totalRows - 1;

            if (!firstRow)
            {
                if (!firstCol)
                {
                    // Add up-left
                    result.Add(this.GetSeatIndex(currentColumn - 1, currentRow - 1));
                }

                // Add up
                result.Add(this.GetSeatIndex(currentColumn, currentRow - 1));

                if (!lastCol)
                {
                    // Add up-right
                    result.Add(this.GetSeatIndex(currentColumn + 1, currentRow - 1));
                }
            }

            if (!firstCol)
            {
                // Add left
                result.Add(this.GetSeatIndex(currentColumn - 1, currentRow));
            }

            if (!lastCol)
            {
                // Add right
                result.Add(this.GetSeatIndex(currentColumn + 1, currentRow));
            }

            if (!lastRow)
            {
                if (!firstCol)
                {
                    // Add down-left
                    result.Add(this.GetSeatIndex(currentColumn - 1, currentRow + 1));
                }

                // Add down
                result.Add(this.GetSeatIndex(currentColumn, currentRow + 1));

                if (!lastCol)
                {
                    // Add down-right
                    result.Add(this.GetSeatIndex(currentColumn + 1, currentRow + 1));
                }
            }

            return result;
        }

        private int GetSeatIndex(int col, int row)
        {
            return col + (row * this.totalColumns);
        }
    }
}
