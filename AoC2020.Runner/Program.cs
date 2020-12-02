namespace AoC2020.Runner
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using AoC2020.Solutions;

    public static class Program
    {
        private static void Main(string[] args)
        {
            int day = int.Parse(args[0]);
            int part = int.Parse(args[1]);
            int executions = args.Length == 2 ? 1 : int.Parse(args[2]);

            // Load the data
            var locationUri = new UriBuilder(Assembly.GetExecutingAssembly().Location!);
            string location = Uri.UnescapeDataString(locationUri.Path);
            string locationDirectory = Path.GetDirectoryName(location) !;
            string inputFileName = Path.Combine(locationDirectory, "Input", $"day{day:D2}.txt");
            string data = File.ReadAllText(inputFileName);

            string result = string.Empty;
            var times = new List<double>(executions);

            for (int i = 0; i < executions; i++)
            {
                ISolution instance = SolutionFactory.GetSolution(day, part);

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                result = instance.Solve(data);

                stopwatch.Stop();

                times.Add(stopwatch.Elapsed.TotalMilliseconds);
            }

            Console.WriteLine(result);

            if (executions == 1)
            {
                Console.WriteLine($"Result obtained in {times[0]}ms");
            }
            else
            {
                Console.WriteLine($"Processing executed {executions} times:");
                Console.WriteLine($"\tAvg: {times.Average():0.00}ms");
                Console.WriteLine($"\tMin: {times.Min():0.00}ms");
                Console.WriteLine($"\tMax: {times.Max():0.00}ms");
            }
        }
    }
}
