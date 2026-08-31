namespace Html2Markdown.Renderers;

/// <summary>
/// Renders an unordered list (&gt;ul&gt;) HTML element to Markdown.
/// </summary>
public sealed class UnorderedListTagRenderer : AbstractListRenderer, IHtmlTagRenderer
{
    /// <inheritdoc/>
    public string TagName => "ul";

    /// <inheritdoc/>
    public string Render(IElement element, HtmlTagRenderingContext context) =>
        RenderList(element, context);
}