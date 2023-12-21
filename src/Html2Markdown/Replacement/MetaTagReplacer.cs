namespace Html2Markdown.Replacement;
/// <summary>
/// Removes the meta tag.
/// </summary>
public class MetaTagReplacer : PatternReplacer
{
    public MetaTagReplacer()
    {
        Pattern = "<meta[^>]*>";
        Replacement = "";
    }
}