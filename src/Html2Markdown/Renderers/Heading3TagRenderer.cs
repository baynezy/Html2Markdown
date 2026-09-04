namespace Html2Markdown.Renderers;

/// <summary>
/// Renders a heading 3 (&gt;h3&gt;) HTML element to Markdown.
/// </summary>
public sealed class Heading3TagRenderer : HeadingTagRenderer
{
    /// <summary>
    /// Initialises a new instance of the <see cref="Heading3TagRenderer"/> class.
    /// </summary>
    public Heading3TagRenderer() : base("h3")
    {
    }
}