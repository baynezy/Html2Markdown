using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace Html2Markdown.Replacement;

internal static partial class HtmlParser
{
	private static readonly Regex NoChildren = HtmlListHasNoChildren();

	internal static string ReplaceLists(string html)
	{
		var finalHtml = html;
		while (HasNoChildLists(finalHtml))
		{
			var listToReplace = NoChildren.Match(finalHtml).Value;
			var formattedList = ReplaceList(listToReplace);
			finalHtml = finalHtml.Replace(listToReplace, formattedList);
		}

		return finalHtml;
	}

	private static string ReplaceList(string html)
	{
		var list = FindHtmlList().Match(html);
		var listType = list.Groups[1].Value;
		var listItems = FindHtmlListItems().Split(list.Groups[2].Value);
			
		if (ListOnlyHasEmptyStringsForChildren(listItems)) return string.Empty;
			
		listItems = listItems.Skip(1).ToArray();
			
		if (ListIsEmpty(listItems)) return string.Empty;
			
		var counter = 0;
		var markdownList = new List<string>();
		listItems.ToList().ForEach(listItem =>
		{
			var listPrefix = listType.Equals("ol") ? $"{++counter}.  " : "*   ";
			//In case of multiline Html, a line can end with a new line. In this case we want to remove the closing tag as well as the new line
			//otherwise we may only keep the line breaks between tags and create a double line break in the markdown
			var closingTag = listItem.EndsWith($"</li>{Environment.NewLine}") ? $"</li>{Environment.NewLine}" : "</li>";
			var finalList = listItem.Replace(closingTag, string.Empty);

			if (finalList.Trim().Length == 0) {
				return;
			}

			finalList = SpacesAtTheStartOfALine().Replace(finalList, string.Empty);
			finalList = TwoNewLines().Replace(finalList, $"{Environment.NewLine}{Environment.NewLine}    ");
			// indent nested lists
			finalList = NestedList().Replace(finalList, "\n$1    $2");
			// remove the indent from the first line
			if (listItem.StartsWith("<p>"))
			{
				finalList = ReplaceParagraph(finalList, true);
			}
			markdownList.Add($"{listPrefix}{finalList}");
		});

		//If a new line is already ending the markdown item, then we don't need to add another one
		return Environment.NewLine + Environment.NewLine + markdownList.Aggregate((current, item) =>  current.EndsWith(Environment.NewLine) ? current + item : current + Environment.NewLine + item) + Environment.NewLine + Environment.NewLine;
	}

	private static bool ListIsEmpty(IReadOnlyCollection<string> listItems)
	{
		return listItems.Count == 0;
	}

	private static bool ListOnlyHasEmptyStringsForChildren(IEnumerable<string> listItems)
	{
		return listItems.All(string.IsNullOrEmpty);
	}

	private static bool HasNoChildLists(string html)
	{
		return NoChildren.Match(html).Success;
	}

	internal static string ReplacePre(string html)
	{
		var doc = GetHtmlDocument(html);
		var nodes = doc.DocumentNode.SelectNodes("//pre");
		if (nodes == null) {
			return html;
		}

		nodes.ToList().ForEach(node =>
		{
			var tagContents = node.InnerHtml;
			var markdown = ConvertPre(tagContents);

			ReplaceNode(node, markdown);
		});

		return doc.DocumentNode.OuterHtml;
	}

	private static string ConvertPre(string html)
	{
		var tag = TabsToSpaces(html);
		tag = IndentNewLines(tag);
		return Environment.NewLine + Environment.NewLine + tag + Environment.NewLine;
	}

	private static string IndentNewLines(string tag)
	{
		return tag.Replace(Environment.NewLine, Environment.NewLine + "    ");
	}

	private static string TabsToSpaces(string tag)
	{
		return tag.Replace("\t", "    ");
	}

	internal static string ReplaceImg(string html)
	{
		var doc = GetHtmlDocument(html);
		var nodes = doc.DocumentNode.SelectNodes("//img");
		if (nodes == null) {
			return html;
		}

		nodes.ToList().ForEach(node =>
		{
					
			var src = node.Attributes.GetAttributeOrEmpty("src");
			var alt = node.Attributes.GetAttributeOrEmpty("alt");
			var title = node.Attributes.GetAttributeOrEmpty("title");

			var markdown = $"![{alt}]({src}{(title.Length > 0 ? $" \"{title}\"" : "")})";

			ReplaceNode(node, markdown);
		});

		return doc.DocumentNode.OuterHtml;
	}

	internal static string ReplaceAnchor(string html)
	{
		var doc = GetHtmlDocument(html);
		var nodes = doc.DocumentNode.SelectNodes("//a");
		if (nodes == null) {
			return html;
		}

		nodes.ToList().ForEach(node =>
		{
			var linkText = node.InnerHtml;
			var href = node.Attributes.GetAttributeOrEmpty("href");
			var title = node.Attributes.GetAttributeOrEmpty("title");

			var markdown = "";

			if (!IsEmptyLink(linkText, href))
			{
				markdown = $"[{linkText}]({href}{(title.Length > 0 ? $" \"{title}\"" : "")})";
			}

			ReplaceNode(node, markdown);
		});

		return doc.DocumentNode.OuterHtml;
	}
	
	internal static string ReplaceCode(string html, bool supportSyntaxHighlighting)
	{
		var doc = GetHtmlDocument(html);
		var nodes = doc.DocumentNode.SelectNodes("//code");

		if (nodes == null) {
			return html;
		}

		nodes.ToList().ForEach(node =>
		{
			var code = node.InnerHtml;
			var language = supportSyntaxHighlighting ? GetSyntaxHighlightLanguage(node) : "";

			string markdown;
			if(IsSingleLineCodeBlock(code))
			{
				markdown = "`" + code + "`";
			}
			else
			{
				markdown = ReplaceBreakTagsWithNewLines(code);
				markdown = InitialCrLf().Replace(markdown, "");
				markdown = FinalCrLf().Replace(markdown, "");
				markdown = "```" + language + Environment.NewLine + markdown + Environment.NewLine + "```";
			}

			ReplaceNode(node, markdown);
		});

		return doc.DocumentNode.OuterHtml;
	}

	private static string ReplaceBreakTagsWithNewLines(string code)
	{
		return BreakTag().Replace(code, "");
	}

	private static bool IsSingleLineCodeBlock(string code)
	{
		// single line code blocks do not have new line characters
		return !code.Contains(Environment.NewLine);
	}

	private static string GetSyntaxHighlightLanguage(HtmlNode node)
	{
		// extract the language for syntax highlighting from a code tag
		// depending on the implementations, language can be declared in the tag as :
		// <code class="language-csharp">
		// <code class="lang-csharp">
		// <code class="csharp">
		var classAttributeValue = node.Attributes["class"]?.Value;

		if(string.IsNullOrEmpty(classAttributeValue)){
			return string.Empty;
		}

		return classAttributeValue.StartsWith("lang") 
			? classAttributeValue.Split('-').Last() 
			: classAttributeValue;
	}

	internal static string ReplaceBlockquote(string html)
	{
		var doc = GetHtmlDocument(html);
		var nodes = doc.DocumentNode.SelectNodes("//blockquote");
		if (nodes == null) {
			return html;
		}

		nodes.ToList().ForEach(node =>
		{
			var quote = node.InnerHtml;
			var lines = quote.TrimStart().Split(new[] { Environment.NewLine }, StringSplitOptions.None);
			var markdown = "";

			lines.ToList().ForEach(line =>
			{
				markdown += $"> {line.TrimEnd()}{Environment.NewLine}";
			});

			markdown = EmptyQuoteLines().Replace(markdown, "");

			markdown = Environment.NewLine + Environment.NewLine + markdown + Environment.NewLine + Environment.NewLine;

			ReplaceNode(node, markdown);
		});

		return doc.DocumentNode.OuterHtml;
	}

	internal static string ReplaceEntities(string html)
	{
		return WebUtility.HtmlDecode(html);
	}

	internal static string ReplaceParagraph(string html) => ReplaceParagraph(html, false);

	private static string ReplaceParagraph(string html, bool nestedIntoList)
	{
		var doc = GetHtmlDocument(html);
		var nodes = doc.DocumentNode.SelectNodes("//p");
		if (nodes == null) {
			return html;
		}

		nodes.ToList().ForEach(node =>
		{
			var text = node.InnerHtml;
			var markdown = Spaces().Replace(text, " ");
			markdown = markdown.Replace(Environment.NewLine, " ");

			//If a paragraph is contained in a list, we don't want to add new line characters
			var openingTag = nestedIntoList ? "" : Environment.NewLine + Environment.NewLine;
			var closingTag = nestedIntoList ? "" : Environment.NewLine;

			markdown = openingTag + markdown + closingTag;
			ReplaceNode(node, markdown);
		});

		return doc.DocumentNode.OuterHtml;
	}

	private static bool IsEmptyLink(string linkText, string href)
	{
		var length = linkText.Length + href.Length;
		return length == 0;
	}

	private static HtmlDocument GetHtmlDocument(string html)
	{
		var doc = new HtmlDocument();
		doc.LoadHtml(html);
		return doc;
	}

	private static void ReplaceNode(HtmlNode node, string markdown)
	{
		if (string.IsNullOrEmpty(markdown))
		{
			node.ParentNode.RemoveChild(node);
		}
		else
		{
			node.ReplaceNodeWithString(markdown);
		}
	}

    [GeneratedRegex(@"<(ul|ol)\b[^>]*>([\s\S]*?)<\/\1>")]
    private static partial Regex FindHtmlList();
    [GeneratedRegex(@"<(ul|ol)\b[^>]*>(?:(?!<ul|<ol)[\s\S])*?<\/\1>")]
    private static partial Regex HtmlListHasNoChildren();
    [GeneratedRegex("<li[^>]*>")]
    private static partial Regex FindHtmlListItems();
    [GeneratedRegex(@"\s+")]
    private static partial Regex Spaces();
    [GeneratedRegex(@"(>\s\r?\n)+$")]
    private static partial Regex EmptyQuoteLines();
    [GeneratedRegex(@"^\s+")]
    private static partial Regex SpacesAtTheStartOfALine();
    [GeneratedRegex("\\n{2}")]
    private static partial Regex TwoNewLines();
    [GeneratedRegex(@"\n([ ]*)+(\*|\d+\.)")]
    private static partial Regex NestedList();
    [GeneratedRegex("^\r?\n")]
    private static partial Regex InitialCrLf();
    [GeneratedRegex("\r?\n$")]
    private static partial Regex FinalCrLf();
    [GeneratedRegex(@"<\s*?/?\s*?br\s*?>")]
    private static partial Regex BreakTag();
}