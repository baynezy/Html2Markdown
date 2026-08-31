namespace Html2Markdown.Renderers;

/// <summary>
/// Provides a collection of default HTML tag renderers for converting HTML to Markdown.
/// </summary>
public static class HtmlTagRenderers
{
    /// <summary>
    /// Gets the default set of HTML tag renderers for converting HTML to Markdown.
    /// </summary>
    public static IReadOnlyCollection<IHtmlTagRenderer> Defaults { get; } =
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
}