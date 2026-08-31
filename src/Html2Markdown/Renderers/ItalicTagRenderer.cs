namespace Html2Markdown.Renderers;

/// <summary>
/// Renders an italic (&gt;i&gt;) HTML element to Markdown.
/// </summary>
public sealed class ItalicTagRenderer : WrappedTagRenderer
{
    /// <summary>
    /// Initialises a new instance of the <see cref="ItalicTagRenderer"/> class.
    /// </summary>
    public ItalicTagRenderer() : base("i", "*")
    {
    }
}