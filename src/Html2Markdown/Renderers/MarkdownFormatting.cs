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

    internal static string CollapseWhitespace(string value)
    {
        var span = value.AsSpan();
        var index = 0;

        index = FindFirstNonWhiteSpaceCharacter(index, span);

        if (ValueOnlyContainsWhiteSpace(index, span))
        {
            return string.Empty;
        }

        var builder = index > 0
            ? new StringBuilder(value.Length)
            : null;
        var wordStart = index;
        var firstWord = true;

        while (index < span.Length)
        {
            index = FindLastNonWhiteSpaceCharacter(index, span);

            var wordEnd = index;

            if (builder is not null)
            {
                if (!firstWord)
                {
                    builder.Append(' ');
                }

                builder.Append(span[wordStart..wordEnd]);
            }

            if (EndOfDocumentIsReached(index, span))
            {
                return builder?.ToString() ?? value;
            }

            var whitespaceStart = index;

            index = FindFirstNonWhiteSpaceCharacter(index, span);

            var isTrailingWhitespace = index == span.Length;
            var isSingleSpace = span[whitespaceStart] == ' ' && index - whitespaceStart == 1;
            if (builder is null && (isTrailingWhitespace || !isSingleSpace))
            {
                builder = new StringBuilder(value.Length);
                builder.Append(span[..wordEnd]);
            }

            wordStart = index;
            firstWord = false;
        }

        return builder?.ToString() ?? value;
    }

    private static bool EndOfDocumentIsReached(int index, ReadOnlySpan<char> span) => index == span.Length;

    private static int FindLastNonWhiteSpaceCharacter(int index, ReadOnlySpan<char> span)
    {
        while (index < span.Length && !char.IsWhiteSpace(span[index]))
        {
            index++;
        }

        return index;
    }

    private static bool ValueOnlyContainsWhiteSpace(int index, ReadOnlySpan<char> span) => index == span.Length;

    private static int FindFirstNonWhiteSpaceCharacter(int index, ReadOnlySpan<char> span)
    {
        while (index < span.Length && char.IsWhiteSpace(span[index]))
        {
            index++;
        }

        return index;
    }

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