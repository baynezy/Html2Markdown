using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Html2Markdown.Replacement;

namespace Html2Markdown
{
	/// <summary>
	/// An Html to Markdown converter.
	/// </summary>
	public class Converter
	{
		private readonly IList<IReplacer> _replacers = new List<IReplacer>
		{
			new PatternReplacer
			{
				Pattern = @"</?(strong|b)>",
				Replacement = @"**"
			},
			new PatternReplacer
			{
				Pattern = @"</?(em|i)>",
				Replacement = @"*"
			},
			new PatternReplacer
			{
				Pattern = @"<br\s/>",
				Replacement = @"  " + Environment.NewLine
			},
			new PatternReplacer
			{
				Pattern = @"</h[1-6]>",
				Replacement = Environment.NewLine + Environment.NewLine
			},
			new PatternReplacer
			{
				Pattern = @"<h1[^>]*>",
				Replacement = Environment.NewLine + Environment.NewLine + "# "
			},
			new PatternReplacer
			{
				Pattern = @"<h2[^>]*>",
				Replacement = Environment.NewLine + Environment.NewLine + "## "
			},
			new PatternReplacer
			{
				Pattern = @"<h3[^>]*>",
				Replacement = Environment.NewLine + Environment.NewLine + "### "
			},
			new PatternReplacer
			{
				Pattern = @"<h4[^>]*>",
				Replacement = Environment.NewLine + Environment.NewLine + "#### "
			},
			new PatternReplacer
			{
				Pattern = @"<h5[^>]*>",
				Replacement = Environment.NewLine + Environment.NewLine + "##### "
			},
			new PatternReplacer
			{
				Pattern = @"<h6[^>]*>",
				Replacement = Environment.NewLine + Environment.NewLine + "###### "
			},
			new PatternReplacer
			{
				Pattern = @"<blockquote>",
				Replacement = Environment.NewLine + Environment.NewLine + @">"
			},
			new PatternReplacer
			{
				Pattern = @"</blockquote>",
				Replacement = Environment.NewLine + Environment.NewLine
			},
			new PatternReplacer
			{
				Pattern = @"<p>",
				Replacement = Environment.NewLine + Environment.NewLine
			},
			new PatternReplacer
			{
				Pattern = @"</p>",
				Replacement = Environment.NewLine
			},
			new PatternReplacer
			{
				Pattern = @"<hr/>",
				Replacement = Environment.NewLine + Environment.NewLine + "* * *" + Environment.NewLine
			},
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
				Pattern = @"<title>.*?</title>",
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
			new CustomReplacer
			{
				CustomAction = HtmlParser.ReplaceImg
			},
			new CustomReplacer
			{
				CustomAction = HtmlParser.ReplaceLists
			},
			new CustomReplacer
			{
				CustomAction = HtmlParser.ReplaceAnchor
			},
			new CustomReplacer
			{
				CustomAction = HtmlParser.ReplaceCode
			},
			new CustomReplacer
			{
				CustomAction = HtmlParser.ReplacePre
			}
		};

		/// <summary>
		/// Converts Html contained in a file to a Markdown string
		/// </summary>
		/// <param name="path">The path to the file which is being converted</param>
		/// <returns>A Markdown representation of the passed in Html</returns>
		public string ConvertFile(string path)
		{
			using (var reader = new StreamReader(path))
			{
				var html = reader.ReadToEnd();
				html = StandardiseWhitespace(html);
				return Convert(html);
			}
		}

		private static string StandardiseWhitespace(string html)
		{
			return Regex.Replace(html, @"([^\r])\n", "$1\r\n");
		}

		/// <summary>
		/// Converts an Html string to a Markdown string
		/// </summary>
		/// <param name="html">The Html string you wish to convert</param>
		/// <returns>A Markdown representation of the passed in Html</returns>
		public string Convert(string html)
		{
			return CleanWhiteSpace(_replacers.Aggregate(html, (current, element) => element.Replace(current)));
		}

		private static string CleanWhiteSpace(string markdown)
		{
			var cleaned = Regex.Replace(markdown, @"\r\n\s+\r\n", "\r\n\r\n");
			cleaned = Regex.Replace(cleaned, @"(?:\r\n){3,}", "\r\n\r\n");
			cleaned = Regex.Replace(cleaned, @"^(\r\n)+", "");
			cleaned = Regex.Replace(cleaned, @"(\r\n)+$", "");
			return cleaned;
		}
	}
}
