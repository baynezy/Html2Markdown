namespace Html2Markdown.Renderers;

/// <summary>
/// Renders a meta (&gt;meta&gt;) HTML element to Markdown.
/// </summary>
public sealed class MetaTagRenderer : IgnoredTagRenderer
{
    /// <summary>
    /// Initialises a new instance of the <see cref="MetaTagRenderer"/> class.
    /// </summary>
    public MetaTagRenderer() : base("meta")
    {
    }
}