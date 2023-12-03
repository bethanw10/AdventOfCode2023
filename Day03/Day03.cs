namespace Day03;

internal class Day03
{
    private static void Main()
    {
        var input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

        var lines = input.Split("\r\n");

        Part1(lines);
        Part2(lines);
    }

    public static void Part1(string[] lines)
    {
        var total = 0;

        var delta = new[]
        {
            new Point(1, 0),  new Point(1, 1),  new Point(1, -1),
            new Point(0, 1),                    new Point(0, -1),
            new Point(-1, 1), new Point(-1, 0), new Point(-1, -1),
        };

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
                        foundAdjacentSymbol = delta
                            .Select(d => new Point(d.X + x, d.Y + y))
                            .Where(p => p.Y >= 0 && p.Y < lines.Length &&
                                        p.X >= 0 && p.X < lines[p.Y].Length)
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

        Console.WriteLine(total);
    }

    public static void Part2(string[] lines)
    {
       
    }

  
}

internal class SchematicNumber
{
    public string Num = "";
    public int X1;
    public int X2;
    public int Y;

    public override string ToString()
    {
        return $"{Num} x: {X1} - {X2} y: {Y}";
    }
}

internal record Point(int X, int Y);