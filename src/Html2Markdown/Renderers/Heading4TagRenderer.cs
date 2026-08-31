namespace Html2Markdown.Renderers;

/// <summary>
/// Renders a heading 4 (&gt;h4&gt;) HTML element to Markdown.
/// </summary>
public sealed class Heading4TagRenderer : HeadingTagRenderer
{
    /// <summary>
    /// Initialises a new instance of the <see cref="Heading4TagRenderer"/> class.
    /// </summary>
    public Heading4TagRenderer() : base("h4")
    {
    }
}