using System;
using AngleSharp.Dom;

namespace Html2Markdown.Test.Renderers;

public class CustomTagRendererTests
{
    [Fact]
    public void Convert_WhenCustomRendererHandlesUnknownTag_ThenUsesCustomRenderer()
    {
        // arrange
        ConverterOptions options = new();
        options.TagRenderers.Add(new MarkTagRenderer());
        Converter converter = new(options);

        // act
        var markdown = converter.Convert("<p>This is <mark><strong>important</strong></mark>.</p>");

        // assert
        markdown.Should()
            .Be("This is ==**important**==.");
    }

    [Fact]
    public void Convert_WhenCustomRendererHandlesDefaultTag_ThenOverridesDefaultRenderer()
    {
        // arrange
        ConverterOptions options = new();
        options.TagRenderers.Add(new StrongTagRenderer());
        Converter converter = new(options);

        // act
        var markdown = converter.Convert("<p>This is <strong>important</strong>.</p>");

        // assert
        markdown.Should()
            .Be("This is __important__.");
    }

    [Fact]
    public void Constructor_WhenOptionsIsNull_ThenThrowsArgumentNullException()
    {
        // arrange
        ConverterOptions options = null;

        // act
        // ReSharper disable once ExpressionIsAlwaysNull
        Action action = () => _ = new Converter(options);

        // assert
        action.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(options));
    }

    [Fact]
    public void Constructor_WhenOptionsContainsNullRenderer_ThenThrowsArgumentException()
    {
        // arrange
        ConverterOptions options = new();
        options.TagRenderers.Add(null);

        // act
        Action action = () => _ = new Converter(options);

        // assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(options))
            .WithMessage("Tag renderers cannot contain null.*");
    }

    [Fact]
    public void Constructor_WhenCustomRendererHasEmptyTagName_ThenThrowsArgumentException()
    {
        // arrange
        ConverterOptions options = new();
        options.TagRenderers.Add(new EmptyTagRenderer());

        // act
        Action action = () => _ = new Converter(options);

        // assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(options))
            .WithMessage("Tag renderer names cannot be empty.*");
    }

    [Fact]
    public void Convert_WhenHtmlIsNull_ThenThrowsArgumentNullException()
    {
        // arrange
        Converter converter = new();
        const string html = null;

        // act
        Action action = () => _ = converter.Convert(html);

        // assert
        action.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(html));
    }

    [Fact]
    public void Convert_WhenCustomRendererRendersANullNode_ThenThrowsArgumentNullException()
    {
        // arrange
        ConverterOptions options = new();
        options.TagRenderers.Add(new NullNodeTagRenderer());
        Converter converter = new(options);

        // act
        Action action = () => _ = converter.Convert("<p>This is <mark>important</mark>.</p>");

        // assert
        action.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName("node");
    }

    [Fact]
    public void Convert_WhenCustomRendererRendersNullChildren_ThenThrowsArgumentNullException()
    {
        // arrange
        ConverterOptions options = new();
        options.TagRenderers.Add(new NullChildrenTagRenderer());
        Converter converter = new(options);

        // act
        Action action = () => _ = converter.Convert("<p>This is <mark>important</mark>.</p>");

        // assert
        action.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName("parent");
    }

    [Fact]
    public void Convert_WhenMultipleCustomRenderersAreRegistered_ThenAllAreUsed()
    {
        // arrange
        ConverterOptions options = new();
        options.TagRenderers.Add(new MarkTagRenderer());
        options.TagRenderers.Add(new StrongTagRenderer());
        Converter converter = new(options);

        // act
        var markdown = converter.Convert("<p>This is <mark><strong>important</strong></mark>.</p>");

        // assert
        markdown.Should()
            .Be("This is ==__important__==.");
    }

    [Fact]
    public void Convert_WhenConvertTablesOptionIsActiveWithoutCustomRenderer_ThenRendersAsAGfmTable()
    {
        // arrange
        Converter converter = new(new ConverterOptions { ConvertTables = true });

        // act
        var markdown = converter.Convert("<table><tr><th>Heading</th></tr><tr><td>Value</td></tr></table>");

        // assert
        markdown.Should()
            .Contain("| Heading |");
    }

    [Fact]
    public void Convert_WhenACustomRendererOverridesADefaultTag_ThenOtherConvertersStillUseTheDefaultRenderer()
    {
        // arrange
        ConverterOptions options = new();
        options.TagRenderers.Add(new StrongTagRenderer());
        Converter customConverter = new(options);
        Converter defaultConverter = new();

        // act
        _ = customConverter.Convert("<p>This is <strong>important</strong>.</p>");
        var markdown = defaultConverter.Convert("<p>This is <strong>important</strong>.</p>");

        // assert
        markdown.Should()
            .Be("This is **important**.");
    }

    [Fact]
    public void Convert_WhenTableConversionIsEnabled_ThenOtherConvertersStillLeaveTablesAsHtml()
    {
        // arrange
        Converter tableConverter = new(new ConverterOptions {ConvertTables = true});
        Converter defaultConverter = new();
        const string html = "<table><tr><th>Name</th></tr><tr><td>Bob</td></tr></table>";

        // act
        _ = tableConverter.Convert(html);
        var markdown = defaultConverter.Convert(html);

        // assert
        markdown.Should()
            .Contain("<table>");
    }

    private sealed class MarkTagRenderer : IHtmlTagRenderer
    {
        public string TagName => "mark";

        public string Render(IElement element, HtmlTagRenderingContext context) =>
            $"=={context.RenderChildren(element)}==";
    }

    private sealed class StrongTagRenderer : IHtmlTagRenderer
    {
        public string TagName => "strong";

        public string Render(IElement element, HtmlTagRenderingContext context) =>
            $"__{context.RenderChildren(element)}__";
    }

    private sealed class EmptyTagRenderer : IHtmlTagRenderer
    {
        public string TagName => string.Empty;

        public string Render(IElement element, HtmlTagRenderingContext context) =>
            context.RenderChildren(element);
    }

    private sealed class NullNodeTagRenderer : IHtmlTagRenderer
    {
        public string TagName => "mark";

        public string Render(IElement element, HtmlTagRenderingContext context) =>
            context.Render(null);
    }

    private sealed class NullChildrenTagRenderer : IHtmlTagRenderer
    {
        public string TagName => "mark";

        public string Render(IElement element, HtmlTagRenderingContext context) =>
            context.RenderChildren(null);
    }
}