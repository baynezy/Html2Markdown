namespace Html2Markdown.Renderers;

/// <summary>
/// Renders a table (&gt;table&gt;) HTML element to Markdown.
/// </summary>
public sealed class TableTagRenderer : RawHtmlTagRenderer
{
    /// <summary>
    /// Initialises a new instance of the <see cref="TableTagRenderer"/> class.
    /// </summary>
    public TableTagRenderer() : base("table")
    {
    }
}