using Html2Markdown.Replacement;

namespace Html2Markdown.Scheme;

/// <summary>
/// A group of IReplacer to deal with converting HTML entities
/// </summary>
public abstract class AbstractScheme : IScheme {
	protected readonly IList<IReplacer> ReplacerCollection = new List<IReplacer>();

	protected static void AddReplacementGroup(IList<IReplacer> replacers, IReplacementGroup replacementGroup)
	{
		replacementGroup.Replacers().ToList().ForEach(replacers.Add);
	}

	public IList<IReplacer> Replacers()
	{
		return ReplacerCollection;
	}
}