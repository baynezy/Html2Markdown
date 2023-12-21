namespace Html2Markdown.Replacement;
/// <summary>
/// Replaces HTML lists with their Markdown equivalent.
/// </summary>
public class HtmlListReplacer : CustomReplacer
{
    public HtmlListReplacer()
    {
        CustomAction = HtmlParser.ReplaceLists;
    }
}