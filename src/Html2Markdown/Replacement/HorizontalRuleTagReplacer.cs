namespace Html2Markdown.Replacement;
/// <summary>
/// Replaces the HTML horizontal rule tag with its Markdown equivalent.
/// </summary>
public class HorizontalRuleTagReplacer : PatternReplacer
{
    public HorizontalRuleTagReplacer()
    {
        Pattern = "<hr[^>]*>";
        Replacement = Environment.NewLine + Environment.NewLine + "* * *" + Environment.NewLine;
    }
}