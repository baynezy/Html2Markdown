namespace Html2Markdown.Replacement;
/// <summary>
/// Removes the script tag.
/// </summary>
public class ScriptTagReplacer : PatternReplacer
{
    public ScriptTagReplacer()
    {
        Pattern = "</?script[^>]*>";
        Replacement = "";
    }
}