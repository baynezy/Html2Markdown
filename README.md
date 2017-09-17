# HTML2Markdown

Converts HTML to [Markdown](http://daringfireball.net/projects/markdown/syntax).

[![Stories in Ready](https://badge.waffle.io/baynezy/Html2Markdown.svg?label=ready&title=Stories%20in%20Ready)](http://waffle.io/baynezy/Html2Markdown)

---

![Html2Markdown](https://cloud.githubusercontent.com/assets/1049999/11505182/0480ad76-9841-11e5-8a62-126d4b7c03be.png)

## Build Status

| Branch | Status |
| ------ | ------ |
| master | [![master](https://ci.appveyor.com/api/projects/status/cbi6sknslvu3rq6n/branch/master?svg=true)](https://ci.appveyor.com/project/baynezy/html2markdown) |
| develop | [![develop](https://ci.appveyor.com/api/projects/status/cbi6sknslvu3rq6n/branch/develop?svg=true)](https://ci.appveyor.com/project/baynezy/html2markdown) |

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

## Installing via NuGet

[![NuGet version](https://badge.fury.io/nu/Html2Markdown.svg)](http://badge.fury.io/nu/Html2Markdown)

    Install-Package Html2Markdown

## Usage

### Strings

```csharp
var html = "Something to <strong>convert</strong>";
var converter = new Converter();
var markdown = converter.Convert(html);
```

### Files

```csharp
var path = "file.html";
var converter = new Converter();
var markdown = converter.ConvertFile(path);
```

## Try it

This library is showcased at [http://html2markdown.bayn.es](http://html2markdown.bayn.es).

## Contributing

[For those interested in contributing then please read the guidelines](CONTRIBUTING.md)

## License

This project is licensed under [Apache License 2.0](http://www.apache.org/licenses/LICENSE-2.0).