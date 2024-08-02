namespace Html2Markdown.Replacement;

/// <summary>
/// Replaces the HTML horizontal rule tag with its Markdown equivalent.
/// </summary>
public class HorizontalRuleTagReplacer : PatternReplacer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HorizontalRuleTagReplacer"/> class.
    /// Sets up the pattern and replacement for converting HTML horizontal rule tags to Markdown.
    /// </summary>
    public HorizontalRuleTagReplacer()
    {
        Pattern = "<hr[^>]*>";
        Replacement = Environment.NewLine + Environment.NewLine + "* * *" + Environment.NewLine;
    }
}