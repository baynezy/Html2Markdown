namespace Html2Markdown.Replacement;
/// <summary>
/// Replace the paragraph tag with its Markdown equivalent.
/// </summary>
public class ParagraphTagReplacer : CustomReplacer
{
    public ParagraphTagReplacer()
    {
        CustomAction = HtmlParser.ReplaceParagraph;
    }
}