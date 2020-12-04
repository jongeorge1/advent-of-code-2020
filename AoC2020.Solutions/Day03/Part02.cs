namespace AoC2020.Solutions.Day03
{
    using System;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            string[] map = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            return (this.CountTreesOnSlope(map, 1, 1)
                * this.CountTreesOnSlope(map, 3, 1)
                * this.CountTreesOnSlope(map, 5, 1)
                * this.CountTreesOnSlope(map, 7, 1)
                * this.CountTreesOnSlope(map, 1, 2)).ToString();
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
