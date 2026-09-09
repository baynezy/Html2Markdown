using System.IO;

namespace Html2Markdown.Test;

public class ConvertFromFileTest {
	private readonly string _testPath = TestPath();

	[Fact]
	public Task ConvertFile_WhenReadingInHtmlFile_ThenConvertToMarkdown()
	{
		var sourcePath = _testPath + "TestHtml.txt";

		return CheckFileConversion(sourcePath);
	}

	[Fact]
	public Task ConvertFile_WhenFileHasMixedLineEndings_ThenStandardiseThemBeforeConverting()
	{
		var sourcePath = _testPath + "TestHtmlMixedLineEndings.txt";

		return CheckFileConversion(sourcePath);
	}

	[Fact]
	public void ConvertFile_WhenFileEndsWithCarriageReturn_ThenStandardisesAndConvertsSuccessfully()
	{
		var tempPath = Path.GetTempFileName();
		try
		{
			File.WriteAllText(tempPath, "<strong>Hello</strong>\r");

			var converter = new Converter();
			var result = converter.ConvertFile(tempPath);

			result.Should().Be("**Hello**");
		}
		finally
		{
			if (File.Exists(tempPath))
			{
				File.Delete(tempPath);
			}
		}
	}

	private static string TestPath()
	{
		const string route = @"..\..\..\Files\";
		var environmentPath = System.Environment.GetEnvironmentVariable("TestPath");

		return environmentPath ?? route;
	}

	private static Task CheckFileConversion(string path)
	{
		var converter = new Converter();

		var result = converter.ConvertFile(path);

		return Verifier.Verify(result);
	}
}