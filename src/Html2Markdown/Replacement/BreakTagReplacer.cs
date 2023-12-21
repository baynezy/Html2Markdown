namespace Html2Markdown.Replacement;
/// <summary>
/// Replaces the HTML break tag with its Markdown equivalent.
/// </summary>
public class BreakTagReplacer : PatternReplacer
{
    public BreakTagReplacer()
    {
        Pattern = "<br[^>]*>";
        Replacement = "  " + Environment.NewLine;
    }
}