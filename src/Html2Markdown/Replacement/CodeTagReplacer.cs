namespace Html2Markdown.Replacement;

/// <summary>
/// Replaces the HTML code tag with its Markdown equivalent.
/// </summary>
public class CodeTagReplacer : CustomReplacer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CodeTagReplacer"/> class.
    /// Sets the custom action to replace code tags with Markdown formatted code blocks.
    /// </summary>
    public CodeTagReplacer()
    {
        CustomAction = html => HtmlParser.ReplaceCode(html, false);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodeTagReplacer"/> class with an option to support syntax highlighting.
    /// Sets the custom action to replace code tags with Markdown formatted code blocks, optionally supporting syntax highlighting.
    /// </summary>
    /// <param name="supportSyntaxHighlighting">If set to <c>true</c>, supports syntax highlighting in the Markdown output.</param>
    public CodeTagReplacer(bool supportSyntaxHighlighting)
    {
        CustomAction = html => HtmlParser.ReplaceCode(html, supportSyntaxHighlighting);
    }
}