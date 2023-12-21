namespace Html2Markdown.Replacement;
/// <summary>
/// Removes the HTML tag.
/// </summary>
public class HtmlTagReplacer : PatternReplacer
{
    public HtmlTagReplacer()
    {
        Pattern = "</?html[^>]*>";
        Replacement = "";
    }
}