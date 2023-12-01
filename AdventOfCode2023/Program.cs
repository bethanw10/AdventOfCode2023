using System.Text.RegularExpressions;

namespace Day1
{

    internal class Program
    {
        static void Main()
        {
            var input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

            var lines = input.Split("\r\n");

            Part1(lines);
            Part2(lines);
        }

        public static void Part1(string[] lines)
        {
            var total = 0;

            foreach (var line in lines)
            {
                var firstDigit = line.First(c => char.IsDigit(c));
                var lastDigit = line.Last(c => char.IsDigit(c));

                var calibrationValue = int.Parse($"{firstDigit}{lastDigit}");
                
                total += calibrationValue;
            }

            Console.WriteLine(total);
        }

        public static void Part2(string[] lines)
        {
            var total = 0;

            var regex = new Regex("(?=(?<digit>\\d|one|two|three|four|five|six|seven|eight|nine))");

            foreach (var line in lines)
            {
                var matches = regex.Matches(line);

                var firstDigit = matches.First().Groups["digit"].Value;
                var lastDigit = matches.Last().Groups["digit"].Value;

                var calibrationValue = (ConvertToInt(firstDigit) * 10) + ConvertToInt(lastDigit);

                total += calibrationValue;
            }

            Console.WriteLine(total);
        }

        private static int ConvertToInt(string number)
        {
            if (number.Length == 1)
            {
                return int.Parse(number);
            }

            return number switch
            {
                "one" => 1,
                "two" => 2,
                "three" => 3,
                "four" => 4,
                "five" => 5,
                "six" => 6,
                "seven" => 7,
                "eight" => 8,
                "nine" => 9,
                _ => throw new Exception($"Could not convert {number} to an int"),
            };
        }
    }
}