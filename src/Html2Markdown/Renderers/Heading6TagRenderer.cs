namespace Html2Markdown.Renderers;

/// <summary>
/// Renders a heading 6 (&gt;h6&gt;) HTML element to Markdown.
/// </summary>
public sealed class Heading6TagRenderer : HeadingTagRenderer
{
    /// <summary>
    /// Initialises a new instance of the <see cref="Heading6TagRenderer"/> class.
    /// </summary>
    public Heading6TagRenderer() : base("h6")
    {
    }
}