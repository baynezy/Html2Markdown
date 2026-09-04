using System;

namespace Html2Markdown.Test.Renderers;

public class ListRendererTests
{
    [Theory]
    [InlineData("<ul><li><ul></ul></li></ul>")]
    [InlineData("<ul><li><ul></ul><ul></ul></li></ul>")]
    public void Convert_WhenAListItemOnlyContainsEmptyNestedLists_ThenRemovesTheList(string html)
    {
        // arrange
        Converter converter = new();

        // act
        var markdown = converter.Convert(html);

        // assert
        markdown.Should()
            .BeEmpty();
    }

    [Fact]
    public void Convert_WhenAnEmptyListItemContainsANestedListWithAnEmptyItem_ThenIgnoresTheEmptyItem()
    {
        // arrange
        Converter converter = new();

        // act
        var markdown = converter.Convert("<ul><li><ul><li></li><li>Item</li></ul></li></ul>");

        // assert
        markdown.Should()
            .Be("*   1.  Item");
    }

    [Fact]
    public void Convert_WhenAnEmptyListItemContainsANestedListWithSeveralItems_ThenNumbersEachItemOnItsOwnLine()
    {
        // arrange
        Converter converter = new();

        // act
        var markdown = converter.Convert("<ul><li><ul><li>One</li><li>Two</li><li>Three</li></ul></li></ul>");

        // assert
        markdown.Should()
            .Be($"*   1.  One{Environment.NewLine}    2.  Two{Environment.NewLine}    3.  Three");
    }
}
