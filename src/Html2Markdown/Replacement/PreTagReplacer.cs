namespace Html2Markdown.Replacement;
/// <summary>
/// Replaces the pre tag with a Markdown equivalent.
/// </summary>
public class PreTagReplacer : CustomReplacer
{
    public PreTagReplacer()
    {
        CustomAction = HtmlParser.ReplacePre;
    }
}