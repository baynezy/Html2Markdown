using Html2Markdown.Replacement;

namespace Html2Markdown.Scheme;

/// <summary>
/// A group of IReplacer to deal with converting HTML entities.
/// </summary>
public abstract class AbstractScheme : IScheme {
    
    /// <summary>
    /// The collection of IReplacer instances used for replacements.
    /// </summary>
    protected readonly IList<IReplacer> ReplacerCollection = new List<IReplacer>();

    /// <summary>
    /// Adds a group of replacements to the provided list of replacers.
    /// </summary>
    /// <param name="replacers">The list of replacers to add to.</param>
    /// <param name="replacementGroup">The group of replacements to add.</param>
    protected static void AddReplacementGroup(IList<IReplacer> replacers, IReplacementGroup replacementGroup)
    {
        replacementGroup.Replacers().ToList().ForEach(replacers.Add);
    }

    /// <summary>
    /// Gets the collection of IReplacer instances.
    /// </summary>
    /// <returns>The list of IReplacer instances.</returns>
    public IList<IReplacer> Replacers()
    {
        return ReplacerCollection;
    }
}