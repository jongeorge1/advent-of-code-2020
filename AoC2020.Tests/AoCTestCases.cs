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
        [TestCase(2, 1, "1-3 a: abcde\r\n1 - 3 b: cdefg\r\n2 - 9 c: ccccccccc", "2")]
        [TestCase(2, 2, "1-3 a: abcde\r\n1 - 3 b: cdefg\r\n2 - 9 c: ccccccccc", "1")]
        [TestCase(3, 1, "..##.......\r\n#...#...#..\r\n.#....#..#.\r\n..#.#...#.#\r\n.#...##..#.\r\n..#.##.....\r\n.#.#.#....#\r\n.#........#\r\n#.##...#...\r\n#...##....#\r\n.#..#...#.#", "7")]
        [TestCase(3, 2, "..##.......\r\n#...#...#..\r\n.#....#..#.\r\n..#.#...#.#\r\n.#...##..#.\r\n..#.##.....\r\n.#.#.#....#\r\n.#........#\r\n#.##...#...\r\n#...##....#\r\n.#..#...#.#", "336")]
        [TestCase(4, 1, "ecl:gry pid:860033327 eyr:2020 hcl:#fffffd\r\nbyr: 1937 iyr: 2017 cid: 147 hgt: 183cm\r\n\r\niyr: 2013 ecl: amb cid: 350 eyr: 2023 pid: 028048884\r\nhcl:#cfa07d byr:1929\r\n\r\nhcl:#ae17e1 iyr:2013\r\neyr: 2024\r\necl: brn pid: 760753108 byr: 1931\r\nhgt: 179cm\r\n\r\nhcl:#cfa07d eyr:2025 pid:166559648\r\niyr:2011 ecl: brn hgt: 59in", "2")]
        [TestCase(4, 2, "eyr:1972 cid:100\r\nhcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926", "0")]
        [TestCase(4, 2, "iyr:2019\r\nhcl:#602927 eyr:1967 hgt:170cm\r\necl:grn pid: 012533040 byr: 1946", "0")]
        [TestCase(4, 2, "hcl:dab227 iyr:2012\r\necl: brn hgt: 182cm pid: 021572410 eyr: 2020 byr: 1992 cid: 277", "0")]
        [TestCase(4, 2, "hgt:59cm ecl:zzz\r\neyr: 2038 hcl: 74454a iyr: 2023\r\npid: 3556412378 byr: 2007", "0")]
        [TestCase(4, 2, "hgt:59 ecl:zzz\r\neyr: 2038 hcl: 74454a iyr: 2023\r\npid: 3556412378 byr: 2007", "0")]
        [TestCase(4, 2, "pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980\r\nhcl:#623a2f", "1")]
        [TestCase(4, 2, "eyr:2029 ecl:blu cid:129 byr:1989\r\niyr: 2014 pid: 896056539 hcl:#a97842 hgt:165cm", "1")]
        [TestCase(4, 2, "hcl:#888785\r\nhgt: 164cm byr: 2001 iyr: 2015 cid: 88\r\npid: 545766238 ecl: hzl\r\neyr: 2022", "1")]
        [TestCase(4, 2, "iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719", "1")]
        [TestCase(5, 1, "FBFBBFFRLR", "357")]
        [TestCase(5, 1, "BFFFBBFRRR", "567")]
        [TestCase(5, 1, "FFFBBBFRRR", "119")]
        [TestCase(5, 1, "BBFFBBFRLL", "820")]
        [TestCase(5, 1, "FBFBBFFRLR\r\nBFFFBBFRRR\r\nFFFBBBFRRR\r\nBBFFBBFRLL", "820")]
        [TestCase(6, 1, "abc\r\n\r\na\r\nb\r\nc\r\n\r\nab\r\nac\r\n\r\na\r\na\r\na\r\na\r\n\r\nb", "11")]
        [TestCase(6, 2, "abc\r\n\r\na\r\nb\r\nc\r\n\r\nab\r\nac\r\n\r\na\r\na\r\na\r\na\r\n\r\nb", "6")]
        public void Tests(int day, int part, string input, string expectedResult)
        {
            ISolution solution = SolutionFactory.GetSolution(day, part);
            string result = solution.Solve(input);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}