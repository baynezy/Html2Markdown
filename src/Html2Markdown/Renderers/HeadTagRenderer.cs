namespace Html2Markdown.Renderers;

/// <summary>
/// Renders a head (&gt;head&gt;) HTML element to Markdown.
/// </summary>
public sealed class HeadTagRenderer : IgnoredTagRenderer
{
    /// <summary>
    /// Initialises a new instance of the <see cref="HeadTagRenderer"/> class.
    /// </summary>
    public HeadTagRenderer() : base("head")
    {
    }
}