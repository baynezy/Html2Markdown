namespace Html2Markdown.Replacement;

/// <summary>
/// Removes the HTML tag.
/// </summary>
public class HtmlTagReplacer : PatternReplacer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HtmlTagReplacer"/> class.
    /// Sets up the pattern and replacement for removing HTML tags.
    /// </summary>
    public HtmlTagReplacer()
    {
        Pattern = "</?html[^>]*>";
        Replacement = "";
    }
}