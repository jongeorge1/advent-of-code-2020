namespace AoC2020.Solutions.Day16
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
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

            int[] myTicket = inputComponents[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .Select(ticketInput => ticketInput.Split(',').Select(int.Parse).ToArray()).First();

            int[][] nearbyTickets = inputComponents[2].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .Select(ticketInput => ticketInput.Split(',').Select(int.Parse).ToArray())
                .ToArray();

            var validTickets = nearbyTickets.Where(
                ticketValues => ticketValues.All(
                    ticketValue => rules.Any(
                        rule => rule.Validate(ticketValue)))).ToList();

            validTickets.Add(myTicket);

            var ruleLayoutCandidates = Enumerable.Range(0, myTicket.Length)
                .Select(i => rules.Where(rule => validTickets.All(ticket => rule.Validate(ticket[i]))).Select(rule => rule.Name).ToList()).ToList();

            // We now need to make the final selection. We do this by iterating the possible rules until we've
            // identified a single candidate for each slot.
            var usedRules = new List<string>(rules.Length);
            string[] finalRuleSelection = new string[rules.Length];
            bool unknowns = true;

            while (unknowns)
            {
                unknowns = false;

                for (int i = 0; i < ruleLayoutCandidates.Count; i++)
                {
                    if (string.IsNullOrEmpty(finalRuleSelection[i]))
                    {
                        var candidatesForSlot = ruleLayoutCandidates[i].Where(candidate => !usedRules.Contains(candidate)).ToList();
                        if (candidatesForSlot.Count == 1)
                        {
                            finalRuleSelection[i] = candidatesForSlot[0];
                            usedRules.Add(candidatesForSlot[0]);
                        }
                        else
                        {
                            unknowns = true;
                        }
                    }
                }
            }

            long result = 1;
            for (int i = 0; i < myTicket.Length; i++)
            {
                if (finalRuleSelection[i].StartsWith("departure"))
                {
                    result *= myTicket[i];
                }
            }

            return result.ToString();
        }
    }
}
