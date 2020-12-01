namespace AoC2020.Solutions
{
    using System;

    public static class SolutionFactory
    {
        public static ISolution GetSolution(int day, int part)
        {
            string className = $"AoC2020.Solutions.Day{day:D2}.Part{part:D2}";
            Type? targetType = typeof(ISolution).Assembly.GetType(className);
            var instance = (ISolution?)Activator.CreateInstance(targetType!);

            return instance!;
        }
    }
}
