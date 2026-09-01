namespace Html2Markdown.Renderers;

/// <summary>
/// Renders an included frame (&gt;iframe&gt;) HTML element to Markdown.
/// </summary>
public sealed class IncludedFrameTagRenderer : RawHtmlTagRenderer
{
    /// <summary>
    /// Initialises a new instance of the <see cref="IncludedFrameTagRenderer"/> class.
    /// </summary>
    public IncludedFrameTagRenderer() : base("iframe")
    {
    }
}