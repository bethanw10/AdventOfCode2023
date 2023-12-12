using System.Text.RegularExpressions;

namespace Day06;

internal class Day06
{
	private static void Main()
	{
		var input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

		var lines = input.Split("\r\n");

		var times = lines[0]
			.Split(" ", StringSplitOptions.RemoveEmptyEntries)
			.Select(long.Parse)
			.Skip(1).ToArray();

		var distances = lines[1]
			.Split(" ", StringSplitOptions.RemoveEmptyEntries)
			.Select(long.Parse)
			.Skip(1).ToArray();

		Console.WriteLine($"Part 1: {Part1And2(times, distances)}");

		var time = long.Parse(lines[0]
			.Replace("Time:", "")
			.Replace(" ", ""));

		var distance = long.Parse(lines[1]
			.Replace("Distance:", "")
			.Replace(" ", ""));

		Console.WriteLine($"Part 2: {Part1And2(new [] { time }, new[] { distance })}");
	}

	private static double Part1And2(long[] times, long[] distances)
	{
		var total = 1;

		for (var i = 0; i < times.Length; i++)
		{
			var time = times[i];
			var distanceRecord = distances[i];

			var totalWaysToWin = 0;

			for (var seconds = 0; seconds < time; seconds++)
			{
				var score = seconds * (time - seconds);

				if (score > distanceRecord)
				{
					totalWaysToWin++;
				}
			}

			total *= totalWaysToWin;
		}

		return total;
	}
}