using System.Collections.Generic;
using Html2Markdown.Replacement;

namespace Html2Markdown.Scheme {
	/// <summary>
	/// Allows creation of custom conversion schemes to control conversion
	/// </summary>
	public interface IScheme {
		IList<IReplacer> Replacers();
	}
}