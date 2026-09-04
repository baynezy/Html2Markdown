namespace Html2Markdown.Renderers;

/// <summary>
/// Renders a canvas (&gt;canvas&gt;) HTML element to Markdown.
/// </summary>
public sealed class CanvasTagRenderer : RawHtmlTagRenderer
{
    /// <summary>
    /// Initialises a new instance of the <see cref="CanvasTagRenderer"/> class.
    /// </summary>
    public CanvasTagRenderer() : base("canvas")
    {
    }
}