namespace Html2Markdown.Replacement;

/// <summary>
/// Replaces HTML image tags with their Markdown equivalent.
/// </summary>
public class ImageTagReplacer : CustomReplacer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ImageTagReplacer"/> class.
    /// Sets the custom action to replace HTML image tags.
    /// </summary>
    public ImageTagReplacer()
    {
        CustomAction = HtmlParser.ReplaceImg;
    }
}