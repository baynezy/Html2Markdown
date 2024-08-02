namespace Html2Markdown.Replacement;

/// <summary>
/// Allows for multiple replacements to be applied to the HTML.
/// </summary>
public abstract class CompositeReplacer : IReplacer
{
    private readonly IList<IReplacer> _replacements = new List<IReplacer>();

    /// <summary>
    /// Adds a replacer to the list of replacements.
    /// </summary>
    /// <param name="replacer">The <see cref="IReplacer"/> instance to add.</param>
    protected void AddReplacer(IReplacer replacer)
    {
        _replacements.Add(replacer);
    }

    /// <summary>
    /// Applies all replacements to the given HTML string.
    /// </summary>
    /// <param name="html">The HTML string to process.</param>
    /// <returns>The processed HTML string with all replacements applied.</returns>
    public string Replace(string html)
    {
        return _replacements.Aggregate(html, (current, replacer) => replacer.Replace(current));
    }
}