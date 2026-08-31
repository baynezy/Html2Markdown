namespace Html2Markdown.Renderers;

/// <summary>
/// Renders a list item (&gt;li&gt;) HTML element to Markdown.
/// </summary>
public sealed class ListItemTagRenderer : AbstractListRenderer, IHtmlTagRenderer
{
    /// <inheritdoc/>
    public string TagName => "li";

    /// <inheritdoc/>
    public string Render(IElement element, HtmlTagRenderingContext context) =>
        RenderListItem(element, context);
}