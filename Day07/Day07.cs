namespace Day07;

internal class Day04
{
	private static void Main()
	{
		var input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

		var lines = input.Split("\r\n").Select(l => l.Split(" ").ToArray()).ToArray();

		Console.WriteLine($"Part 1: {Part1And2(lines)}");
		Console.WriteLine($"Part 2: {Part1And2(lines, true)}");
	}

	private static int Part1And2(string[][] lines, bool part2 = false)
	{
		var sorted = lines.OrderBy(l => l[0], new Part1HandComparer(part2)).ToArray();

		return sorted.Select((hand, i) => (i + 1) * int.Parse(hand[1])).Sum();
	}

	private enum HandType
	{
		HighCard = 1,
		OnePair = 2,
		TwoPair = 3,
		ThreeOfAKind = 4,
		FullHouse = 5,
		FourOfAKind = 6,
		FiveOfAKind = 7
	}

	private class Part1HandComparer(bool part2) : IComparer<string>
	{
		private readonly List<char> _cardTypes =
            new() {
			    '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A'
		    };

		public int Compare(string handA, string handB)
		{
			var typeA = GetHandType(handA, part2);
			var typeB = GetHandType(handB, part2);

			if (typeA < typeB)
			{
				return -1;
			}

			if (typeA > typeB)
			{
				return 1;
			}

			for (var i = 0; i < handA.Length; i++)
			{
				var cardA = handA[i];
				var cardB = handB[i];

				var strengthA = part2 && cardA == 'J' ? -1 : _cardTypes.IndexOf(cardA);
				var strengthB = part2 && cardB == 'J' ? -1 : _cardTypes.IndexOf(cardB);

				if (strengthA < strengthB)
				{
					return -1;
				}

				if (strengthA > strengthB)
				{
					return 1;
				}
			}

			return 0;
		}
	}

	private static HandType GetHandType(string cards, bool part2 = false)
	{
		var cardCount = cards
			.GroupBy(c => c)
			.ToDictionary(g => g.Key, g => g.Count());

		var type = cardCount.Count switch
		{
			1 => HandType.FiveOfAKind,
			2 when cardCount.Values.Any(c => c == 4) => HandType.FourOfAKind,
			2 => HandType.FullHouse,
			3 when cardCount.Values.Any(c => c == 3) => HandType.ThreeOfAKind,
			3 => HandType.TwoPair,
			4 => HandType.OnePair,
			_ => HandType.HighCard
		};

		var jokerCount = cards.Count(c => c == 'J');
		if (jokerCount > 0 && part2)
		{
			type = type switch
			{
				HandType.FourOfAKind or HandType.FullHouse => HandType.FiveOfAKind,
				HandType.ThreeOfAKind => HandType.FourOfAKind,
				HandType.TwoPair when jokerCount == 2 => HandType.FourOfAKind,
				HandType.TwoPair => HandType.FullHouse,
				HandType.OnePair => HandType.ThreeOfAKind,
				HandType.HighCard => HandType.OnePair,
				_ => type
			};
		}

		return type;
	}

	private static void PrintSortedHands(string[][] sorted)
	{
		Console.WriteLine();
		foreach (var strings in sorted)
		{
			Console.WriteLine(strings[0] + " " + strings[1] + " " + GetHandType(strings[0], true));
		}
		Console.WriteLine();
	}
}