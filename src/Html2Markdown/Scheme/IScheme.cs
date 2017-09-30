using System.Collections.Generic;
using Html2Markdown.Replacement;

namespace Html2Markdown.Scheme {
	/// <summary>
	/// Allows creation of custom conversion schemes to control conversion
	/// </summary>
	public interface IScheme {
		/// <summary>
		/// The collection of IReplacer that for this scheme
		/// </summary>
		/// <returns>IList of IReplacer for use by Converter</returns>
		IList<IReplacer> Replacers();
	}
}