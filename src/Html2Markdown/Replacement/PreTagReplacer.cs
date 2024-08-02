namespace Html2Markdown.Replacement;

/// <summary>
/// Replaces the pre tag with a Markdown equivalent.
/// </summary>
public class PreTagReplacer : CustomReplacer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PreTagReplacer"/> class.
    /// Sets the custom action to replace HTML pre tags.
    /// </summary>
    public PreTagReplacer()
    {
        CustomAction = HtmlParser.ReplacePre;
    }
}