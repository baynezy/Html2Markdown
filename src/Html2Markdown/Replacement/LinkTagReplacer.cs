namespace Html2Markdown.Replacement;

/// <summary>
/// Removes the link tag.
/// </summary>
public class LinkTagReplacer : PatternReplacer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LinkTagReplacer"/> class.
    /// Sets up the pattern and replacement for removing link tags.
    /// </summary>
    public LinkTagReplacer()
    {
        Pattern = "<link[^>]*>";
        Replacement = "";
    }
}