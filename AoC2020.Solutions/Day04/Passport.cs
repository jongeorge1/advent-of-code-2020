namespace AoC2020.Solutions.Day04
{
    using System;
    using System.Globalization;
    using System.Linq;

    public class Passport
    {
        private static readonly string[] RequiredEntries = new[] { "ecl", "pid", "eyr", "hcl", "byr", "iyr", "hgt" };

        private static readonly string[] ValidEyeColours = new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

        private readonly string[] keyValuePairs;

        public Passport(string input)
        {
            this.keyValuePairs = input.Split(new string[] { Environment.NewLine, " ", ":" }, StringSplitOptions.RemoveEmptyEntries);

            if (this.keyValuePairs.Length % 2 == 1)
            {
                throw new ArgumentException("Unexpected number of passport fields");
            }

            this.ContainsAllRequiredFields = RequiredEntries.All(x => this.keyValuePairs.Contains(x));
        }

        public bool ContainsAllRequiredFields { get; }

        public bool IsValid()
        {
            if (!this.ContainsAllRequiredFields)
            {
                return false;
            }

            for (int i = 0; i < this.keyValuePairs.Length - 1; i += 2)
            {
                string val = this.keyValuePairs[i + 1];

                switch (this.keyValuePairs[i])
                {
                    case "byr":
                        if (!ValidateNumericField(val, 4, 1920, 2002))
                        {
                            return false;
                        }

                        break;

                    case "iyr":
                        if (!ValidateNumericField(val, 4, 2010, 2020))
                        {
                            return false;
                        }

                        break;

                    case "eyr":
                        if (!ValidateNumericField(val, 4, 2020, 2030))
                        {
                            return false;
                        }

                        break;

                    case "hgt":
                        if (!int.TryParse(val[0..^2], out int height))
                        {
                            return false;
                        }

                        string? heightType = val[^2..^0];

                        if (heightType != "in" && heightType != "cm")
                        {
                            return false;
                        }

                        if (heightType == "in" && (height < 59 || height > 76))
                        {
                            return false;
                        }

                        if (heightType == "cm" && (height < 150 || height > 193))
                        {
                            return false;
                        }

                        break;

                    case "hcl":
                        if (val.Length != 7 || val[0] != '#')
                        {
                            return false;
                        }

                        if (!int.TryParse(val[1..], NumberStyles.HexNumber, null, out int _))
                        {
                            return false;
                        }

                        break;

                    case "ecl":
                        if (!ValidEyeColours.Contains(val))
                        {
                            return false;
                        }

                        break;

                    case "pid":
                        if (val.Length != 9)
                        {
                            return false;
                        }

                        if (!long.TryParse(val, out long _))
                        {
                            return false;
                        }

                        break;

                    case "cid":
                        // Do nothing
                        break;

                    default:
                        throw new ArgumentException($"Unexpected field '{this.keyValuePairs[i]}'");
                }
            }

            return true;
        }

        private static bool ValidateNumericField(string field, int expectedLength, int minValue, int maxValue)
        {
            if (field.Length != expectedLength)
            {
                return false;
            }

            return int.TryParse(field, out int val) && val >= minValue && val <= maxValue;
        }
    }
}
