using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2022.Day04;

public class DaySolver
{
	private const string Day = "Day05";
	private readonly ITestOutputHelper _output;

	public DaySolver(ITestOutputHelper output)
	{
		_output = output;
	}

	[Fact]
	public void Puzzle01Test()
	{
		var testResult = SolvePuzzle01("TestInput", 3, 3);
		_output.WriteLine("Test result : ");
		_output.WriteLine(testResult);

		testResult.Should().Be("CMZ");

		_output.WriteLine("Result : ");
		var result = SolvePuzzle01("Input", 9, 8);
		_output.WriteLine(result.ToString());
	}

	public string SolvePuzzle01(string input, int nbrCols, int nbrLines)
	{
		var allData = Utils.GetLines("Day05", input);
		var allLines = GetLists(allData, nbrCols, nbrLines).ToList();
		for (var i = nbrLines + 2; i < allData.Length; i++)
		{
			var instructions = allData[i].Split(' ');
			var nbr = Convert.ToInt32(instructions[1]);
			var from = Convert.ToInt32(instructions[3]) - 1;
			var to = Convert.ToInt32(instructions[5]) - 1;

			var dataToMove = allLines[from].Take(nbr).Reverse().ToList();
			allLines[from] = allLines[from].Skip(nbr);
			dataToMove.AddRange(allLines[to]);
			allLines[to] = dataToMove;
		}
		return string.Join("", allLines.Select(x => x.First()));
	}

	private IEnumerable<IEnumerable<char>> GetLists(string[] lines, int nbrCols, int nbrLines)
	{
		for (var i = 0; i < nbrCols; i++)
		{
			yield return GetCharList(lines, ((i+1)*4) - 3, nbrLines);
		}
	}

	private IEnumerable<char> GetCharList(string[] lines, int pos, int nbrLines)
	{
		for (var i = 0; i < nbrLines; i++)
		{
			if (lines[i][pos] != ' ')
				yield return lines[i][pos];
		}
	}

	[Fact]
	public void CanGetLines()
	{
		var lists = GetLists(Utils.GetLines("Day05", "TestInput"), 3, 3).ToList();
		lists.Should().HaveCount(3);
		lists[0].Should().ContainInOrder('N', 'Z').And.HaveCount(2);
		lists[1].Should().ContainInOrder('D', 'C', 'M').And.HaveCount(3);
		lists[2].Should().ContainInOrder('P').And.HaveCount(1);
	}
	
	[Fact]
	public void CanGetAllLines()
	{
		var result =
		GetCharList(Utils.GetLines("Day05", "TestInput"), 5, 3)
			.Should().ContainInOrder('D', 'C', 'M');
		
		GetCharList(Utils.GetLines("Day05", "TestInput"), 1, 3)
			.Should().ContainInOrder('N', 'Z').And.HaveCount(2);
	}




	[Fact]
	public void Puzzle02Test()
	{
		var testResult = SolvePuzzle02("TestInput", 3, 3);
		_output.WriteLine("Test result : ");
		_output.WriteLine(testResult);

		testResult.Should().Be("MCD");

		_output.WriteLine("Result : ");
		var result = SolvePuzzle02("Input", 9, 8);
		_output.WriteLine(result.ToString());
	}
	
	public string SolvePuzzle02(string input, int nbrCols, int nbrLines)
	{
		var allData = Utils.GetLines("Day05", input);
		var allLines = GetLists(allData, nbrCols, nbrLines).ToList();
		for (var i = nbrLines + 2; i < allData.Length; i++)
		{
			var instructions = allData[i].Split(' ');
			var nbr = Convert.ToInt32(instructions[1]);
			var from = Convert.ToInt32(instructions[3]) - 1;
			var to = Convert.ToInt32(instructions[5]) - 1;

			var dataToMove = allLines[from].Take(nbr).ToList();
			allLines[from] = allLines[from].Skip(nbr);
			dataToMove.AddRange(allLines[to]);
			allLines[to] = dataToMove;
		}
		return string.Join("", allLines.Select(x => x.First()));
	}

}