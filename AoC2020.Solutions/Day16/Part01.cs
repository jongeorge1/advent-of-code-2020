namespace AoC2020.Solutions.Day16
{
    using System;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            // First, break up into the components of the input.
            string[] inputComponents = input.Split(Environment.NewLine + Environment.NewLine);

            // We should now have three components
            // 0: rules
            // 1: our ticket
            // 2: other tickets
            Rule[] rules = inputComponents[0].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new Rule(x)).ToArray();

            int[][] nearbyTickets = inputComponents[2].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .Select(ticketInput => ticketInput.Split(',').Select(int.Parse).ToArray())
                .ToArray();

            var invalidValues = nearbyTickets.SelectMany(
                ticketValues => ticketValues.Where(
                    ticketValue => rules.All(
                        rule => !rule.Validate(ticketValue))));

            return invalidValues.Sum().ToString();
        }
    }
}
