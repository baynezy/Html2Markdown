namespace Html2Markdown.Replacement.CommonMark;

/// <summary>
/// A group of IReplacer to deal with converting HTML that is
/// used for layout
/// </summary>
public class CommonMarkLayoutReplacementGroup : IReplacementGroup
{
	private readonly IList<IReplacer> _replacements = new List<IReplacer> {
		new HorizontalRuleTagReplacer(),
		new CodeTagReplacer(true),
		new PreTagReplacer(),
		new ParagraphTagReplacer(),
		new BreakTagReplacer(),
		new BlockquoteTagReplacer()
	};

	public IEnumerable<IReplacer> Replacers()
	{
		return _replacements;
	}
}