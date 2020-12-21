namespace AoC2020.Solutions.Day19
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            string[] components = input.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            string[] ruleInputs = components[0].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            string[] messages = components[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int maxLength = messages.Max(x => x.Length);

            var rules = ruleInputs.Select(RuleFactory.GetRule).ToDictionary(x => x.Number, x => x);
            rules[8] = new NestedRule("8: 42 | 42 8");
            rules[11] = new NestedRule("11: 42 31 | 42 11 31");

            // Find all the things that could match rule 0.
            string regexString = rules[0].ToRegex(rules);

            var regex = new Regex(string.Concat("^", regexString, "$"));

            return messages.Count(x => regex.IsMatch(x)).ToString();
        }

        private interface IRule
        {
            int Number { get; }

            string ToRegex(IDictionary<int, IRule> allRules);
        }

        private static class RuleFactory
        {

            public static IRule GetRule(string input)
            {
                if (input.Contains('"'))
                {
                    return new SingleCharacterRule(input);
                }
                else
                {
                    return new NestedRule(input);
                }
            }
        }

        private class NestedRule : IRule
        {
            private readonly int[][] rulesets;
            private IEnumerable<string>? possibleMessages;
            private string? regex;

            public NestedRule(string input)
            {
                string[] primaryComponents = input.Split(new[] { ':', '|' }, StringSplitOptions.RemoveEmptyEntries);
                this.Number = int.Parse(primaryComponents[0]);

                this.rulesets = primaryComponents.Skip(1)
                    .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray())
                    .ToArray();
            }

            public int Number { get; }

            public string ToRegex(IDictionary<int, IRule> allRules)
            {
                this.regex ??= this.ToRegexInternal(allRules);
                return this.regex;
            }

            private string ToRegexInternal(IDictionary<int, IRule> allRules)
            {
                if (this.Number == 8)
                {
                    // Recursive special case: 1 or more instances of 42.
                    return string.Concat("(", allRules[42].ToRegex(allRules), ")+");
                }

                if (this.Number == 11)
                {
                    // Recursive special case: 1 or more of 42 followed by the same number of 31.
                    string? left = allRules[42].ToRegex(allRules);
                    string? right = allRules[31].ToRegex(allRules);

                    // Now build enough representations of this to hit the test cases.
                    IEnumerable<string> possibles = Enumerable.Range(1, 20).Select(c => string.Concat(string.Concat(Enumerable.Repeat(left, c)), string.Concat(Enumerable.Repeat(right, c))));
                    return string.Concat("(", string.Join('|', possibles), ")");
                }

                string[] regexes = this.rulesets.Select(
                    x => string.Concat(x.Select(i => allRules[i].ToRegex(allRules)))).ToArray();

                if (regexes.Length == 1)
                {
                    return regexes[0];
                }

                return string.Concat("(", string.Join('|', regexes), ")");
            }
        }

        private class SingleCharacterRule : IRule
        {
            private string regex;

            public SingleCharacterRule(string input)
            {
                string[] primaryComponents = input.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                this.Number = int.Parse(primaryComponents[0]);

                this.regex = primaryComponents[1].Trim(' ', '"');
            }

            public int Number { get; }

            public string ToRegex(IDictionary<int, IRule> allRules) => this.regex;
        }
    }
}
