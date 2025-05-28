using Html2Markdown.Scheme;

namespace Html2Markdown.Test;

public class CommonMarkSchemeConverterTest : MarkdownSchemeConverterTest
{
    private readonly IScheme _scheme = new CommonMark();

    protected override Task CheckConversion(string html, IScheme scheme = null)
    {
	    return base.CheckConversion(html, _scheme);
    }

    [Fact]
    public Task Convert_WhenThereMultilineCodeTagsWithSyntaxHighlight_ThenReplaceWithMultilineMarkdownBlock001()
    {
        const string html = """
                            So this text has multiline code.
                            <code class='language-javascript'>
                            	&lt;p&gt;
                            		Some code we are looking at
                            	&lt;/p&gt;
                            </code>
                            """;

        return CheckConversion(html);
    }

    [Fact]
    public Task Convert_WhenThereMultilineCodeTagsWithSyntaxHighlight_ThenReplaceWithMultilineMarkdownBlock002()
    {
        const string html = """
                            So this text has multiline code.
                            <code class='javascript'>
                            	&lt;p&gt;
                            		Some code we are looking at
                            	&lt;/p&gt;
                            </code>
                            """;

        return CheckConversion(html);
    }

    [Fact]
    public Task Convert_WhenThereMultilineCodeTagsWithSyntaxHighlight_ThenReplaceWithMultilineMarkdownBlock003()
    {
        const string html = """
                            So this text has multiline code.
                            <code class='lang-javascript'>
                            	&lt;p&gt;
                            		Some code we are looking at
                            	&lt;/p&gt;
                            </code>
                            """;

        return CheckConversion(html);
    }
}