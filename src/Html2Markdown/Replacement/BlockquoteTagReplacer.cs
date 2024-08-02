namespace Html2Markdown.Replacement;

/// <summary>
/// Replaces a blockquote tag with the appropriate Markdown format.
/// </summary>
public class BlockquoteTagReplacer : CustomReplacer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlockquoteTagReplacer"/> class.
    /// Sets the custom action to replace blockquote tags with Markdown formatted blockquotes.
    /// </summary>
    public BlockquoteTagReplacer()
    {
        CustomAction = HtmlParser.ReplaceBlockquote;
    }
}