namespace Day10;

internal class Day10
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
        var map = new Dictionary<string, int>();

        foreach (var line in lines)
        {
            
        }

        return 0;
    }

    private static int Part2(string[] lines)
    {
        return 0;
    }
}