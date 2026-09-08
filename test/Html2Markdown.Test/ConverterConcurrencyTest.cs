using System.Collections.Generic;
using System.Linq;

namespace Html2Markdown.Test;

public class ConverterConcurrencyTest
{
    [Fact]
    public async Task Convert_WhenCalledConcurrently_ThenEveryConversionProducesTheSameMarkdown()
    {
        // arrange
        Converter converter = new();
        const string html = "<h1>Title</h1><p>This is <strong>important</strong>.</p><ul><li>One</li><li>Two</li></ul>";
        var expected = converter.Convert(html);

        // act
        var results = await Task.WhenAll(Enumerable.Range(0, 50)
            .Select(_ => Task.Run(() => converter.Convert(html))));

        // assert
        results.Should()
            .AllSatisfy(markdown => markdown.Should()
                .Be(expected));
    }
}
