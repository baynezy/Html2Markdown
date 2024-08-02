namespace Html2Markdown.Replacement;

/// <summary>
/// Removes the title tag.
/// </summary>
public class TitleTagReplacer : PatternReplacer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TitleTagReplacer"/> class.
    /// Sets up the pattern and replacement for removing title tags.
    /// </summary>
    public TitleTagReplacer()
    {
        Pattern = "<title[^>]*>.*?</title>";
        Replacement = "";
    }
}