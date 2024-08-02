using Html2Markdown.Replacement;
using Html2Markdown.Replacement.CommonMark;

namespace Html2Markdown.Scheme;

/// <summary>
/// Collection of IReplacer for converting CommonMark Markdown
/// https://commonmark.org/
///
/// Currently supports :
/// * Syntax Highlighting
/// </summary>
public class CommonMark : AbstractScheme
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommonMark"/> class.
    /// Sets up the replacement groups for converting CommonMark Markdown.
    /// </summary>
    public CommonMark()
    {
        AddReplacementGroup(ReplacerCollection, new TextFormattingReplacementGroup());

        AddReplacementGroup(ReplacerCollection, new HeadingReplacementGroup());

        AddReplacementGroup(ReplacerCollection, new IllegalHtmlReplacementGroup());

        AddReplacementGroup(ReplacerCollection, new CommonMarkLayoutReplacementGroup());

        AddReplacementGroup(ReplacerCollection, new EntitiesReplacementGroup());
    }
}