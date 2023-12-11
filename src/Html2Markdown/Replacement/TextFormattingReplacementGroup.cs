namespace Html2Markdown.Replacement;

/// <summary>
/// A group of IReplacer to deal with converting HTML for
/// formatting text
/// </summary>
public class TextFormattingReplacementGroup : IReplacementGroup
{
	private readonly IList<IReplacer> _replacements = new List<IReplacer> {
		new PatternReplacer
		{
			Pattern = @"<(?:strong|b)>(\s+)",
			Replacement = " **"
		},
		new PatternReplacer
		{
			Pattern = "<(?:strong|b)>",
			Replacement = "**"
		},
		new PatternReplacer
		{
			Pattern = @"(\s+)</(strong|b)>",
			Replacement = "** "
		},
		new PatternReplacer
		{
			Pattern = "</(strong|b)>",
			Replacement = "**"
		},
		new PatternReplacer
		{
			Pattern = @"<(?:em|i)>(\s+)",
			Replacement = " *"
		},
		new PatternReplacer
		{
			Pattern = "<(?:em|i)>",
			Replacement = "*"
		},
		new PatternReplacer
		{
			Pattern = @"(\s+)</(em|i)>",
			Replacement = "* "
		},
		new PatternReplacer
		{
			Pattern = "</(em|i)>",
			Replacement = "*"
		},
		new CustomReplacer
		{
			CustomAction = HtmlParser.ReplaceImg
		},
		new CustomReplacer
		{
			CustomAction = HtmlParser.ReplaceLists
		},
		new CustomReplacer
		{
			CustomAction = HtmlParser.ReplaceAnchor
		}
	};

	public IEnumerable<IReplacer> Replacers()
	{
			return _replacements;
		}
}