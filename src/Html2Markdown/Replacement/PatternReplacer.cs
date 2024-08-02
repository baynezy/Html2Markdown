using System.Text.RegularExpressions;

namespace Html2Markdown.Replacement;

/// <summary>
/// Allows replacement with a regular expression.
/// </summary>
public class PatternReplacer : IReplacer
{
    /// <summary>
    /// Gets the pattern to match in the HTML.
    /// </summary>
    public string Pattern { get; init; }

    /// <summary>
    /// Gets the replacement string for the matched pattern.
    /// </summary>
    public string Replacement { get; init; }

    /// <summary>
    /// Replaces occurrences of the pattern in the provided HTML with the replacement string.
    /// </summary>
    /// <param name="html">The HTML content to process.</param>
    /// <returns>The processed HTML with replacements.</returns>
    public string Replace(string html)
    {
        // SECURITY: https://sonarcloud.io/organizations/baynezy/rules?open=csharpsquid%3AS6444&rule_key=csharpsquid%3AS6444
        var regex = new Regex(Pattern, RegexOptions.None, TimeSpan.FromSeconds(1));

        return regex.Replace(html, Replacement);
    }
}