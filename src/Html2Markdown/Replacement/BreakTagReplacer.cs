namespace Html2Markdown.Replacement;

/// <summary>
/// Replaces the HTML break tag with its Markdown equivalent.
/// </summary>
public class BreakTagReplacer : PatternReplacer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BreakTagReplacer"/> class.
    /// Sets the pattern to match break tags and the replacement to a Markdown line break.
    /// </summary>
    public BreakTagReplacer()
    {
        Pattern = "<br[^>]*>";
        Replacement = "  " + Environment.NewLine;
    }
}