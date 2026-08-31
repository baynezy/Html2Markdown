namespace Html2Markdown.Renderers;

/// <summary>
/// Renders a div (&gt;div&gt;) HTML element to Markdown.
/// </summary>
public sealed class DivTagRenderer : RawHtmlTagRenderer
{
    /// <summary>
    /// Initialises a new instance of the <see cref="DivTagRenderer"/> class.
    /// </summary>
    public DivTagRenderer() : base("div")
    {
    }
}