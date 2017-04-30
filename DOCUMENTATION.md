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

## Try it

This library is showcased at [http://html2markdown.bayn.es](http://html2markdown.bayn.es).

## Contributing

For those interested in contributing then [please read the guidelines](http://bit.ly/html2md-contributing)

## License

This project is licensed under [Apache License 2.0](http://www.apache.org/licenses/LICENSE-2.0).