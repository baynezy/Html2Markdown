namespace Html2Markdown.Replacement;

/// <summary>
/// Replaces the HTML heading tag with its Markdown equivalent.
/// </summary>
public class HeadingTagReplacer : CustomReplacer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HeadingTagReplacer"/> class.
    /// Sets up the patterns and replacements for converting HTML heading tags to Markdown.
    /// </summary>
    /// <param name="heading">The heading level to be replaced (e.g., H1, H2).</param>
    public HeadingTagReplacer(Heading heading)
    {
        var headingNumber = (int) heading;

        CustomAction = html => HtmlParser.ReplaceHeading(html, headingNumber);
    }
}