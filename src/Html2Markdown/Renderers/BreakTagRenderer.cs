namespace Html2Markdown.Renderers;

/// <summary>
/// Renders a line break (&gt;br&gt;) HTML element to Markdown.
/// </summary>
public sealed class BreakTagRenderer : AbstractRenderer, IHtmlTagRenderer
{
    /// <inheritdoc/>
    public string TagName => "br";
    
    /// <inheritdoc/>
    public string Render(IElement element, HtmlTagRenderingContext context) =>
        RenderLineBreak();
}