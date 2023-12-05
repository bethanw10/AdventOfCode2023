using System.Text.RegularExpressions;

namespace Day05;

internal class Day05
{

	public record AlmanacMap(int Source, int Destination, int Range);

	private static void Main()
    {
        var input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

        var lines = input.Split("\r\n\r\n");

        var seeds = lines[0].Split(" ").Skip(1).Select(int.Parse);

        var maps = lines[1..]
	        .Select(l => l
		        .Split("\r\n").Skip(1)
		        .Select(m => m.Split(" "))
		        .Select( a => new AlmanacMap(int.Parse(a[0]), int.Parse(a[1]), int.Parse(a[2])) )
		        .ToArray()
	        )
	        .ToArray();

        Console.WriteLine($"Part 1: {Part1(seeds, maps)}");
        Console.WriteLine($"Part 2: {Part2(lines)}");
    }

	private static double Part1(IEnumerable<int> seeds, AlmanacMap[][] maps)
	{
		foreach (var seed in seeds)
		{
			var relevantMap = maps[0].FirstOrDefault(m => seed >= m.Source && seed <= m.Source + m.Range);

			if (relevantMap != null)
			{
				var destinationNumber = relevantMap.Destination + (seed - relevantMap.Source);

				Console.WriteLine(destinationNumber);
			}
		}

		return 0;
	}

	private static int Part2(IEnumerable<string> lines)
	{
		return 0;
	}
}