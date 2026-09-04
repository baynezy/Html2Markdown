namespace Html2Markdown.Renderers;

/// <summary>
/// Base class for HTML tag renderers that handle specific HTML elements and convert them to Markdown.
/// </summary>
public abstract class AbstractRenderer
{
    /// <summary>
    /// Renders the text content of an HTML element as a Markdown code block or inline code, depending on whether it contains line breaks.
    /// </summary>
    /// <param name="element">The HTML element containing the code to render.</param>
    /// <returns>The rendered Markdown string.</returns>
    protected static string RenderCode(IElement element)
    {
        var code = element.TextContent;
        if (!code.Contains('\n') && !code.Contains('\r'))
        {
            return $"`{code}`";
        }

        return $"```{Environment.NewLine}{code.Trim('\r', '\n')}{Environment.NewLine}```";
    }
    
    /// <summary>
    /// Renders the children of an HTML element wrapped in a specified Markdown marker (e.g., for bold or italic text).
    /// </summary>
    /// <param name="element">The HTML element whose children to render.</param>
    /// <param name="context">The rendering context.</param>
    /// <param name="marker">The Markdown marker to wrap the content with.</param>
    /// <returns>The rendered Markdown string.</returns>
    protected static string RenderWrapped(IElement element, HtmlTagRenderingContext context, string marker) =>
        MarkdownFormatting.Wrap(context.RenderChildren(element), marker);

    /// <summary>
    /// Renders a line break in Markdown, represented by two spaces followed by a newline.
    /// </summary>
    /// <returns>The rendered Markdown string.</returns>
    protected static string RenderLineBreak() => "  " + Environment.NewLine;
}