namespace Html2Markdown.Replacement;

/// <summary>
/// A group of IReplacer to deal with converting HTML entities
/// </summary>
public class EntitiesReplacementGroup : IReplacementGroup
{
	private readonly IList<IReplacer> _replacements = new List<IReplacer> {
		new HtmlEntitiesReplacer()
	};

	public IEnumerable<IReplacer> Replacers()
	{
		return _replacements;
	}
}