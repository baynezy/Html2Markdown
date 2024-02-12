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
		// SECURITY: https://sonarcloud.io/organizations/baynezy/rules?open=csharpsquid%3AS6444&rule_key=csharpsquid%3AS6444
		var regex = new Regex(Pattern, RegexOptions.None, TimeSpan.FromSeconds(1));

		return regex.Replace(html, Replacement);
	}
}