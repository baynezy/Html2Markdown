using System.Text.RegularExpressions;

namespace Html2Markdown.Replacement;
/// <summary>
/// Allows replacement with a regular expression.
/// </summary>
public class PatternReplacer : IReplacer
{
	public string Pattern { get; init; }

	public string Replacement { get; init; }
	public string Replace(string html)
	{
		var regex = new Regex(Pattern);

		return regex.Replace(html, Replacement);
	}
}