namespace Html2Markdown.Renderers;

internal sealed record MarkdownUrl(string Href, string Text, string Title)
{
    internal string ToMarkdown() => $"[{Text}]({Href}{(Title.Length > 0 ? $" \"{Title}\"" : string.Empty)})";
}
