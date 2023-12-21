namespace Html2Markdown.Replacement;
/// <summary>
/// Removes the HTML comment tag.
/// </summary>
public class HtmlCommentReplacer : PatternReplacer
{
    public HtmlCommentReplacer()
    {
        Pattern = "<!--[^-]+-->";
        Replacement = "";
    }
}