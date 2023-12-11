namespace Day09;

internal class Day09
{
    private static void Main()
    {
        var input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

        var lines = input.Split("\r\n");

        Console.WriteLine($"Part 1: {Part1(lines)}");
        Console.WriteLine($"Part 2: {Part2(lines)}");
    }

    private static int Part1(string[] lines)
    {
        return lines
            .Select(line => line.Split(' ').Select(int.Parse).ToArray())
            .Select(GetNextInSequence).Sum();
    }

    private static int GetNextInSequence(int[] numbers)
    {
        var differences = numbers.Zip(numbers.Skip(1), (x, y) => y - x).ToArray();

        return differences.All(d => d == 0) 
            ? numbers.Last() 
            : numbers.Last() + GetNextInSequence(differences);
    }

    private static int Part2(string[] lines)
    {
        return lines
            .Select(line => line.Split(' ').Select(int.Parse).ToArray())
            .Select(GetLastInSequence).Sum();
    }

    private static int GetLastInSequence(int[] numbers)
    {
        var differences = numbers.Zip(numbers.Skip(1), (x, y) => y - x).ToArray();

        return differences.All(d => d == 0)
            ? numbers.First()
            : numbers.First() - GetLastInSequence(differences);
    }
}