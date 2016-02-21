# HTML2Markdown

Converts HTML to [Markdown](http://daringfireball.net/projects/markdown/syntax).

[![Stories in Ready](https://badge.waffle.io/baynezy/Html2Markdown.svg?label=ready&title=Stories%20in%20Ready)](http://waffle.io/baynezy/Html2Markdown)

---

![Html2Markdown](https://cloud.githubusercontent.com/assets/1049999/11505182/0480ad76-9841-11e5-8a62-126d4b7c03be.png)

## Build Status
<table>
    <tr>
        <th>master</th>
		<td><a href="https://ci.appveyor.com/project/baynezy/html2markdown"><img src="https://ci.appveyor.com/api/projects/status/cbi6sknslvu3rq6n/branch/master?svg=true" alt="master" title="master" /></a></td>
    </tr>
    <tr>
        <th>develop</th>
		<td><a href="https://ci.appveyor.com/project/baynezy/html2markdown"><img src="https://ci.appveyor.com/api/projects/status/cbi6sknslvu3rq6n/branch/develop?svg=true" alt="develop" title="develop" /></a></td>
    </tr>
</table>

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

### Pull Requests

After forking the repository please create a pull request before creating the fix. This way we can talk about how the fix will be implemented. This will greatly increase your chance of your patch getting merged into the code base.
