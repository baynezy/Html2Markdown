namespace Html2Markdown;

/// <summary>
/// Renders a supported HTML element to Markdown.
/// </summary>
public interface IHtmlTagRenderer
{
    /// <summary>
    /// Gets the HTML tag name handled by this renderer.
    /// </summary>
    string TagName { get; }

    /// <summary>
    /// Renders the supplied HTML element to Markdown.
    /// </summary>
    /// <param name="element">The element to render.</param>
    /// <param name="context">The active rendering context.</param>
    /// <returns>The Markdown representation of the element.</returns>
    string Render(IElement element, HtmlTagRenderingContext context);
}
