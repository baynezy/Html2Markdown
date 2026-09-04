# HTML2Markdown

Converts HTML to [Markdown](http://daringfireball.net/projects/markdown/syntax).

![Html2Markdown](https://cloud.githubusercontent.com/assets/1049999/11505182/0480ad76-9841-11e5-8a62-126d4b7c03be.png)

## Documentation

Fully navigable documentation available on [GitHub Pages](http://baynezy.github.io/Html2Markdown/)

## Support

This project will currently convert the following HTML tags:-

- `<a>`
- `<strong>`
- `<b>`
- `<em>`
- `<i>`
- `<br>`
- `<code>`
- `<h1>`
- `<h2>`
- `<h3>`
- `<h4>`
- `<h5>`
- `<h6>`
- `<blockquote>`
- `<img>`
- `<hr>`
- `<p>`
- `<pre>`
- `<ul>`
- `<ol>`

The converter preserves the original HTML for `<div>`, `<table>`, `<iframe>`, and
`<canvas>` elements, including their contents. Set `ConverterOptions.ConvertTables`
to convert supported tables to GitHub-Flavoured Markdown instead.

## Installing via NuGet

    Install-Package Html2Markdown

## Usage

### Strings

    var html = "Something to <strong>convert</strong>";
    var converter = new Converter();
    var markdown = converter.Convert(html);

### Files

    var path = "file.html";
    var converter = new Converter();
    var markdown = converter.ConvertFile(path);

### Custom tags

Register tag renderers when you need to add support for an unsupported tag or
replace the default behaviour for a supported tag. Custom renderers receive the
current HTML element and a rendering context that can render child nodes through
the active renderer set.

```csharp
using AngleSharp.Dom;
using Html2Markdown;

var options = new ConverterOptions();
options.TagRenderers.Add(new MarkTagRenderer());

var converter = new Converter(options);
var markdown = converter.Convert("<mark>highlighted</mark>");

public sealed class MarkTagRenderer : IHtmlTagRenderer
{
    public string TagName => "mark";

    public string Render(IElement element, HtmlTagRenderingContext context)
    {
        return $"=={context.RenderChildren(element)}==";
    }
}
```

If a custom renderer uses the same `TagName` as a built-in renderer, the custom
renderer replaces the built-in behaviour for that tag. Tag names are matched
case-insensitively, and options are copied when the `Converter` is constructed.

### Tables

Tables are preserved as raw HTML by default. Enable GitHub-Flavoured Markdown
table conversion when needed:

```csharp
var options = new ConverterOptions { ConvertTables = true };
var converter = new Converter(options);
var markdown = converter.Convert("<table><tr><th>Name</th></tr><tr><td>Sam</td></tr></table>");
```

Enabled conversion creates a GitHub-Flavoured Markdown pipe table. It supports
headers, `align="left"`, `align="center"`, and `align="right"`, plus `rowspan`
and `colspan`; spanned positions are represented by empty Markdown cells.
Tables without a header use an empty synthetic header, and captions are emitted
after the table. Tables with multiline cell content remain raw HTML because they
cannot be represented safely as a Markdown table.

## Observability

The library emits OpenTelemetry-compatible traces and metrics through the built-in
.NET diagnostics APIs, so no additional package reference is required. Telemetry is
only collected when your application subscribes to it.

Both the `ActivitySource` and the `Meter` are named `Html2Markdown`, which is
exposed as `Html2Markdown.Observability.ActivityConfig.ServiceName`.

| Signal | Name                     | Details                                                                                       |
|--------|--------------------------|-----------------------------------------------------------------------------------------------|
| Trace  | `Render <tag>`           | An activity is started for each HTML element that is rendered, for example `Render strong`.    |
| Metric | `html.elements.rendered` | Counter of rendered HTML elements, tagged with `tag` (the element's local name, e.g. `p`).     |

Subscribe with the OpenTelemetry SDK:

```csharp
using Html2Markdown.Observability;

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(ActivityConfig.ServiceName))
    .WithMetrics(metrics => metrics.AddMeter(ActivityConfig.ServiceName));
```

Nested elements produce nested activities, so a rendered document forms a trace
tree that mirrors the structure of the source HTML.

## Try it

This library is showcased at [http://html2markdown.bayn.es](http://html2markdown.bayn.es).

## Contributing

For those interested in contributing then [please read the guidelines](http://bit.ly/html2md-contributing)

## License

This project is licensed under [Apache License 2.0](http://www.apache.org/licenses/LICENSE-2.0).