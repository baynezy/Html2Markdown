namespace Html2Markdown.Renderers;

/// <summary>
/// Renders a heading 1 (&gt;h1&gt;) HTML element to Markdown.
/// </summary>
public sealed class Heading1TagRenderer : HeadingTagRenderer
{
    /// <summary>
    /// Initialises a new instance of the <see cref="Heading1TagRenderer"/> class.
    /// </summary>
    public Heading1TagRenderer() : base("h1")
    {
    }
}