namespace Html2Markdown.Renderers;

internal sealed class MarkdownRenderer
{
    private readonly Dictionary<string, IHtmlTagRenderer> _tagRenderers;

    internal MarkdownRenderer(IEnumerable<IHtmlTagRenderer> customTagRenderers)
    {
        ArgumentNullException.ThrowIfNull(customTagRenderers);

        _tagRenderers = HtmlTagRenderers.Defaults.ToDictionary(
            renderer => renderer.TagName,
            StringComparer.OrdinalIgnoreCase);

        foreach (var renderer in customTagRenderers)
        {
            ArgumentNullException.ThrowIfNull(renderer);

            if (string.IsNullOrWhiteSpace(renderer.TagName))
            {
                throw new ArgumentException("Tag renderer names cannot be empty.", nameof(customTagRenderers));
            }

            _tagRenderers[renderer.TagName] = renderer;
        }
    }

    internal string RenderChildren(INode parent, ConversionContext context)
    {
        StringBuilder builder = new();
        foreach (var child in parent.ChildNodes)
        {
            builder.Append(Render(child, context));
        }

        return builder.ToString();
    }

    internal string Render(INode node, ConversionContext context)
    {
        return node switch
        {
            IText text => text.Data,
            IElement element => RenderElement(element, context),
            _ => string.Empty
        };
    }

    private string RenderElement(IElement element, ConversionContext context)
    {
        return _tagRenderers.TryGetValue(element.LocalName, out var renderer)
            ? renderer.Render(element, new HtmlTagRenderingContext(this, context))
            : RenderChildren(element, context);
    }
}