namespace Html2Markdown.Renderers;

internal static class MarkdownFormatting
{
    internal static string Wrap(string content, string marker)
    {
        if (content.Length == 0)
        {
            return string.Empty;
        }

        var span = content.AsSpan();
        var core = span.Trim();

        if (core.IsEmpty)
        {
            return " ";
        }

        var leading = char.IsWhiteSpace(span[0]) ? " " : string.Empty;
        var trailing = char.IsWhiteSpace(span[^1]) ? " " : string.Empty;

        return $"{leading}{marker}{core}{marker}{trailing}";
    }

    internal static string Block(string content) =>
        $"{Environment.NewLine}{Environment.NewLine}{content}{Environment.NewLine}{Environment.NewLine}";

    internal static string CollapseWhitespace(string value) =>
        string.Join(" ", value.Split((char[]) null, StringSplitOptions.RemoveEmptyEntries));

    internal static string NormaliseBlockWhitespace(string markdown)
    {
        // This is here to reduce the number of allocations for strings that don't contain any newlines, which is a
        // common case.
        if (markdown.AsSpan()
                .IndexOfAny('\r', '\n') == -1)
        {
            return markdown;
        }

        StringBuilder builder = new();
        var consecutiveNewLines = 0;

        // Not using LINQ here to avoid creating an intermediate collection of characters, which would be inefficient
        // for large strings.
        foreach (var character in markdown)
        {
            switch (character)
            {
                case '\r':
                    continue;
                case '\n':
                {
                    consecutiveNewLines++;
                    if (consecutiveNewLines <= 2)
                    {
                        builder.Append(Environment.NewLine);
                    }

                    continue;
                }
                default:
                    consecutiveNewLines = 0;
                    builder.Append(character);
                    break;
            }
        }

        return builder.ToString();
    }
}