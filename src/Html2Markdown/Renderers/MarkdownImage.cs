namespace Html2Markdown.Renderers;

internal sealed record MarkdownImage(string Src, string Alt, string Title)
{
    internal string ToMarkdown() => $"![{Alt}]({Src}{(Title.Length > 0 ? $" \"{Title}\"" : string.Empty)})";
}
