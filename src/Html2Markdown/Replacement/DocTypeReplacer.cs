namespace Html2Markdown.Replacement;

/// <summary>
/// Removes the doctype tag.
/// </summary>
public class DocTypeReplacer : PatternReplacer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocTypeReplacer"/> class.
    /// Sets the pattern to match doctype tags and the replacement to an empty string.
    /// </summary>
    public DocTypeReplacer()
    {
        Pattern = "<!DOCTYPE[^>]*>";
        Replacement = "";
    }
}