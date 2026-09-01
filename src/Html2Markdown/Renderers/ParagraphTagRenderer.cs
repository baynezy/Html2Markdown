namespace Html2Markdown.Renderers;

/// <summary>
/// Renders a paragraph (&gt;p&gt;) HTML element to Markdown.
/// </summary>
public sealed class ParagraphTagRenderer : IHtmlTagRenderer
{
    /// <inheritdoc/>
    public string TagName => "p";

    /// <inheritdoc/>
    public string Render(IElement element, HtmlTagRenderingContext context) =>
        RenderParagraph(element, context);

    private static string RenderParagraph(IElement element, HtmlTagRenderingContext context)
    {
        var content = MarkdownFormatting.CollapseWhitespace(context.RenderChildren(element));
        return context.IsInList ? content : $"{Environment.NewLine}{Environment.NewLine}{content}{Environment.NewLine}";
    }
}