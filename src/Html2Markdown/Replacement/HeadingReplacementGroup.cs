namespace Html2Markdown.Replacement;

/// <summary>
/// A group of IReplacer to deal with converting HTML headers
/// </summary>
public class HeadingReplacementGroup : IReplacementGroup
{
	private readonly IList<IReplacer> _replacements = new List<IReplacer> {
		new PatternReplacer
		{
			Pattern = "</h[1-6]>",
			Replacement = Environment.NewLine + Environment.NewLine
		},
		new PatternReplacer
		{
			Pattern = "<h1[^>]*>",
			Replacement = Environment.NewLine + Environment.NewLine + "# "
		},
		new PatternReplacer
		{
			Pattern = "<h2[^>]*>",
			Replacement = Environment.NewLine + Environment.NewLine + "## "
		},
		new PatternReplacer
		{
			Pattern = "<h3[^>]*>",
			Replacement = Environment.NewLine + Environment.NewLine + "### "
		},
		new PatternReplacer
		{
			Pattern = "<h4[^>]*>",
			Replacement = Environment.NewLine + Environment.NewLine + "#### "
		},
		new PatternReplacer
		{
			Pattern = "<h5[^>]*>",
			Replacement = Environment.NewLine + Environment.NewLine + "##### "
		},
		new PatternReplacer
		{
			Pattern = "<h6[^>]*>",
			Replacement = Environment.NewLine + Environment.NewLine + "###### "
		}
	};

	public IEnumerable<IReplacer> Replacers()
	{
			return _replacements;
		}
}