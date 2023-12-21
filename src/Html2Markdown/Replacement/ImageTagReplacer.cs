namespace Html2Markdown.Replacement;
/// <summary>
/// Replaces HTML image tags with their Markdown equivalent.
/// </summary>
public class ImageTagReplacer : CustomReplacer
{
    public ImageTagReplacer()
    {
        CustomAction = HtmlParser.ReplaceImg;
    }
}