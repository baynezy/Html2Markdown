using System.Collections.Generic;

namespace Html2Markdown.Replacement
{
	/// <summary>
	/// Groups IReplacer instances into groups. This enables
	/// easier configuration of IScheme
	/// </summary>
	public interface IReplacementGroup
	{
		/// <summary>
		/// Get the IReplacer for this group
		/// </summary>
		/// <returns>Collection of IReplacer for this group</returns>
		IList<IReplacer> Replacers();
	}
}