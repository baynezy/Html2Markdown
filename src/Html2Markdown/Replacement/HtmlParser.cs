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
			var finalList = listItem.Replace(@"</li>", string.Empty);

			if (finalList.Trim().Length == 0) {
				return;
			}

			finalList = Regex.Replace(finalList, @"^\s+", string.Empty);
			finalList = Regex.Replace(finalList, @"\n{2}", $"{Environment.NewLine}{Environment.NewLine}    ");
			// indent nested lists
			finalList = Regex.Replace(finalList, @"\n([ ]*)+(\*|\d+\.)", "\n$1    $2");
			markdownList.Add($"{listPrefix}{finalList}");
		});

		return Environment.NewLine + Environment.NewLine + markdownList.Aggregate((current, item) => current + Environment.NewLine + item) + Environment.NewLine + Environment.NewLine;
	}

	private static bool ListIsEmpty(string[] listItems)
	{
		return listItems.Length == 0;
	}

	private static bool ListOnlyHasEmptyStringsForChildren(string[] listItems)
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

			var markdown = $@"![{alt}]({src}{(title.Length > 0 ? $" \"{title}\"" : "")})";

			ReplaceNode(node, markdown);
		});

		return doc.DocumentNode.OuterHtml;
	}

	public static string ReplaceAnchor(string html)
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
				markdown = $@"[{linkText}]({href}{(title.Length > 0 ? $" \"{title}\"" : "")})";
			}

			ReplaceNode(node, markdown);
		});

		return doc.DocumentNode.OuterHtml;
	}

	public static string ReplaceCode(string html)
	{
		var finalHtml = html;
		var doc = GetHtmlDocument(finalHtml);
		var nodes = doc.DocumentNode.SelectNodes("//code");

		if (nodes == null) {
			return finalHtml;
		}

		nodes.ToList().ForEach(node =>
		{
			var code = node.InnerHtml;
			string markdown;
			if(IsSingleLineCodeBlock(code))
			{
				markdown = "`" + code + "`";
			}
			else
			{
				markdown = ReplaceBreakTagsWithNewLines(code);
				markdown = Regex.Replace(markdown, "^\r?\n", "");
				markdown = Regex.Replace(markdown, "\r?\n$", "");
				markdown = "```" + Environment.NewLine + markdown + Environment.NewLine + "```";
			}

			ReplaceNode(node, markdown);
		});

		return doc.DocumentNode.OuterHtml;
	}

	private static string ReplaceBreakTagsWithNewLines(string code)
	{
		return Regex.Replace(code, "<\\s*?/?\\s*?br\\s*?>", "");
	}

	private static bool IsSingleLineCodeBlock(string code)
	{
		// single line code blocks do not have new line characters
		return code.IndexOf(Environment.NewLine, StringComparison.Ordinal) == -1;
	}

	public static string ReplaceBlockquote(string html)
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

			markdown = Regex.Replace(markdown, @"(>\s\r?\n)+$", "");

			markdown = Environment.NewLine + Environment.NewLine + markdown + Environment.NewLine + Environment.NewLine;

			ReplaceNode(node, markdown);
		});

		return doc.DocumentNode.OuterHtml;
	}

	public static string ReplaceEntities(string html)
	{
		return WebUtility.HtmlDecode(html);
	}

	public static string ReplaceParagraph(string html)
	{
		var doc = GetHtmlDocument(html);
		var nodes = doc.DocumentNode.SelectNodes("//p");
		if (nodes == null) {
			return html;
		}

		nodes.ToList().ForEach(node =>
		{
			var text = node.InnerHtml;
			var markdown = Regex.Replace(text, @"\s+", " ");
			markdown = markdown.Replace(Environment.NewLine, " ");
			markdown = Environment.NewLine + Environment.NewLine + markdown + Environment.NewLine;
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
}