using System;
using System.Collections.Generic;
using System.Linq;
using Html2Markdown.Renderers;

namespace Html2Markdown.Test.Renderers;

public class DefaultTagRendererTests
{
    [Fact]
    public void TagName_WhenTheDefaultRenderersAreCreated_ThenTheyExposeTheirHtmlTagName()
    {
        // arrange
        List<IHtmlTagRenderer> renderers =
        [
            new AnchorTagRenderer(),
            new BoldTagRenderer(),
            new BlockquoteTagRenderer(),
            new BreakTagRenderer(),
            new CanvasTagRenderer(),
            new CodeTagRenderer(),
            new DivTagRenderer(),
            new EmphasisTagRenderer(),
            new Heading1TagRenderer(),
            new Heading2TagRenderer(),
            new Heading3TagRenderer(),
            new Heading4TagRenderer(),
            new Heading5TagRenderer(),
            new Heading6TagRenderer(),
            new HeadTagRenderer(),
            new HorizontalRuleTagRenderer(),
            new IncludedFrameTagRenderer(),
            new ImageTagRenderer(),
            new ItalicTagRenderer(),
            new ListItemTagRenderer(),
            new LinkTagRenderer(),
            new MetaTagRenderer(),
            new OrderedListTagRenderer(),
            new ParagraphTagRenderer(),
            new PreformattedTagRenderer(),
            new ScriptTagRenderer(),
            new StrongTagRenderer(),
            new StyleTagRenderer(),
            new TableTagRenderer(),
            new TitleTagRenderer(),
            new UnorderedListTagRenderer()
        ];

        // act
        var tagNames = renderers.Select(renderer => renderer.TagName)
            .ToList();

        // assert
        tagNames.Should()
            .Equal("a", "b", "blockquote", "br", "canvas", "code", "div", "em", "h1", "h2", "h3", "h4", "h5", "h6",
                "head", "hr", "iframe", "img", "i", "li", "link", "meta", "ol", "p", "pre", "script", "strong",
                "style", "table", "title", "ul");
    }

    [Fact]
    public void Defaults_WhenTheDefaultRenderersAreRequested_ThenEveryTagIsRegisteredOnlyOnce()
    {
        // arrange
        var renderers = HtmlTagRenderers.Defaults;

        // act
        var tagNames = renderers.Select(renderer => renderer.TagName)
            .ToList();

        // assert
        tagNames.Should()
            .OnlyHaveUniqueItems();
    }

    [Fact]
    public void Convert_WhenTheBoldRendererIsRegisteredExplicitly_ThenWrapsTheContentInDoubleAsterisks()
    {
        // arrange
        ConverterOptions options = new();
        options.TagRenderers.Add(new BoldTagRenderer());
        Converter converter = new(options);

        // act
        var markdown = converter.Convert("<b>bold</b>");

        // assert
        markdown.Should()
            .Be("**bold**");
    }

    [Fact]
    public void Convert_WhenTheStrongRendererIsRegisteredExplicitly_ThenWrapsTheContentInDoubleAsterisks()
    {
        // arrange
        ConverterOptions options = new();
        options.TagRenderers.Add(new StrongTagRenderer());
        Converter converter = new(options);

        // act
        var markdown = converter.Convert("<strong>strong</strong>");

        // assert
        markdown.Should()
            .Be("**strong**");
    }

    [Fact]
    public void Convert_WhenTheEmphasisRendererIsRegisteredExplicitly_ThenWrapsTheContentInASingleAsterisk()
    {
        // arrange
        ConverterOptions options = new();
        options.TagRenderers.Add(new EmphasisTagRenderer());
        Converter converter = new(options);

        // act
        var markdown = converter.Convert("<em>emphasis</em>");

        // assert
        markdown.Should()
            .Be("*emphasis*");
    }

    [Fact]
    public void Convert_WhenTheItalicRendererIsRegisteredExplicitly_ThenWrapsTheContentInASingleAsterisk()
    {
        // arrange
        ConverterOptions options = new();
        options.TagRenderers.Add(new ItalicTagRenderer());
        Converter converter = new(options);

        // act
        var markdown = converter.Convert("<i>italic</i>");

        // assert
        markdown.Should()
            .Be("*italic*");
    }

    [Theory]
    [InlineData("<script>alert('x');</script>Text")]
    [InlineData("<style>body { color: red; }</style>Text")]
    public void Convert_WhenThereIsAnIgnoredTag_ThenRendersNothingForIt(string html)
    {
        // arrange
        Converter converter = new();

        // act
        var markdown = converter.Convert(html);

        // assert
        markdown.Should()
            .Be("Text");
    }

    [Theory]
    [InlineData("<img>", "![]()")]
    [InlineData("""<img src="/image.png">""", "![](/image.png)")]
    [InlineData("""<img alt="Alt text">""", "![Alt text]()")]
    public void Convert_WhenAnImageIsMissingAttributes_ThenTreatsThemAsEmpty(string html, string expected)
    {
        // arrange
        Converter converter = new();

        // act
        var markdown = converter.Convert(html);

        // assert
        markdown.Should()
            .Be(expected);
    }

    [Fact]
    public void Convert_WhenAWrappedTagHasNoContent_ThenRendersNothingForIt()
    {
        // arrange
        Converter converter = new();

        // act
        var markdown = converter.Convert("<p>Hello <strong></strong>World</p>");

        // assert
        markdown.Should()
            .Be("Hello World");
    }

    [Fact]
    public void Convert_WhenThereIsAnHtmlComment_ThenRendersNothingForIt()
    {
        // arrange
        Converter converter = new();

        // act
        var markdown = converter.Convert("<p>Hello<!-- a comment --> World</p>");

        // assert
        markdown.Should()
            .Be("Hello World");
    }

    [Fact]
    public void Convert_WhenAMultilineBlockquoteContainsAnHtmlComment_ThenRendersNothingForTheComment()
    {
        // arrange
        Converter converter = new();

        // act
        var markdown = converter.Convert("<blockquote><p>Line 1<br />Line 2<br />Line 3<!-- a comment --></p></blockquote>");

        // assert
        markdown.Should()
            .Be($"> Line 1  {Environment.NewLine}> Line 2  {Environment.NewLine}> Line 3");
    }
}
