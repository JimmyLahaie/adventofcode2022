using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2022.Day11;

public class DaySolver
{
	private const string Day = "Day11";
	private readonly ITestOutputHelper _output;

	public DaySolver(ITestOutputHelper output)
	{
		_output = output;
	}

	[Fact]
	public void Solve01()
	{
		_output.ShowResult(Day, SolvePuzzle01, 10605);
	}

	public long SolvePuzzle01(string input)
	{
		var monkeys = Utils.ReadText(Day, input)
			.Split("\r\n\r\n")
			.Select(x => x
				.Split("\r\n")
				.Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries)).ToArray())
			.Select(x => new Money
			{
				Items = x[1].Skip(2).Select(y => Convert.ToInt64(y.Replace(",", ""))).ToList(),
				OperationVal1 = x[2][3], 
				Operation = x[2][4],
				OperationVal2 = x[2][5],
				TestDivisible = Convert.ToInt32(x[3][3]),
				TestTrue =  Convert.ToInt32(x[4][5]),
				TestFalse =  Convert.ToInt32(x[5][5]),
			}).ToList();

		for (var round = 0; round < 20; round++)
		{
			foreach (var monkey in monkeys)
			{
				foreach (var item in monkey.Items)
				{
					var itemWorryLevel = monkey.ApplyOperation(item) / 3;
					//var toAdd = itemWorryLevel / 3;

					var toThrowAt = itemWorryLevel % monkey.TestDivisible == 0 ? monkey.TestTrue : monkey.TestFalse;
					monkeys[toThrowAt].Items.Add(itemWorryLevel);
				}
				monkey.ItemsChecked += monkey.Items.Count;
				monkey.Items.Clear();
			}
		}

		var toMultiply = monkeys.Select(x => x.ItemsChecked).OrderByDescending(x => x).Take(2).ToArray();
		return toMultiply[0] * toMultiply[1];
	}

	[Fact]
	public void Solve02()
	{
		_output.ShowResult(Day, SolvePuzzle02, 2713310158, "TestInput");
	}

	public long SolvePuzzle02(string input)
	{
		var monkeys = Utils.ReadText(Day, input)
			.Split("\r\n\r\n")
			.Select(x => x
				.Split("\r\n")
				.Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries)).ToArray())
			.Select(x => new Money
			{
				Items = x[1].Skip(2).Select(y => Convert.ToInt64(y.Replace(",", ""))).ToList(),
				OperationVal1 = x[2][3], 
				Operation = x[2][4],
				OperationVal2 = x[2][5],
				TestDivisible = Convert.ToInt32(x[3][3]),
				TestTrue =  Convert.ToInt32(x[4][5]),
				TestFalse =  Convert.ToInt32(x[5][5]),
			}).ToList();



		var minVal = monkeys.Select(x => x.TestDivisible).Aggregate((long)1, (x, y) => x * y);
		
		for (var round = 0; round < 10000; round++)
		{
			foreach (var monkey in monkeys)
			{
				foreach (var item in monkey.Items)
				{
					var itemWorryLevel = monkey.ApplyOperation(item) % minVal;

					var toThrowAt = itemWorryLevel % monkey.TestDivisible == 0 ? monkey.TestTrue : monkey.TestFalse;
					
					monkeys[toThrowAt].Items.Add(itemWorryLevel);
				}
				monkey.ItemsChecked += monkey.Items.Count;
				monkey.Items.Clear();
			}
		}

		var toMultiply = monkeys.Select(x => x.ItemsChecked).OrderByDescending(x => x).Take(2).ToArray();
		return toMultiply[0] * toMultiply[1];
	}

	private class Money
	{
		public List<long> Items { get; set; }
		public string OperationVal1 { get; set; }
		public string Operation { get; set; }
		public string OperationVal2 { get; set; }
		public long TestDivisible { get; set; }
		public int TestTrue { get; set; }
		public int TestFalse { get; set; }

		public long ItemsChecked { get; set; }

		public long ApplyOperation(long oldValue)
		{
			
			var val1 = OperationVal1 == "old" ? oldValue : Convert.ToInt64(OperationVal1);
			var val2 = OperationVal2 == "old" ? oldValue : Convert.ToInt64(OperationVal2);
			
			return Operation == "+"
				? val1 + val2
				: val1 * val2;
		}
	}
}