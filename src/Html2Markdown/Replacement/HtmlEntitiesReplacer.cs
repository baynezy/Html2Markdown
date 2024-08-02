namespace Html2Markdown.Replacement;

/// <summary>
/// Replaces HTML entities with their Markdown equivalent.
/// </summary>
public class HtmlEntitiesReplacer : CustomReplacer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HtmlEntitiesReplacer"/> class.
    /// Sets the custom action to replace HTML entities.
    /// </summary>
    public HtmlEntitiesReplacer()
    {
        CustomAction = HtmlParser.ReplaceEntities;
    }
}