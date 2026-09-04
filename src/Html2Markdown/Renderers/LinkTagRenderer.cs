namespace Html2Markdown.Renderers;

/// <summary>
/// Renders a link (&gt;link&gt;) HTML element to Markdown.
/// </summary>
public sealed class LinkTagRenderer : IgnoredTagRenderer
{
    /// <summary>
    /// Initialises a new instance of the <see cref="LinkTagRenderer"/> class.
    /// </summary>
    public LinkTagRenderer() : base("link")
    {
    }
}