namespace Html2Markdown.Renderers;

/// <summary>
/// Base class for HTML tag renderers that handle list elements (ul, ol, li).
/// </summary>
public abstract class AbstractListRenderer : AbstractRenderer
{
    /// <summary>
    /// Renders a list element (ul or ol) to Markdown, handling nested lists and list items.
    /// </summary>
    /// <param name="element">The list element to render.</param>
    /// <param name="context">The rendering context.</param>
    /// <returns>The rendered Markdown string.</returns>
    protected static string RenderList(IElement element, HtmlTagRenderingContext context)
    {
        var items = element.Children.Where(child => child.LocalName == "li")
            .ToList();
        if (items.Count == 0)
        {
            return string.Concat(element.Children
                .Where(child => child.LocalName is "ul" or "ol")
                .Select(child => RenderList(child, context)));
        }

        var listType = ShouldRenderAsCompatibilityMixedList(element, context, items)
            ? "ul"
            : element.LocalName;
        var listContext = context.EnterList(listType);
        StringBuilder builder = new();
        foreach (var item in items)
        {
            builder.Append(RenderListItem(item, listContext));
            listContext = listContext.WithOrderedListIndex(listContext.OrderedListIndex + 1);
        }

        var list = builder.ToString()
            .TrimEnd();
        return context.IsInList
            ? Environment.NewLine + Environment.NewLine + list
            : MarkdownFormatting.Block(list);
    }
    
    private static bool ShouldRenderAsCompatibilityMixedList(IElement element, HtmlTagRenderingContext context,
        IEnumerable<IElement> items)
    {
        return !context.IsInList
               && element.LocalName == "ol"
               && items.Any(item => item.ChildNodes.Where(child => child is not IElement {LocalName: "ul" or "ol"})
                                        .All(child => string.IsNullOrWhiteSpace(context.Render(child))) &&
                                    item.Children.Any(child => child.LocalName is "ul" or "ol"));
    }
    
    /// <summary>
    /// Renders a list item (li) element to Markdown, handling nested lists and content.
    /// </summary>
    /// <param name="element">The list item element to render.</param>
    /// <param name="context">The rendering context.</param>
    /// <returns>The rendered Markdown string.</returns>
    protected static string RenderListItem(IElement element, HtmlTagRenderingContext context)
    {
        var itemContext = context;
        if (context.ListType == "ol" && int.TryParse(element.GetAttribute("value"), out var value))
        {
            itemContext = itemContext.WithOrderedListIndex(value);
        }

        var children = element.ChildNodes.ToList();
        StringBuilder content = new();
        foreach (var child in children.Where(child => child is not IElement {LocalName: "ul" or "ol"}))
        {
            content.Append(itemContext.Render(child));
        }

        var prefix = context.ListType == "ol" ? $"{itemContext.OrderedListIndex}.  " : "*   ";
        var item = MarkdownFormatting.CollapseWhitespace(content.ToString())
            .Trim();
        var nestedLists = element.Children.Where(child => child.LocalName is "ul" or "ol")
            .ToList();
        switch (item.Length)
        {
            case 0 when nestedLists.Count == 0:
                return string.Empty;
            case 0 when context.ListDepth == 1 && nestedLists.Count == 1:
                return RenderEmptyTopLevelListItemWithNestedList(nestedLists[0], context);
        }

        StringBuilder builder = new();
        if (item.Length > 0)
        {
            builder.Append(new string(' ', (context.ListDepth - 1) * 4));
            builder.Append(prefix);
            builder.Append(item);
        }

        foreach (var nested in nestedLists.Select(nestedList => RenderList(nestedList, context)
                         .TrimEnd('\r', '\n'))
                     .Where(nested => nested.Length != 0))
        {
            builder.Append(nested);
        }

        return builder.Length == 0
            ? string.Empty
            : builder.Append(Environment.NewLine)
                .ToString();
    }
    
    private static string RenderEmptyTopLevelListItemWithNestedList(IElement nestedList, HtmlTagRenderingContext context)
    {
        var items = nestedList.Children.Where(child => child.LocalName == "li")
            .Select(child => MarkdownFormatting.CollapseWhitespace(
                    context.RenderChildrenWithDefaultContext(child))
                .Trim())
            .Where(content => content.Length > 0)
            .ToList();
        if (items.Count == 0)
        {
            return string.Empty;
        }

        StringBuilder builder = new();
        builder.Append("*   1.  ");
        builder.Append(items[0]);
        builder.Append(Environment.NewLine);
        for (var index = 1; index < items.Count; index++)
        {
            builder.Append("    ");
            builder.Append(index + 1);
            builder.Append(".  ");
            builder.Append(items[index]);
            builder.Append(Environment.NewLine);
        }

        return builder.ToString();
    }
}