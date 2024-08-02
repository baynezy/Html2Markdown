using Html2Markdown.Replacement;

namespace Html2Markdown.Scheme;

/// <summary>
/// Collection of IReplacer for converting vanilla Markdown
/// See : https://daringfireball.net/projects/markdown/syntax
/// </summary>
public class Markdown : AbstractScheme
{
	/// <summary>
	/// Initializes a new instance of the <see cref="Markdown"/> class.
	/// Sets up the replacement groups for converting vanilla Markdown.
	/// </summary>
	public Markdown()
	{
		AddReplacementGroup(ReplacerCollection, new TextFormattingReplacementGroup());
		AddReplacementGroup(ReplacerCollection, new HeadingReplacementGroup());
		AddReplacementGroup(ReplacerCollection, new IllegalHtmlReplacementGroup());
		AddReplacementGroup(ReplacerCollection, new LayoutReplacementGroup());
		AddReplacementGroup(ReplacerCollection, new EntitiesReplacementGroup());
	}
}