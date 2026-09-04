namespace Html2Markdown.Renderers;

/// <summary>
/// Renders a preformatted (&gt;pre&gt;) HTML element to Markdown.
/// </summary>
public sealed class PreformattedTagRenderer : AbstractRenderer, IHtmlTagRenderer
{
    /// <inheritdoc/>
    public string TagName => "pre";

    /// <inheritdoc/>
    public string Render(IElement element, HtmlTagRenderingContext context) =>
        RenderPre(element);

    private static string RenderPre(IElement element)
    {
        var content = element.Children.SingleOrDefault(child => child.LocalName == "code") is { } code
            ? RenderCode(code)
            : element.TextContent.Replace("\t", "    ");

        return MarkdownFormatting.Block(content);
    }
}