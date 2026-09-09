using System.Text;
using Html2Markdown.Renderers.Utils;

namespace Html2Markdown.Test.Renderers.Utils;

[Collection(nameof(MarkdownRendererTests))]
public class StringBuilderPoolTests
{
    [Fact]
    public void Rent_WhenBuilderWasReturned_ThenReusesTheCachedInstanceAndClearsItsContents()
    {
        // arrange
        var builder = StringBuilderPool.Rent();
        builder.Append("content");
        StringBuilderPool.Return(builder);

        // act
        var rentedBuilder = StringBuilderPool.Rent();

        // assert
        rentedBuilder.Should()
            .BeSameAs(builder);
        rentedBuilder.ToString().Should()
            .BeEmpty();
    }

    [Fact]
    public void Return_WhenBuilderExceedsTheMaximumCapacity_ThenDoesNotCacheIt()
    {
        // arrange
        var largeBuilder = new StringBuilder(StringBuilderPoolTestConstants.MaximumRetainedCapacity + 1);
        StringBuilderPool.Return(largeBuilder);

        // act
        var rentedBuilder = StringBuilderPool.Rent();

        // assert
        rentedBuilder.Should()
            .NotBeSameAs(largeBuilder);
    }

    private static class StringBuilderPoolTestConstants
    {
        internal const int MaximumRetainedCapacity = 16 * 1024;
    }
}
