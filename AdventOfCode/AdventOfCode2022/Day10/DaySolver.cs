using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2022.Day10;

public class DaySolver
{
	private const string Day = "Day10";
	private readonly ITestOutputHelper _output;

	public DaySolver(ITestOutputHelper output)
	{
		_output = output;
	}

	[Fact]
	public void Solve01()
	{
		_output.ShowResult(Day, SolvePuzzle01, 13140);
	}

	public int SolvePuzzle01(string input)
	{
		var cycles = GetCycles(input);

		return ValueCycle(cycles,20) 
		       + ValueCycle(cycles,60) 
		       + ValueCycle(cycles,100) 
		       + ValueCycle(cycles,140) 
		       + ValueCycle(cycles,180) 
		       + ValueCycle(cycles,220);
	}

	private static List<int> GetCycles(string input)
	{
		var cycles = new List<int>();
		var sum = 1;
		var commands = Utils.GetLines(Day, input).Select(x => x.Split());

		foreach (var cmd in commands)
		{
			cycles.Add(sum);
			switch (cmd[0])
			{
				case "noop":
					break;
				case "addx":
					cycles.Add(sum);
					sum += Convert.ToInt32(cmd[1]);
					break;
				default:
					throw new Exception($"{cmd[0]} ???");
			}
		}

		return cycles;
	}

	private int ValueCycle(List<int> cycles, int rank)
	{
		return cycles[rank - 1] * rank;
	}

	[Fact]
	public void Solve02()
	{
		
		var testResult = SolvePuzzle02("TestInput");
		_output.WriteLine("Test result : ");
		_output.WriteLine(testResult.ToString());
			
		testResult.Should().BeEquivalentTo(new []{
			"##..##..##..##..##..##..##..##..##..##..",
			"###...###...###...###...###...###...###.",
			"####....####....####....####....####....",
			"#####.....#####.....#####.....#####.....",
			"######......######......######......####",
			"#######.......#######.......#######....."}, opt => opt.WithStrictOrdering());
		
		_output.WriteLine("Result : ");
		var result = SolvePuzzle02("Input");
		_output.WriteLine(result.ToString());
		//ZKJFBJFZ
		result.ToList().ForEach(_output.WriteLine);
	}

	public string[] SolvePuzzle02(string input)
	{
		var result = new string[6];
		var cycles = GetCycles(input);
		for (int i = 0; i < 6; i++)
		{
			for (int j = 0; j < 40; j++)
			{
				var cycleIndex = (i*40) + j;
				if (j >= cycles[cycleIndex] - 1 && j <= cycles[cycleIndex] + 1)
				{
					result[i] += "#";
				}
				else
				{
					result[i] += ".";
				}
			}
		}

		return result;
	}
}