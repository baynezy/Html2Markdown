using System.Threading.Tasks;
using NUnit.Framework;
using VerifyNUnit;

namespace Html2Markdown.Test;

[TestFixture]
public class ConvertFromFileTest {
	private string _testPath;

	[SetUp]
	public void SetUp() {
		_testPath = TestPath();
	}

	[Test]
	public Task ConvertFile_WhenReadingInHtmlFile_ThenConvertToMarkdown()
	{
		var sourcePath = _testPath + "TestHtml.txt";

		return CheckFileConversion(sourcePath);
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