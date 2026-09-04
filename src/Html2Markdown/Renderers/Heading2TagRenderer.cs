namespace Html2Markdown.Renderers;

/// <summary>
/// Renders a heading 2 (&gt;h2&gt;) HTML element to Markdown.
/// </summary>
public sealed class Heading2TagRenderer : HeadingTagRenderer
{
    /// <summary>
    /// Initialises a new instance of the <see cref="Heading2TagRenderer"/> class.
    /// </summary>
    public Heading2TagRenderer() : base("h2")
    {
    }
}