namespace AoC2020.Solutions.Day08
{
    public struct Instruction
    {
        public Instruction(string input)
        {
            string[] components = input.Split(' ');
            this.Name = components[0];
            this.Parameter = int.Parse(components[1]);
        }

        public Instruction(string name, int parameter)
        {
            this.Name = name;
            this.Parameter = parameter;
        }

        public string Name { get; }

        public int Parameter { get; }
    }
}
