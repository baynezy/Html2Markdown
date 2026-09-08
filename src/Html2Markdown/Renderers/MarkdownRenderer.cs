using System.Collections.Frozen;
using Html2Markdown.Observability;

namespace Html2Markdown.Renderers;

internal sealed class MarkdownRenderer
{
    private static readonly FrozenDictionary<string, IHtmlTagRenderer> DefaultTagRenderers = BuildDefaults(false);

    private static readonly FrozenDictionary<string, IHtmlTagRenderer> DefaultTagRenderersWithTables =
        BuildDefaults(true);

    private readonly IReadOnlyDictionary<string, IHtmlTagRenderer> _tagRenderers;

    internal MarkdownRenderer(IEnumerable<IHtmlTagRenderer> customTagRenderers, bool convertTables)
    {
        ArgumentNullException.ThrowIfNull(customTagRenderers);

        var defaults = convertTables ? DefaultTagRenderersWithTables : DefaultTagRenderers;
        Dictionary<string, IHtmlTagRenderer> overriddenTagRenderers = null;

        foreach (var renderer in customTagRenderers)
        {
            ArgumentNullException.ThrowIfNull(renderer);

            if (string.IsNullOrWhiteSpace(renderer.TagName))
            {
                throw new ArgumentException("Tag renderer names cannot be empty.", nameof(customTagRenderers));
            }

            overriddenTagRenderers ??= new Dictionary<string, IHtmlTagRenderer>(
                defaults,
                StringComparer.OrdinalIgnoreCase);
            overriddenTagRenderers[renderer.TagName] = renderer;
        }

        _tagRenderers = overriddenTagRenderers ?? (IReadOnlyDictionary<string, IHtmlTagRenderer>)defaults;
    }

    private static FrozenDictionary<string, IHtmlTagRenderer> BuildDefaults(bool convertTables)
    {
        var renderers = HtmlTagRenderers.Defaults.ToDictionary(
            renderer => renderer.TagName,
            StringComparer.OrdinalIgnoreCase);

        if (convertTables)
        {
            renderers["table"] = new MarkdownTableTagRenderer();
        }

        return renderers.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase);
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
        ActivityConfig.RenderedElementsCounter.Add(1, new KeyValuePair<string, object>("tag", element.LocalName));
        using var activity = ActivityConfig.ActivitySource.StartActivity($"Render {element.LocalName}");
        
        return _tagRenderers.TryGetValue(element.LocalName, out var renderer)
            ? renderer.Render(element, new HtmlTagRenderingContext(this, context))
            : RenderChildren(element, context);
    }
}