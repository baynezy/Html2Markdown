using System.IO;

namespace Html2Markdown;

/// <summary>
/// An HTML to Markdown converter.
/// </summary>
public class Converter
{
    private readonly IReadOnlyCollection<IHtmlTagRenderer> _tagRenderers;
    private readonly bool _convertTables;

    /// <summary>
    /// Initialises a new instance of the <see cref="Converter"/> class with the default conversion options.
    /// </summary>
    public Converter() : this(new ConverterOptions())
    {
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="Converter"/> class.
    /// </summary>
    /// <param name="options">The options used to configure conversion.</param>
    public Converter(ConverterOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        _tagRenderers = [.. options.TagRenderers];
        if (_tagRenderers.Any(renderer => renderer is null))
        {
            throw new ArgumentException("Tag renderers cannot contain null.", nameof(options));
        }

        if (_tagRenderers.Any(renderer => string.IsNullOrWhiteSpace(renderer.TagName)))
        {
            throw new ArgumentException("Tag renderer names cannot be empty.", nameof(options));
        }

        _convertTables = options.ConvertTables;
    }

	/// <summary>
	/// Converts HTML contained in a file to a Markdown string
	/// </summary>
	/// <param name="path">The path to the file which is being converted</param>
	/// <returns>A Markdown representation of the passed in HTML</returns>
	public string ConvertFile(string path)
	{
		using var stream = new FileStream(path, FileMode.Open);
		using var reader = new StreamReader(stream);
		var html = reader.ReadToEnd();
		html = StandardiseWhitespace(html);
		return Convert(html);
	}

	/// <summary>
	/// Converts an HTML string to a Markdown string
	/// </summary>
	/// <param name="html">The HTML string you wish to convert</param>
	/// <returns>A Markdown representation of the passed in HTML</returns>
	public string Convert(string html)
	{
        ArgumentNullException.ThrowIfNull(html);

		return HtmlToMarkdownConverter.Convert(html, _tagRenderers, _convertTables);
	}

    private static string StandardiseWhitespace(string html)
    {
        return html
            .Replace("\r\n", "\n")
            .Replace("\r", "\n")
            .Replace("\n", Environment.NewLine);
    }
}