namespace Html2Markdown.Replacement;
/// <summary>
/// Removes the body tag.
/// </summary>
public class BodyTagReplacer : PatternReplacer
{
    public BodyTagReplacer()
    {
        Pattern = "</?body[^>]*>";
        Replacement = "";
    }
}