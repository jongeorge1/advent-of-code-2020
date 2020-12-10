namespace AoC2020.Solutions.Day08
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            Instruction[] data = input
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new Instruction(x))
                .ToArray();

            var state = new ConsoleState
            {
                Accumulator = 0,
                Pointer = 0,
                Instructions = data,
                ExecutedInstructions = new List<int>(),
            };

            state.RunToEnd();

            return state.Accumulator.ToString();
        }
    }
}
