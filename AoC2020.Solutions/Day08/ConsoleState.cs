#nullable disable
namespace AoC2020.Solutions.Day08
{
    using System.Collections.Generic;

    public struct ConsoleState
    {
        public bool HasLooped => this.ExecutedInstructions.Contains(this.Pointer);

        public int Accumulator { get; set; }

        public int Pointer { get; set; }

        public Instruction[] Instructions { get; set; }

        public List<int> ExecutedInstructions { get; set; }

        public void ExecuteCurrentInstruction()
        {
            this.ExecutedInstructions.Add(this.Pointer);
            Instruction instruction = this.Instructions[this.Pointer];

            switch (instruction.Name)
            {
                case "acc":
                    this.Accumulator += instruction.Parameter;
                    this.Pointer++;
                    break;

                case "jmp":
                    this.Pointer += instruction.Parameter;
                    break;

                case "nop":
                    this.Pointer++;
                    break;
            }
        }

        public ConsoleState Clone()
        {
            return new ConsoleState
            {
                Accumulator = this.Accumulator,
                ExecutedInstructions = new List<int>(this.ExecutedInstructions),
                Pointer = this.Pointer,
                Instructions = (Instruction[])this.Instructions.Clone(),
            };
        }

        public bool RunToEnd()
        {
            while (!this.HasLooped && this.Pointer < this.Instructions.Length)
            {
                this.ExecuteCurrentInstruction();
            }

            return !this.HasLooped;
        }
    }
}
