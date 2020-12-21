namespace AoC2020.Solutions.Day19
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            string[] components = input.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            string[] ruleInputs = components[0].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            string[] messages = components[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var rules = ruleInputs.Select(RuleFactory.GetRule).ToDictionary(x => x.Number, x => x);

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
