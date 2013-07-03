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

		private static void CheckConversion(string html, string expected)
		{
			var converter = new Converter();

			var result = converter.Convert(html);

			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
