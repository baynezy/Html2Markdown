namespace Html2Markdown.Renderers;
/// <summary>
/// Renders a strong (&gt;strong&gt;) HTML element to Markdown.
/// </summary>
public sealed class StrongTagRenderer : WrappedTagRenderer
{
    /// <summary>
    /// Initialises a new instance of the <see cref="StrongTagRenderer"/> class.
    /// </summary>
    public StrongTagRenderer() : base("strong", "**")
    {
    }
}