namespace Html2Markdown.Replacement;

/// <summary>
/// Replace the paragraph tag with its Markdown equivalent.
/// </summary>
public class ParagraphTagReplacer : CustomReplacer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ParagraphTagReplacer"/> class.
    /// Sets the custom action to replace HTML paragraph tags.
    /// </summary>
    public ParagraphTagReplacer()
    {
        CustomAction = HtmlParser.ReplaceParagraph;
    }
}