using System.Text.RegularExpressions;

namespace Day04;

internal class Day04
{
	private static void Main()
    {
        var input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

        var lines = input.Split("\r\n");

        Console.WriteLine($"Part 1: {Part1(lines)}");
        Console.WriteLine($"Part 2: {Part2(lines)}");
    }

	private static double Part1(IEnumerable<string> lines)
    {
	    var total = CalculateWins(lines)
		    .Where(numWins => numWins.Value > 0)
		    .Select(numWins => Math.Pow(2, numWins.Value - 1))
			.Sum();

	    return total;
    }

	private static int Part2(IEnumerable<string> lines)
    {
		var cards = CalculateWins(lines);

		var cardCounts = new Dictionary<int, int>();
		foreach (var (id, winCount) in cards)
		{
			cardCounts.TryAdd(id, 0);
			cardCounts[id] += 1;

			for (var i = 1; i <= winCount; i++)
			{
				cardCounts.TryAdd(id + i, 0);
				cardCounts[id + i] += cardCounts[id];
			}
		}

		return cardCounts.Values.Sum();
    }

    private static Dictionary<int, int> CalculateWins(IEnumerable<string> lines)
    {
	    var regex = new Regex(@"Card *(?'id'\d*): (?'winning'[\d| ]*)\|(?'have'[\d| ]*)");

		return lines
		    .Select(l => regex.Match(l))
		    .Select(m => new
			    {
				    id = int.Parse(m.Groups["id"].Value),
				    winning = m.Groups["winning"].Value.Split(" ", StringSplitOptions.RemoveEmptyEntries),
				    have = m.Groups["have"].Value.Split(" ", StringSplitOptions.RemoveEmptyEntries)
			    }
		    )
		    .ToDictionary(c => c.id, c => c.have.Count(c.winning.Contains));
    }
}