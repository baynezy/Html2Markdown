using System;
using Html2Markdown.Renderers;

namespace Html2Markdown.Test.Renderers;

public class MarkdownFormattingTests
{
    [Fact]
    public void Wrap_WhenTheContentIsEmpty_ThenReturnsAnEmptyString()
    {
        // arrange
        var content = string.Empty;

        // act
        var result = MarkdownFormatting.Wrap(content, "**");

        // assert
        result.Should()
            .BeEmpty();
    }

    [Fact]
    public void Wrap_WhenTheContentHasNoSurroundingWhitespace_ThenWrapsTheContentWithTheMarker()
    {
        // arrange
        const string content = "bold";

        // act
        var result = MarkdownFormatting.Wrap(content, "**");

        // assert
        result.Should()
            .Be("**bold**");
    }

    [Fact]
    public void Wrap_WhenTheContentHasLeadingWhitespace_ThenMovesASingleSpaceOutsideTheMarker()
    {
        // arrange
        const string content = "  bold";

        // act
        var result = MarkdownFormatting.Wrap(content, "*");

        // assert
        result.Should()
            .Be(" *bold*");
    }

    [Fact]
    public void Wrap_WhenTheContentHasTrailingWhitespace_ThenMovesASingleSpaceOutsideTheMarker()
    {
        // arrange
        const string content = "bold  ";

        // act
        var result = MarkdownFormatting.Wrap(content, "*");

        // assert
        result.Should()
            .Be("*bold* ");
    }

    [Fact]
    public void Wrap_WhenTheContentHasWhitespaceAtBothEnds_ThenMovesASingleSpaceToEachSideOfTheMarker()
    {
        // arrange
        const string content = "\tbold\n";

        // act
        var result = MarkdownFormatting.Wrap(content, "**");

        // assert
        result.Should()
            .Be(" **bold** ");
    }

    [Fact]
    public void Wrap_WhenTheContentHasInnerWhitespace_ThenPreservesTheInnerWhitespace()
    {
        // arrange
        const string content = "  bold  text  ";

        // act
        var result = MarkdownFormatting.Wrap(content, "**");

        // assert
        result.Should()
            .Be(" **bold  text** ");
    }

    [Fact]
    public void Block_WhenGivenContent_ThenSurroundsItWithTwoBlankLines()
    {
        // arrange
        const string content = "content";

        // act
        var result = MarkdownFormatting.Block(content);

        // assert
        result.Should()
            .Be(
                $"{Environment.NewLine}{Environment.NewLine}content{Environment.NewLine}{Environment.NewLine}");
    }

    [Theory]
    [InlineData("one  two", "one two")]
    [InlineData("  one \t two \n three  ", "one two three")]
    [InlineData("single", "single")]
    [InlineData("   ", "")]
    [InlineData("", "")]
    public void CollapseWhitespace_WhenGivenAValue_ThenReducesRunsOfWhitespaceToASingleSpace(string value,
        string expected)
    {
        // arrange
        // act
        var result = MarkdownFormatting.CollapseWhitespace(value);

        // assert
        result.Should()
            .Be(expected);
    }

    [Fact]
    public void CollapseWhitespace_WhenTheValueIsAlreadyNormalised_ThenReturnsTheOriginalInstance()
    {
        // arrange
        const string value = "one two three";

        // act
        var result = MarkdownFormatting.CollapseWhitespace(value);

        // assert
        result.Should()
            .BeSameAs(value);
    }

    [Fact]
    public void NormaliseBlockWhitespace_WhenThereAreCarriageReturns_ThenRemovesThem()
    {
        // arrange
        const string markdown = "one\r\ntwo\rthree";

        // act
        var result = MarkdownFormatting.NormaliseBlockWhitespace(markdown);

        // assert
        result.Should()
            .Be($"one{Environment.NewLine}twothree");
    }

    [Fact]
    public void NormaliseBlockWhitespace_WhenThereIsASingleNewLine_ThenReplacesItWithTheEnvironmentNewLine()
    {
        // arrange
        const string markdown = "one\ntwo";

        // act
        var result = MarkdownFormatting.NormaliseBlockWhitespace(markdown);

        // assert
        result.Should()
            .Be($"one{Environment.NewLine}two");
    }

    [Fact]
    public void NormaliseBlockWhitespace_WhenThereAreTwoNewLines_ThenKeepsBothOfThem()
    {
        // arrange
        const string markdown = "one\n\ntwo";

        // act
        var result = MarkdownFormatting.NormaliseBlockWhitespace(markdown);

        // assert
        result.Should()
            .Be($"one{Environment.NewLine}{Environment.NewLine}two");
    }

    [Fact]
    public void NormaliseBlockWhitespace_WhenThereAreMoreThanTwoNewLines_ThenLimitsThemToTwo()
    {
        // arrange
        const string markdown = "one\n\n\n\ntwo";

        // act
        var result = MarkdownFormatting.NormaliseBlockWhitespace(markdown);

        // assert
        result.Should()
            .Be($"one{Environment.NewLine}{Environment.NewLine}two");
    }

    [Fact]
    public void NormaliseBlockWhitespace_WhenNewLinesAreSeparatedByContent_ThenCountsEachRunSeparately()
    {
        // arrange
        const string markdown = "one\n\n\ntwo\n\n\nthree";

        // act
        var result = MarkdownFormatting.NormaliseBlockWhitespace(markdown);

        // assert
        result.Should()
            .Be(
                $"one{Environment.NewLine}{Environment.NewLine}two{Environment.NewLine}{Environment.NewLine}three");
    }

    [Fact]
    public void NormaliseBlockWhitespace_WhenThereIsNoWhitespaceToNormalise_ThenReturnsTheMarkdownUnchanged()
    {
        // arrange
        const string markdown = "one two three";

        // act
        var result = MarkdownFormatting.NormaliseBlockWhitespace(markdown);

        // assert
        result.Should()
            .BeSameAs(markdown);
    }

    [Fact]
    public void NormaliseBlockWhitespace_WhenNewLineIsAtIndexOne_ThenReplacesItWithEnvironmentNewLine()
    {
        // arrange
        const string markdown = "o\nt";

        // act
        var result = MarkdownFormatting.NormaliseBlockWhitespace(markdown);

        // assert
        result.Should()
            .Be($"o{Environment.NewLine}t");
    }
}
