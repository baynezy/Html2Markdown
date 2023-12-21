namespace Html2Markdown.Replacement;
/// <summary>
/// Replaces the HTML code tag with its Markdown equivalent.
/// </summary>
public class CodeTagReplacer : CustomReplacer
{
    public CodeTagReplacer()
    {
        CustomAction = HtmlParser.ReplaceCode;
    }

    public CodeTagReplacer(bool supportSyntaxHighlighting)
    {
        CustomAction = html => HtmlParser.ReplaceCode(html, supportSyntaxHighlighting);
    }
}