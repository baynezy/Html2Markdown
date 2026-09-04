using Html2Markdown.Renderers;

namespace Html2Markdown;

/// <summary>
/// Provides rendering helpers for custom HTML tag renderers.
/// </summary>
public sealed class HtmlTagRenderingContext
{
    private readonly MarkdownRenderer renderer;
    private readonly ConversionContext context;

    internal HtmlTagRenderingContext(MarkdownRenderer renderer, ConversionContext context)
    {
        this.renderer = renderer;
        this.context = context;
    }

    /// <summary>
    /// Renders all child nodes of the supplied parent node using the active renderer set.
    /// </summary>
    /// <param name="parent">The parent node whose children should be rendered.</param>
    /// <returns>The Markdown representation of the parent node's children.</returns>
    public string RenderChildren(INode parent)
    {
        ArgumentNullException.ThrowIfNull(parent);

        return renderer.RenderChildren(parent, context);
    }

    /// <summary>
    /// Renders a node using the active renderer set.
    /// </summary>
    /// <param name="node">The node to render.</param>
    /// <returns>The Markdown representation of the node.</returns>
    public string Render(INode node)
    {
        ArgumentNullException.ThrowIfNull(node);

        return renderer.Render(node, context);
    }

    internal bool IsInList => context.IsInList;

    internal int ListDepth => context.ListDepth;

    internal string ListType => context.ListType;

    internal int OrderedListIndex => context.OrderedListIndex;

    internal HtmlTagRenderingContext EnterList(string listType) =>
        new(renderer, context.EnterList(listType));

    internal HtmlTagRenderingContext WithOrderedListIndex(int index) =>
        new(renderer, context.WithOrderedListIndex(index));

    internal string RenderWithDefaultContext(INode node) =>
        renderer.Render(node, ConversionContext.Default);

    internal string RenderChildrenWithDefaultContext(INode parent) =>
        renderer.RenderChildren(parent, ConversionContext.Default);
}
