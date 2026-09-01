# Migrating from Html2Markdown 7 to 8

Html2Markdown 8 replaces the v7 regular-expression replacement pipeline with
depth-first traversal of an AngleSharp HTML DOM. This is a breaking release.

## Update the package

Update the `Html2Markdown` package reference to version 8.x.

```xml
<PackageReference Include="Html2Markdown" Version="8.*" />
```

Or, with the .NET CLI:

```pwsh
dotnet add package Html2Markdown --version 8.*
```

## Public API retained

The standard conversion API is unchanged. Existing uses of the parameterless
`Converter` constructor, `Convert`, and `ConvertFile` continue to work.

```csharp
var converter = new Converter();
var markdown = converter.Convert("<strong>Important</strong>");
```

## Remove custom schemes

Version 7 allowed callers to customise conversion by implementing `IScheme` and
passing it to `Converter`.

```csharp
var converter = new Converter(customConversionScheme);
```

Version 8 removes `IScheme`, the `Scheme` namespace, replacement types, and the
`Converter(IScheme)` constructor. For standard conversions, remove scheme
implementations and construct `Converter` without an argument.

```csharp
var converter = new Converter();
```

If the removed scheme added or replaced tag-specific conversion behaviour, move
that behaviour to an `IHtmlTagRenderer` and register it with `ConverterOptions`.
The rendering context can render child nodes through the active renderer set.

```csharp
using AngleSharp.Dom;
using Html2Markdown;

var options = new ConverterOptions();
options.TagRenderers.Add(new MarkTagRenderer());

var converter = new Converter(options);

public sealed class MarkTagRenderer : IHtmlTagRenderer
{
    public string TagName => "mark";

    public string Render(IElement element, HtmlTagRenderingContext context)
    {
        return $"=={context.RenderChildren(element)}==";
    }
}
```

When a custom renderer uses the same `TagName` as a built-in renderer, it
replaces the built-in behaviour for that tag. Tag names are matched
case-insensitively, and options are copied when the `Converter` is constructed.
If the removed scheme changed behaviour outside a single HTML tag, apply that
transformation before calling `Convert` or transform the resulting Markdown
afterwards.

## Review converted Markdown

The new DOM-based pipeline parses HTML before conversion. Review converted
output, particularly if inputs contain malformed HTML or rely on the
regex-based handling of comments, entities, whitespace, or nesting in version
7.

Version 8 preserves the original HTML, including its contents, for these block
elements by default:

- `<div>`
- `<table>`
- `<iframe>`
- `<canvas>`

Set `ConverterOptions.ConvertTables` to `true` when table content should be
converted to GitHub-Flavoured Markdown:

```csharp
var options = new ConverterOptions { ConvertTables = true };
var converter = new Converter(options);
var markdown = converter.Convert("<table><tr><th>Name</th></tr><tr><td>Sam</td></tr></table>");
```

Enabled table conversion supports headers, cell alignment, `rowspan`, and
`colspan`. Markdown cannot represent merged cells directly, so positions
covered by a span become empty cells. Tables without a header receive an empty
synthetic header. Tables containing multiline cell content remain raw HTML.

Update downstream consumers if they expected table contents to be converted to
Markdown or discarded without enabling this option. Continue to review documents
that contain tables when upgrading.

## Validate before deployment

Run representative production HTML through version 8 and compare the resulting
Markdown with the output expected by downstream renderers, storage, and search
processes. Pay particular attention to documents containing custom v7 schemes,
the preserved HTML block elements, and imperfect HTML markup.
