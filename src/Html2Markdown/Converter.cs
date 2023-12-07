using System.IO;
using System.Text.RegularExpressions;
using Html2Markdown.Replacement;
using Html2Markdown.Scheme;

namespace Html2Markdown;

/// <summary>
/// An Html to Markdown converter.
/// </summary>
public partial class Converter
{
	private readonly IList<IReplacer> _replacers;
		
	/// <summary>
	/// Create a Converter with the standard Markdown conversion scheme
	/// </summary>
	public Converter() 
	{
			_replacers = new Markdown().Replacers();
		}

	/// <summary>
	/// Create a converter with a custom conversion scheme
	/// </summary>
	/// <param name="scheme">Conversion scheme to control conversion</param>
	public Converter(IScheme scheme)
	{
			_replacers = scheme.Replacers();
		}

	/// <summary>
	/// Converts Html contained in a file to a Markdown string
	/// </summary>
	/// <param name="path">The path to the file which is being converted</param>
	/// <returns>A Markdown representation of the passed in Html</returns>
	public string ConvertFile(string path)
	{
		using var stream = new FileStream(path, FileMode.Open);
		using var reader = new StreamReader(stream);
		var html = reader.ReadToEnd();
		html = StandardiseWhitespace(html);
		return Convert(html);
	}

	private static string StandardiseWhitespace(string html)
	{
		return MatchCarriageReturn().Replace(html, "$1\r\n");
	}

	/// <summary>
	/// Converts an Html string to a Markdown string
	/// </summary>
	/// <param name="html">The Html string you wish to convert</param>
	/// <returns>A Markdown representation of the passed in Html</returns>
	public string Convert(string html)
	{
		return CleanWhiteSpace(_replacers.Aggregate(html, (current, element) => element.Replace(current)));
	}

	private static string CleanWhiteSpace(string markdown)
	{
		var cleaned = MatchCarriageReturnsWithSpacesInBetween().Replace(markdown, Environment.NewLine + Environment.NewLine);
		cleaned = MatchThreeCarriageReturns().Replace(cleaned, Environment.NewLine + Environment.NewLine);
		cleaned = MatchBlockQuotes().Replace(cleaned, "> " + Environment.NewLine + Environment.NewLine);
		cleaned = MatchCarriageReturnsAtTheBeginning().Replace(cleaned, "");
		cleaned = MatchCarriageReturnsAtTheEnd().Replace(cleaned, "");
		return cleaned.Trim();
	}

    [GeneratedRegex(@"([^\r])\n")]
    private static partial Regex MatchCarriageReturn();
    [GeneratedRegex(@"\r?\n\s+\r?\n")]
    private static partial Regex MatchCarriageReturnsWithSpacesInBetween();
    [GeneratedRegex(@"(\r?\n){3,}")]
    private static partial Regex MatchThreeCarriageReturns();
    [GeneratedRegex(@"(> \r?\n){2,}")]
    private static partial Regex MatchBlockQuotes();
    [GeneratedRegex(@"^(\r?\n)+")]
    private static partial Regex MatchCarriageReturnsAtTheBeginning();
    [GeneratedRegex(@"(\r?\n)+$")]
    private static partial Regex MatchCarriageReturnsAtTheEnd();
}