using FluentAssertions;
using Xunit.Abstractions;

namespace AdventOfCode2022;

public static class Utils
{
	public static string ReadText(string day, string filename)
	{
		return System.IO.File.ReadAllText(
			@$"C:\Dev\Git\AdventOfCode\AdventOfCode\AdventOfCode2022\{day}\{filename}.txt");
	}

	public static string[] GetLines(string day, string filename)
	{
		return ReadText(day, filename).Split("\r\n");
	}

	public static void ShowResult<T>(this ITestOutputHelper output, string day, Func<string ,T> solve, T expectedResult, string testInput = "TestInput")
	{
			var testResult = solve(testInput);
			output.WriteLine("Test result : ");
			output.WriteLine(testResult.ToString());
			
			testResult.Should().Be(expectedResult);
		
			output.WriteLine("Result : ");
			var result = solve("Input");
			output.WriteLine(result.ToString());
	}
}