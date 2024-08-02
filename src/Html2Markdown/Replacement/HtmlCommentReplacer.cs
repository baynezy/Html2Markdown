namespace Html2Markdown.Replacement;

/// <summary>
/// Removes the HTML comment tag.
/// </summary>
public class HtmlCommentReplacer : PatternReplacer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HtmlCommentReplacer"/> class.
    /// Sets up the pattern and replacement for removing HTML comment tags.
    /// </summary>
    public HtmlCommentReplacer()
    {
        Pattern = "<!--[^-]+-->";
        Replacement = "";
    }
}