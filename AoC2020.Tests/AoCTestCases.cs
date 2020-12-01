// <copyright file="AoCTestCases.cs" company="Endjin">
// Copyright (c) Endjin. All rights reserved.
// </copyright>

namespace AoC2020.Tests
{
    using AoC2020.Solutions;
    using NUnit.Framework;

    public class AoCTestCases
    {
        [TestCase(1, 1, "1721\r\n979\r\n366\r\n299\r\n675\r\n1456", "514579")]
        [TestCase(1, 2, "1721\r\n979\r\n366\r\n299\r\n675\r\n1456", "241861950")]
        public void Tests(int day, int part, string input, string expectedResult)
        {
            ISolution solution = SolutionFactory.GetSolution(day, part);
            string result = solution.Solve(input);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}