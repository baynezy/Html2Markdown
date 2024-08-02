namespace Html2Markdown.Replacement;

/// <summary>
/// Removes the script tag.
/// </summary>
public class ScriptTagReplacer : PatternReplacer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScriptTagReplacer"/> class.
    /// Sets up the pattern and replacement for removing script tags.
    /// </summary>
    public ScriptTagReplacer()
    {
        Pattern = "</?script[^>]*>";
        Replacement = "";
    }
}