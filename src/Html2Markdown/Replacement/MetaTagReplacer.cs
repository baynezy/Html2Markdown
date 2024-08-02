namespace Html2Markdown.Replacement;

/// <summary>
/// Removes the meta tag.
/// </summary>
public class MetaTagReplacer : PatternReplacer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MetaTagReplacer"/> class.
    /// Sets up the pattern and replacement for removing meta tags.
    /// </summary>
    public MetaTagReplacer()
    {
        Pattern = "<meta[^>]*>";
        Replacement = "";
    }
}