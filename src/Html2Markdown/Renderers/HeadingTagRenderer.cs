namespace Html2Markdown.Renderers;

/// <summary>
/// Renders a heading (&gt;h1&gt;, &gt;h2&gt;, &gt;h3&gt;, &gt;h4&gt;, &gt;h5&gt;, &gt;h6&gt;) HTML element to Markdown.
/// </summary>
public abstract class HeadingTagRenderer : IHtmlTagRenderer
{
    /// <summary>
    /// Initialises a new instance of the <see cref="HeadingTagRenderer"/> class.
    /// </summary>
    /// <param name="tagName">The name of the HTML tag to render.</param>
    protected HeadingTagRenderer(string tagName)
    {
        TagName = tagName;
    }

    /// <inheritdoc/>
    public string TagName { get; }

    /// <inheritdoc/>
    public string Render(IElement element, HtmlTagRenderingContext context) =>
        RenderHeading(element, context);
    
    private static string RenderHeading(IElement element, HtmlTagRenderingContext context)
    {
        var level = int.Parse(element.LocalName[1..]);
        return MarkdownFormatting.Block(
            $"{new string('#', level)} {MarkdownFormatting.CollapseWhitespace(context.RenderChildren(element))}");
    }
}