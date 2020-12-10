namespace AoC2020.Solutions.Day10
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            var adapters = input
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            adapters.Add(0);
            adapters.Add(adapters.Max() + 3);
            adapters.Sort();

            // Now go through and for each item, find the locations it can branch to.
            var adaptersWithPossibleConnections = adapters.ToDictionary(
                x => x,
                x => adapters.Where(a => a > x && a <= x + 3).ToList());

            // Now work back through the list, counting the number of possible connection chains for each element. This
            // works because we know that by the time we process an element, we will have processed all of its possible
            // connections.
            var adaptersWithBranchCounts = new Dictionary<int, long>();

            for (int i = adapters.Count - 1; i >= 0; i--)
            {
                int currentAdapter = adapters[i];
                List<int> possibleConnections = adaptersWithPossibleConnections[currentAdapter];
                if (possibleConnections.Count == 0)
                {
                    adaptersWithBranchCounts[currentAdapter] = 0;
                }
                else
                {
                    // One of the "branches" is not actually a "branch" - it's the continuation of the main chain, so
                    // we don't count it. We still need to count the branches out from that element though.
                    adaptersWithBranchCounts[currentAdapter] = possibleConnections.Count - 1 + possibleConnections.Sum(x => adaptersWithBranchCounts[x]);
                }
            }

            // We need to add one to the result to deal with the fact that we never counted the "main" branch for each
            // adapter.
            return (adaptersWithBranchCounts[0] + 1).ToString();
        }
    }
}
