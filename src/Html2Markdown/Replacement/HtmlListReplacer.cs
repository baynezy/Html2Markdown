namespace Html2Markdown.Replacement;

/// <summary>
/// Replaces HTML lists with their Markdown equivalent.
/// </summary>
public class HtmlListReplacer : CustomReplacer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HtmlListReplacer"/> class.
    /// Sets the custom action to replace HTML lists.
    /// </summary>
    public HtmlListReplacer()
    {
        CustomAction = HtmlParser.ReplaceLists;
    }
}