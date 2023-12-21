namespace Html2Markdown.Replacement;

/// <summary>
/// A group of IReplacer to deal with converting HTML for
/// formatting text
/// </summary>
public class TextFormattingReplacementGroup : IReplacementGroup
{
	private readonly IList<IReplacer> _replacements = new List<IReplacer> {
		new StrongTagReplacer(),
		new EmphasisTagReplacer(),
		new ImageTagReplacer(),
		new HtmlListReplacer(),
		new AnchorTagReplacer()
	};

	public IEnumerable<IReplacer> Replacers()
	{
		return _replacements;
	}
}