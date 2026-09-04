namespace Html2Markdown.Renderers;

/// <summary>
/// Renders a heading 5 (&gt;h5&gt;) HTML element to Markdown.
/// </summary>
public sealed class Heading5TagRenderer : HeadingTagRenderer
{
    /// <summary>
    /// Initialises a new instance of the <see cref="Heading5TagRenderer"/> class.
    /// </summary>
    public Heading5TagRenderer() : base("h5")
    {
    }
}