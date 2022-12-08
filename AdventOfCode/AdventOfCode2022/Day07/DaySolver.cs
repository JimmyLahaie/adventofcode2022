using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2022.Day07;

public class DaySolver
{
	private const string Day = "Day07";
	private readonly ITestOutputHelper _output;

	public DaySolver(ITestOutputHelper output)
	{
		_output = output;
	}
	
	[Fact]
	public void Solve01()
	{
		_output.ShowResult(Day, SolvePuzzle01, 95437);
	}

	public int SolvePuzzle01(string input)
	{
		var sizes = GetDirectorySizes(input);

		return sizes.Where(x => x.Value <= 100000).Sum(x => x.Value);
	}
	
	[Fact]
	public void Solve02()
	{
		_output.ShowResult(Day, SolvePuzzle02, 24933642);
	}

	public int SolvePuzzle02(string input)
	{
		var sizes = GetDirectorySizes(input);

		var freeSpace = 70000000 - sizes["/|"];
		var requiredSpace = 30000000 - freeSpace;
		
		return sizes.Where(x => x.Value >= requiredSpace)
			.Select(x => x.Value)
			.Min();
	}

	private Dictionary<string, int> GetDirectorySizes(string input)
	{
		var data = Utils.GetLines(Day, input);

		var curDir = new List<string>();
		var sizes = new Dictionary<string, int>();
		foreach (var line in data)
		{
			var lineInfo = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			if (lineInfo[0] == "$")
			{
				if (lineInfo[1] == "cd")
				{
					switch (lineInfo[2])
					{
						case "/":
							curDir = new List<string>() { "/" };
							break;
						case "..":
							curDir = curDir.Take(curDir.Count - 1).ToList();
							break;
						default:
							curDir.Add(lineInfo[2]);
							break;
					}
				}
				else if (lineInfo[1] == "ls")
				{
					//do nothing... don't care
				}
			}
			else
			{
				if (lineInfo[0] == "dir")
				{
					//do nothing... don't care
				}
				else
				{
					//it's a file
					AddSizeToThree(sizes, curDir, Convert.ToInt32(lineInfo[0]));
				}
			}
		}

		return sizes;
	}

	private void AddSizeToThree(Dictionary<string,int> sizes, List<string> curDir, int fileSize)
	{
		var dirName = "";
		foreach (var dir in curDir)
		{
			dirName += dir + "|";
			if (!sizes.ContainsKey(dirName))
			{
				sizes.Add(dirName, 0);
			}

			sizes[dirName] = sizes[dirName] + fileSize;
		}
	}
}