using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2022.Day08;

public class DaySolver
{
	private const string Day = "Day08";
	private readonly ITestOutputHelper _output;

	public DaySolver(ITestOutputHelper output)
	{
		_output = output;
	}

	[Fact]
	public void Solve01()
	{
		_output.ShowResult(Day, SolvePuzzle01, 21);
	}

	public int SolvePuzzle01(string input)
	{
		var forest = Utils.GetLines(Day, input)
			.Select(line => line.Select(c => Convert.ToInt32(c)).ToArray()).ToArray();

		var maxFromTop = GetArrayWithMaxFromTop(forest);
		var maxFromBottom = GetArrayWithMaxFromBottom(forest);
		var maxFromLeft = GetArrayWithMaxFromLeft(forest);
		var maxFromRight = GetArrayWithMaxFromRight(forest);

		var sum = 0;
		for (var x = 0; x < forest.Length; x++)
		{
			for (var y = 0; y < forest[x].Length; y++)
			{
				if (x == 0 ||
				    y == 0 ||
				    x == forest.Length - 1 ||
				    y == forest[x].Length - 1 ||
				    forest[x][y] > maxFromTop[x - 1, y] ||
				    forest[x][y] > maxFromBottom[x + 1, y] ||
				    forest[x][y] > maxFromLeft[x, y - 1] ||
				    forest[x][y] > maxFromRight[x, y + 1]
				   )
				{
					sum++;
				}
			}
		}

		return sum;

	}

	private int[,] GetArrayWithMaxFromTop(int[][] forest)
	{
		var maxFromTop = new int[forest.Length, forest[0].Length];
		for (var x = 0; x < forest.Length; x++)
		{
			for (var y = 0; y < forest[0].Length; y++)
			{
				if (x == 0)
				{
					maxFromTop[x, y] = forest[x][y];
				}
				else
				{
					maxFromTop[x, y] = Math.Max(forest[x][y], maxFromTop[x - 1, y]);
				}
			}
		}

		return maxFromTop;
	}


	private int[,] GetArrayWithMaxFromBottom(int[][] forest)
	{
		var maxFromTop = new int[forest.Length, forest[0].Length];
		for (var x = forest.Length - 1; x >= 0; x--)
		{
			for (var y = 0; y < forest[0].Length; y++)
			{
				if (x == forest.Length - 1)
				{
					maxFromTop[x, y] = forest[x][y];
				}
				else
				{
					maxFromTop[x, y] = Math.Max(forest[x][y], maxFromTop[x + 1, y]);
				}
			}
		}

		return maxFromTop;
	}

	private int[,] GetArrayWithMaxFromLeft(int[][] forest)
	{
		var maxFromTop = new int[forest.Length, forest[0].Length];
		for (var y = 0; y < forest[0].Length; y++)
		{
			for (var x = 0; x < forest.Length; x++)
			{
				if (y == 0)
				{
					maxFromTop[x, y] = forest[x][y];
				}
				else
				{
					maxFromTop[x, y] = Math.Max(forest[x][y], maxFromTop[x, y - 1]);
				}
			}
		}

		return maxFromTop;
	}

	private int[,] GetArrayWithMaxFromRight(int[][] forest)
	{
		var maxFromTop = new int[forest.Length, forest[0].Length];
		for (var y = forest[0].Length - 1; y >= 0; y--)
		{
			for (var x = 0; x < forest.Length; x++)
			{
				if (y == forest[0].Length - 1)
				{
					maxFromTop[x, y] = forest[x][y];
				}
				else
				{
					maxFromTop[x, y] = Math.Max(forest[x][y], maxFromTop[x, y + 1]);
				}
			}
		}

		return maxFromTop;
	}

	[Fact]
	public void CanGetMaxFromTop()
	{
		var result = GetArrayWithMaxFromTop(new[]
		{
			new[] { 1, 2, 3, 4 },
			new[] { 2, 1, 5, 1 },
			new[] { 1, 1, 3, 1 },
			new[] { 4, 1, 1, 1 },
		});

		result.Should().BeEquivalentTo(new[,]
		{
			{ 1, 2, 3, 4 },
			{ 2, 2, 5, 4 },
			{ 2, 2, 5, 4 },
			{ 4, 2, 5, 4 }
		}, options => options.WithStrictOrdering());
	}

	[Fact]
	public void CanGetMaxFromBottom()
	{
		var result = GetArrayWithMaxFromBottom(new[]
		{
			new[] { 1, 2, 3, 4 },
			new[] { 2, 1, 5, 1 },
			new[] { 1, 1, 3, 1 },
			new[] { 4, 1, 1, 1 },
		});

		result.Should().BeEquivalentTo(new[,]
		{
			{ 4, 2, 5, 4 },
			{ 4, 1, 5, 1 },
			{ 4, 1, 3, 1 },
			{ 4, 1, 1, 1 }
		}, options => options.WithStrictOrdering());
	}

	[Fact]
	public void CanGetMaxFromLeft()
	{
		var result = GetArrayWithMaxFromLeft(new[]
		{
			new[] { 1, 2, 3, 4 },
			new[] { 2, 1, 5, 1 },
			new[] { 1, 1, 3, 1 },
			new[] { 4, 1, 1, 1 },
		});

		result.Should().BeEquivalentTo(new[,]
		{
			{ 1, 2, 3, 4 },
			{ 2, 2, 5, 5 },
			{ 1, 1, 3, 3 },
			{ 4, 4, 4, 4 }
		}, options => options.WithStrictOrdering());
	}

	[Fact]
	public void CanGetMaxFromRight()
	{
		var result = GetArrayWithMaxFromRight(new[]
		{
			new[] { 1, 2, 3, 4 },
			new[] { 2, 1, 5, 1 },
			new[] { 1, 1, 3, 1 },
			new[] { 4, 1, 1, 1 },
		});

		result.Should().BeEquivalentTo(new[,]
		{
			{ 4, 4, 4, 4 },
			{ 5, 5, 5, 1 },
			{ 3, 3, 3, 1 },
			{ 4, 1, 1, 1 }
		}, options => options.WithStrictOrdering());
	}

	[Fact]
	public void Solve02()
	{
		_output.ShowResult(Day, SolvePuzzle02, 8);
	}

	public int SolvePuzzle02(string input)
	{
		var forest = Utils.GetLines(Day, input)
			.Select(line => line.Select(c => Convert.ToInt32(c)).ToArray()).ToArray();

		var bestScore = 0;
		for (int x = 0; x < forest.Length; x++)
		{
			for (int y = 0; y < forest[x].Length; y++)
			{
				var curScore = CalculateScenicScore(x, y, forest);
				if (curScore > bestScore)
				{
					bestScore = curScore;
				}
			}
		}

		return bestScore;
	}

	private int CalculateScenicScore(int treeX, int treeY, int[][] forest)
	{
		return CalculateTopScenicScore(treeX, treeY, forest) *
		       CalculateBottomScenicScore(treeX, treeY, forest) *
		       CalculateLeftScenicScore(treeX, treeY, forest) *
		       CalculateRightScenicScore(treeX, treeY, forest);
	}

	private int CalculateTopScenicScore(int treeX, int treeY, int[][] forest)
	{
		var topCount = 0;
		if (treeX > 0)
		{
			do
			{
				topCount++;
			} while (treeX - topCount > 0 && forest[treeX - topCount][treeY] < forest[treeX][treeY]);
		}

		return topCount;
	}
	
	private int CalculateBottomScenicScore(int treeX, int treeY, int[][] forest)
	{
		var bottomCount = 0;
		if (treeX < (forest.Length - 1))
		{
			do
			{
				bottomCount++;
			} while (treeX + bottomCount < forest.Length - 1 && forest[treeX + bottomCount][treeY] < forest[treeX][treeY]);
		}

		return bottomCount;
	}
	
	private int CalculateLeftScenicScore(int treeX, int treeY, int[][] forest)
	{
		var leftScore = 0;
		if (treeY > 0)
		{
			do
			{
				leftScore++;
			} while (treeY - leftScore > 0 && forest[treeX][treeY - leftScore] < forest[treeX][treeY]);
		}

		return leftScore;
	}
	
	private int CalculateRightScenicScore(int treeX, int treeY, int[][] forest)
	{
		var rightScore = 0;
		if (treeY < forest[0].Length - 1)
		{
			do
			{
				rightScore++;
			} while (treeY + rightScore < forest[0].Length - 1 && forest[treeX][treeY + rightScore] < forest[treeX][treeY]);
		}

		return rightScore;
	}

	[Fact]
	public void CanCalculateScenicTopScore()
	{
		var forest = new[]
		{
			new[] { 1, 2, 3, 4, 1 },
			new[] { 2, 1, 5, 1, 1 },
			new[] { 1, 1, 1, 1, 1 },
			new[] { 4, 1, 3, 1, 4 },
		};

		CalculateTopScenicScore(0, 0, forest).Should().Be(0);
		CalculateTopScenicScore(2, 0, forest).Should().Be(1);
		CalculateTopScenicScore(3, 4, forest).Should().Be(3);
		CalculateTopScenicScore(1, 1, forest).Should().Be(1);
		CalculateTopScenicScore(3, 2, forest).Should().Be(2);
		CalculateTopScenicScore(2, 4, forest).Should().Be(1);
	}
	
	[Fact]
	public void CanCalculateScenicBottomScore()
	{
		var forest = new[]
		{
			new[] { 1, 2, 3, 4, 1 },
			new[] { 2, 1, 5, 1, 1 },
			new[] { 1, 1, 1, 1, 1 },
			new[] { 4, 1, 3, 1, 4 },
		};

		CalculateBottomScenicScore(0, 0, forest).Should().Be(1);
		CalculateBottomScenicScore(1, 0, forest).Should().Be(2);
		CalculateBottomScenicScore(0, 3, forest).Should().Be(3);
		CalculateBottomScenicScore(1, 2, forest).Should().Be(2);
		CalculateBottomScenicScore(2, 2, forest).Should().Be(1);
	}
	
	[Fact]
	public void CanCalculateScenicLeftScore()
	{
		var forest = new[]
		{
			new[] { 1, 2, 3, 4, 1 },
			new[] { 2, 1, 5, 1, 1 },
			new[] { 1, 1, 1, 1, 1 },
			new[] { 4, 1, 3, 1, 4 },
		};

		CalculateLeftScenicScore(0, 0, forest).Should().Be(0);
		CalculateLeftScenicScore(0, 1, forest).Should().Be(1);
		CalculateLeftScenicScore(0, 3, forest).Should().Be(3);
		CalculateLeftScenicScore(3, 2, forest).Should().Be(2);
		CalculateLeftScenicScore(3, 4, forest).Should().Be(4);
	}
	
	[Fact]
	public void CanCalculateScenicRightScore()
	{
		var forest = new[]
		{
			new[] { 1, 2, 3, 4, 1 },
			new[] { 2, 1, 5, 1, 1 },
			new[] { 2, 1, 1, 1, 1 },
			new[] { 4, 1, 3, 1, 4 },
		};

		CalculateRightScenicScore(0, 4, forest).Should().Be(0);
		CalculateRightScenicScore(1, 0, forest).Should().Be(2);
		CalculateRightScenicScore(1, 2, forest).Should().Be(2);
		CalculateRightScenicScore(2, 0, forest).Should().Be(4);
		CalculateRightScenicScore(2, 1, forest).Should().Be(1);
		CalculateRightScenicScore(3, 0, forest).Should().Be(4);
	}

}