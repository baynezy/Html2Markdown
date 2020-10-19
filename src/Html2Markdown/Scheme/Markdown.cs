using System;
using System.Linq;
using System.Collections.Generic;
using Html2Markdown.Replacement;

namespace Html2Markdown.Scheme
{
	/// <summary>
	/// Collection of IReplacer for converting vanilla Markdown
	/// </summary>
	public class Markdown : AbstractScheme
	{
		public Markdown()
		{
			AddReplacementGroup(_replacers, new TextFormattingReplacementGroup());
			AddReplacementGroup(_replacers, new HeadingReplacementGroup());
			AddReplacementGroup(_replacers, new IllegalHtmlReplacementGroup());
			AddReplacementGroup(_replacers, new LayoutReplacementGroup());
			AddReplacementGroup(_replacers, new EntitiesReplacementGroup());
		}
	}
}