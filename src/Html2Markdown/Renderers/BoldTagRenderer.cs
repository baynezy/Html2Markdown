namespace Html2Markdown.Renderers;

/// <summary>
/// Renders a bold (&gt;b&gt;) HTML element to Markdown.
/// </summary>
public sealed class BoldTagRenderer : WrappedTagRenderer
{
    /// <summary>
    /// Initialises a new instance of the <see cref="BoldTagRenderer"/> class.
    /// </summary>
    public BoldTagRenderer() : base("b", "**")
    {
    }
}