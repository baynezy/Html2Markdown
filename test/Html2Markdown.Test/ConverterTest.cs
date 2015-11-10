using NUnit.Framework;

namespace Html2Markdown.Test
{
	[TestFixture]
	class ConverterTest
	{
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
			const string html = @"So this text has a break.<br />Convert it.";
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

    &lt;p&gt;
        Some code we are looking at
    &lt;/p&gt;

";

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

        &lt;p&gt;
            Some code we are looking at
        &lt;/p&gt;

";

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
        public void Convert_WhenThereAreBlockquoteTags_ThenReplaceWithMarkDownBlockQuote()
        {
            const string html = @"This code has a <blockquote>blockquote</blockquote>. Convert it.";
            const string expected = @"This code has a 

>blockquote

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
		public void Convert_WhenThereAreHorizontalRuleTags_ThenReplaceWithMarkDownHorizontalRule()
		{
			const string html = @"This code is seperated by a horizonrtal rule.<hr/>Convert it!";
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


        Predefined text
";

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

		private static void CheckConversion(string html, string expected)
		{
			var converter = new Converter();

			var result = converter.Convert(html);

			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
