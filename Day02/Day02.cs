namespace Day02;

internal class Day03
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
        var total = lines.Select(line => line.Split(":"))
            .Select(gameIdSets => new
            {
                id = int.Parse(gameIdSets[0].Replace("Game ", "")),
                sets = gameIdSets[1].Split("; ")
            })
            .Where(idToSets => GameIsPossible(idToSets.sets))
            .Select(idToSets => idToSets.id)
            .Sum();

        return total;
    }

    private static bool GameIsPossible(IEnumerable<string> sets)
    {
        var cubeLimit = new Dictionary<string, int>
        {
            ["red"] = 12, ["green"] = 13, ["blue"] = 14
        };

        foreach (var cubeReveal in sets.Select(s => s.Split(", ")).SelectMany(s => s))
        {
            var colorTotal = cubeReveal.Trim().Split(" ");

            var limit = cubeLimit[colorTotal[1]];
            var count = int.Parse(colorTotal[0]);

            if (count > limit)
            {
                return false;
            }
        }

        return true;
    }

    public static int Part2(string[] lines)
    {
        var total = 0;

        foreach (var line in lines)
        {
            var minimumCubeSet = new Dictionary<string, int> {
                ["blue"] = 0, ["red"] = 0, ["green"] = 0
            };

            var gameIdSets = line.Split(":");
            var sets = gameIdSets[1].Split("; ");

            foreach (var set in sets)
            {
                foreach (var cubeReveal in set.Split(", "))
                {
                    var colorTotal = cubeReveal.Trim().Split(" ");

                    var color = colorTotal[1];
                    var count = int.Parse(colorTotal[0]);

                    if (count > minimumCubeSet[color])
                    {
                        minimumCubeSet[color] = count;
                    }
                }
            }

            total += minimumCubeSet["blue"] * minimumCubeSet["red"] * minimumCubeSet["green"];
        }

        return total;
    }
}