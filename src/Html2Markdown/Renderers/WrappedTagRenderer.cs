namespace Html2Markdown.Renderers;

/// <summary>
/// Renders an HTML element to Markdown by wrapping its content with a specified marker.
/// </summary>
public abstract class WrappedTagRenderer : AbstractRenderer, IHtmlTagRenderer
{
    private readonly string _marker;

    /// <summary>
    /// Initialises a new instance of the <see cref="WrappedTagRenderer"/> class with the specified tag name and marker.
    /// </summary>
    /// <param name="tagName">The name of the HTML tag to render.</param>
    /// <param name="marker">The marker to wrap the content with.</param>
    protected WrappedTagRenderer(string tagName, string marker)
    {
        TagName = tagName;
        _marker = marker;
    }

    /// <inheritdoc/>
    public string TagName { get; }

    /// <inheritdoc/>
    public string Render(IElement element, HtmlTagRenderingContext context) =>
        RenderWrapped(element, context, _marker);
}