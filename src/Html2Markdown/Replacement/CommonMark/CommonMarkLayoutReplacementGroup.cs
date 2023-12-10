namespace Html2Markdown.Replacement.CommonMark;

/// <summary>
/// A group of IReplacer to deal with converting HTML that is
/// used for layout
/// </summary>
public class CommonMarkLayoutReplacementGroup : IReplacementGroup
{
	private readonly IList<IReplacer> _replacements = new List<IReplacer> {
		new PatternReplacer
		{
			Pattern = @"<hr[^>]*>",
			Replacement = Environment.NewLine + Environment.NewLine + "* * *" + Environment.NewLine
		},
		new CustomReplacer
		{
			CustomAction = ReplaceCode
		},
		new CustomReplacer
		{
			CustomAction = HtmlParser.ReplacePre
		},
		new CustomReplacer
		{
			CustomAction = HtmlParser.ReplaceParagraph
		},
		new PatternReplacer
		{
			Pattern = @"<br[^>]*>",
			Replacement = @"  " + Environment.NewLine
		},
		new CustomReplacer
		{
			CustomAction = HtmlParser.ReplaceBlockquote
		}
	};

	private static string ReplaceCode(string html)
	{
		return HtmlParser.ReplaceCode(html, true);
	}

	public IEnumerable<IReplacer> Replacers()
	{
			return _replacements;
		}
}