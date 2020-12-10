namespace AoC2020.Solutions.Day08
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
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

            // We know that the current state is wrong, so we won't bother executing it. We'll simply loop through
            // all the instructions, and every time we find a jmp or a nop, we'll execute a variant of the instructions
            // with that instruction changed.
            for (int pointer = 0; pointer < state.Instructions.Length; pointer++)
            {
                string? newInstruction = null;

                if (state.Instructions[pointer].Name == "jmp")
                {
                    newInstruction = "nop";
                }
                else if (state.Instructions[pointer].Name == "nop")
                {
                    newInstruction = "jmp";
                }

                if (newInstruction != null)
                {
                    ConsoleState copiedState = state.Clone();
                    copiedState.Instructions[pointer] = new Instruction(newInstruction, copiedState.Instructions[pointer].Parameter);
                    if (copiedState.RunToEnd())
                    {
                        return copiedState.Accumulator.ToString();
                    }
                }
            }

            return string.Empty;
        }
    }
}
