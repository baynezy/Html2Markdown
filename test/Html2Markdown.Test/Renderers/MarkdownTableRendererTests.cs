using Html2Markdown.Renderers;

namespace Html2Markdown.Test.Renderers;

public class MarkdownTableRendererTests
{
    [Fact]
    public void Convert_WhenTableConversionIsDisabled_ThenPreservesTheTableAsRawHtml()
    {
        // arrange
        ConverterOptions options = new() { ConvertTables = false };
        Converter converter = new(options);

        // act
        var markdown = converter.Convert("<table><tr><td>Cell</td></tr></table>");

        // assert
        markdown.Should()
            .Be("<table><tbody><tr><td>Cell</td></tr></tbody></table>");
    }

    [Fact]
    public void Convert_WhenTableConversionIsEnabled_ThenRendersAHeaderAndBodyAsAGfmTable()
    {
        // arrange
        var converter = CreateTableConverter();

        // act
        var markdown = converter.Convert("""
                                         <table>
                                             <thead><tr><th>Name</th><th>City</th></tr></thead>
                                             <tbody><tr><td>Sam</td><td>London</td></tr></tbody>
                                         </table>
                                         """);

        // assert
        markdown.Should()
            .Be("""
                | Name | City |
                | --- | --- |
                | Sam | London |
                """);
    }

    [Fact]
    public void Convert_WhenTableHasNoHeader_ThenRendersAnEmptySyntheticHeader()
    {
        // arrange
        var converter = CreateTableConverter();

        // act
        var markdown = converter.Convert("<table><tr><td>A</td><td>B</td></tr></table>");

        // assert
        markdown.Should()
            .Be("""
                |  |  |
                | --- | --- |
                | A | B |
                """);
    }

    [Fact]
    public void Convert_WhenHeaderCellsSpecifyAlignment_ThenRendersAlignedGfmSeparators()
    {
        // arrange
        var converter = CreateTableConverter();

        // act
        var markdown = converter.Convert("""
                                         <table>
                                             <tr>
                                                 <th align="left">Left</th>
                                                 <th align="center">Centre</th>
                                                 <th align="right">Right</th>
                                             </tr>
                                         </table>
                                         """);

        // assert
        markdown.Should()
            .Be("""
                | Left | Centre | Right |
                | :--- | :--: | ---: |
                """);
    }

    [Fact]
    public void Convert_WhenCellsUseColumnAndRowSpans_ThenExpandsThemToEmptyCells()
    {
        // arrange
        var converter = CreateTableConverter();

        // act
        var markdown = converter.Convert("""
                                         <table>
                                             <tr><th colspan="2">Header</th><th>Last</th></tr>
                                             <tr><td rowspan="2">Tall</td><td>Middle</td><td>End</td></tr>
                                             <tr><td>Next</td><td>Final</td></tr>
                                         </table>
                                         """);

        // assert
        markdown.Should()
            .Be("""
                | Header |  | Last |
                | --- | --- | --- |
                | Tall | Middle | End |
                |  | Next | Final |
                """);
    }

    [Fact]
    public void Convert_WhenARowSpanFillsTheNextRow_ThenAppendsCellsAfterTheSpannedColumns()
    {
        // arrange
        var converter = CreateTableConverter();

        // act
        var markdown = converter.Convert("""
                                         <table>
                                             <tr><th colspan="2" rowspan="2">Spanning</th></tr>
                                             <tr><td>After span</td></tr>
                                         </table>
                                         """);

        // assert
        markdown.Should()
            .Be("""
                | Spanning |  |  |
                | --- | --- | --- |
                |  |  | After span |
                """);
    }

    [Fact]
    public void Convert_WhenARowSpanExtendsPastTheLastSourceRow_ThenAddsAnEmptyExpandedRow()
    {
        // arrange
        var converter = CreateTableConverter();

        // act
        var markdown = converter.Convert("<table><tr><th rowspan=\"2\">Heading</th></tr></table>");

        // assert
        markdown.Should()
            .Be("""
                | Heading |
                | --- |
                |  |
                """);
    }

    [Fact]
    public void Convert_WhenCellContainsInlineMarkdownAndPipe_ThenRendersAndEscapesTheCellContent()
    {
        // arrange
        var converter = CreateTableConverter();

        // act
        var markdown = converter.Convert("<table><tr><th>Heading</th></tr><tr><td><strong>Bold</strong> | text</td></tr></table>");

        // assert
        markdown.Should()
            .Be("""
                | Heading |
                | --- |
                | **Bold** \| text |
                """);
    }

    [Fact]
    public void Convert_WhenTableCannotBeRepresentedAsGfm_ThenPreservesRawHtml()
    {
        // arrange
        var converter = CreateTableConverter();

        // act
        var markdown = converter.Convert("<table><tr><td>First<br />Second</td></tr></table>");

        // assert
        markdown.Should()
            .Be("<table><tbody><tr><td>First<br>Second</td></tr></tbody></table>");
    }

    [Fact]
    public void Convert_WhenTableContainsANestedTable_ThenPreservesTheOuterTableAsRawHtml()
    {
        // arrange
        var converter = CreateTableConverter();

        // act
        var markdown = converter.Convert("<table><tr><td><table><tr><td>Nested</td></tr></table></td></tr></table>");

        // assert
        markdown.Should()
            .Be("<table><tbody><tr><td><table><tbody><tr><td>Nested</td></tr></tbody></table></td></tr></tbody></table>");
    }

    [Fact]
    public void Convert_WhenTheadAndHeaderCellsExistOutsideThead_ThenUsesTheadAsTheHeader()
    {
        // arrange
        var converter = CreateTableConverter();

        // act
        var markdown = converter.Convert("""
                                         <table>
                                             <thead><tr><th>Preferred</th></tr></thead>
                                             <tbody><tr><th>Body heading</th></tr></tbody>
                                         </table>
                                         """);

        // assert
        markdown.Should()
            .Be("""
                | Preferred |
                | --- |
                | Body heading |
                """);
    }

    [Fact]
    public void Convert_WhenTheadContainsOnlyDataCells_ThenUsesItInsteadOfALaterHeaderCell()
    {
        // arrange
        var converter = CreateTableConverter();

        // act
        var markdown = converter.Convert("""
                                         <table>
                                             <thead><tr><td>Preferred</td></tr></thead>
                                             <tbody><tr><th>Later heading</th></tr></tbody>
                                         </table>
                                         """);

        // assert
        markdown.Should()
            .Be("""
                | Preferred |
                | --- |
                | Later heading |
                """);
    }

    [Fact]
    public void Convert_WhenAHeaderRowContainsBothHeaderAndDataCells_ThenRecognisesItAsTheHeader()
    {
        // arrange
        var converter = CreateTableConverter();

        // act
        var markdown = converter.Convert("<table><tr><th>Heading</th><td>Also heading</td></tr><tr><td>Value</td><td>Other value</td></tr></table>");

        // assert
        markdown.Should()
            .Be("""
                | Heading | Also heading |
                | --- | --- |
                | Value | Other value |
                """);
    }

    [Fact]
    public void Convert_WhenAnyCellContainsMultilineContent_ThenPreservesTheTableAsRawHtml()
    {
        // arrange
        var converter = CreateTableConverter();

        // act
        var markdown = converter.Convert("<table><tr><th>Heading</th><th>Other</th></tr><tr><td>Value</td><td>First<br />Second</td></tr></table>");

        // assert
        markdown.Should()
            .Be("<table><tbody><tr><th>Heading</th><th>Other</th></tr><tr><td>Value</td><td>First<br>Second</td></tr></tbody></table>");
    }

    [Fact]
    public void Convert_WhenASyntheticHeaderUsesAlignment_ThenUsesTheFirstRowAlignmentForTheSeparator()
    {
        // arrange
        var converter = CreateTableConverter();

        // act
        var markdown = converter.Convert("<table><tr><td align=\"left\">A</td><td align=\"center\">B</td><td align=\"right\">C</td></tr></table>");

        // assert
        markdown.Should()
            .Be("""
                |  |  |  |
                | :--- | :--: | ---: |
                | A | B | C |
                """);
    }

    [Fact]
    public void Convert_WhenASyntheticHeaderHasMultipleRows_ThenUsesAlignmentOnlyFromTheFirstRow()
    {
        // arrange
        var converter = CreateTableConverter();

        // act
        var markdown = converter.Convert("""
                                         <table>
                                             <tr><td align="left">First</td></tr>
                                             <tr><td align="right">Second</td></tr>
                                         </table>
                                         """);

        // assert
        markdown.Should()
            .Be("""
                |  |
                | :--- |
                | First |
                | Second |
                """);
    }

    [Fact]
    public void Convert_WhenBodyRowsAreWiderThanTheHeader_ThenPadsTheHeaderAndMissingAlignment()
    {
        // arrange
        var converter = CreateTableConverter();

        // act
        var markdown = converter.Convert("<table><tr><th align=\"left\">Heading</th></tr><tr><td>First</td><td>Second</td></tr></table>");

        // assert
        markdown.Should()
            .Be("""
                | Heading |  |
                | :--- | --- |
                | First | Second |
                """);
    }

    [Fact]
    public void Convert_WhenRowsAreUneven_ThenPadsShorterBodyRows()
    {
        // arrange
        var converter = CreateTableConverter();

        // act
        var markdown = converter.Convert("<table><tr><th>One</th><th>Two</th></tr><tr><td>Only</td></tr></table>");

        // assert
        markdown.Should()
            .Be("""
                | One | Two |
                | --- | --- |
                | Only |  |
                """);
    }

    [Fact]
    public void Convert_WhenTableHasCaption_ThenRendersItAfterTheTable()
    {
        // arrange
        var converter = CreateTableConverter();

        // act
        var markdown = converter.Convert("<table><caption>Summary</caption><tr><th>Heading</th></tr><tr><td>Value</td></tr></table>");

        // assert
        markdown.Should()
            .Be("""
                | Heading |
                | --- |
                | Value |

                Summary
                """);
    }

    [Fact]
    public void Convert_WhenAnEmptyTableHasCaption_ThenRendersTheCaption()
    {
        // arrange
        var converter = CreateTableConverter();

        // act
        var markdown = converter.Convert("<table><caption>Summary</caption></table>");

        // assert
        markdown.Should()
            .Be("Summary");
    }

    [Fact]
    public void Convert_WhenAnEmptyTableHasNoCaption_ThenRendersNothing()
    {
        // arrange
        var converter = CreateTableConverter();

        // act
        var markdown = converter.Convert("<table></table>");

        // assert
        markdown.Should()
            .BeEmpty();
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("invalid")]
    public void Convert_WhenSpanIsInvalid_ThenTreatsItAsOneCell(string span)
    {
        // arrange
        var converter = CreateTableConverter();

        // act
        var markdown = converter.Convert($"<table><tr><th colspan=\"{span}\">Heading</th></tr><tr><td>Value</td></tr></table>");

        // assert
        markdown.Should()
            .Be("""
                | Heading |
                | --- |
                | Value |
                """);
    }

    [Fact]
    public void Convert_WhenACustomTableRendererIsRegistered_ThenItOverridesTheOptInRenderer()
    {
        // arrange
        ConverterOptions options = new() { ConvertTables = true };
        options.TagRenderers.Add(new TableTagRenderer());
        Converter converter = new(options);

        // act
        var markdown = converter.Convert("<table><tr><td>Cell</td></tr></table>");

        // assert
        markdown.Should()
            .Be("<table><tbody><tr><td>Cell</td></tr></tbody></table>");
    }

    private static Converter CreateTableConverter()
    {
        ConverterOptions options = new();
        options.TagRenderers.Add(new MarkdownTableTagRenderer());

        return new Converter(options);
    }
}
