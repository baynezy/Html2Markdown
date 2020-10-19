using System.Collections.Generic;
using System.Linq;
using Html2Markdown.Replacement;

namespace Html2Markdown.Scheme
{
	/// <summary>
	/// A group of IReplacer to deal with converting HTML entities
	/// </summary>
	public abstract class AbstractScheme : IScheme {
		protected readonly IList<IReplacer> _replacers = new List<IReplacer>();

		protected void AddReplacementGroup(IList<IReplacer> replacers, IReplacementGroup replacementGroup)
		{
			replacementGroup.Replacers().ToList().ForEach(r => replacers.Add(r));
		}

		public IList<IReplacer> Replacers()
		{
			return _replacers;
		}
	}
}