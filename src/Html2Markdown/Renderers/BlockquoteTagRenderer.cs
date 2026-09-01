namespace Html2Markdown.Renderers;

/// <summary>
/// Renders a blockquote (&gt;blockquote&gt;) HTML element to Markdown.
/// </summary>
public sealed class BlockquoteTagRenderer : AbstractRenderer, IHtmlTagRenderer
{
    /// <inheritdoc/>
    public string TagName => "blockquote";

    /// <inheritdoc/>
    public string Render(IElement element, HtmlTagRenderingContext context) =>
        RenderBlockquote(element, context);

    private static string RenderBlockquote(IElement element, HtmlTagRenderingContext context)
    {
        var paragraph = element.Children.FirstOrDefault(child => child.LocalName == "p");
        if (paragraph is not null)
        {
            var paragraphContent = RenderBlockquoteChildren(paragraph, context)
                .Trim();
            var parts = paragraphContent.Split(["  \r\n", "  \n"], StringSplitOptions.None);
            if (parts.Length > 2)
            {
                return MarkdownFormatting.Block(string.Join(
                    Environment.NewLine,
                    paragraphContent
                        .Split(["\r\n", "\n"], StringSplitOptions.None)
                        .Select(line => $"> {line.TrimStart()}")));
            }

            paragraphContent = context.RenderChildren(paragraph)
                .Trim();
            parts = paragraphContent.Split(["  \r\n", "  \n"], StringSplitOptions.None);
            if (parts.Length == 2)
            {
                return MarkdownFormatting.Block(
                    $"> {parts[1].Trim()}{Environment.NewLine}> {Environment.NewLine}{Environment.NewLine}> {parts[0].Trim()}");
            }
        }

        var content = context.RenderChildren(element)
            .Trim();
        var quote = string.Join(
            Environment.NewLine,
            content.Split(["\r\n", "\n"], StringSplitOptions.None)
                .Select(line => $"> {line.TrimEnd()}"));

        return MarkdownFormatting.Block(quote);
    }
    
    private static string RenderBlockquoteChildren(INode parent, HtmlTagRenderingContext context)
    {
        StringBuilder builder = new();
        foreach (var child in parent.ChildNodes)
        {
            builder.Append(RenderBlockquoteInline(child, context));
        }

        return builder.ToString();
    }
    
    private static string RenderBlockquoteInline(INode node, HtmlTagRenderingContext context)
    {
        return node switch
        {
            IText text => text.Data,
            IElement {LocalName: "em" or "i"} element =>
                MarkdownFormatting.Wrap(RenderBlockquoteChildren(element, context), "_"),
            IElement {LocalName: "strong" or "b"} element =>
                MarkdownFormatting.Wrap(RenderBlockquoteChildren(element, context), "**"),
            IElement {LocalName: "br"} => RenderLineBreak(),
            IElement element => context.RenderWithDefaultContext(element),
            _ => string.Empty
        };
    }
}