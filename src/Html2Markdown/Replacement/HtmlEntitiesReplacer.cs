namespace Html2Markdown.Replacement;
/// <summary>
/// Replaces HTML entities with their Markdown equivalent.
/// </summary>
public class HtmlEntitiesReplacer : CustomReplacer
{
    public HtmlEntitiesReplacer()
    {
        CustomAction = HtmlParser.ReplaceEntities;
    }
}