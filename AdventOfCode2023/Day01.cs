using System.Text.RegularExpressions;

namespace Day01;

internal class Day01
{
    private static void Main()
    {
        var input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

        var lines = input.Split("\r\n");

        Console.WriteLine($"Part 1: {Part1(lines)}");
        Console.WriteLine($"Part 2: {Part2(lines)}");
    }

    public static int Part1(string[] lines)
    {
        var total = lines
            .Select(line => new
            {
                first = line.First(char.IsDigit),
                last = line.Last(char.IsDigit), 
            })
            .Select(digits => int.Parse($"{digits.first}{digits.last}"))
            .Sum();

        return total;
    }

    public static int Part2(string[] lines)
    {
        var regex = new Regex("(?=(?<digit>\\d|one|two|three|four|five|six|seven|eight|nine))");

        var total = lines
            .Select(line => regex.Matches(line))
            .Select(matches => new
            {
                firstDigit = matches.First().Groups["digit"].Value,
                lastDigit = matches.Last().Groups["digit"].Value
            })
            .Select(t => ConvertToInt(t.firstDigit) * 10 + ConvertToInt(t.lastDigit))
            .Sum();

        return total;
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