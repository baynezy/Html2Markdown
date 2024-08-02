namespace Html2Markdown.Replacement;

/// <summary>
/// A group of IReplacer to deal with converting HTML that is
/// used for layout.
/// </summary>
public class LayoutReplacementGroup : IReplacementGroup
{
    private readonly IList<IReplacer> _replacements = new List<IReplacer> {
        new HorizontalRuleTagReplacer(),
        new CodeTagReplacer(),
        new PreTagReplacer(),
        new ParagraphTagReplacer(),
        new BreakTagReplacer(),
        new BlockquoteTagReplacer()
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