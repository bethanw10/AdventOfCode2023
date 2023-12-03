namespace Day03;

internal class Day03
{
    internal record Point(int X, int Y);

    private static readonly Point[] Delta = {
        new(1, 0),  new(1, 1),  new(1, -1),
        new(0, 1),              new(0, -1),
        new(-1, 1), new(-1, 0), new(-1, -1),
    };

    private static void Main()
    {
        var input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

        var lines = input.Split("\r\n");

        Console.WriteLine(Part1(lines));
        Console.WriteLine(Part2(lines));
    }

    public static string Part1(string[] lines)
    {
        var total = 0;
        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            var number = "";
            var foundAdjacentSymbol = false;

            for (var x = 0; x < line.Length; x++)
            {
                var c = line[x];
                if (char.IsDigit(c))
                {
                    number += c;

                    if (!foundAdjacentSymbol)
                    {
                        foundAdjacentSymbol = Delta
                            .Select(d => new Point(d.X + x, d.Y + y))
                            .Where(p => IsInBounds(lines, p))
                            .Select(p => lines[p.Y][p.X])
                            .Any(ch => ch != '.' && !char.IsDigit(ch));
                    }
                }
                else
                {
                    if (foundAdjacentSymbol)
                    {
                        total += int.Parse(number);
                    }

                    number = "";
                    foundAdjacentSymbol = false;
                }
            }

            if (foundAdjacentSymbol)
            {
                total += int.Parse(number);
            }
        }

        return total.ToString();
    }

    public static string Part2(string[] lines)
    {
        var allAsterisks = new Dictionary<Point, List<int>>();

        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            var number = "";
            var asterisks = new HashSet<Point>();

            for (var x = 0; x < line.Length; x++)
            {
                var c = line[x];
                if (char.IsDigit(c))
                {
                    number += c;

                    var adjacentAsterisks = Delta
                        .Select(d => new Point(d.X + x, d.Y + y))
                        .Where(p => IsInBounds(lines, p) && lines[p.Y][p.X] == '*')
                        .ToArray();
                    
                    foreach (var asterisk in adjacentAsterisks)
                    {
                        asterisks.Add(asterisk);
                    }
                }
                else
                {
                    AddAsterisks(number, asterisks, allAsterisks);

                    number = "";
                    asterisks = new HashSet<Point>();
                }
            }

            AddAsterisks(number, asterisks, allAsterisks);
        }

        var total = allAsterisks
            .Where(a => a.Value.Count == 2)
            .Select(a => a.Value.Aggregate(1, (n1, n2) => n1 * n2))
            .Sum();

        return total.ToString();
    }

    private static void AddAsterisks(string number, HashSet<Point> adjacentAsterisks, Dictionary<Point, List<int>> gears)
    {
        if (!adjacentAsterisks.Any())
        {
            return;
        }

        var parsedNum = int.Parse(number);

        foreach (var asterisk in adjacentAsterisks)
        {
            if (gears.TryGetValue(asterisk, out var numbers))
            {
                numbers.Add(parsedNum);
            }
            else
            {
                gears.Add(asterisk, new List<int> { parsedNum });
            }
        }
    }

    private static bool IsInBounds(string[] lines, Point p)
    {
        return p.Y >= 0 && p.Y < lines.Length &&
               p.X >= 0 && p.X < lines[p.Y].Length;
    }
}