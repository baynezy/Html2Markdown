namespace Html2Markdown.Renderers;

/// <summary>
/// Renders a horizontal rule (&gt;hr&gt;) HTML element to Markdown.
/// </summary>
public sealed class HorizontalRuleTagRenderer : IHtmlTagRenderer
{
    /// <inheritdoc/>
    public string TagName => "hr";

    /// <inheritdoc/>
    public string Render(IElement element, HtmlTagRenderingContext context) =>
        RenderHorizontalRule();
    
    private static string RenderHorizontalRule() =>
        $"{Environment.NewLine}{Environment.NewLine}* * *{Environment.NewLine}";
}