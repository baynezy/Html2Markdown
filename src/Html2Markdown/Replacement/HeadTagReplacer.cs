namespace Html2Markdown.Replacement;
/// <summary>
/// Removes the doctype tag.
/// </summary>
public class HeadTagReplacer : PatternReplacer
{
    public HeadTagReplacer()
    {
        Pattern = "</?head[^>]*>";
        Replacement = "";
    }
}