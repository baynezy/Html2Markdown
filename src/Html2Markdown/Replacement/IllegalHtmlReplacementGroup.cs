namespace Html2Markdown.Replacement;

/// <summary>
/// A group of IReplacer to deal with removing illegal HTML
/// </summary>
public class IllegalHtmlReplacementGroup : IReplacementGroup
{
	private readonly IList<IReplacer> _replacements = new List<IReplacer> {
		new PatternReplacer
		{
			Pattern = @"<!DOCTYPE[^>]*>",
			Replacement = ""
		},
		new PatternReplacer
		{
			Pattern = @"</?html[^>]*>",
			Replacement = ""
		},
		new PatternReplacer
		{
			Pattern = @"</?head[^>]*>",
			Replacement = ""
		},
		new PatternReplacer
		{
			Pattern = @"</?body[^>]*>",
			Replacement = ""
		},
		new PatternReplacer
		{
			Pattern = @"<title[^>]*>.*?</title>",
			Replacement = ""
		},
		new PatternReplacer
		{
			Pattern = @"<meta[^>]*>",
			Replacement = ""
		},
		new PatternReplacer
		{
			Pattern = @"<link[^>]*>",
			Replacement = ""
		},
		new PatternReplacer
		{
			Pattern = @"<!--[^-]+-->",
			Replacement = ""
		},
		new PatternReplacer
		{
			Pattern = @"</?script[^>]*>",
			Replacement = ""
		}
	};

	public IEnumerable<IReplacer> Replacers()
	{
			return _replacements;
		}
}