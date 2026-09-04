namespace Html2Markdown.Renderers;

/// <summary>
/// Renders a title (&gt;title&gt;) HTML element to Markdown.
/// </summary>
public sealed class TitleTagRenderer : IgnoredTagRenderer
{
    /// <summary>
    /// Initialises a new instance of the <see cref="TitleTagRenderer"/> class.
    /// </summary>
    public TitleTagRenderer() : base("title")
    {
    }
}