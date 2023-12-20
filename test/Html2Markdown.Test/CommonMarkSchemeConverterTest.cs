using Html2Markdown.Scheme;

namespace Html2Markdown.Test;

public class CommonMarkSchemeConverterTest : MarkdownSchemeConverterTest
{
    private readonly IScheme _scheme = new CommonMark();

    protected override Task CheckConversion(string html, IScheme scheme = null)
    {
	    return base.CheckConversion(html, _scheme);
    }

    [Test]
    public Task Convert_WhenThereMultilineCodeTagsWithSyntaxHighlight_ThenReplaceWithMultilineMarkdownBlock001()
    {
        const string html = @"So this text has multiline code.
<code class='language-javascript'>
	&lt;p&gt;
		Some code we are looking at
	&lt;/p&gt;
</code>";

        return CheckConversion(html);
    }

    [Test]
    public Task Convert_WhenThereMultilineCodeTagsWithSyntaxHighlight_ThenReplaceWithMultilineMarkdownBlock002()
    {
        const string html = @"So this text has multiline code.
<code class='javascript'>
	&lt;p&gt;
		Some code we are looking at
	&lt;/p&gt;
</code>";

        return CheckConversion(html);
    }

    [Test]
    public Task Convert_WhenThereMultilineCodeTagsWithSyntaxHighlight_ThenReplaceWithMultilineMarkdownBlock003()
    {
        const string html = @"So this text has multiline code.
<code class='lang-javascript'>
	&lt;p&gt;
		Some code we are looking at
	&lt;/p&gt;
</code>";

        return CheckConversion(html);
    }

    [Test]
    public Task Convert_WhenThereIsAnOrderedListWithNestedParagraphs_ThenReplaceWithMarkdownLists()
    {
	    const string html = @"<p>This code is with an ordered list and paragraphs.</p><ol><li><p>Yes, this is a <code>code</code> element</p></li><li><p>No :</p><ul><li><code>Some code we are looking at</code></li></ul></li></ol>";

	    return CheckConversion(html);
    }

    [Test]
    public Task Convert_WhenThereIsAMultilineOrderedListWithNestedParagraphsAndCodeElement_ThenReplaceWithMarkdownLists()
    {
	    const string html = @"<p>This code is with an ordered list and paragraphs.</p>
<ol>
<li><p>Yes, this is a <code>code</code> element</p>
</li>
<li><p>No :</p>
<ul>
<li><code>Some code we are looking at</code></li>
</ul>
</li>
</ol>
";

	    return CheckConversion(html);
    }
}