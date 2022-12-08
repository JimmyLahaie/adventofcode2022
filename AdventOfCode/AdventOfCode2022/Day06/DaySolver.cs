using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2022.Day06;

public class DaySolver
{
	private const string Day = "Day06";
	private readonly ITestOutputHelper _output;

	public DaySolver(ITestOutputHelper output)
	{
		_output = output;
	}
	
	[Fact]
	public void Puzzle01Test()
	{
		_output.ShowResult(Day, SolvePuzzle01, 11);
	}
	
	public int SolvePuzzle01(string input)
	{
		var allText = Utils.ReadText("Day06", input);
		var word = allText.Substring(0, 4);

		for (var i = 4; i < allText.Length; i++)
		{
			if (word.Distinct().Count() == 4)
			{
				return i;
			}

			word = word.Substring(1, 3) + allText[i];
		}

		return -1;
	}

	
	
	[Fact]
	public void Puzzle02Test()
	{
		_output.ShowResult(Day, SolvePuzzle01, 26);
	}
	
	public int SolvePuzzle02(string input)
	{
		var allText = Utils.ReadText("Day06", input);
		var word = allText.Substring(0, 14);

		for (var i = 14; i < allText.Length; i++)
		{
			if (word.Distinct().Count() == 14)
			{
				return i;
			}

			word = word.Substring(1, 13) + allText[i];
		}

		return -1;
	}

}