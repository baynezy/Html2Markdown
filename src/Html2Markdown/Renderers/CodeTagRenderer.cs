namespace Html2Markdown.Renderers;

/// <summary>
/// Renders a code (&gt;code&gt;) HTML element to Markdown.
/// </summary>
public sealed class CodeTagRenderer : AbstractRenderer, IHtmlTagRenderer
{
    /// <inheritdoc/>
    public string TagName => "code";

    /// <inheritdoc/>
    public string Render(IElement element, HtmlTagRenderingContext context) =>
        RenderCode(element);
}