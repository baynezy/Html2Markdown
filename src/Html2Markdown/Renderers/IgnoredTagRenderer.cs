namespace Html2Markdown.Renderers;

/// <summary>
/// Represents a base class for HTML tag renderers that ignore the content of the tag and render it as an empty string.
/// </summary>
public abstract class IgnoredTagRenderer : IHtmlTagRenderer
{
    /// <summary>
    /// Initialises a new instance of the <see cref="IgnoredTagRenderer"/> class with the specified tag name.
    /// </summary>
    /// <param name="tagName">The name of the HTML tag to ignore.</param>
    protected IgnoredTagRenderer(string tagName)
    {
        TagName = tagName;
    }

    /// <inheritdoc/>
    public string TagName { get; }

    /// <inheritdoc/>
    public string Render(IElement element, HtmlTagRenderingContext context) => string.Empty;
}