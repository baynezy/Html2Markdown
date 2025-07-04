using System;
using Html2Markdown.Scheme;

namespace Html2Markdown.Test;

public class MarkdownSchemeConverterTest
{

	#region Schemes

	[Fact]
	public void Converter_WhenProvidingMarkdownAsACustomScheme_ThenShouldConvertEquivalentlyToNoScheme()
	{
		// arrange
		const string html = """So this is <a href="http://www.simonbaynes.com/">a link</a>. Convert it""";

		var scheme = new Markdown();

		var converterWithScheme = new Converter(scheme);
		var converterWithoutScheme = new Converter();
		
		// act
		var resultWithScheme = converterWithScheme.Convert(html);
		var resultWithoutScheme = converterWithoutScheme.Convert(html);
		
		// assert
		resultWithoutScheme.Should().Be(resultWithScheme);
	}

	#endregion

	#region Links
		
	[Fact]
	public Task Convert_WhenThereAreHtmlLinks_ThenConvertToMarkDownLinks()
	{
		const string html = """So this is <a href="http://www.simonbaynes.com/">a link</a>. Convert it""";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreHtmlLinksWithAttributesAfterTheHref_ThenConvertToMarkDownLink()
	{
		const string html = """So this is <a href="http://www.simonbaynes.com/" alt="example">a link</a>. Convert it""";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreHtmlLinksWithAttributesBeforeTheHref_ThenConvertToMarkDownLink()
	{
		const string html = """So this is <a alt="example" href="http://www.simonbaynes.com/">a link</a>. Convert it""";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreHtmlLinksWithTitleAttributeAfterTheHref_ThenConvertToMarkDownLink()
	{
		const string html = """So this is <a href="http://www.simonbaynes.com/" title="example">a link</a>. Convert it""";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreHtmlLinksWithTitleAttributeBeforeTheHref_ThenConvertToMarkDownLink()
	{
		const string html = """So this is <a title="example" href="http://www.simonbaynes.com/">a link</a>. Convert it""";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreMultipleHtmlLinks_ThenConvertThemToMarkDownLinks()
	{
		const string html = """So this is <a href="http://www.simonbaynes.com/">a link</a> and so is <a href="http://www.google.com/">this</a>. Convert them""";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreEmptyLinks_ThenRemoveThemFromResult()
	{
		const string html = """So this is <a name="curio"></a> and so is <a href="http://www.google.com/">this</a>. Convert them""";

		return CheckConversion(html);
	}

	#endregion

	#region Strong and Bold Tags

	[Fact]
	public Task Convert_WhenThereAreStrongTags_ThenConvertToMarkDownDoubleAsterisks()
	{
		const string html = "So this text is <strong>bold</strong>. Convert it.";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreStrongTagsFollowedByASpace_ThenConvertToMarkDownDoubleAsterisks()
	{
		const string html = "This is a<strong> test</strong> that causes problems.";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreStrongTagsFollowedByAMultipleSpaces_ThenConvertToMarkDownDoubleAsterisks()
	{
		const string html = "This is a<strong>  test</strong> that causes problems.";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreClosingStrongTagsFollowedByASpace_ThenConvertToMarkDownDoubleAsterisks()
	{
		const string html = "This is a <strong>test </strong>that causes problems.";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreClosingStrongTagsFollowedByAMultipleSpaces_ThenConvertToMarkDownDoubleAsterisks()
	{
		const string html = "This is a <strong>test  </strong>that causes problems.";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreMultipleStrongTags_ThenConvertToMarkDownDoubleAsterisks()
	{
		const string html = "So this text is <strong>bold</strong> and <strong>is this</strong>. Convert it.";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreBoldTags_ThenConvertToMarkDownDoubleAsterisks()
	{
		const string html = "So this text is <b>bold</b>. Convert it.";

		return CheckConversion(html);
	}
	
	[Fact]
	public Task Convert_WhenThereIsABoldTagWithProperties_ThenConvertToMarkDownDoubleAsterisks()
	{
		const string html = """So this text is <b id="something">bold</b>. Convert it.""";

		return CheckConversion(html);
	}
	
	[Fact]
	public Task Convert_WhenThereIsAStrongTagWithProperties_ThenConvertToMarkDownDoubleAsterisks()
	{
		const string html = """So this text is <strong id="something">bold</strong>. Convert it.""";

		return CheckConversion(html);
	}

	#endregion

	#region Emphasis and Italic Tags

	[Fact]
	public Task Convert_WhenThereAreEmphasisTags_ThenConvertToMarkDownSingleAsterisk()
	{
		const string html = "So this text is <em>italic</em>. Convert it.";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreEmphasisTagsFollowedByASpace_ThenConvertToMarkDownSingleAsterisk()
	{
		const string html = "This is a<em> test</em> that causes problems.";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreEmphasisTagsFollowedByMultipleSpaces_ThenConvertToMarkDownSingleAsterisk()
	{
		const string html = "This is a<em>  test</em> that causes problems.";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreClosingEmphasisTagsFollowedByASpace_ThenConvertToMarkDownSingleAsterisk()
	{
		const string html = "This is a <em>test </em>that causes problems.";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreClosingEmphasisTagsFollowedByMultipleSpaces_ThenConvertToMarkDownSingleAsterisk()
	{
		const string html = "This is a <em>test  </em>that causes problems.";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreItalicTags_ThenConvertToMarkDownSingleAsterisk()
	{
		const string html = "So this text is <i>italic</i>. Convert it.";

		return CheckConversion(html);
	}

	#endregion

	#region Break tags
		
	[Fact]
	public Task Convert_WhenThereAreBreakTags_ThenConvertToMarkDownDoubleSpacesWitCarriageReturns()
	{
		const string html = "So this text has a break.<br/>Convert it.";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreBreakTagsWithWhitespace_ThenConvertToMarkDownDoubleSpacesWitCarriageReturns()
	{
		const string html = "So this text has a break.<br />Convert it.";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreBreakTagsThatAreNotSelfClosing_ThenConvertToMarkDownDoubleSpacesWitCarriageReturns()
	{
		const string html = "So this text has a break.<br>Convert it.";

		return CheckConversion(html);
	}

	#endregion

	#region Code Tags

	[Fact]
	public Task Convert_WhenThereAreCodeTags_ThenReplaceWithBackTick()
	{
		const string html = "So this text has code <code>alert();</code>. Convert it.";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereMultilineCodeTags_ThenReplaceWithMultilineMarkdownBlock001()
	{
		const string html = """
		                    So this text has multiline code.
		                    <code>
		                    &lt;p&gt;
		                    	Some code we are looking at
		                    &lt;/p&gt;
		                    </code>
		                    """;

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereMultilineCodeTags_ThenReplaceWithMultilineMarkdownBlock002()
	{
		const string html = """
		                    So this text has multiline code.
		                    <code>
		                    	&lt;p&gt;
		                    		Some code we are looking at
		                    	&lt;/p&gt;
		                    </code>
		                    """;

		return CheckConversion(html);
	}

	/// https://github.com/baynezy/Html2Markdown/issues/112
	[Fact]
	public Task Convert_WhenThereMultilineCodeTags_ThenReplaceWithMultilineMarkdownBlock003()
	{
		const string html = """
		                    <code>
		                    	class solution {<br>
		                    		int i;<br>
		                    		string name = “name”;<br>
		                    	}
		                    </code>
		                    """;

		return CheckConversion(html);
	}

	#endregion

	#region Header Tags

	[Fact]
	public Task Convert_WhenThereAreH1Tags_ThenReplaceWithMarkDownHeader()
	{
		const string html = "This code has a <h1>header</h1>. Convert it.";

		return CheckConversion(html);
	}
	
	[Fact]
	public Task Convert_WhenThereAreH2Tags_ThenReplaceWithMarkDownHeader()
	{
		const string html = "This code has a <h2>header</h2>. Convert it.";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreH3Tags_ThenReplaceWithMarkDownHeader()
	{
		const string html = "This code has a <h3>header</h3>. Convert it.";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreH4Tags_ThenReplaceWithMarkDownHeader()
	{
		const string html = "This code has a <h4>header</h4>. Convert it.";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreH5Tags_ThenReplaceWithMarkDownHeader()
	{
		const string html = "This code has a <h5>header</h5>. Convert it.";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreH6Tags_ThenReplaceWithMarkDownHeader()
	{
		const string html = "This code has a <h6>header</h6>. Convert it.";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreH1TagsWithAttributes_ThenReplaceWithMarkDownHeader()
	{
		const string html = """This code has a <h1 title="header">header</h1>. Convert it.""";

		return CheckConversion(html);
	}


	[Fact]
	public Task Convert_WhenThereAreH2TagsWithAttributes_ThenReplaceWithMarkDownHeader()
	{
		const string html = """This code has a <h2 title="header">header</h2>. Convert it.""";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreH3TagsWithAttributes_ThenReplaceWithMarkDownHeader()
	{
		const string html = """This code has a <h3 title="header">header</h3>. Convert it.""";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreH4TagsWithAttributes_ThenReplaceWithMarkDownHeader()
	{
		const string html = """This code has a <h4 title="header">header</h4>. Convert it.""";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreH5TagsWithAttributes_ThenReplaceWithMarkDownHeader()
	{
		const string html = """This code has a <h5 title="header">header</h5>. Convert it.""";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreH6TagsWithAttributes_ThenReplaceWithMarkDownHeader()
	{
		const string html = """This code has a <h6 title="header">header</h6>. Convert it.""";

		return CheckConversion(html);
	}

	#endregion

	#region Blockquote Tags
		
	[Fact]
	public Task Convert_WhenThereAreBlockquoteTags_ThenReplaceWithMarkDownBlockQuote()
	{
		const string html = "This code has a <blockquote>blockquote</blockquote>. Convert it.";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereIsABlockquoteTagWithNestedHtml_ThenReplaceWithMarkDownBlockQuote()
	{
		const string html = "<blockquote><em>“Qualquer coisa que possas fazer ou sonhar, podes começá-la. A ousadia encerra em si mesma genialidade, poder e magia.<br />Ouse fazer, e o poder lhe será dado!”</em><br /><strong>— Johann Wolfgang von Goethe</strong></blockquote>";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereIsAMultilineBlockquoteTag_ThenReplaceWithMarkDownBlockQuote()
	{
		const string html = """
		                    <blockquote>
		                        <p class="right" align="right"><em>“Ao estipular seus objetivos, mire na Lua; pois mesmo que aconteça de você não alcançá-los, ainda estará entre as estrelas!”</em><br />
		                        <strong>— Dr. Lair Ribeiro</strong></p>
		                      </blockquote>
		                    """;

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereIsABlockquoteTagWithAttributes_ThenReplaceWithMarkDownBlockQuote()
	{
		const string html = """This code has a <blockquote id="thing">blockquote</blockquote>. Convert it.""";

		return CheckConversion(html);
	}

	#endregion

	#region Paragraph Tags
		
	[Fact]
	public Task Convert_WhenThereAreParagraphTags_ThenReplaceWithDoubleLineBreakBeforeAndOneAfter()
	{
		const string html = "This code has no markup.<p>This code is in a paragraph.</p>Convert it!";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreParagraphTagsWithAttributes_ThenReplaceWithDoubleLineBreakBeforeAndOneAfter()
	{
		const string html = """This code has no markup.<p class="something">This code is in a paragraph.</p>Convert it!""";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreParagraphTagsWithNewLinesInThem_ThenReplaceWithMarkdownParagraphButNoBreakTags()
	{
		const string html = """
		                    <p>
		                      text
		                      text
		                      text
		                    </p>
		                    """;

		return CheckConversion(html);
	}

	#endregion

	#region Horizontal Rule Tags
		
	[Fact]
	public Task Convert_WhenThereAreHorizontalRuleTags_ThenReplaceWithMarkDownHorizontalRule()
	{
		const string html = "This code is seperated by a horizontal rule.<hr/>Convert it!";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreHorizontalRuleTagsWithWhiteSpace_ThenReplaceWithMarkDownHorizontalRule()
	{
		const string html = "This code is seperated by a horizontal rule.<hr />Convert it!";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreHorizontalRuleTagsWithAttributes_ThenReplaceWithMarkDownHorizontalRule()
	{
		const string html = """This code is seperated by a horizontal rule.<hr class="something" />Convert it!""";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreHorizontalRuleTagsThatAreNonSelfClosing_ThenReplaceWithMarkDownHorizontalRule()
	{
		const string html = "This code is seperated by a horizontal rule.<hr>Convert it!";

		return CheckConversion(html);
	}

	#endregion

	#region Image Tags
		
	[Fact]
	public Task Convert_WhenThereAreImgTags_ThenReplaceWithMarkdownImage()
	{
		const string html = """This code is with and image <img alt="something" title="convert" src="https://assets-cdn.github.com/images/spinners/octocat-spinner-32.gif" /> Convert it!""";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreImgTagsWithoutATitle_ThenReplaceWithMarkdownImage()
	{
		const string html = """This code is with an image <img alt="something" src="https://assets-cdn.github.com/images/spinners/octocat-spinner-32.gif" /> Convert it!""";

		return CheckConversion(html);
	}

	#endregion

	#region Pre Tags
		
	[Fact]
	public Task Convert_WhenThereArePreTags_ThenReplaceWithMarkdownPre()
	{
		const string html = """
		                    This code is with a pre tag <pre>
		                    	Predefined text</pre>
		                    """;

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreOtherTagsNestedInThePreTag_ThenReplaceWithMarkdownPre()
	{
		const string html = """
		                    <pre><code>Install-Package Html2Markdown
		                    </code></pre>
		                    """;

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreMultiplePreTags_ThenReplaceWithMarkdownPre()
	{
		const string html = """
		                    <h2>Installing via NuGet</h2>

		                    <pre><code>Install-Package Html2Markdown
		                    </code></pre>

		                    <h2>Usage</h2>

		                    <pre><code>var converter = new Converter();
		                    var result = converter.Convert(html);
		                    </code></pre>
		                    """;

		return CheckConversion(html);
	}

	#endregion

	#region Lists
		
	[Fact]
	public Task Convert_WhenThereAreUnorderedLists_ThenReplaceWithMarkdownLists()
	{
		const string html = "This code is with an unordered list.<ul><li>Yes</li><li>No</li></ul>";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreEmptyUnorderedLists_ThenReplaceWithNothing()
	{
		const string html = "This code is with an unordered list.<ul></ul>";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreUnorderedListsWihtoutClosingTags_ThenReplaceWithMarkdownLists()
	{
		const string html = "This code is with an unordered list.<ul><li>Yes<li>No</ul>";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereAreOrderedLists_ThenReplaceWithMarkdownLists()
	{
		const string html = "This code is with an ordered list.<ol><li>Yes</li><li>No</li></ol>";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereIsAnUnorderedListWithANestedOrderList_ThenReplaceWithMarkdownLists()
	{
		const string html = "This code is with an unordered list.<ul><li>Yes</li><li><ol><li>No</li><li>Maybe</li></ol></li></ul>";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereIsAnOrderedListWithANestedUnorderedList_ThenReplaceWithMarkdownLists()
	{
		const string html = "This code is with an unordered list.<ol><li>Yes</li><li><ul><li>No</li><li>Maybe</li></ul></li></ol>";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereIsAnOrderedListWithNestedParagraphs_ThenReplaceWithMarkdownLists()
	{
		const string html = @"<p>This code is with an ordered list and paragraphs.</p><ol><li><p>Yes, this is a <code>code</code> element</p></li><li><p>No :</p><ul><li><code>Some code we are looking at</code></li></ul></li></ol>";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereIsAMultilineOrderedListWithNestedParagraphsAndCodeElement_ThenReplaceWithMarkdownLists()
	{
		const string html = """
		                    <p>This code is with an ordered list and paragraphs.</p>
		                    <ol>
		                    <li><p>Yes, this is a <code>code</code> element</p>
		                    </li>
		                    <li><p>No :</p>
		                    <ul>
		                    <li><code>Some code we are looking at</code></li>
		                    </ul>
		                    </li>
		                    </ol>

		                    """;

		return CheckConversion(html);
	}
	
	// See issue https://github.com/baynezy/Html2Markdown/issues/395
	[Fact]
	public Task Convert_WhenThereAreMultipleUnorderedLists_ThenReplaceWithMarkdownLists()
	{
		const string html = "<ul><ul><li>first list val</li></ul></ul>";

		return CheckConversion(html);
	}
	
	// See issue https://github.com/baynezy/Html2Markdown/issues/109
	[Fact]
	public Task Convert_WhenThereIsAnEmptyList_ThenRemoveTheList()
	{
		const string html = "<ul><li></li></ul>";

		return CheckConversion(html);
	}
	
	// See issue https://github.com/baynezy/Html2Markdown/issues/170
	[Fact]
	public Task Convert_WhenThereIsAnOrderedListWithValueAttribute_ThenReplaceWithNumberedMarkdownList()
	{
		const string html = "<ol><li>First</li><li value=\"100\">Hundredth</li></ol>";

		return CheckConversion(html);
	}
	
	// See issue https://github.com/baynezy/Html2Markdown/issues/110
	[Fact]
	public Task Convert_WhenThereAreThreeLevelNestedLists_ThenReplaceWithCorrectIndentation()
	{
		const string html = """
		                    <ul>
		                        <li><a>Level 1</a>
		                            <ul>
		                                <li><a>Level 2</a>
		                                </li>
		                                <li><a>Level 2</a>
		                                </li>
		                                <li><a>Level 2</a>
		                                    <ul>
		                                        <li><a>Level 3</a></li>
		                                        <li><a>Level 3</a></li>
		                                    </ul>
		                                </li>
		                                <li><a>Level 2</a>
		                                </li>
		                                <li><a>Level 2</a>
		                                </li>
		                            </ul>
		                        </li>
		                    </ul>
		                    """;

		return CheckConversion(html);
	}

	#endregion

	#region Extra HTML Removal
		
	[Fact]
	public Task Convert_WhenThereIsAnHtmlDoctype_ThenRemoveFromResult()
	{
		const string html = """
		                    <!DOCTYPE HTML PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
		                    Doctypes should be removed
		                    """;

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereIsAnHtmlTag_ThenRemoveFromResult()
	{
		const string html = """
		                    <html>
		                    <p>HTML tags should be removed</p>
		                    </html>
		                    """;

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereIsAnHtmlTagWithAttributes_ThenRemoveFromResult()
	{
		const string html = """
		                    <html xmlns="http://www.w3.org/1999/xhtml" xml:lang="pt-br">
		                    <p>HTML tags should be removed</p>
		                    </html>
		                    """;

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereIsASingleLineComment_ThenRemoveFromResult()
	{
		const string html = """
		                    <!-- a comment -->
		                    <p>Comments should be removed</p>
		                    """;

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereIsAMultiLineComment_ThenRemoveFromResult()
	{
		const string html = """
		                    <!-- 
		                    a comment
		                    -->
		                    <p>Comments should be removed</p>
		                    """;

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereIsAHeadTag_ThenRemoveFromResult()
	{
		const string html = """
		                    <head>
		                    <p>HTML tags should be removed</p>
		                    </head>
		                    """;

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereIsAHeadTagWithAttributes_ThenRemoveFromResult()
	{
		const string html = """
		                    <head id="something">
		                    <p>HTML tags should be removed</p>
		                    </head>
		                    """;

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereIsAMetaTag_ThenRemoveFromResult()
	{
		const string html = """
		                    <meta name="language" content="pt-br">
		                    <p>Meta tags should be removed</p>
		                    """;

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereIsATitleTag_ThenRemoveFromResult()
	{
		const string html = """
		                    <title>Remove me</title>
		                    <p>Title tags should be removed</p>
		                    """;

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereIsATitleTagWithAttributes_ThenRemoveFromResult()
	{
		const string html = """
		                    <title id="something">Remove me</title>
		                    <p>Title tags should be removed</p>
		                    """;

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereIsALinkTag_ThenRemoveFromResult()
	{
		const string html = """
		                    <link type="text/css" rel="stylesheet" href="https://dl.dropboxusercontent.com/u/28729896/modelo-similar-blog-ss-para-sublime-text.css">
		                    <p>Link tags should be removed</p>
		                    """;

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereIsABodyTag_ThenRemoveFromResult()
	{
		const string html = """
		                    <body>
		                    <p>Body tags should be removed</p>
		                    </body>
		                    """;

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereIsABodyTagWithAttributes_ThenRemoveFromResult()
	{
		const string html = """
		                    <body id="something">
		                    <p>Body tags should be removed</p>
		                    </body>
		                    """;

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereIsAScriptTag_ThenRemoveFromResult() 
	{
		const string html = """
		                    <!DOCTYPE html>
		                    <html>
		                    <head>
		                    	<script src="scripts/jquery.min.js" type="text/javascript">
		                    	</script>
		                    </head>
		                    <body>
		                    Hello World
		                    </body>
		                    </html>
		                    """;

		return CheckConversion(html);
	}
		
	[Fact]
	public Task Convert_WhenAListHasNoItems_ThenRemoveTheList()
	{
		const string html = "<ul></ul>";

		return CheckConversion(html);
	}

	// See issue https://github.com/baynezy/Html2Markdown/issues/269
	[Fact]
	public Task Convert_Bug269()
	{
		const string html = "<ol> Test </ol>";

		return CheckConversion(html);
	} 

	#endregion

	#region Entities
		
	[Fact]
	public Task Convert_WhenThereIsAnAmpersandEntity_ThenReplaceWithActualCharacter()
	{
		const string html = "<p>Enties like &amp; should be converted</p>";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereIsAnLessThanEntity_ThenReplaceWithActualCharacter()
	{
		const string html = "<p>Enties like &lt; should be converted</p>";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereIsAGreaterThanEntity_ThenReplaceWithActualCharacter()
	{
		const string html = "<p>Enties like &gt; should be converted</p>";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereIsABulletEntity_ThenReplaceWithActualCharacter()
	{
		const string html = "<p>Enties like &#8226; should be converted</p>";

		return CheckConversion(html);
	}

	#endregion

	#region Complex Tests
		
	[Fact]
	public Task Convert_ComplexTest_001()
	{
		var html =
			"<p>This is the second part of a two part series about building real-time web applications with server-sent events.</p>" + Environment.NewLine + Environment.NewLine + "<ul>\r\n<li><a href=\"http://bayn.es/real-time-web-applications-with-server-sent-events-pt-1/\">Building Web Apps with Server-Sent Events - Part 1</a></li>\r\n</ul>" + Environment.NewLine + Environment.NewLine + "<h2 id=\"reconnecting\">Reconnecting</h2>" + Environment.NewLine + Environment.NewLine + "<p>In this post we are going to look at handling reconnection if the browser loses contact with the server. Thankfully the native JavaScript functionality for SSEs (the <a href=\"https://developer.mozilla.org/en-US/docs/Web/API/EventSource\">EventSource</a>) handles this natively. You just need to make sure that your server-side implementation supports the mechanism.</p>" + Environment.NewLine + Environment.NewLine + "<p>When the server reconnects your SSE end point it will send a special HTTP header <code>Last-Event-Id</code> in the reconnection request. In the previous part of this blog series we looked at just sending events with the <code>data</code> component. Which looked something like this:-</p>" + Environment.NewLine + Environment.NewLine + "<pre><code>data: The payload we are sending\\n\\n\r\n</code></pre>" + Environment.NewLine + Environment.NewLine + "<p>Now while this is enough to make the events make it to your client-side implementation. We need more information to handle reconnection. To do this we need to add an event id to the output.</p>" + Environment.NewLine + Environment.NewLine + "<p>E.g.</p>" + Environment.NewLine + Environment.NewLine + "<pre><code>id: 1439887379635\\n\r\ndata: The payload we are sending\\n\\n\r\n</code></pre>" + Environment.NewLine + Environment.NewLine + "<p>The important thing to understand here is that each event needs a unique identifier, so that the client can communicate back to the server (using the <code>Last-Event-Id</code> header) which was the last event it received on reconnection.</p>" + Environment.NewLine + Environment.NewLine + "<h2 id=\"persistence\">Persistence</h2>" + Environment.NewLine + Environment.NewLine + "<p>In our previous example we used <a href=\"http://redis.io/topics/pubsub\">Redis Pub/Sub</a> to inform <a href=\"https://nodejs.org/\">Node.js</a> that it needs to push a new SSE to the client. Redis Pub/Sub is a topic communication which means it will be delivered to all <em>connected clients</em>, and then it will be removed from the topic. So there is no persistence for when clients reconnect. To implement this we need to add a persistence layer and so in this demo I have chosen to use <a href=\"https://www.mongodb.org/\">MongoDB</a>.</p>" + Environment.NewLine + Environment.NewLine + "<p>Essentially we will be pushing events into both Redis and MongoDB. Redis will still be our method of initiating an SSE getting sent to the browser, but we will also be be storing that event into MongoDB so we can query it on a reconnection to get anything we've missed.</p>" + Environment.NewLine + Environment.NewLine + "<h2 id=\"thecode\">The Code</h2>" + Environment.NewLine + Environment.NewLine + "<p>OK so let us look at how we can actually implement this.</p>" + Environment.NewLine + Environment.NewLine + "<h3 id=\"updateserverevent\">Update ServerEvent</h3>" + Environment.NewLine + Environment.NewLine + "<p>We need to update the ServerEvent object to support having an <code>id</code> for an event.</p>" + Environment.NewLine + Environment.NewLine + "<pre><code>function ServerEvent(name) {\r\n    this.name = name || \"\";\r\n    this.data = \"\";\r\n};" + Environment.NewLine + Environment.NewLine + "ServerEvent.prototype.addData = function(data) {\r\n    var lines = data.split(/\\n/);" + Environment.NewLine + Environment.NewLine + "    for (var i = 0; i &lt; lines.length; i++) {\r\n        var element = lines[i];\r\n        this.data += \"data:\" + element + \"\\n\";\r\n    }\r\n}" + Environment.NewLine + Environment.NewLine + "ServerEvent.prototype.payload = function() {\r\n    var payload = \"\";\r\n    if (this.name != \"\") {\r\n        payload += \"id: \" + this.name + \"\\n\";\r\n    }" + Environment.NewLine + Environment.NewLine + "    payload += this.data;\r\n    return payload + \"\\n\";\r\n}\r\n</code></pre>" + Environment.NewLine + Environment.NewLine + "<p>This is pretty straightforward string manipulation and won't impress anyone, but it is foundation for what will follow.</p>" + Environment.NewLine + Environment.NewLine + "<h3 id=\"storeeventsinmongodb\">Store Events in MongoDB</h3>" + Environment.NewLine + Environment.NewLine + "<p>We need to update the <code>post.js</code> code to also store new events in MongoDB.</p>" + Environment.NewLine + Environment.NewLine + "<pre><code>app.put(\"/api/post-update\", function(req, res) {\r\n    var json = req.body;\r\n    json.timestamp = Date.now();" + Environment.NewLine + Environment.NewLine + "    eventStorage.save(json).then(function(doc) {\r\n        dataChannel.publish(JSON.stringify(json));\r\n    }, errorHandling);" + Environment.NewLine + Environment.NewLine + "    res.status(204).end();\r\n});\r\n</code></pre>" + Environment.NewLine + Environment.NewLine + "<p>The <code>event-storage</code> module looks as follows:</p>" + Environment.NewLine + Environment.NewLine + "<pre><code>var Q = require(\"q\"),\r\n    config = require(\"./config\"),\r\n    mongo = require(\"mongojs\"),\r\n    db = mongo(config.mongoDatabase),\r\n    collection = db.collection(config.mongoScoresCollection);" + Environment.NewLine + Environment.NewLine + "module.exports.save = function(data) {\r\n    var deferred = Q.defer();\r\n    collection.save(data, function(err, doc){\r\n        if(err) {\r\n            deferred.reject(err);\r\n        }\r\n        else {\r\n            deferred.resolve(doc);\r\n        }\r\n    });" + Environment.NewLine + Environment.NewLine + "    return deferred.promise;\r\n};\r\n</code></pre>" + Environment.NewLine + Environment.NewLine + "<p>Here we are just using basic MongoDB commands to save a new event into the collection. Yep that is it, we are now additionally persisting the events so they can be retrieved later.</p>" + Environment.NewLine + Environment.NewLine + "<h3 id=\"retrievingeventsonreconnection\">Retrieving Events on Reconnection</h3>" + Environment.NewLine + Environment.NewLine + "<p>When an <code>EventSource</code> reconnects after a disconnection it passes a special header <code>Last-Event-Id</code>. So we need to look for that and return the events that got broadcast while the client was disconnected.</p>" + Environment.NewLine + Environment.NewLine + "<pre><code>app.get(\"/api/updates\", function(req, res){\r\n    initialiseSSE(req, res);" + Environment.NewLine + Environment.NewLine + "    if (typeof(req.headers[\"last-event-id\"]) != \"undefined\") {\r\n        replaySSEs(req, res);\r\n    }\r\n});" + Environment.NewLine + Environment.NewLine + "function replaySSEs(req, res) {\r\n    var lastId = req.headers[\"last-event-id\"];" + Environment.NewLine + Environment.NewLine + "    eventStorage.findEventsSince(lastId).then(function(docs) {\r\n        for (var index = 0; index &lt; docs.length; index++) {\r\n            var doc = docs[index];\r\n            var messageEvent = new ServerEvent(doc.timestamp);\r\n            messageEvent.addData(doc.update);\r\n            outputSSE(req, res, messageEvent.payload());\r\n        }\r\n    }, errorHandling);\r\n};\r\n</code></pre>" + Environment.NewLine + Environment.NewLine + "<p>What we are doing here is querying MongoDB for the events that were missed. We then iterate over them and output them to the browser.</p>" + Environment.NewLine + Environment.NewLine + "<p>The code for querying MongoDB is as follows:</p>" + Environment.NewLine + Environment.NewLine + "<pre><code>module.exports.findEventsSince = function(lastEventId) {\r\n    var deferred = Q.defer();" + Environment.NewLine + Environment.NewLine + "    collection.find({\r\n        timestamp: {$gt: Number(lastEventId)}\r\n    })\r\n    .sort({timestamp: 1}, function(err, docs) {\r\n        if (err) {\r\n            deferred.reject(err);\r\n        }\r\n        else {\r\n            deferred.resolve(docs);\r\n        }\r\n    });" + Environment.NewLine + Environment.NewLine + "    return deferred.promise;\r\n};\r\n</code></pre>" + Environment.NewLine + Environment.NewLine + "<h2 id=\"testing\">Testing</h2>" + Environment.NewLine + Environment.NewLine + "<p>To test this you will need to run both apps at the same time.</p>" + Environment.NewLine + Environment.NewLine + "<pre><code>node app.js\r\n</code></pre>" + Environment.NewLine + Environment.NewLine + "<p>and </p>" + Environment.NewLine + Environment.NewLine + "<pre><code>node post.js\r\n</code></pre>" + Environment.NewLine + Environment.NewLine + "<p>Once they are running open two browser windows <a href=\"http://localhost:8181/\">http://localhost:8181/</a> and <a href=\"http://localhost:8082/api/post-update\">http://localhost:8082/api/post-update</a></p>" + Environment.NewLine + Environment.NewLine + "<p>Now you can post updates as before. If you stop <code>app.js</code> but continue posting events, when you restart <code>app.js</code> within 10 seconds the <code>EventSource</code> will reconnect. This will deliver all missed events.</p>" + Environment.NewLine + Environment.NewLine + "<h2 id=\"conclusion\">Conclusion</h2>" + Environment.NewLine + Environment.NewLine + "<p>This very simple code gives you a very elegant and powerful push architecture to create real-time apps.</p>" + Environment.NewLine + Environment.NewLine + "<h3 id=\"improvements\">Improvements</h3>" + Environment.NewLine + Environment.NewLine + "<p>A possible improvement would be to render the events from MongoDB server-side when the page is first output. Then we would get updates client-side as they are pushed to the browser.</p>" + Environment.NewLine + Environment.NewLine + "<h3 id=\"download\">Download</h3>" + Environment.NewLine + Environment.NewLine + "<p>If you want to play with this application you can fork or browse it on <a href=\"https://github.com/baynezy/RealtimeDemo/tree/part-2\">GitHub</a>.</p>";

		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_ComplexTest_002()
	{
		const string html = """
		                    <p>Some other HTML</p>

		                    <blockquote>
		                    <p class="right" align="right"><em>“Qualquer coisa que possas fazer ou sonhar, podes começá-la. A ousadia encerra em si mesma genialidade, poder e magia.<br />Ouse fazer, e o poder lhe será dado!”</em><br /><strong>— Johann Wolfgang von Goethe</strong></p>
		                    </blockquote>
		                    """;

		return CheckConversion(html);
	}

	// Issue #81 https://github.com/baynezy/Html2Markdown/issues/81
	[Fact]
	public void Convert_WhenConvertingContentFromIssue81_ThenShouldNotError()
	{
		const string content = "\n\n<p style=\"margin:0in;font-family:Calibri;font-size:11.0pt;\"><span style=\"font-weight:bold;\">Repro steps amended functionality:</span> Followed\nduplication plan. Location field now only shows location at the booking's site. Testing passed.</p>\n\n<p style=\"margin:0in;font-family:Calibri;font-size:11.0pt;\">&nbsp;</p>\n\n<p style=\"margin:0in;font-family:Calibri;font-size:11.0pt;\"><span style=\"font-weight:bold;\">Exploratory Testing</span>: n/a</p>\n\n<p style=\"margin:0in;font-family:Calibri;font-size:11.0pt;\">&nbsp;</p>\n\n<p style=\"margin:0in;font-family:Calibri;font-size:11.0pt;\"><span style=\"font-weight:bold;\">Areas Affected and Tested: </span>Inpatient Bookings, Bed Move screen<br></p>\n\n";

		var converter = new Converter();
		
		Action action = () => converter.Convert(content);
		
		action.Should().NotThrow();
	}

	[Fact]
	public Task Convert_WhenThereAreParagraphTagsEitherSideOfAList_ThenThereShouldBeAppropriateSpacing()
	{
		const string html = "<p>a</p><ul><li>First</li><li>Last</li></ul><p>b</p>";
			
		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_WhenThereIsAParagraphTagAfterAList_ThenThereShouldBeAppropriateSpacing()
	{
		const string html = "<p>a</p><ul><li>First</li><li>Last</li></ul>b";
			
		return CheckConversion(html);
	}

	[Fact]
	public Task Convert_Bug_114() // https://github.com/baynezy/Html2Markdown/issues/114
	{
		const string html = """
		                    <html>
		                    <body>
		                    <h2 style="box-sizing: border-box; margin-top: 24px; margin-bottom: 16px; font-size: 1.5em; font-weight: 600; line-height: 1.25; padding-bottom: 0.3em; border-bottom: 1px solid var(--color-border-muted); color: rgb(201, 209, 217); font-family: -apple-system, BlinkMacSystemFont, &quot;Segoe UI&quot;, Helvetica, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(13, 17, 23); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;">Support</h2><p style="box-sizing: border-box; margin-top: 0px; margin-bottom: 16px; color: rgb(201, 209, 217); font-family: -apple-system, BlinkMacSystemFont, &quot;Segoe UI&quot;, Helvetica, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(13, 17, 23); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;">This project will currently convert the following HTML tags:-</p>
		                    <ul style="box-sizing: border-box; padding-left: 2em; margin-top: 0px; margin-bottom: 16px; color: rgb(201, 209, 217); font-family: -apple-system, BlinkMacSystemFont, &quot;Segoe UI&quot;, Helvetica, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(13, 17, 23); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;">
		                    <li style="box-sizing: border-box;"><code style="box-sizing: border-box; font-family: ui-monospace, SFMono-Regular, &quot;SF Mono&quot;, Menlo, Consolas, &quot;Liberation Mono&quot;, monospace; font-size: 13.6px; padding: 0.2em 0.4em; margin: 0px; background-color: var(--color-neutral-muted); border-radius: 6px;">&lt;a&gt;</code></li>
		                    <li style="box-sizing: border-box; margin-top: 0.25em;"><code style="box-sizing: border-box; font-family: ui-monospace, SFMono-Regular, &quot;SF Mono&quot;, Menlo, Consolas, &quot;Liberation Mono&quot;, monospace; font-size: 13.6px; padding: 0.2em 0.4em; margin: 0px; background-color: var(--color-neutral-muted); border-radius: 6px;">&lt;strong&gt;</code></li>
		                    <li style="box-sizing: border-box; margin-top: 0.25em;"><code style="box-sizing: border-box; font-family: ui-monospace, SFMono-Regular, &quot;SF Mono&quot;, Menlo, Consolas, &quot;Liberation Mono&quot;, monospace; font-size: 13.6px; padding: 0.2em 0.4em; margin: 0px; background-color: var(--color-neutral-muted); border-radius: 6px;">&lt;b&gt;</code></li>
		                    <li style="box-sizing: border-box; margin-top: 0.25em;"><code style="box-sizing: border-box; font-family: ui-monospace, SFMono-Regular, &quot;SF Mono&quot;, Menlo, Consolas, &quot;Liberation Mono&quot;, monospace; font-size: 13.6px; padding: 0.2em 0.4em; margin: 0px; background-color: var(--color-neutral-muted); border-radius: 6px;">&lt;em&gt;</code></li>
		                    <li style="box-sizing: border-box; margin-top: 0.25em;"><code style="box-sizing: border-box; font-family: ui-monospace, SFMono-Regular, &quot;SF Mono&quot;, Menlo, Consolas, &quot;Liberation Mono&quot;, monospace; font-size: 13.6px; padding: 0.2em 0.4em; margin: 0px; background-color: var(--color-neutral-muted); border-radius: 6px;">&lt;i&gt;</code></li>
		                    <li style="box-sizing: border-box; margin-top: 0.25em;"><code style="box-sizing: border-box; font-family: ui-monospace, SFMono-Regular, &quot;SF Mono&quot;, Menlo, Consolas, &quot;Liberation Mono&quot;, monospace; font-size: 13.6px; padding: 0.2em 0.4em; margin: 0px; background-color: var(--color-neutral-muted); border-radius: 6px;">&lt;br&gt;</code></li>
		                    <li style="box-sizing: border-box; margin-top: 0.25em;"><code style="box-sizing: border-box; font-family: ui-monospace, SFMono-Regular, &quot;SF Mono&quot;, Menlo, Consolas, &quot;Liberation Mono&quot;, monospace; font-size: 13.6px; padding: 0.2em 0.4em; margin: 0px; background-color: var(--color-neutral-muted); border-radius: 6px;">&lt;code&gt;</code></li>
		                    <li style="box-sizing: border-box; margin-top: 0.25em;"><code style="box-sizing: border-box; font-family: ui-monospace, SFMono-Regular, &quot;SF Mono&quot;, Menlo, Consolas, &quot;Liberation Mono&quot;, monospace; font-size: 13.6px; padding: 0.2em 0.4em; margin: 0px; background-color: var(--color-neutral-muted); border-radius: 6px;">&lt;h1&gt;</code></li>
		                    <li style="box-sizing: border-box; margin-top: 0.25em;"><code style="box-sizing: border-box; font-family: ui-monospace, SFMono-Regular, &quot;SF Mono&quot;, Menlo, Consolas, &quot;Liberation Mono&quot;, monospace; font-size: 13.6px; padding: 0.2em 0.4em; margin: 0px; background-color: var(--color-neutral-muted); border-radius: 6px;">&lt;h2&gt;</code></li>
		                    <li style="box-sizing: border-box; margin-top: 0.25em;"><code style="box-sizing: border-box; font-family: ui-monospace, SFMono-Regular, &quot;SF Mono&quot;, Menlo, Consolas, &quot;Liberation Mono&quot;, monospace; font-size: 13.6px; padding: 0.2em 0.4em; margin: 0px; background-color: var(--color-neutral-muted); border-radius: 6px;">&lt;h3&gt;</code></li>
		                    <li style="box-sizing: border-box; margin-top: 0.25em;"><code style="box-sizing: border-box; font-family: ui-monospace, SFMono-Regular, &quot;SF Mono&quot;, Menlo, Consolas, &quot;Liberation Mono&quot;, monospace; font-size: 13.6px; padding: 0.2em 0.4em; margin: 0px; background-color: var(--color-neutral-muted); border-radius: 6px;">&lt;h4&gt;</code></li>
		                    <li style="box-sizing: border-box; margin-top: 0.25em;"><code style="box-sizing: border-box; font-family: ui-monospace, SFMono-Regular, &quot;SF Mono&quot;, Menlo, Consolas, &quot;Liberation Mono&quot;, monospace; font-size: 13.6px; padding: 0.2em 0.4em; margin: 0px; background-color: var(--color-neutral-muted); border-radius: 6px;">&lt;h5&gt;</code></li>
		                    <li style="box-sizing: border-box; margin-top: 0.25em;"><code style="box-sizing: border-box; font-family: ui-monospace, SFMono-Regular, &quot;SF Mono&quot;, Menlo, Consolas, &quot;Liberation Mono&quot;, monospace; font-size: 13.6px; padding: 0.2em 0.4em; margin: 0px; background-color: var(--color-neutral-muted); border-radius: 6px;">&lt;h6&gt;</code></li>
		                    <li style="box-sizing: border-box; margin-top: 0.25em;"><code style="box-sizing: border-box; font-family: ui-monospace, SFMono-Regular, &quot;SF Mono&quot;, Menlo, Consolas, &quot;Liberation Mono&quot;, monospace; font-size: 13.6px; padding: 0.2em 0.4em; margin: 0px; background-color: var(--color-neutral-muted); border-radius: 6px;">&lt;blockquote&gt;</code></li>
		                    <li style="box-sizing: border-box; margin-top: 0.25em;"><code style="box-sizing: border-box; font-family: ui-monospace, SFMono-Regular, &quot;SF Mono&quot;, Menlo, Consolas, &quot;Liberation Mono&quot;, monospace; font-size: 13.6px; padding: 0.2em 0.4em; margin: 0px; background-color: var(--color-neutral-muted); border-radius: 6px;">&lt;img&gt;</code></li>
		                    <li style="box-sizing: border-box; margin-top: 0.25em;"><code style="box-sizing: border-box; font-family: ui-monospace, SFMono-Regular, &quot;SF Mono&quot;, Menlo, Consolas, &quot;Liberation Mono&quot;, monospace; font-size: 13.6px; padding: 0.2em 0.4em; margin: 0px; background-color: var(--color-neutral-muted); border-radius: 6px;">&lt;hr&gt;</code></li>
		                    <li style="box-sizing: border-box; margin-top: 0.25em;"><code style="box-sizing: border-box; font-family: ui-monospace, SFMono-Regular, &quot;SF Mono&quot;, Menlo, Consolas, &quot;Liberation Mono&quot;, monospace; font-size: 13.6px; padding: 0.2em 0.4em; margin: 0px; background-color: var(--color-neutral-muted); border-radius: 6px;">&lt;p&gt;</code></li>
		                    <li style="box-sizing: border-box; margin-top: 0.25em;"><code style="box-sizing: border-box; font-family: ui-monospace, SFMono-Regular, &quot;SF Mono&quot;, Menlo, Consolas, &quot;Liberation Mono&quot;, monospace; font-size: 13.6px; padding: 0.2em 0.4em; margin: 0px; background-color: var(--color-neutral-muted); border-radius: 6px;">&lt;pre&gt;</code></li>
		                    <li style="box-sizing: border-box; margin-top: 0.25em;"><code style="box-sizing: border-box; font-family: ui-monospace, SFMono-Regular, &quot;SF Mono&quot;, Menlo, Consolas, &quot;Liberation Mono&quot;, monospace; font-size: 13.6px; padding: 0.2em 0.4em; margin: 0px; background-color: var(--color-neutral-muted); border-radius: 6px;">&lt;ul&gt;</code></li>
		                    <li style="box-sizing: border-box; margin-top: 0.25em;"><code style="box-sizing: border-box; font-family: ui-monospace, SFMono-Regular, &quot;SF Mono&quot;, Menlo, Consolas, &quot;Liberation Mono&quot;, monospace; font-size: 13.6px; padding: 0.2em 0.4em; margin: 0px; background-color: var(--color-neutral-muted); border-radius: 6px;">&lt;ol&gt;</code></li>
		                    </ul>
		                    <h2 style="box-sizing: border-box; margin-top: 24px; margin-bottom: 16px; font-size: 1.5em; font-weight: 600; line-height: 1.25; padding-bottom: 0.3em; border-bottom: 1px solid var(--color-border-muted); color: rgb(201, 209, 217); font-family: -apple-system, BlinkMacSystemFont, &quot;Segoe UI&quot;, Helvetica, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(13, 17, 23); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;"><a id="user-content-installing-via-nuget" class="anchor" aria-hidden="true" href="https://github.com/baynezy/Html2Markdown#installing-via-nuget" style="box-sizing: border-box; background-color: transparent; color: var(--color-accent-fg); text-decoration: none; float: left; padding-right: 4px; margin-left: -20px; line-height: 1;"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.775 3.275a.75.75 0 001.06 1.06l1.25-1.25a2 2 0 112.83 2.83l-2.5 2.5a2 2 0 01-2.83 0 .75.75 0 00-1.06 1.06 3.5 3.5 0 004.95 0l2.5-2.5a3.5 3.5 0 00-4.95-4.95l-1.25 1.25zm-4.69 9.64a2 2 0 010-2.83l2.5-2.5a2 2 0 012.83 0 .75.75 0 001.06-1.06 3.5 3.5 0 00-4.95 0l-2.5 2.5a3.5 3.5 0 004.95 4.95l1.25-1.25a.75.75 0 00-1.06-1.06l-1.25 1.25a2 2 0 01-2.83 0z"></path></svg></a>Installing via NuGet</h2><p style="box-sizing: border-box; margin-top: 0px; margin-bottom: 16px; color: rgb(201, 209, 217); font-family: -apple-system, BlinkMacSystemFont, &quot;Segoe UI&quot;, Helvetica, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(13, 17, 23); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;"><a href="http://badge.fury.io/nu/Html2Markdown" rel="nofollow" style="box-sizing: border-box; background-color: transparent; color: var(--color-accent-fg); text-decoration: none;"><img src="https://camo.githubusercontent.com/2ee778ef534fdd413d5055d3202813398f39235a3d60b13974d43bc1bf1523a1/68747470733a2f2f62616467652e667572792e696f2f6e752f48746d6c324d61726b646f776e2e737667" alt="NuGet version" data-canonical-src="https://badge.fury.io/nu/Html2Markdown.svg" style="box-sizing: content-box; border-style: none; max-width: 100%; background-color: var(--color-canvas-default);"></a></p><div class="highlight highlight-source-powershell position-relative" style="box-sizing: border-box; position: relative !important; margin-bottom: 16px; color: rgb(201, 209, 217); font-family: -apple-system, BlinkMacSystemFont, &quot;Segoe UI&quot;, Helvetica, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(13, 17, 23); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;"><pre style="box-sizing: border-box; font-family: ui-monospace, SFMono-Regular, &quot;SF Mono&quot;, Menlo, Consolas, &quot;Liberation Mono&quot;, monospace; font-size: 13.6px; margin-top: 0px; margin-bottom: 0px; overflow-wrap: normal; padding: 16px; overflow: auto; line-height: 1.45; background-color: var(--color-canvas-subtle); border-radius: 6px; word-break: normal;">    <span class="pl-c1" style="box-sizing: border-box; color: var(--color-prettylights-syntax-constant);">Install-Package</span> Html2Markdown</pre></div><h2 style="box-sizing: border-box; margin-top: 24px; margin-bottom: 16px; font-size: 1.5em; font-weight: 600; line-height: 1.25; padding-bottom: 0.3em; border-bottom: 1px solid var(--color-border-muted); color: rgb(201, 209, 217); font-family: -apple-system, BlinkMacSystemFont, &quot;Segoe UI&quot;, Helvetica, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(13, 17, 23); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;"><a id="user-content-usage" class="anchor" aria-hidden="true" href="https://github.com/baynezy/Html2Markdown#usage" style="box-sizing: border-box; background-color: transparent; color: var(--color-accent-fg); text-decoration: none; float: left; padding-right: 4px; margin-left: -20px; line-height: 1;"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.775 3.275a.75.75 0 001.06 1.06l1.25-1.25a2 2 0 112.83 2.83l-2.5 2.5a2 2 0 01-2.83 0 .75.75 0 00-1.06 1.06 3.5 3.5 0 004.95 0l2.5-2.5a3.5 3.5 0 00-4.95-4.95l-1.25 1.25zm-4.69 9.64a2 2 0 010-2.83l2.5-2.5a2 2 0 012.83 0 .75.75 0 001.06-1.06 3.5 3.5 0 00-4.95 0l-2.5 2.5a3.5 3.5 0 004.95 4.95l1.25-1.25a.75.75 0 00-1.06-1.06l-1.25 1.25a2 2 0 01-2.83 0z"></path></svg></a>Usage</h2><h3 style="box-sizing: border-box; margin-top: 24px; margin-bottom: 16px; font-size: 1.25em; font-weight: 600; line-height: 1.25; color: rgb(201, 209, 217); font-family: -apple-system, BlinkMacSystemFont, &quot;Segoe UI&quot;, Helvetica, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(13, 17, 23); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;"><a id="user-content-strings" class="anchor" aria-hidden="true" href="https://github.com/baynezy/Html2Markdown#strings" style="box-sizing: border-box; background-color: transparent; color: var(--color-accent-fg); text-decoration: none; float: left; padding-right: 4px; margin-left: -20px; line-height: 1;"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.775 3.275a.75.75 0 001.06 1.06l1.25-1.25a2 2 0 112.83 2.83l-2.5 2.5a2 2 0 01-2.83 0 .75.75 0 00-1.06 1.06 3.5 3.5 0 004.95 0l2.5-2.5a3.5 3.5 0 00-4.95-4.95l-1.25 1.25zm-4.69 9.64a2 2 0 010-2.83l2.5-2.5a2 2 0 012.83 0 .75.75 0 001.06-1.06 3.5 3.5 0 00-4.95 0l-2.5 2.5a3.5 3.5 0 004.95 4.95l1.25-1.25a.75.75 0 00-1.06-1.06l-1.25 1.25a2 2 0 01-2.83 0z"></path></svg></a>Strings</h3><div class="highlight highlight-source-cs position-relative" style="box-sizing: border-box; position: relative !important; margin-bottom: 16px; color: rgb(201, 209, 217); font-family: -apple-system, BlinkMacSystemFont, &quot;Segoe UI&quot;, Helvetica, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(13, 17, 23); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;"><pre style="box-sizing: border-box; font-family: ui-monospace, SFMono-Regular, &quot;SF Mono&quot;, Menlo, Consolas, &quot;Liberation Mono&quot;, monospace; font-size: 13.6px; margin-top: 0px; margin-bottom: 0px; overflow-wrap: normal; padding: 16px; overflow: auto; line-height: 1.45; background-color: var(--color-canvas-subtle); border-radius: 6px; word-break: normal;"><span class="pl-k" style="box-sizing: border-box; color: var(--color-prettylights-syntax-keyword);">var</span> <span class="pl-en" style="box-sizing: border-box; color: var(--color-prettylights-syntax-entity);">html</span> <span class="pl-k" style="box-sizing: border-box; color: var(--color-prettylights-syntax-keyword);">=</span> <span class="pl-s" style="box-sizing: border-box; color: var(--color-prettylights-syntax-string);"><span class="pl-pds" style="box-sizing: border-box; color: var(--color-prettylights-syntax-string);">"</span>Something to &lt;strong&gt;convert&lt;/strong&gt;<span class="pl-pds" style="box-sizing: border-box; color: var(--color-prettylights-syntax-string);">"</span></span>;
		                    <span class="pl-k" style="box-sizing: border-box; color: var(--color-prettylights-syntax-keyword);">var</span> <span class="pl-en" style="box-sizing: border-box; color: var(--color-prettylights-syntax-entity);">converter</span> <span class="pl-k" style="box-sizing: border-box; color: var(--color-prettylights-syntax-keyword);">=</span> <span class="pl-k" style="box-sizing: border-box; color: var(--color-prettylights-syntax-keyword);">new</span> <span class="pl-k" style="box-sizing: border-box; color: var(--color-prettylights-syntax-keyword);">Converter</span>();
		                    <span class="pl-k" style="box-sizing: border-box; color: var(--color-prettylights-syntax-keyword);">var</span> <span class="pl-en" style="box-sizing: border-box; color: var(--color-prettylights-syntax-entity);">markdown</span> <span class="pl-k" style="box-sizing: border-box; color: var(--color-prettylights-syntax-keyword);">=</span> <span class="pl-smi" style="box-sizing: border-box; color: var(--color-prettylights-syntax-storage-modifier-import);">converter</span>.<span class="pl-en" style="box-sizing: border-box; color: var(--color-prettylights-syntax-entity);">Convert</span>(<span class="pl-smi" style="box-sizing: border-box; color: var(--color-prettylights-syntax-storage-modifier-import);">html</span>);</pre></div>
		                    </body>
		                    </html>
		                    """;
		
		return CheckConversion(html);
	}

	#endregion

	protected virtual Task CheckConversion(string html, IScheme scheme = null)
	{
		var converter = scheme == null ? new Converter() : new Converter(scheme);

		var result = converter.Convert(html);

		return Verifier.Verify(result);
	}
}