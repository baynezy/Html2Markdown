namespace Html2Markdown.Renderers;
/// <summary>
/// Renders a script (&gt;script&gt;) HTML element to Markdown.
/// </summary>
public sealed class ScriptTagRenderer : IgnoredTagRenderer
{
    /// <summary>
    /// Initialises a new instance of the <see cref="ScriptTagRenderer"/> class.
    /// </summary>
    public ScriptTagRenderer() : base("script")
    {
    }
}