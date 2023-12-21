namespace Html2Markdown.Replacement;
/// <summary>
/// Removes the title tag.
/// </summary>
public class TitleTagReplacer : PatternReplacer
{
    public TitleTagReplacer()
    {
        Pattern = "<title[^>]*>.*?</title>";
        Replacement = "";
    }
}