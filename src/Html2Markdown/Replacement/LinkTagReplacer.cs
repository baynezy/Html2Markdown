namespace Html2Markdown.Replacement;
/// <summary>
/// Removes the link tag.
/// </summary>
public class LinkTagReplacer : PatternReplacer
{
    public LinkTagReplacer()
    {
        Pattern = "<link[^>]*>";
        Replacement = "";
    }
}