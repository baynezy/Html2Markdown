namespace Html2Markdown.Renderers;

internal static class MarkdownFormatting
{
    internal static string Wrap(string content, string marker)
    {
        if (content.Length == 0)
        {
            return string.Empty;
        }

        var leadingWhitespace = content.TakeWhile(char.IsWhiteSpace)
            .Count();
        var trailingWhitespace = content.Reverse()
            .TakeWhile(char.IsWhiteSpace)
            .Count();
        var leading = leadingWhitespace > 0 ? " " : string.Empty;
        var core = content[leadingWhitespace..^trailingWhitespace];
        var trailing = trailingWhitespace > 0 ? " " : string.Empty;

        return $"{leading}{marker}{core}{marker}{trailing}";
    }

    internal static string Block(string content) =>
        $"{Environment.NewLine}{Environment.NewLine}{content}{Environment.NewLine}{Environment.NewLine}";

    internal static string CollapseWhitespace(string value) =>
        string.Join(" ", value.Split((char[]) null, StringSplitOptions.RemoveEmptyEntries));

    internal static string NormaliseBlockWhitespace(string markdown)
    {
        StringBuilder builder = new();
        var consecutiveNewLines = 0;
        foreach (var character in markdown.Where(character => character != '\r'))
        {
            if (character == '\n')
            {
                consecutiveNewLines++;
                if (consecutiveNewLines <= 2)
                {
                    builder.Append(Environment.NewLine);
                }

                continue;
            }

            consecutiveNewLines = 0;
            builder.Append(character);
        }

        return builder.ToString();
    }
}
