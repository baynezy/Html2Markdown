namespace Html2Markdown.Renderers;

/// <summary>
/// Renders an image (&gt;img&gt;) HTML element to Markdown.
/// </summary>
public sealed class ImageTagRenderer : IHtmlTagRenderer
{
    /// <inheritdoc/>
    public string TagName => "img";

    /// <inheritdoc/>
    public string Render(IElement element, HtmlTagRenderingContext context) =>
        RenderImage(element);

    private static string RenderImage(IElement element)
    {
        var src = element.GetAttribute("src") ?? string.Empty;
        var alt = element.GetAttribute("alt") ?? string.Empty;
        var title = element.GetAttribute("title") ?? string.Empty;

        return new MarkdownImage(src, alt, title).ToMarkdown();
    }
}