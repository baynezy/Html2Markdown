namespace Html2Markdown.Replacement;

/// <summary>
/// A group of IReplacer to deal with removing illegal HTML.
/// </summary>
public class IllegalHtmlReplacementGroup : IReplacementGroup
{
    private readonly IList<IReplacer> _replacements = new List<IReplacer> {
        new DocTypeReplacer(),
        new HtmlTagReplacer(),
        new HeadTagReplacer(),
        new BodyTagReplacer(),
        new TitleTagReplacer(),
        new MetaTagReplacer(),
        new LinkTagReplacer(),
        new HtmlCommentReplacer(),
        new ScriptTagReplacer()
    };

    /// <summary>
    /// Returns the list of IReplacer instances.
    /// </summary>
    /// <returns>An IEnumerable of IReplacer.</returns>
    public IEnumerable<IReplacer> Replacers()
    {
        return _replacements;
    }
}