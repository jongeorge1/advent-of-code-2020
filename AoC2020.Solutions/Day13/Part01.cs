namespace AoC2020.Solutions.Day13
{
    using System;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            // Get rid of the newlines
            string[] data = input
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int earliestDepartureTime = int.Parse(data[0]);
            int[] departureIntervals = data[1].Split(',').Where(x => x != "x").Select(int.Parse).ToArray();

            (int serviceNumber, int minutesToWait) earliestSuitableDeparture = (0, int.MaxValue);

            foreach (int current in departureIntervals)
            {
                // What is the lowest multiple of the departure interval that's larger than
                // the departure time?
                // Given the answer requires multiplication of the wait time, we can assume
                // that none of the services will depart exactly on our arrival time.
                int departureTime = (1 + (earliestDepartureTime / current)) * current;
                int minutesToWait = departureTime - earliestDepartureTime;
                if (minutesToWait < earliestSuitableDeparture.minutesToWait)
                {
                    earliestSuitableDeparture = (current, minutesToWait);
                }
            }

            return (earliestSuitableDeparture.serviceNumber * earliestSuitableDeparture.minutesToWait).ToString();
        }
    }
}
