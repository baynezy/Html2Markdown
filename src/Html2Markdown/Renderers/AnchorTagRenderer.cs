namespace Html2Markdown.Renderers;

/// <summary>
/// Renders an anchor (&gt;a&gt;) HTML element to Markdown.
/// </summary>
public sealed class AnchorTagRenderer : AbstractRenderer, IHtmlTagRenderer
{
    /// <inheritdoc/>
    public string TagName => "a";

    /// <inheritdoc/>
    public string Render(IElement element, HtmlTagRenderingContext context) =>
        RenderAnchor(element, context);

    private static string RenderAnchor(IElement element, HtmlTagRenderingContext context)
    {
        var text = context.RenderChildren(element);
        var href = element.GetAttribute("href") ?? string.Empty;
        var title = element.GetAttribute("title") ?? string.Empty;

        return text.Length == 0 && href.Length == 0
            ? string.Empty
            : new MarkdownUrl(href, text, title).ToMarkdown();
    }
}