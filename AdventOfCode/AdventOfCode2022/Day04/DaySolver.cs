using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2022.Day05;

public class DaySolver
{
	private const string Day = "Day04";
	private readonly ITestOutputHelper _output;

	public DaySolver(ITestOutputHelper output)
	{
		_output = output;
	}
	
	[Fact]
	public void Puzzle01Test()
	{
		_output.ShowResult(Day, SolvePuzzle01, 2);
	}
	
	public int SolvePuzzle01(string input)
	{
		return Utils.GetLines("Day05", input)
			.Select(x => x.Split(','))
			.Select(x => (x[0].Split('-') ,x[1].Split('-')))
			.Select(x => (
				(Min: Convert.ToInt32(x.Item1[0]), Max: Convert.ToInt32(x.Item1[1])),
				(Min: Convert.ToInt32(x.Item2[0]), Max: Convert.ToInt32(x.Item2[1]))
			))
			.Count(x => (x.Item1.Min <= x.Item2.Min && x.Item1.Max >= x.Item2.Max)
			 || (x.Item2.Min <= x.Item1.Min && x.Item2.Max >= x.Item1.Max)
			);
	}

	
	/*
	[Fact]
	public void Puzzle02Test()
	{
		var testResult = SolvePuzzle02("TestInput");
		_output.WriteLine("Test result : ");
		_output.WriteLine(testResult.ToString());
			
		testResult.Should().Be(4);
		
		_output.WriteLine("Result : ");
		var result = SolvePuzzle02("Input");
		_output.WriteLine(result.ToString());
	}
	
	public int SolvePuzzle02(string input)
	{
		return Utils.GetLines("Day04", input)
			.Select(x => x.Split(','))
			.Select(x => (x[0].Split('-') ,x[1].Split('-')))
			.Select(x => (
				(Min: Convert.ToInt32(x.Item1[0]), Max: Convert.ToInt32(x.Item1[1])),
				(Min: Convert.ToInt32(x.Item2[0]), Max: Convert.ToInt32(x.Item2[1]))
			))
			.Count(x => (x.Item1.Min <= x.Item2.Max && x.Item1.Max >= x.Item2.Min)
			);
	}
*/
}