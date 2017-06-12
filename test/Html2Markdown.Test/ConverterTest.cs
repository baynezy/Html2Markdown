using NUnit.Framework;

namespace Html2Markdown.Test
{
	[TestFixture]
	class ConverterTest
	{
		private string _testPath;

		[SetUp]
		public void SetUp() {
			_testPath = TestPath();
		}

		[Test]
		public void Convert_WhenThereAreHtmlLinks_ThenConvertToMarkDownLinks()
		{
			const string html = @"So this is <a href=""http://www.simonbaynes.com/"">a link</a>. Convert it";
			const string expected = @"So this is [a link](http://www.simonbaynes.com/). Convert it";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreHtmlLinksWithAttributesAfterTheHref_ThenConvertToMarkDownLink()
		{
			const string html = @"So this is <a href=""http://www.simonbaynes.com/"" alt=""example"">a link</a>. Convert it";
			const string expected = @"So this is [a link](http://www.simonbaynes.com/). Convert it";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreHtmlLinksWithAttributesBeforeTheHref_ThenConvertToMarkDownLink()
		{
			const string html = @"So this is <a alt=""example"" href=""http://www.simonbaynes.com/"">a link</a>. Convert it";
			const string expected = @"So this is [a link](http://www.simonbaynes.com/). Convert it";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreHtmlLinksWithTitleAttributeAfterTheHref_ThenConvertToMarkDownLink()
		{
			const string html = @"So this is <a href=""http://www.simonbaynes.com/"" title=""example"">a link</a>. Convert it";
			const string expected = @"So this is [a link](http://www.simonbaynes.com/ ""example""). Convert it";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreHtmlLinksWithTitleAttributeBeforeTheHref_ThenConvertToMarkDownLink()
		{
			const string html = @"So this is <a title=""example"" href=""http://www.simonbaynes.com/"">a link</a>. Convert it";
			const string expected = @"So this is [a link](http://www.simonbaynes.com/ ""example""). Convert it";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreMultipleHtmlLinks_ThenConvertThemToMarkDownLinks()
		{
			const string html = @"So this is <a href=""http://www.simonbaynes.com/"">a link</a> and so is <a href=""http://www.google.com/"">this</a>. Convert them";
			const string expected = @"So this is [a link](http://www.simonbaynes.com/) and so is [this](http://www.google.com/). Convert them";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreEmptyLinks_ThenRemoveThemFromResult()
		{
			const string html = @"So this is <a name=""curio""></a> and so is <a href=""http://www.google.com/"">this</a>. Convert them";
			const string expected = @"So this is  and so is [this](http://www.google.com/). Convert them";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreStrongTags_ThenConvertToMarkDownDoubleAsterisks()
		{
			const string html = @"So this text is <strong>bold</strong>. Convert it.";
			const string expected = @"So this text is **bold**. Convert it.";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreMultipleStrongTags_ThenConvertToMarkDownDoubleAsterisks()
		{
			const string html = @"So this text is <strong>bold</strong> and <strong>is this</strong>. Convert it.";
			const string expected = @"So this text is **bold** and **is this**. Convert it.";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreBoldTags_ThenConvertToMarkDownDoubleAsterisks()
		{
			const string html = @"So this text is <b>bold</b>. Convert it.";
			const string expected = @"So this text is **bold**. Convert it.";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreEmphsisTags_ThenConvertToMarkDownSingleAsterisk()
		{
			const string html = @"So this text is <em>italic</em>. Convert it.";
			const string expected = @"So this text is *italic*. Convert it.";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreItalicTags_ThenConvertToMarkDownSingleAsterisk()
		{
			const string html = @"So this text is <i>italic</i>. Convert it.";
			const string expected = @"So this text is *italic*. Convert it.";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreBreakTags_ThenConvertToMarkDownDoubleSpacesWitCarriageReturns()
		{
			const string html = @"So this text has a break.<br/>Convert it.";
			const string expected = @"So this text has a break.  
Convert it.";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreBreakTagsWithWhitespace_ThenConvertToMarkDownDoubleSpacesWitCarriageReturns()
		{
			const string html = @"So this text has a break.<br />Convert it.";
			const string expected = @"So this text has a break.  
Convert it.";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreBreakTagsThatAreNotSelfClosing_ThenConvertToMarkDownDoubleSpacesWitCarriageReturns()
		{
			const string html = @"So this text has a break.<br>Convert it.";
			const string expected = @"So this text has a break.  
Convert it.";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreCodeTags_ThenReplaceWithBackTick()
		{
			const string html = @"So this text has code <code>alert();</code>. Convert it.";
			const string expected = @"So this text has code `alert();`. Convert it.";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereMultilineCodeTags_ThenReplaceWithMultilineMarkdownBlock001()
		{
			const string html = @"So this text has multiline code.
<code>
&lt;p&gt;
	Some code we are looking at
&lt;/p&gt;
</code>";
			const string expected = @"So this text has multiline code.

    <p>
        Some code we are looking at
    </p>";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereMultilineCodeTags_ThenReplaceWithMultilineMarkdownBlock002()
		{
			const string html = @"So this text has multiline code.
<code>
	&lt;p&gt;
		Some code we are looking at
	&lt;/p&gt;
</code>";
			const string expected = @"So this text has multiline code.

        <p>
            Some code we are looking at
        </p>";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreH1Tags_ThenReplaceWithMarkDownHeader()
		{
			const string html = @"This code has a <h1>header</h1>. Convert it.";
			const string expected = @"This code has a 

# header

. Convert it.";

			CheckConversion(html, expected);
		}


		[Test]
		public void Convert_WhenThereAreH2Tags_ThenReplaceWithMarkDownHeader()
		{
			const string html = @"This code has a <h2>header</h2>. Convert it.";
			const string expected = @"This code has a 

## header

. Convert it.";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreH3Tags_ThenReplaceWithMarkDownHeader()
		{
			const string html = @"This code has a <h3>header</h3>. Convert it.";
			const string expected = @"This code has a 

### header

. Convert it.";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreH4Tags_ThenReplaceWithMarkDownHeader()
		{
			const string html = @"This code has a <h4>header</h4>. Convert it.";
			const string expected = @"This code has a 

#### header

. Convert it.";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreH5Tags_ThenReplaceWithMarkDownHeader()
		{
			const string html = @"This code has a <h5>header</h5>. Convert it.";
			const string expected = @"This code has a 

##### header

. Convert it.";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreH6Tags_ThenReplaceWithMarkDownHeader()
		{
			const string html = @"This code has a <h6>header</h6>. Convert it.";
			const string expected = @"This code has a 

###### header

. Convert it.";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreH1TagsWithAttributes_ThenReplaceWithMarkDownHeader()
		{
			const string html = @"This code has a <h1 title=""header"">header</h1>. Convert it.";
			const string expected = @"This code has a 

# header

. Convert it.";

			CheckConversion(html, expected);
		}


		[Test]
		public void Convert_WhenThereAreH2TagsWithAttributes_ThenReplaceWithMarkDownHeader()
		{
			const string html = @"This code has a <h2 title=""header"">header</h2>. Convert it.";
			const string expected = @"This code has a 

## header

. Convert it.";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreH3TagsWithAttributes_ThenReplaceWithMarkDownHeader()
		{
			const string html = @"This code has a <h3 title=""header"">header</h3>. Convert it.";
			const string expected = @"This code has a 

### header

. Convert it.";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreH4TagsWithAttributes_ThenReplaceWithMarkDownHeader()
		{
			const string html = @"This code has a <h4 title=""header"">header</h4>. Convert it.";
			const string expected = @"This code has a 

#### header

. Convert it.";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreH5TagsWithAttributes_ThenReplaceWithMarkDownHeader()
		{
			const string html = @"This code has a <h5 title=""header"">header</h5>. Convert it.";
			const string expected = @"This code has a 

##### header

. Convert it.";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreH6TagsWithAttributes_ThenReplaceWithMarkDownHeader()
		{
			const string html = @"This code has a <h6 title=""header"">header</h6>. Convert it.";
			const string expected = @"This code has a 

###### header

. Convert it.";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreBlockquoteTags_ThenReplaceWithMarkDownBlockQuote()
		{
			const string html = @"This code has a <blockquote>blockquote</blockquote>. Convert it.";
			const string expected = @"This code has a 

> blockquote

. Convert it.";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereIsABlockquoteTagWithNestedHtml_ThenReplaceWithMarkDownBlockQuote()
		{
			const string html = @"<blockquote><em>“Qualquer coisa que possas fazer ou sonhar, podes começá-la. A ousadia encerra em si mesma genialidade, poder e magia.<br />Ouse fazer, e o poder lhe será dado!”</em><br /><strong>— Johann Wolfgang von Goethe</strong></blockquote>";

			const string expected = @"> *“Qualquer coisa que possas fazer ou sonhar, podes começá-la. A ousadia encerra em si mesma genialidade, poder e magia.
> Ouse fazer, e o poder lhe será dado!”*
> **— Johann Wolfgang von Goethe**";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereIsAMultilineBlockquoteTag_ThenReplaceWithMarkDownBlockQuote()
		{
			const string html = @"<blockquote>
    <p class=""right"" align=""right""><em>“Ao estipular seus objetivos, mire na Lua; pois mesmo que aconteça de você não alcançá-los, ainda estará entre as estrelas!”</em><br />
    <strong>— Dr. Lair Ribeiro</strong></p>
  </blockquote>";

			const string expected = @"> *“Ao estipular seus objetivos, mire na Lua; pois mesmo que aconteça de você não alcançá-los, ainda estará entre as estrelas!”*
>  **— Dr. Lair Ribeiro**";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereIsABlockquoteTagWithAttributes_ThenReplaceWithMarkDownBlockQuote()
		{
			const string html = @"This code has a <blockquote id=""thing"">blockquote</blockquote>. Convert it.";
			const string expected = @"This code has a 

> blockquote

. Convert it.";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreParagraphTags_ThenReplaceWithDoubleLineBreakBeforeAndOneAfter()
		{
			const string html = @"This code has no markup.<p>This code is in a paragraph.</p>Convert it!";
			const string expected = @"This code has no markup.

This code is in a paragraph.
Convert it!";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreParagraphTagsWithAttributes_ThenReplaceWithDoubleLineBreakBeforeAndOneAfter()
		{
			const string html = @"This code has no markup.<p class=""something"">This code is in a paragraph.</p>Convert it!";
			const string expected = @"This code has no markup.

This code is in a paragraph.
Convert it!";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreParagraphTagsWithNewLinesInThem_ThenReplaceWithMarkdownParagraphButNoBreakTags()
		{
			const string html = @"<p>
  text
  text
  text
</p>";
			const string expected = @" text text text ";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreHorizontalRuleTags_ThenReplaceWithMarkDownHorizontalRule()
		{
			const string html = @"This code is seperated by a horizonrtal rule.<hr/>Convert it!";
			const string expected = @"This code is seperated by a horizonrtal rule.

* * *
Convert it!";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreHorizontalRuleTagsWithWhiteSpace_ThenReplaceWithMarkDownHorizontalRule()
		{
			const string html = @"This code is seperated by a horizonrtal rule.<hr />Convert it!";
			const string expected = @"This code is seperated by a horizonrtal rule.

* * *
Convert it!";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreHorizontalRuleTagsWithAttributes_ThenReplaceWithMarkDownHorizontalRule()
		{
			const string html = @"This code is seperated by a horizonrtal rule.<hr class=""something"" />Convert it!";
			const string expected = @"This code is seperated by a horizonrtal rule.

* * *
Convert it!";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreHorizontalRuleTagsThatAreNonSelfClosing_ThenReplaceWithMarkDownHorizontalRule()
		{
			const string html = @"This code is seperated by a horizonrtal rule.<hr>Convert it!";
			const string expected = @"This code is seperated by a horizonrtal rule.

* * *
Convert it!";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreImgTags_ThenReplaceWithMarkdownImage()
		{
			const string html = @"This code is with and image <img alt=""something"" title=""convert"" src=""https://assets-cdn.github.com/images/spinners/octocat-spinner-32.gif"" /> Convert it!";
			const string expected = @"This code is with and image ![something](https://assets-cdn.github.com/images/spinners/octocat-spinner-32.gif ""convert"") Convert it!";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreImgTagsWithoutATitle_ThenReplaceWithMarkdownImage()
		{
			const string html = @"This code is with an image <img alt=""something"" src=""https://assets-cdn.github.com/images/spinners/octocat-spinner-32.gif"" /> Convert it!";
			const string expected = @"This code is with an image ![something](https://assets-cdn.github.com/images/spinners/octocat-spinner-32.gif) Convert it!";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereArePreTags_ThenReplaceWithMarkdownPre()
		{
			const string html = @"This code is with a pre tag <pre>
	Predefined text</pre>";
			const string expected = @"This code is with a pre tag 

        Predefined text";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreOtherTagsNestedInThePreTag_ThenReplaceWithMarkdownPre()
		{
			const string html = @"<pre><code>Install-Package Html2Markdown
</code></pre>";
			const string expected = @"        Install-Package Html2Markdown";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreMultiplePreTags_ThenReplaceWithMarkdownPre()
		{
			const string html = @"<h2>Installing via NuGet</h2>

<pre><code>Install-Package Html2Markdown
</code></pre>

<h2>Usage</h2>

<pre><code>var converter = new Converter();
var result = converter.Convert(html);
</code></pre>";
			const string expected = @"## Installing via NuGet

        Install-Package Html2Markdown

## Usage

        var converter = new Converter();
        var result = converter.Convert(html);";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreUnorderedLists_ThenReplaceWithMarkdownLists()
		{
			const string html = @"This code is with an unordered list.<ul><li>Yes</li><li>No</li></ul>";
			const string expected = @"This code is with an unordered list.

*   Yes
*   No";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereAreOrderedLists_ThenReplaceWithMarkdownLists()
		{
			const string html = @"This code is with an unordered list.<ol><li>Yes</li><li>No</li></ol>";
			const string expected = @"This code is with an unordered list.

1.  Yes
2.  No";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereIsAnUnorderedListWithANestedOrderList_ThenReplaceWithMarkdownLists()
		{
			const string html = @"This code is with an unordered list.<ul><li>Yes</li><li><ol><li>No</li><li>Maybe</li></ol></li></ul>";
			const string expected = @"This code is with an unordered list.

*   Yes
*   1.  No
    2.  Maybe";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereIsAnOrderedListWithANestedUnorderList_ThenReplaceWithMarkdownLists()
		{
			const string html = @"This code is with an unordered list.<ol><li>Yes</li><li><ul><li>No</li><li>Maybe</li></ul></li></ol>";
			const string expected = @"This code is with an unordered list.

1.  Yes
2.  *   No
    *   Maybe";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereIsAnHtmlDoctype_ThenRemoveFromResult()
		{
			const string html = @"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">
Doctypes should be removed";
			const string expected = @"Doctypes should be removed";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereIsAnHtmlTag_ThenRemoveFromResult()
		{
			const string html = @"<html>
<p>HTML tags should be removed</p>
</html>";
			const string expected = @"HTML tags should be removed";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereIsAnHtmlTagWithAttributes_ThenRemoveFromResult()
		{
			const string html = @"<html xmlns=""http://www.w3.org/1999/xhtml"" xml:lang=""pt-br"">
<p>HTML tags should be removed</p>
</html>";
			const string expected = @"HTML tags should be removed";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereIsASingleLineComment_ThenRemoveFromResult()
		{
			const string html = @"<!-- a comment -->
<p>Comments should be removed</p>";
			const string expected = @"Comments should be removed";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereIsAMultiLineComment_ThenRemoveFromResult()
		{
			const string html = @"<!-- 
a comment
-->
<p>Comments should be removed</p>";
			const string expected = @"Comments should be removed";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereIsAHeadTag_ThenRemoveFromResult()
		{
			const string html = @"<head>
<p>HTML tags should be removed</p>
</head>";
			const string expected = @"HTML tags should be removed";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereIsAHeadTagWithAttributes_ThenRemoveFromResult()
		{
			const string html = @"<head id=""something"">
<p>HTML tags should be removed</p>
</head>";
			const string expected = @"HTML tags should be removed";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereIsAMetaTag_ThenRemoveFromResult()
		{
			const string html = @"<meta name=""language"" content=""pt-br"">
<p>Meta tags should be removed</p>";
			const string expected = @"Meta tags should be removed";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereIsATitleTag_ThenRemoveFromResult()
		{
			const string html = @"<title>Remove me</title>
<p>Title tags should be removed</p>";
			const string expected = @"Title tags should be removed";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereIsATitleTagWithAttributes_ThenRemoveFromResult()
		{
			const string html = @"<title id=""something"">Remove me</title>
<p>Title tags should be removed</p>";
			const string expected = @"Title tags should be removed";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereIsALinkTag_ThenRemoveFromResult()
		{
			const string html = @"<link type=""text/css"" rel=""stylesheet"" href=""https://dl.dropboxusercontent.com/u/28729896/modelo-similar-blog-ss-para-sublime-text.css"">
<p>Link tags should be removed</p>";
			const string expected = @"Link tags should be removed";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereIsABodyTag_ThenRemoveFromResult()
		{
			const string html = @"<body>
<p>Body tags should be removed</p>
</body>";
			const string expected = @"Body tags should be removed";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereIsABodyTagWithAttributes_ThenRemoveFromResult()
		{
			const string html = @"<body id=""something"">
<p>Body tags should be removed</p>
</body>";
			const string expected = @"Body tags should be removed";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereIsAnAmpersandEntity_ThenReplaceWithActualCharacter()
		{
			const string html = @"<p>Enties like &amp; should be converted</p>";
			const string expected = @"Enties like & should be converted";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereIsAnLessThanEntity_ThenReplaceWithActualCharacter()
		{
			const string html = @"<p>Enties like &lt; should be converted</p>";
			const string expected = @"Enties like < should be converted";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereIsAGreaterThanEntity_ThenReplaceWithActualCharacter()
		{
			const string html = @"<p>Enties like &gt; should be converted</p>";
			const string expected = @"Enties like > should be converted";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_WhenThereIsABulletEntity_ThenReplaceWithActualCharacter()
		{
			const string html = @"<p>Enties like &#8226; should be converted</p>";
			const string expected = @"Enties like • should be converted";

			CheckConversion(html, expected);
		}

		[Test]
		public void ConvertFile_WhenReadingInHtmlFile_ThenConvertToMarkdown()
		{
			var sourcePath = _testPath + "TestHtml.txt";
			const string expected = @"## Installing via NuGet

        Install-Package Html2Markdown

## Usage

        var converter = new Converter();
        var result = converter.Convert(html);";

			CheckFileConversion(sourcePath, expected);
		}

		[Test]
		public void Convert_ComplexTest_001()
		{
			const string html =
				"<p>This is the second part of a two part series about building real-time web applications with server-sent events.</p>\r\n\r\n<ul>\r\n<li><a href=\"http://bayn.es/real-time-web-applications-with-server-sent-events-pt-1/\">Building Web Apps with Server-Sent Events - Part 1</a></li>\r\n</ul>\r\n\r\n<h2 id=\"reconnecting\">Reconnecting</h2>\r\n\r\n<p>In this post we are going to look at handling reconnection if the browser loses contact with the server. Thankfully the native JavaScript functionality for SSEs (the <a href=\"https://developer.mozilla.org/en-US/docs/Web/API/EventSource\">EventSource</a>) handles this natively. You just need to make sure that your server-side implementation supports the mechanism.</p>\r\n\r\n<p>When the server reconnects your SSE end point it will send a special HTTP header <code>Last-Event-Id</code> in the reconnection request. In the previous part of this blog series we looked at just sending events with the <code>data</code> component. Which looked something like this:-</p>\r\n\r\n<pre><code>data: The payload we are sending\\n\\n\r\n</code></pre>\r\n\r\n<p>Now while this is enough to make the events make it to your client-side implementation. We need more information to handle reconnection. To do this we need to add an event id to the output.</p>\r\n\r\n<p>E.g.</p>\r\n\r\n<pre><code>id: 1439887379635\\n\r\ndata: The payload we are sending\\n\\n\r\n</code></pre>\r\n\r\n<p>The important thing to understand here is that each event needs a unique identifier, so that the client can communicate back to the server (using the <code>Last-Event-Id</code> header) which was the last event it received on reconnection.</p>\r\n\r\n<h2 id=\"persistence\">Persistence</h2>\r\n\r\n<p>In our previous example we used <a href=\"http://redis.io/topics/pubsub\">Redis Pub/Sub</a> to inform <a href=\"https://nodejs.org/\">Node.js</a> that it needs to push a new SSE to the client. Redis Pub/Sub is a topic communication which means it will be delivered to all <em>connected clients</em>, and then it will be removed from the topic. So there is no persistence for when clients reconnect. To implement this we need to add a persistence layer and so in this demo I have chosen to use <a href=\"https://www.mongodb.org/\">MongoDB</a>.</p>\r\n\r\n<p>Essentially we will be pushing events into both Redis and MongoDB. Redis will still be our method of initiating an SSE getting sent to the browser, but we will also be be storing that event into MongoDB so we can query it on a reconnection to get anything we've missed.</p>\r\n\r\n<h2 id=\"thecode\">The Code</h2>\r\n\r\n<p>OK so let us look at how we can actually implement this.</p>\r\n\r\n<h3 id=\"updateserverevent\">Update ServerEvent</h3>\r\n\r\n<p>We need to update the ServerEvent object to support having an <code>id</code> for an event.</p>\r\n\r\n<pre><code>function ServerEvent(name) {\r\n    this.name = name || \"\";\r\n    this.data = \"\";\r\n};\r\n\r\nServerEvent.prototype.addData = function(data) {\r\n    var lines = data.split(/\\n/);\r\n\r\n    for (var i = 0; i &lt; lines.length; i++) {\r\n        var element = lines[i];\r\n        this.data += \"data:\" + element + \"\\n\";\r\n    }\r\n}\r\n\r\nServerEvent.prototype.payload = function() {\r\n    var payload = \"\";\r\n    if (this.name != \"\") {\r\n        payload += \"id: \" + this.name + \"\\n\";\r\n    }\r\n\r\n    payload += this.data;\r\n    return payload + \"\\n\";\r\n}\r\n</code></pre>\r\n\r\n<p>This is pretty straightforward string manipulation and won't impress anyone, but it is foundation for what will follow.</p>\r\n\r\n<h3 id=\"storeeventsinmongodb\">Store Events in MongoDB</h3>\r\n\r\n<p>We need to update the <code>post.js</code> code to also store new events in MongoDB.</p>\r\n\r\n<pre><code>app.put(\"/api/post-update\", function(req, res) {\r\n    var json = req.body;\r\n    json.timestamp = Date.now();\r\n\r\n    eventStorage.save(json).then(function(doc) {\r\n        dataChannel.publish(JSON.stringify(json));\r\n    }, errorHandling);\r\n\r\n    res.status(204).end();\r\n});\r\n</code></pre>\r\n\r\n<p>The <code>event-storage</code> module looks as follows:</p>\r\n\r\n<pre><code>var Q = require(\"q\"),\r\n    config = require(\"./config\"),\r\n    mongo = require(\"mongojs\"),\r\n    db = mongo(config.mongoDatabase),\r\n    collection = db.collection(config.mongoScoresCollection);\r\n\r\nmodule.exports.save = function(data) {\r\n    var deferred = Q.defer();\r\n    collection.save(data, function(err, doc){\r\n        if(err) {\r\n            deferred.reject(err);\r\n        }\r\n        else {\r\n            deferred.resolve(doc);\r\n        }\r\n    });\r\n\r\n    return deferred.promise;\r\n};\r\n</code></pre>\r\n\r\n<p>Here we are just using basic MongoDB commands to save a new event into the collection. Yep that is it, we are now additionally persisting the events so they can be retrieved later.</p>\r\n\r\n<h3 id=\"retrievingeventsonreconnection\">Retrieving Events on Reconnection</h3>\r\n\r\n<p>When an <code>EventSource</code> reconnects after a disconnection it passes a special header <code>Last-Event-Id</code>. So we need to look for that and return the events that got broadcast while the client was disconnected.</p>\r\n\r\n<pre><code>app.get(\"/api/updates\", function(req, res){\r\n    initialiseSSE(req, res);\r\n\r\n    if (typeof(req.headers[\"last-event-id\"]) != \"undefined\") {\r\n        replaySSEs(req, res);\r\n    }\r\n});\r\n\r\nfunction replaySSEs(req, res) {\r\n    var lastId = req.headers[\"last-event-id\"];\r\n\r\n    eventStorage.findEventsSince(lastId).then(function(docs) {\r\n        for (var index = 0; index &lt; docs.length; index++) {\r\n            var doc = docs[index];\r\n            var messageEvent = new ServerEvent(doc.timestamp);\r\n            messageEvent.addData(doc.update);\r\n            outputSSE(req, res, messageEvent.payload());\r\n        }\r\n    }, errorHandling);\r\n};\r\n</code></pre>\r\n\r\n<p>What we are doing here is querying MongoDB for the events that were missed. We then iterate over them and output them to the browser.</p>\r\n\r\n<p>The code for querying MongoDB is as follows:</p>\r\n\r\n<pre><code>module.exports.findEventsSince = function(lastEventId) {\r\n    var deferred = Q.defer();\r\n\r\n    collection.find({\r\n        timestamp: {$gt: Number(lastEventId)}\r\n    })\r\n    .sort({timestamp: 1}, function(err, docs) {\r\n        if (err) {\r\n            deferred.reject(err);\r\n        }\r\n        else {\r\n            deferred.resolve(docs);\r\n        }\r\n    });\r\n\r\n    return deferred.promise;\r\n};\r\n</code></pre>\r\n\r\n<h2 id=\"testing\">Testing</h2>\r\n\r\n<p>To test this you will need to run both apps at the same time.</p>\r\n\r\n<pre><code>node app.js\r\n</code></pre>\r\n\r\n<p>and </p>\r\n\r\n<pre><code>node post.js\r\n</code></pre>\r\n\r\n<p>Once they are running open two browser windows <a href=\"http://localhost:8181/\">http://localhost:8181/</a> and <a href=\"http://localhost:8082/api/post-update\">http://localhost:8082/api/post-update</a></p>\r\n\r\n<p>Now you can post updates as before. If you stop <code>app.js</code> but continue posting events, when you restart <code>app.js</code> within 10 seconds the <code>EventSource</code> will reconnect. This will deliver all missed events.</p>\r\n\r\n<h2 id=\"conclusion\">Conclusion</h2>\r\n\r\n<p>This very simple code gives you a very elegant and powerful push architecture to create real-time apps.</p>\r\n\r\n<h3 id=\"improvements\">Improvements</h3>\r\n\r\n<p>A possible improvement would be to render the events from MongoDB server-side when the page is first output. Then we would get updates client-side as they are pushed to the browser.</p>\r\n\r\n<h3 id=\"download\">Download</h3>\r\n\r\n<p>If you want to play with this application you can fork or browse it on <a href=\"https://github.com/baynezy/RealtimeDemo/tree/part-2\">GitHub</a>.</p>";
			const string expected = @"This is the second part of a two part series about building real-time web applications with server-sent events.

*   [Building Web Apps with Server-Sent Events - Part 1](http://bayn.es/real-time-web-applications-with-server-sent-events-pt-1/)

## Reconnecting

In this post we are going to look at handling reconnection if the browser loses contact with the server. Thankfully the native JavaScript functionality for SSEs (the [EventSource](https://developer.mozilla.org/en-US/docs/Web/API/EventSource)) handles this natively. You just need to make sure that your server-side implementation supports the mechanism.

When the server reconnects your SSE end point it will send a special HTTP header `Last-Event-Id` in the reconnection request. In the previous part of this blog series we looked at just sending events with the `data` component. Which looked something like this:-

        data: The payload we are sending\n\n

Now while this is enough to make the events make it to your client-side implementation. We need more information to handle reconnection. To do this we need to add an event id to the output.

E.g.

        id: 1439887379635\n
        data: The payload we are sending\n\n

The important thing to understand here is that each event needs a unique identifier, so that the client can communicate back to the server (using the `Last-Event-Id` header) which was the last event it received on reconnection.

## Persistence

In our previous example we used [Redis Pub/Sub](http://redis.io/topics/pubsub) to inform [Node.js](https://nodejs.org/) that it needs to push a new SSE to the client. Redis Pub/Sub is a topic communication which means it will be delivered to all *connected clients*, and then it will be removed from the topic. So there is no persistence for when clients reconnect. To implement this we need to add a persistence layer and so in this demo I have chosen to use [MongoDB](https://www.mongodb.org/).

Essentially we will be pushing events into both Redis and MongoDB. Redis will still be our method of initiating an SSE getting sent to the browser, but we will also be be storing that event into MongoDB so we can query it on a reconnection to get anything we've missed.

## The Code

OK so let us look at how we can actually implement this.

### Update ServerEvent

We need to update the ServerEvent object to support having an `id` for an event.

        function ServerEvent(name) {
            this.name = name || """";
            this.data = """";
        };
        ServerEvent.prototype.addData = function(data) {
            var lines = data.split(/\n/);
            for (var i = 0; i < lines.length; i++) {
                var element = lines[i];
                this.data += ""data:"" + element + ""\n"";
            }
        }
        ServerEvent.prototype.payload = function() {
            var payload = """";
            if (this.name != """") {
                payload += ""id: "" + this.name + ""\n"";
            }
            payload += this.data;
            return payload + ""\n"";
        }

This is pretty straightforward string manipulation and won't impress anyone, but it is foundation for what will follow.

### Store Events in MongoDB

We need to update the `post.js` code to also store new events in MongoDB.

        app.put(""/api/post-update"", function(req, res) {
            var json = req.body;
            json.timestamp = Date.now();
            eventStorage.save(json).then(function(doc) {
                dataChannel.publish(JSON.stringify(json));
            }, errorHandling);
            res.status(204).end();
        });

The `event-storage` module looks as follows:

        var Q = require(""q""),
            config = require(""./config""),
            mongo = require(""mongojs""),
            db = mongo(config.mongoDatabase),
            collection = db.collection(config.mongoScoresCollection);
        module.exports.save = function(data) {
            var deferred = Q.defer();
            collection.save(data, function(err, doc){
                if(err) {
                    deferred.reject(err);
                }
                else {
                    deferred.resolve(doc);
                }
            });
            return deferred.promise;
        };

Here we are just using basic MongoDB commands to save a new event into the collection. Yep that is it, we are now additionally persisting the events so they can be retrieved later.

### Retrieving Events on Reconnection

When an `EventSource` reconnects after a disconnection it passes a special header `Last-Event-Id`. So we need to look for that and return the events that got broadcast while the client was disconnected.

        app.get(""/api/updates"", function(req, res){
            initialiseSSE(req, res);
            if (typeof(req.headers[""last-event-id""]) != ""undefined"") {
                replaySSEs(req, res);
            }
        });
        function replaySSEs(req, res) {
            var lastId = req.headers[""last-event-id""];
            eventStorage.findEventsSince(lastId).then(function(docs) {
                for (var index = 0; index < docs.length; index++) {
                    var doc = docs[index];
                    var messageEvent = new ServerEvent(doc.timestamp);
                    messageEvent.addData(doc.update);
                    outputSSE(req, res, messageEvent.payload());
                }
            }, errorHandling);
        };

What we are doing here is querying MongoDB for the events that were missed. We then iterate over them and output them to the browser.

The code for querying MongoDB is as follows:

        module.exports.findEventsSince = function(lastEventId) {
            var deferred = Q.defer();
            collection.find({
                timestamp: {$gt: Number(lastEventId)}
            })
            .sort({timestamp: 1}, function(err, docs) {
                if (err) {
                    deferred.reject(err);
                }
                else {
                    deferred.resolve(docs);
                }
            });
            return deferred.promise;
        };

## Testing

To test this you will need to run both apps at the same time.

        node app.js

and 

        node post.js

Once they are running open two browser windows [http://localhost:8181/](http://localhost:8181/) and [http://localhost:8082/api/post-update](http://localhost:8082/api/post-update)

Now you can post updates as before. If you stop `app.js` but continue posting events, when you restart `app.js` within 10 seconds the `EventSource` will reconnect. This will deliver all missed events.

## Conclusion

This very simple code gives you a very elegant and powerful push architecture to create real-time apps.

### Improvements

A possible improvement would be to render the events from MongoDB server-side when the page is first output. Then we would get updates client-side as they are pushed to the browser.

### Download

If you want to play with this application you can fork or browse it on [GitHub](https://github.com/baynezy/RealtimeDemo/tree/part-2).";

			CheckConversion(html, expected);
		}

		[Test]
		public void Convert_ComplexTest_002()
		{
			const string html = @"<p>Some other HTML</p>

<blockquote>
<p class=""right"" align=""right""><em>“Qualquer coisa que possas fazer ou sonhar, podes começá-la. A ousadia encerra em si mesma genialidade, poder e magia.<br />Ouse fazer, e o poder lhe será dado!”</em><br /><strong>— Johann Wolfgang von Goethe</strong></p>
</blockquote>";

			const string expected = @"Some other HTML

> *“Qualquer coisa que possas fazer ou sonhar, podes começá-la. A ousadia encerra em si mesma genialidade, poder e magia.
> Ouse fazer, e o poder lhe será dado!”*
> **— Johann Wolfgang von Goethe**";

			CheckConversion(html, expected);
		}

		private static void CheckFileConversion(string path, string expected)
		{
			var converter = new Converter();

			var result = converter.ConvertFile(path);

			Assert.That(result, Is.EqualTo(expected));
		}

		private static void CheckConversion(string html, string expected)
		{
			var converter = new Converter();

			var result = converter.Convert(html);

			Assert.That(result, Is.EqualTo(expected));
		}

		private static string TestPath()
		{
			const string route = @"..\..\Files\";
			var environmentPath = System.Environment.GetEnvironmentVariable("Test.Path");

			return environmentPath ?? route;
		}
	}
}