using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2022.Day02;

public class DaySolver
{
	private const string Day = "Day02";
	private readonly ITestOutputHelper _output;

	public DaySolver(ITestOutputHelper output)
	{
		_output = output;
	}
	
	[Fact]
	public void Puzzle01()
	{
		_output.ShowResult(Day, SolvePuzzle01, 15);
	}

	public int SolvePuzzle01(string input)
	{
		Dictionary<string, int> values = new Dictionary<string, int>()
		{
			{ "A X", 3 + 1 },
			{ "A Y", 6 + 2 },
			{ "A Z", 0 + 3 },
			{ "B X", 0 + 1 },
			{ "B Y", 3 + 2 },
			{ "B Z", 6 + 3 },
			{ "C X", 6 + 1 },
			{ "C Y", 0 + 2 },
			{ "C Z", 3 + 3 }
		};
		return Utils.GetLines("Day02", input)
			.Select(x => values[x])
			.Sum();
	}
	
	[Fact]
	public void Puzzle02()
	{
		_output.ShowResult(Day, SolvePuzzle02, 12);
	}

	public int SolvePuzzle02(string input)
	{
		Dictionary<string, int> values = new Dictionary<string, int>()
		{
			{ "A X", 0 + 3 },
			{ "A Y", 3 + 1 },
			{ "A Z", 6 + 2 },
			{ "B X", 0 + 1 },
			{ "B Y", 3 + 2 },
			{ "B Z", 6 + 3 },
			{ "C X", 0 + 2 },
			{ "C Y", 3 + 3 },
			{ "C Z", 6 + 1 }
		};
		return Utils.GetLines("Day02", input)
			.Select(x => values[x])
			.Sum();
	}
}