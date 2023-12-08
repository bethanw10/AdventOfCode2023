namespace Day08;

internal class Day04
{
	internal record Node(string Left, string Right);

	private static void Main()
	{
		var input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

		var lines = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

		var instructions = lines[0];

		var nodes = lines[1..]
			.Select(l => l.Split(" = "))
			.Select(l => new
			{
				name = l.First(),
				nodes = l.Last().Replace("(", "").Replace(")", "").Split(", ")
			})
			.ToDictionary(l => l.name, l => new Node(l.nodes.First(), l.nodes.Last()));

		Console.WriteLine($"Part 1: {Part1(instructions, nodes)}");
		Console.WriteLine($"Part 2: {Part2(instructions, nodes)}");
	}

	private static int Part1(string instructions, Dictionary<string, Node> nodes)
	{
		return GetTotalSteps("AAA", instructions, nodes);
	}

	private static long Part2(string instructions, Dictionary<string, Node> nodes)
	{
		var currentNodes = nodes.Keys.Where(n => n.Last() == 'A').ToList();

		var totalSteps = currentNodes
			.Select(c => GetTotalSteps(c, instructions, nodes))
			.Select(t => (long)t);

		return totalSteps.Aggregate(LCM);
	}

	private static int GetTotalSteps(string startNode, string instructions, Dictionary<string, Node> nodes)
	{
		var currentNode = startNode;
		var stepEnumerator = instructions.GetEnumerator();
		var totalSteps = 0;

		while (currentNode.Last() != 'Z')
		{
			if (!stepEnumerator.MoveNext())
			{
				stepEnumerator.Reset();
				stepEnumerator.MoveNext();
			}

			var step = stepEnumerator.Current;

			var nextNodes = nodes[currentNode];

			currentNode = step == 'L' ? nextNodes.Left : nextNodes.Right;

			totalSteps++;
		}

		return totalSteps;
	}

	private static long GCF(long a, long b)
	{
		while (b != 0)
		{
			var temp = b;
			b = a % b;
			a = temp;
		}
		return a;
	}

	private static long LCM(long a, long b)
	{
		return (a / GCF(a, b)) * b;
	}
}