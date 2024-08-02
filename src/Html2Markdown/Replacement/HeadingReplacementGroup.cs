namespace Html2Markdown.Replacement;

/// <summary>
/// A group of <see cref="IReplacer"/> to deal with converting HTML headers.
/// </summary>
public class HeadingReplacementGroup : IReplacementGroup
{
    private readonly IList<IReplacer> _replacements = new List<IReplacer> {
        new HeadingTagReplacer(Heading.H1),
        new HeadingTagReplacer(Heading.H2),
        new HeadingTagReplacer(Heading.H3),
        new HeadingTagReplacer(Heading.H4),
        new HeadingTagReplacer(Heading.H5),
        new HeadingTagReplacer(Heading.H6)
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