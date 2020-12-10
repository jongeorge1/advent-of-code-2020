namespace AoC2020.Solutions.Day07
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            var data = input
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(GetPackingInfo)
                .ToList();

            return BagsRequiredInside("shiny gold", data).ToString();
        }

        private static int BagsRequiredInside(string containerColour, List<KeyValuePair<string, Dictionary<string, int>>> bagMap)
        {
            KeyValuePair<string, Dictionary<string, int>> containedBags = bagMap.Find(x => x.Key == containerColour);

            if (string.IsNullOrEmpty(containedBags.Key))
            {
                return 0;
            }

            int total = 0;

            foreach (KeyValuePair<string, int> current in containedBags.Value)
            {
                total += current.Value + (current.Value * BagsRequiredInside(current.Key, bagMap));
            }

            return total;
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
