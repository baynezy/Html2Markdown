using NUnit.Framework;

namespace Html2Markdown.Test {
	[TestFixture]
	public class ConvertFromFileTest {
		private string _testPath;

		[SetUp]
		public void SetUp() {
			_testPath = TestPath();
		}

		[Test]
		public void ConvertFile_WhenReadingInHtmlFile_ThenConvertToMarkdown()
		{
			var sourcePath = _testPath + "TestHtml.txt";
			const string expected = @"## Installing via NuGet

```
    Install-Package Html2Markdown
    ```

## Usage

```
    var converter = new Converter();
    var result = converter.Convert(html);
    ```";

			CheckFileConversion(sourcePath, expected);
		}

		private static string TestPath()
		{
			const string route = @"..\..\..\Files\";
			var environmentPath = System.Environment.GetEnvironmentVariable("Test.Path");

			return environmentPath ?? route;
		}

		private static void CheckFileConversion(string path, string expected)
		{
			var converter = new Converter();

			var result = converter.ConvertFile(path);

			Assert.That(result, Is.EqualTo(expected));
		}
	}
}