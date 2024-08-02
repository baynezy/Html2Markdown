namespace Html2Markdown.Replacement;

/// <summary>
/// A group of <see cref="IReplacer"/> to deal with converting HTML entities.
/// </summary>
public class EntitiesReplacementGroup : IReplacementGroup
{
    private readonly IList<IReplacer> _replacements = new List<IReplacer> {
        new HtmlEntitiesReplacer()
    };

    /// <summary>
    /// Returns the list of <see cref="IReplacer"/> instances.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="IReplacer"/>.</returns>
    public IEnumerable<IReplacer> Replacers()
    {
        return _replacements;
    }
}