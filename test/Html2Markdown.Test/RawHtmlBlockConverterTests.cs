namespace Html2Markdown.Test;

public class RawHtmlBlockConverterTests
{
    [Theory]
    [InlineData("<div><strong>bold</strong></div>", "<div><strong>bold</strong></div>")]
    [InlineData("<table><tr><td><em>emphasis</em></td></tr></table>",
        "<table><tbody><tr><td><em>emphasis</em></td></tr></tbody></table>")]
    [InlineData("<iframe src=\"https://example.test\"><b>fallback</b></iframe>",
        "<iframe src=\"https://example.test\"><b>fallback</b></iframe>")]
    [InlineData("<canvas data-test=\"value\">fallback</canvas>", "<canvas data-test=\"value\">fallback</canvas>")]
    public void Convert_WhenSpecifiedRawHtmlBlockContainsConvertibleChildren_ThenPreservesOuterHtml(string html,
        string expected)
    {
        // arrange
        Converter converter = new();

        // act
        var markdown = converter.Convert(html);

        // assert
        markdown.Should()
            .Be(expected);
    }

    [Fact]
    public void Convert_WhenUnknownInlineElementContainsConvertibleChildren_ThenTraversesItsChildren()
    {
        // arrange
        Converter converter = new();

        // act
        var markdown = converter.Convert("<span><strong>bold</strong></span>");

        // assert
        markdown.Should()
            .Be("**bold**");
    }
}