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
        [TestCase(7, 1, "light red bags contain 1 bright white bag, 2 muted yellow bags.\r\ndark orange bags contain 3 bright white bags, 4 muted yellow bags.\r\nbright white bags contain 1 shiny gold bag.\r\nmuted yellow bags contain 2 shiny gold bags, 9 faded blue bags.\r\nshiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.\r\ndark olive bags contain 3 faded blue bags, 4 dotted black bags.\r\nvibrant plum bags contain 5 faded blue bags, 6 dotted black bags.\r\nfaded blue bags contain no other bags.\r\ndotted black bags contain no other bags.", "4")]
        [TestCase(7, 2, "light red bags contain 1 bright white bag, 2 muted yellow bags.\r\ndark orange bags contain 3 bright white bags, 4 muted yellow bags.\r\nbright white bags contain 1 shiny gold bag.\r\nmuted yellow bags contain 2 shiny gold bags, 9 faded blue bags.\r\nshiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.\r\ndark olive bags contain 3 faded blue bags, 4 dotted black bags.\r\nvibrant plum bags contain 5 faded blue bags, 6 dotted black bags.\r\nfaded blue bags contain no other bags.\r\ndotted black bags contain no other bags.", "32")]
        [TestCase(7, 2, "shiny gold bags contain 2 dark red bags.\r\ndark red bags contain 2 dark orange bags.\r\ndark orange bags contain 2 dark yellow bags.\r\ndark yellow bags contain 2 dark green bags.\r\ndark green bags contain 2 dark blue bags.\r\ndark blue bags contain 2 dark violet bags.\r\ndark violet bags contain no other bags.", "126")]
        [TestCase(8, 1, "nop +0\r\nacc +1\r\njmp +4\r\nacc +3\r\njmp -3\r\nacc -99\r\nacc +1\r\njmp -4\r\nacc +6", "5")]
        [TestCase(8, 2, "nop +0\r\nacc +1\r\njmp +4\r\nacc +3\r\njmp -3\r\nacc -99\r\nacc +1\r\njmp -4\r\nacc +6", "8")]
        [TestCase(9, 1, "35\r\n20\r\n15\r\n25\r\n47\r\n40\r\n62\r\n55\r\n65\r\n95\r\n102\r\n117\r\n150\r\n182\r\n127\r\n219\r\n299\r\n277\r\n309\r\n576", "127")]
        [TestCase(9, 2, "35\r\n20\r\n15\r\n25\r\n47\r\n40\r\n62\r\n55\r\n65\r\n95\r\n102\r\n117\r\n150\r\n182\r\n127\r\n219\r\n299\r\n277\r\n309\r\n576", "62")]
        [TestCase(10, 1, "16\r\n10\r\n15\r\n5\r\n1\r\n11\r\n7\r\n19\r\n6\r\n12\r\n4", "35")]
        [TestCase(10, 1, "28\r\n33\r\n18\r\n42\r\n31\r\n14\r\n46\r\n20\r\n48\r\n47\r\n24\r\n23\r\n49\r\n45\r\n19\r\n38\r\n39\r\n11\r\n1\r\n32\r\n25\r\n35\r\n8\r\n17\r\n7\r\n9\r\n4\r\n2\r\n34\r\n10\r\n3", "220")]
        [TestCase(10, 2, "16\r\n10\r\n15\r\n5\r\n1\r\n11\r\n7\r\n19\r\n6\r\n12\r\n4", "8")]
        [TestCase(10, 2, "1\r\n2\r\n3", "4")]
        [TestCase(10, 2, "28\r\n33\r\n18\r\n42\r\n31\r\n14\r\n46\r\n20\r\n48\r\n47\r\n24\r\n23\r\n49\r\n45\r\n19\r\n38\r\n39\r\n11\r\n1\r\n32\r\n25\r\n35\r\n8\r\n17\r\n7\r\n9\r\n4\r\n2\r\n34\r\n10\r\n3", "19208")]
        [TestCase(11, 1, "L.LL.LL.LL\r\nLLLLLLL.LL\r\nL.L.L..L..\r\nLLLL.LL.LL\r\nL.LL.LL.LL\r\nL.LLLLL.LL\r\n..L.L.....\r\nLLLLLLLLLL\r\nL.LLLLLL.L\r\nL.LLLLL.LL", "37")]
        [TestCase(11, 2, "L.LL.LL.LL\r\nLLLLLLL.LL\r\nL.L.L..L..\r\nLLLL.LL.LL\r\nL.LL.LL.LL\r\nL.LLLLL.LL\r\n..L.L.....\r\nLLLLLLLLLL\r\nL.LLLLLL.L\r\nL.LLLLL.LL", "26")]
        [TestCase(12, 1, "F10\r\nN3\r\nF7\r\nR90\r\nF11", "25")]
        [TestCase(12, 2, "F10\r\nN3\r\nF7\r\nR90\r\nF11", "286")]
        [TestCase(13, 1, "939\r\n7,13,x,x,59,x,31,19", "295")]
        [TestCase(13, 2, "0\r\n67,7,59,61", "754018")]
        [TestCase(13, 2, "0\r\n67,x,7,59,61", "779210")]
        [TestCase(13, 2, "0\r\n67,7,x,59,61", "1261476")]
        [TestCase(13, 2, "0\r\n1789,37,47,1889", "1202161486")]
        [TestCase(14, 1, "mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X\r\nmem[8] = 11\r\nmem[7] = 101\r\nmem[8] = 0", "165")]
        [TestCase(14, 2, "mask = 000000000000000000000000000000X1001X\r\nmem[42] = 100\r\nmask = 00000000000000000000000000000000X0XX\r\nmem[26] = 1", "208")]
        [TestCase(15, 1, "0,3,6", "436")]
        [TestCase(15, 1, "1,3,2", "1")]
        [TestCase(15, 1, "2,1,3", "10")]
        [TestCase(15, 1, "1,2,3", "27")]
        [TestCase(15, 1, "2,3,1", "78")]
        [TestCase(15, 1, "3,2,1", "438")]
        [TestCase(15, 1, "3,1,2", "1836")]
        [TestCase(16, 1, "class: 1-3 or 5-7\r\nrow: 6-11 or 33-44\r\nseat: 13-40 or 45-50\r\n\r\nyour ticket:\r\n7,1,14\r\n\r\nnearby tickets:\r\n7,3,47\r\n40,4,50\r\n55,2,20\r\n38,6,12", "71")]
        [TestCase(17, 1, ".#.\r\n..#\r\n###", "112")]
        [TestCase(17, 2, ".#.\r\n..#\r\n###", "848")]
        [TestCase(18, 1, "2 + 5", "7")]
        [TestCase(18, 1, "2 * 5", "10")]
        [TestCase(18, 1, "2 + 5 * 5", "35")]
        [TestCase(18, 1, "2 + (5 * 5)", "27")]
        [TestCase(18, 1, "2 * 3 + (4 * 5)", "26")]
        [TestCase(18, 1, "5 + (8 * 3 + 9 + 3 * 4 * 3)", "437")]
        [TestCase(18, 1, "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", "12240")]
        [TestCase(18, 1, "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", "13632")]
        public void Tests(int day, int part, string input, string expectedResult)
        {
            ISolution solution = SolutionFactory.GetSolution(day, part);
            string result = solution.Solve(input);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}