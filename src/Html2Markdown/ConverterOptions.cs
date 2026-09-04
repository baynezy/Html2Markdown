namespace Html2Markdown;

/// <summary>
/// Configures HTML to Markdown conversion.
/// </summary>
public sealed class ConverterOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether HTML tables are converted to GitHub-Flavoured Markdown tables.
    /// </summary>
    /// <remarks>
    /// The default value is <see langword="false"/>, which preserves tables as raw HTML.
    /// </remarks>
    public bool ConvertTables { get; init; }

    /// <summary>
    /// Gets the custom tag renderers to add to, or replace, the default renderer set.
    /// </summary>
    public ICollection<IHtmlTagRenderer> TagRenderers { get; } = new List<IHtmlTagRenderer>();
}
