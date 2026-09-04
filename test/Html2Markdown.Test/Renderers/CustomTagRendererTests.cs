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
        ConverterOptions options = null!;

        // act
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
        options.TagRenderers.Add(null!);

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
        const string html = null!;

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
            context.Render(null!);
    }
}