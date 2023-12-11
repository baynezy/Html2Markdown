using Html2Markdown.Replacement;

namespace Html2Markdown.Scheme;

/// <summary>
/// Collection of IReplacer for converting vanilla Markdown
/// See : https://daringfireball.net/projects/markdown/syntax
/// </summary>
public class Markdown : AbstractScheme
{
	public Markdown()
	{
			AddReplacementGroup(ReplacerCollection, new TextFormattingReplacementGroup());
			AddReplacementGroup(ReplacerCollection, new HeadingReplacementGroup());
			AddReplacementGroup(ReplacerCollection, new IllegalHtmlReplacementGroup());
			AddReplacementGroup(ReplacerCollection, new LayoutReplacementGroup());
			AddReplacementGroup(ReplacerCollection, new EntitiesReplacementGroup());
		}
}