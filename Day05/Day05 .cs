using System.Text.RegularExpressions;

namespace Day05;

internal class Day05
{
    private record AlmanacMap(long DestinationStart, long SourceStart, long Range)
	{
		public long SourceEnd => SourceStart + Range - 1;
		public long DestinationEnd => DestinationStart + Range - 1;
	}

	private static void Main()
    {
        var input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

        var lines = input.Split("\r\n\r\n");

        var seeds = lines[0]
	        .Split(" ").Skip(1)
	        .Select(long.Parse).ToArray();

        var maps = lines[1..]
	        .Select(l => l
		        .Split("\r\n").Skip(1)
		        .Select(m => m.Split(" "))
		        .Select(a => new AlmanacMap(
			        long.Parse(a[0]),
			        long.Parse(a[1]),
			        long.Parse(a[2])))
		        .ToArray()
	        )
	        .ToArray();

        Console.WriteLine($"Part 1: {Part1(seeds, maps)}");
        Console.WriteLine($"Part 2: {Part2(seeds, maps)}");
    }

	private static long Part1(IEnumerable<long> seeds, AlmanacMap[][] mapGroups)
	{
		var locations = new List<long>();

		foreach (var seed in seeds)
		{
			var currentNumber = seed;

			foreach (var maps in mapGroups)
			{
				var relevantMap = maps.FirstOrDefault(m => 
					currentNumber >= m.SourceStart && 
				    currentNumber <= m.SourceEnd);

				if (relevantMap != null)
				{
					currentNumber += (relevantMap.DestinationStart - relevantMap.SourceStart);
				}
			}

			locations.Add(currentNumber);
		}

		return locations.Min();
	}

	private static long Part2(long[] seeds, AlmanacMap[][] mapGroups)
	{
		var minLocation = long.MaxValue;

		for (var i = 0; i < seeds.Length; i += 2)
		{
			var ranges = new List<(long start, long end)>
			{
				(seeds[i], seeds[i] + seeds[i + 1])
			};
			
			foreach (var maps in mapGroups)
			{
				var newRanges = new List<(long start, long end)>();

				foreach (var (rangeStart, rangeEnd) in ranges)
				{
					var foundRanges = FindRanges(rangeStart, rangeEnd, maps);
					newRanges.AddRange(foundRanges);
				}

				ranges = newRanges;
			}

			var min = ranges.Min(r => r.start);

			if (min < minLocation)
			{
				minLocation = min;
			}
		}

		return minLocation;
	}

	private static List<(long start, long end)> FindRanges(long rangeStart, long rangeEnd, AlmanacMap[] map)
	{
		var newRanges = new List<(long start, long end)>();
		var start = rangeStart;

		while (true)
		{
			var mapContainingStart = map.FirstOrDefault(m =>
				start >= m.SourceStart &&
				start <= m.SourceEnd);

			if (mapContainingStart == null)
			{
				// get lowest start of map, still within range (i.e. lower than end)
				// if none, then return whole of range

				var nextMap = map
					.Where(m => m.SourceStart > start && m.SourceEnd <= rangeEnd)
					.MinBy(m => m.SourceStart);

				if (nextMap == null)
				{
					newRanges.Add((start, rangeEnd)); // no conversion
					return newRanges;
				}

				newRanges.Add((start, nextMap.SourceStart - 1)); // no conversion
				start = nextMap.SourceStart - 1;

				continue;
			}

			var rangeEclipsesMap = rangeEnd >= mapContainingStart.SourceEnd;

			if (rangeEclipsesMap)
			{
				var newStart = start + (mapContainingStart.DestinationStart - mapContainingStart.SourceStart);
				var newEnd = mapContainingStart.SourceEnd + (mapContainingStart.DestinationStart - mapContainingStart.SourceStart);

				var newRange = (newStart, newEnd);

				newRanges.Add(newRange);

				start = mapContainingStart.DestinationEnd + 1;
			}
			else
			{
				var newStart = start + (mapContainingStart.DestinationStart - mapContainingStart.SourceStart);
				var newEnd = rangeEnd + (mapContainingStart.DestinationStart - mapContainingStart.SourceStart);

				var newRange = (newStart, newEnd);

				newRanges.Add(newRange);
				return newRanges;
			}
		}
	}
}