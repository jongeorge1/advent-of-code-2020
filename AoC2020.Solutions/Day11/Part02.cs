namespace AoC2020.Solutions.Day11
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
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
                List<char> visibleSeatStates = this.GetVisibleSeatStates(current, index);

                if (current[index] == 'L' && !visibleSeatStates.Any(i => i == '#'))
                {
                    result[index] = '#';
                }
                else if (current[index] == '#' && visibleSeatStates.Count(i => i == '#') >= 5)
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

        private List<char> GetVisibleSeatStates(char[] data, int index)
        {
            return new List<char>
            {
                this.GetFirstSeatStateForOffset(data, index, -1, -1),
                this.GetFirstSeatStateForOffset(data, index, 0, -1),
                this.GetFirstSeatStateForOffset(data, index, 1, -1),
                this.GetFirstSeatStateForOffset(data, index, -1, 0),
                this.GetFirstSeatStateForOffset(data, index, 1, 0),
                this.GetFirstSeatStateForOffset(data, index, -1, 1),
                this.GetFirstSeatStateForOffset(data, index, 0, 1),
                this.GetFirstSeatStateForOffset(data, index, 1, 1),
            };
        }

        private char GetFirstSeatStateForOffset(char[] data, int index, int columnOffset, int rowOffset)
        {
            return this.GetFirstSeatStateForOffset(
                data,
                index % this.totalColumns,
                index / this.totalColumns,
                columnOffset,
                rowOffset);
        }

        private char GetFirstSeatStateForOffset(char[] data, int currentColumn, int currentRow, int columnOffset, int rowOffset)
        {
            while (true)
            {
                currentColumn += columnOffset;
                currentRow += rowOffset;

                // Make sure we haven't left the grid
                if (currentColumn < 0 || currentColumn >= this.totalColumns || currentRow < 0 || currentRow >= this.totalRows)
                {
                    return '.';
                }

                if (data[this.GetSeatIndex(currentColumn, currentRow)] != '.')
                {
                    return data[this.GetSeatIndex(currentColumn, currentRow)];
                }
            }
        }

        private int GetSeatIndex(int col, int row)
        {
            return col + (row * this.totalColumns);
        }
    }
}
