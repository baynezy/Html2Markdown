using System;
using System.Collections.Generic;

namespace Html2Markdown.Replacement
{
	/// <summary>
	/// A group of IReplacer to deal with converting HTML that is
	/// used for layout
	/// </summary>
	public class LayoutReplacementGroup : IReplacementGroup
	{
		private readonly IList<IReplacer> _replacements = new List<IReplacer> {
			new PatternReplacer
			{
				Pattern = @"<hr[^>]*>",
				Replacement = Environment.NewLine + Environment.NewLine + "* * *" + Environment.NewLine
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
			}
		};

		public IList<IReplacer> Replacers()
		{
			return _replacements;
		}
	}
}