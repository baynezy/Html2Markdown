namespace Html2Markdown.Renderers;
/// <summary>
/// Renders a style (&gt;style&gt;) HTML element to Markdown.
/// </summary>
public sealed class StyleTagRenderer : IgnoredTagRenderer
{
    /// <summary>
    /// Initialises a new instance of the <see cref="StyleTagRenderer"/> class.
    /// </summary>
    public StyleTagRenderer() : base("style")
    {
    }
}