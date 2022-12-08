using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2022.Day01;

public class DayOneSolver
{
	private const string Day = "Day01";
	private readonly ITestOutputHelper _output;

	public DayOneSolver(ITestOutputHelper output)
	{
		this._output = output;
	}
	
	[Fact]
	public void Puzzle01Test()
	{
		_output.ShowResult(Day, SolvePuzzle01, 24000);
	}

	public int SolvePuzzle01(string input)
	{
		return Utils.ReadText(Day, input)
			.Split("\r\n\r\n")
			.Select(x => x.Split("\n"))
			.Select(x => x.Select(y => Convert.ToInt32(y)))
			.Select(x => x.Sum())
			.Max();
	}
	
	[Fact]
	public void Puzzle02Test()
	{
		_output.ShowResult(Day, SolvePuzzle02, 45000);
	}
	
	[Fact]
	public void Puzzle02()
	{
		var result = SolvePuzzle02("Input");
		_output.WriteLine(result.ToString());
	}
	
	public int SolvePuzzle02(string input)
	{
		return Utils.ReadText(Day, input)
			.Split("\r\n\r\n")
			.Select(x => x.Split("\n"))
			.Select(x => x.Select(y => Convert.ToInt32(y)))
			.Select(x => x.Sum())
			.OrderByDescending(x => x)
			.Take(3)
			.Sum();
	}
}