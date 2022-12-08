using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2022.Day03;

public class DaySolver
{
	private const string Day = "Day03";
	private readonly ITestOutputHelper _output;

	public DaySolver(ITestOutputHelper output)
	{
		_output = output;
	}
	
	[Fact]
	public void Puzzle01()
	{
		_output.ShowResult(Day, SolvePuzzle01, 157);
	}
	
	public int SolvePuzzle01(string input)
	{
		return Utils.GetLines("Day03", input)
			.Select(x => Split(x))
			.Select(x => FindDuplicateLetter(x[0], x[1]))
			.Sum(LetterValue);
	}

	private string[] Split(string line)
	{
		var nbrChar = line.Length / 2;
		return new string[]
		{
			line.Substring(0, nbrChar),
			line.Substring(nbrChar, nbrChar),
		};
	}

	public char FindDuplicateLetter(string val1, string val2)
	{
		foreach (var c in val1)
		{
			if (val2.Contains(c))
				return c;
		}

		throw new Exception("No matching letter");
	}

	private int LetterValue(char c)
	{
		var intVal = (int)c; 
		return intVal >= 97 ? intVal - 96 : intVal - 38;
	}
	
	[Fact]
	public void Puzzle02()
	{
		_output.ShowResult(Day, SolvePuzzle02, 70);
	}

	public int SolvePuzzle02(string input)
	{
		return Utils.GetLines("Day03", input)
			.Chunk(3)
			.Select(x => FindDuplicateLetter(x[0], x[1], x[2]))
			.Sum(LetterValue);
	}
	
	public char FindDuplicateLetter(string val1, string val2, string val3)
	{
		foreach (var c in val1)
		{
			if (val2.Contains(c) && val3.Contains(c))
			{
				return c;
			}
		}

		throw new Exception("No matching letter");
	}
	

	[Fact]
	public void SplitTest()
	{
		Split("abcdef").Should().ContainInOrder("abc", "def");
	}

	[Fact]
	public void FindDuplicateLetterTest()
	{
		FindDuplicateLetter("abcd", "efch").Should().Be('c');
	}
	
	[Fact]
	public void LetterValueTest()
	{
		LetterValue('a').Should().Be(1);
		LetterValue('z').Should().Be(26);
		LetterValue('A').Should().Be(27);
		LetterValue('Z').Should().Be(52);
	}

}