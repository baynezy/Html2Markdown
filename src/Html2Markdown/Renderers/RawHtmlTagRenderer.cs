namespace Html2Markdown.Renderers;

/// <summary>
/// Renders an HTML element to Markdown by returning its raw HTML representation.
/// </summary>
public abstract class RawHtmlTagRenderer : IHtmlTagRenderer
{
    /// <summary>
    /// Initialises a new instance of the <see cref="RawHtmlTagRenderer"/> class with the specified tag name.
    /// </summary>
    /// <param name="tagName">The name of the HTML tag to render.</param>
    protected RawHtmlTagRenderer(string tagName)
    {
        TagName = tagName;
    }

    /// <inheritdoc/>
    public string TagName { get; }

    /// <inheritdoc/>
    public string Render(IElement element, HtmlTagRenderingContext context) => element.OuterHtml;
}