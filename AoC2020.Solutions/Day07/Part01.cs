namespace AoC2020.Solutions.Day07
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            var data = input
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(GetPackingInfo)
                .ToList();

            var bagsThatCanUltimatelyContainAShinyGoldBag = new HashSet<string>();
            var bagColoursToProcess = new Queue<string>();
            bagColoursToProcess.Enqueue("shiny gold");
            while (bagColoursToProcess.Count > 0)
            {
                string currentBagColour = bagColoursToProcess.Dequeue();

                // Find everything that could contain this bag.
                foreach (KeyValuePair<string, Dictionary<string, int>> current in data.Where(x => x.Value.ContainsKey(currentBagColour)))
                {
                    if (!bagsThatCanUltimatelyContainAShinyGoldBag.Contains(current.Key))
                    {
                        bagsThatCanUltimatelyContainAShinyGoldBag.Add(current.Key);
                        bagColoursToProcess.Enqueue(current.Key);
                    }
                }
            }

            return bagsThatCanUltimatelyContainAShinyGoldBag.Count.ToString();
        }

        private static KeyValuePair<string, Dictionary<string, int>> GetPackingInfo(string input)
        {
            int index = input.IndexOf(" bags contain");
            string containerColour = input.Substring(0, index);

            // Split the remainder on commas
            IEnumerable<string> containedInput = input[(index + 14) ..].Split(",").Select(x => x.Trim());

            var containedBags = new Dictionary<string, int>();

            foreach (string current in containedInput)
            {
                if (current == "no other bags.")
                {
                    continue;
                }

                int indexOfFirstSpace = current.IndexOf(" ");
                int indexOfBags = current.IndexOf(" bag");

                int numberOfBags = int.Parse(current.Substring(0, indexOfFirstSpace));
                string bagColour = current[indexOfFirstSpace..indexOfBags].Trim();
                containedBags.Add(bagColour, numberOfBags);
            }

            return new KeyValuePair<string, Dictionary<string, int>>(containerColour, containedBags);
        }
    }
}
