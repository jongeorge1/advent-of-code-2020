namespace AoC2020.Solutions.Day03
{
    using System;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            string[] rows = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            return this.CountTreesOnSlope(rows, 3, 1).ToString();
        }

        private int CountTreesOnSlope(string[] map, int colStep, int rowStep)
        {
            int rowCount = map.Length;
            int colCount = map[0].Length;

            int treeCount = 0;
            int currentRow = 0;
            int currentCol = 0;

            do
            {
                if (map[currentRow][currentCol % colCount] == '#')
                {
                    treeCount++;
                }

                currentRow += rowStep;
                currentCol += colStep;
            }
            while (currentRow < rowCount);

            return treeCount;
        }
    }
}
