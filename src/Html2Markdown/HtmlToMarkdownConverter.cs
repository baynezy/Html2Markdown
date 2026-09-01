using AngleSharp.Html.Parser;
using Html2Markdown.Renderers;

namespace Html2Markdown;

internal static class HtmlToMarkdownConverter
{
    internal static string Convert(string html, IReadOnlyCollection<IHtmlTagRenderer> tagRenderers)
    {
        HtmlParser parser = new();
        var document = parser.ParseDocument(html);
        var markdown = new MarkdownRenderer(tagRenderers).RenderChildren(document.Body, ConversionContext.Default);

        return MarkdownFormatting.NormaliseBlockWhitespace(markdown)
            .Trim();
    }
}

internal sealed record ConversionContext(int ListDepth, string ListType, int OrderedListIndex)
{
    internal static ConversionContext Default { get; } = new(0, string.Empty, 1);

    internal bool IsInList => ListDepth > 0;

    internal ConversionContext EnterList(string listType) => new(ListDepth + 1, listType, 1);

    internal ConversionContext WithOrderedListIndex(int index) => this with {OrderedListIndex = index};
}