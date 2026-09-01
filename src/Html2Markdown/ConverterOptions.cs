namespace Html2Markdown;

/// <summary>
/// Configures HTML to Markdown conversion.
/// </summary>
public sealed class ConverterOptions
{
    /// <summary>
    /// Gets the custom tag renderers to add to, or replace, the default renderer set.
    /// </summary>
    public ICollection<IHtmlTagRenderer> TagRenderers { get; } = new List<IHtmlTagRenderer>();
}
