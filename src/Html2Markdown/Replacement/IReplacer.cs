namespace Html2Markdown.Replacement
{
	/// <summary>
	/// Searches an entire HTML string and replaces particular matches
	/// </summary>
	public interface IReplacer
	{
		/// <summary>
		/// Replace specific matches in an HTML string
		/// </summary>
		/// <param name="html">The HTML we are converting</param>
		/// <returns>The new string after conversion</returns>
		string Replace(string html);
	}
}