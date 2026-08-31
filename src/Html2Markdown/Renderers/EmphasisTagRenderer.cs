namespace Html2Markdown.Renderers;

/// <summary>
/// Renders an emphasis (&gt;em&gt;) HTML element to Markdown.
/// </summary>
public sealed class EmphasisTagRenderer : WrappedTagRenderer
{
    /// <summary>
    /// Initialises a new instance of the <see cref="EmphasisTagRenderer"/> class.
    /// </summary>
    public EmphasisTagRenderer() : base("em", "*")
    {
    }
}