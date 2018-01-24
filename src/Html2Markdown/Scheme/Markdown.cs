using System;
using System.Collections.Generic;
using Html2Markdown.Replacement;

namespace Html2Markdown.Scheme {
	/// <summary>
	/// Collection of IReplacer for converting vanilla Markdown
	/// </summary>
	public class Markdown : IScheme
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
				Pattern = @"<hr[^>]*>",
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
				Pattern = @"<title[^>]*>.*?</title>",
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
			new PatternReplacer
			{
				Pattern = @"</?script[^>]*>",
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
			},
			new CustomReplacer
			{
				CustomAction = HtmlParser.ReplaceParagraph
			},
			new PatternReplacer
			{
				Pattern = @"<br[^>]*>",
				Replacement = @"  " + Environment.NewLine
			},
			new CustomReplacer
			{
				CustomAction = HtmlParser.ReplaceBlockquote
			},
			new CustomReplacer
			{
				CustomAction = HtmlParser.ReplaceEntites
			}
		};
		public IList<IReplacer> Replacers()
		{
			return _replacers;
		}
	}
}