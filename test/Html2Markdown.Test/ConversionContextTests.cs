namespace Html2Markdown.Test;

public class ConversionContextTests
{
    [Fact]
    public void Default_ShouldHaveZeroListDepthEmptyListTypeAndNotBeInList()
    {
        // arrange & act
        var context = ConversionContext.Default;
        
        // assert
        context.ListDepth.Should()
            .Be(0);
        context.ListType.Should()
            .BeEmpty();
        context.IsInList.Should()
            .BeFalse();
        context.OrderedListIndex.Should()
            .Be(1);
    }

    [Fact]
    public void EnterList_ShouldIncrementListDepthSetListTypeAndSetIsInListToTrue()
    {
        // arrange & act
        var context = ConversionContext.Default.EnterList("ol");

        // assert
        context.ListDepth.Should()
            .Be(1);
        context.ListType.Should()
            .Be("ol");
        context.IsInList.Should()
            .BeTrue();
        context.OrderedListIndex.Should()
            .Be(1);
    }

    [Fact]
    public void WithOrderedListIndex_ShouldSetOrderedListIndex()
    {
        // arrange & act
        var context = ConversionContext.Default.WithOrderedListIndex(5);

        // assert
        context.OrderedListIndex.Should()
            .Be(5);
    }
}