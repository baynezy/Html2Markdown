namespace Html2Markdown.Replacement;

/// <summary>
/// Removes the body tag.
/// </summary>
public class BodyTagReplacer : PatternReplacer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BodyTagReplacer"/> class.
    /// Sets the pattern to match body tags and the replacement to an empty string.
    /// </summary>
    public BodyTagReplacer()
    {
        Pattern = "</?body[^>]*>";
        Replacement = "";
    }
}