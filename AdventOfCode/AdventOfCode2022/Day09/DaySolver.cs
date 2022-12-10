using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2022.Day09;

public class DaySolver
{
	private const string Day = "Day09";
	private readonly ITestOutputHelper _output;

	public DaySolver(ITestOutputHelper output)
	{
		_output = output;
	}

	[Fact]
	public void Solve01()
	{
		_output.ShowResult(Day, SolvePuzzle01, 13);
	}

	public int SolvePuzzle01(string input)
	{
		var moves = Utils.GetLines(Day, input)
			.Select(l => l.Split(' '));

		var moveFunc = new Dictionary<string, Func<Tuple<int, int>, Tuple<int, int>>>()
		{

		};
		var visited = new List<string>() { "0,0" };
		var tail = new Position();
		var head = new Position();

		foreach (var move in moves)
		{
			for (var i = 0; i < Convert.ToInt32(move[1]); i++)
			{
				tail = head.MoveAnFollow(move[0], tail);
				if (!visited.Contains($"{tail.X},{tail.Y}"))
				{
					visited.Add($"{tail.X},{tail.Y}");
				}
			}
		}

		return visited.Count;
	}

	[Fact]
	public void Solve02()
	{
		_output.ShowResult(Day, SolvePuzzle02, 36, "TestInput2");
	}

	public int SolvePuzzle02(string input)
	{
		var moves = Utils.GetLines(Day, input)
			.Select(l => l.Split(' '));

		var visited = new List<string>() { "0,0" };
		var rope = new Position[10];
		Array.ForEach(rope, position => position = new Position());
	
		foreach (var move in moves)
		{
			for (var i = 0; i < Convert.ToInt32(move[1]); i++)
			{
				rope[0].Move(move[0]);
				
				for (var ropeIndex = 1; ropeIndex < rope.Length; ropeIndex++)
				{
					rope[ropeIndex].Follow(rope[ropeIndex - 1]);
				}

				var tail = rope.Last();
				if (!visited.Contains($"{tail.X},{tail.Y}"))
				{
					visited.Add($"{tail.X},{tail.Y}");
				}
			}
		}

		return visited.Count;
	}

	public struct Position
	{
		public int X { get; set; }
		public int Y { get; set; }
		
		public Position MoveTo(int x, int y)
		{
			X = x;
			Y = y;
			return this;
		}

		public Position MoveAnFollow(string dir, Position tail)
		{
			switch (dir)
			{
				case "U":
					Y++;
					return Math.Abs(Y - tail.Y) > 1 ? tail.MoveTo(X, Y - 1) : tail;
				case "D":
					Y--;
					return Math.Abs(Y - tail.Y) > 1 ? tail.MoveTo(X, Y + 1) : tail;
				case "L":
					X--;
					return Math.Abs(X - tail.X) > 1 ? tail.MoveTo(X + 1, Y) : tail;
				case "R":
					X++;
					return Math.Abs(X - tail.X) > 1 ? tail.MoveTo(X - 1, Y) : tail;
				default:
					throw new Exception($"{dir} ???");
			}
		}
		
		public void Move(string dir)
		{
			switch (dir)
			{
				case "U":
					Y++;
					break;
				case "D":
					Y--;
					break;
				case "L":
					X--;
					break;
				case "R":
					X++;
					break;
				default:
					throw new Exception($"{dir} ???");
			}
		}

		private int DistanceWith(Position other)
		{
			return Math.Max(Math.Abs(Y - other.Y), Math.Abs(X - other.X));
		}

		public void Follow(Position head)
		{
			var diffY = head.Y - Y;
			var diffX = head.X - X;

			if ((diffY == 2 && diffX >= 1) || (diffY >= 1 && diffX == 2))
			{
				Y++;
				X++;
			}
			if ((diffY == 2 && diffX <= -1) || (diffY >= 1 && diffX == -2))
			{
				Y++;
				X--;
			}
			if ((diffY == -2 && diffX >= 1) || (diffY <= -1 && diffX == 2))
			{
				Y--;
				X++;
			}
			if ((diffY == -2 && diffX <= -1) || (diffY <= -1 && diffX == -2))
			{
				Y--;
				X--;
			}
			else if (diffY == 2 && diffX == 0)
			{
				Y++;
			}
			else if (diffY == -2 && diffX == 0)
			{
				Y--;
			}
			else if (diffY == 0 && diffX == 2)
			{
				X++;
			}
			else if (diffY == 0 && diffX == -2)
			{
				X--;
			}
		}

		public override string ToString()
		{
			return $"{X},{Y}";
		}
	}
}