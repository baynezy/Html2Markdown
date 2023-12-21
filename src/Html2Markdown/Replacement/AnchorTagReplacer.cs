namespace Html2Markdown.Replacement;
/// <summary>
/// Replaces an anchor tag with the link text and the link URL in Markdown format.
/// </summary>
public class AnchorTagReplacer : CustomReplacer
{
    public AnchorTagReplacer()
    {
        CustomAction = HtmlParser.ReplaceAnchor;
    }
}