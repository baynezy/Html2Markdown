namespace Html2Markdown.Replacement;

/// <summary>
/// Removes the doctype tag.
/// </summary>
public class HeadTagReplacer : PatternReplacer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HeadTagReplacer"/> class.
    /// Sets up the pattern and replacement for removing HTML head tags.
    /// </summary>
    public HeadTagReplacer()
    {
        Pattern = "</?head[^>]*>";
        Replacement = "";
    }
}