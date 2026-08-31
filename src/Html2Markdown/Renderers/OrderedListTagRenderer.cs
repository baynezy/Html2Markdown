namespace Html2Markdown.Renderers;

/// <summary>
/// Renders an ordered list (&gt;ol&gt;) HTML element to Markdown.
/// </summary>
public sealed class OrderedListTagRenderer : AbstractListRenderer, IHtmlTagRenderer
{
    /// <inheritdoc/>
    public string TagName => "ol";

    /// <inheritdoc/>
    public string Render(IElement element, HtmlTagRenderingContext context) =>
        RenderList(element, context);
}