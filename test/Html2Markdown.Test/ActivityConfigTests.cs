using Html2Markdown.Observability;

namespace Html2Markdown.Test;

public class ActivityConfigTests
{
    [Fact]
    public void ActivityConfig_ShouldHaveCorrectMetadata()
    {
        // assert
        ActivityConfig.ServiceName.Should()
            .Be("Html2Markdown");
        ActivityConfig.ActivitySource.Name.Should()
            .Be("Html2Markdown");
        ActivityConfig.RenderedElementsCounter.Description.Should()
            .Be("Counts the number of rendered HTML elements");
    }
}