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

    [Fact]
    public void Convert_WhenAnEmptyListContainsANestedListDirectly_ThenRendersTheNestedList()
    {
        // arrange
        Converter converter = new();

        // act
        var markdown = converter.Convert("<ul><ul><li>Nested</li></ul></ul>");

        // assert
        markdown.Should()
            .Be("*   Nested");
    }

    [Fact]
    public void Convert_WhenOrderedListContainsOnlyNestedOrderedList_ThenRendersWithCompatibilityMixedListFormatting()
    {
        // arrange
        Converter converter = new();

        // act
        var markdown = converter.Convert("<ol><li><ol><li>One</li><li>Two</li></ol></li></ol>");

        // assert
        markdown.Should()
            .Be($"*   1.  One{Environment.NewLine}    2.  Two");
    }

    [Fact]
    public void Convert_WhenEmptyNestedListItemContainsAnotherNestedListAtDepthTwo_ThenIndentsCorrectly()
    {
        // arrange
        Converter converter = new();

        // act
        var html = "<ul><li>Level 1<ul><li><ul><li>Level 3</li></ul></li></ul></li></ul>";
        var markdown = converter.Convert(html);

        // assert
        // We want to make sure the indentation is correct and it is not formatted using top-level formatting
        markdown.Should()
            .Be($"*   Level 1{Environment.NewLine}{Environment.NewLine}        *   Level 3");
    }

    [Fact]
    public void Convert_WhenListContainsAnItemWithOnlyEmptyNestedListInTheMiddle_ThenDoesNotInsertBlankLine()
    {
        // arrange
        Converter converter = new();

        // act
        var html = "<ul><li>One</li><li><ul></ul></li><li>Two</li></ul>";
        var markdown = converter.Convert(html);

        // assert
        markdown.Should()
            .Be($"*   One{Environment.NewLine}*   Two");
    }
}
