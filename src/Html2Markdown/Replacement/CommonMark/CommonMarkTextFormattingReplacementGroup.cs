namespace Html2Markdown.Replacement;

/// <summary>
/// A group of IReplacer to deal with converting HTML for
/// formatting text
/// </summary>
public class CommonMarkTextFormattingReplacementGroup : IReplacementGroup
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
			CustomAction = ReplaceLists
		},
		new CustomReplacer
		{
			CustomAction = HtmlParser.ReplaceAnchor
		}
	};

	private static string ReplaceLists(string html)
	{
		return HtmlParser.ReplaceLists(html, true);
	}

	public IEnumerable<IReplacer> Replacers()
	{
			return _replacements;
		}
}