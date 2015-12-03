using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using LinqExtensions;

namespace Html2Markdown.Replacement
{
	internal static class HtmlParser
	{
		private static readonly Regex NoChildren = new Regex(@"<(ul|ol)\b[^>]*>(?:(?!<ul|<ol)[\s\S])*?<\/\1>");

		internal static string ReplaceLists(string html)
		{
			while (HasNoChildLists(html))
			{
				var listToReplace = NoChildren.Match(html).Value;
				var formattedList = ReplaceList(listToReplace);
				html = html.Replace(listToReplace, formattedList);
			}

			return html;
		}

		private static string ReplaceList(string html)
		{
			var list = Regex.Match(html, @"<(ul|ol)\b[^>]*>([\s\S]*?)<\/\1>");
			var listType = list.Groups[1].Value;
			var listItems = list.Groups[2].Value.Split(new[] { "</li>" }, StringSplitOptions.None);

			var counter = 0;
			var markdownList = new List<string>();
			listItems.Each(listItem =>
				{
					var listPrefix = (listType.Equals("ol")) ? string.Format("{0}.  ", ++counter) : "*   ";
					var finalList = Regex.Replace(listItem, @"<li[^>]*>", string.Empty);

					if (finalList.Trim().Length == 0) return;

					finalList = Regex.Replace(finalList, @"^\s+", string.Empty);
					finalList = Regex.Replace(finalList, @"\n{2}", string.Format("{0}{1}    ", Environment.NewLine, Environment.NewLine));
					// indent nested lists
					finalList = Regex.Replace(finalList, @"\n([ ]*)+(\*|\d+\.)", string.Format("{0}$1    $2", "\n"));
					markdownList.Add(string.Format("{0}{1}", listPrefix, finalList));
				});

			return Environment.NewLine + Environment.NewLine + markdownList.Aggregate((current, item) => current + Environment.NewLine + item);
		}

		private static bool HasNoChildLists(string html)
		{
			return NoChildren.Match(html).Success;
		}

		internal static string ReplacePre(string html)
		{
			var doc = GetHtmlDocument(html);
			var nodes = doc.DocumentNode.SelectNodes("//pre");
			if (nodes == null) return html;

			nodes.Each(node =>
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
			if (nodes == null) return html;

			nodes.Each(node =>
				{
					var src = node.Attributes.GetAttributeOrEmpty("src");
					var alt = node.Attributes.GetAttributeOrEmpty("alt");
					var title = node.Attributes.GetAttributeOrEmpty("title");

					var markdown = string.Format(@"![{0}]({1}{2})", alt, src, (title.Length > 0) ? string.Format(" \"{0}\"", title) : "");

					ReplaceNode(node, markdown);
				});

			return doc.DocumentNode.OuterHtml;
		}

		public static string ReplaceAnchor(string html)
		{
			var doc = GetHtmlDocument(html);
			var nodes = doc.DocumentNode.SelectNodes("//a");
			if (nodes == null) return html;

			nodes.Each(node =>
				{
					var linkText = node.InnerHtml;
					var href = node.Attributes.GetAttributeOrEmpty("href");
					var title = node.Attributes.GetAttributeOrEmpty("title");

					string markdown = "";

					if (!IsEmptyLink(linkText, href))
					{
						markdown = string.Format(@"[{0}]({1}{2})", linkText, href,
												 (title.Length > 0) ? string.Format(" \"{0}\"", title) : "");
					}

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
			var markdownNode = HtmlNode.CreateNode(markdown);
			if (string.IsNullOrEmpty(markdown))
			{
				node.ParentNode.RemoveChild(node);
			}
			else
			{
				node.ParentNode.ReplaceChild(markdownNode.ParentNode, node);
			}
			
		}

		public static string ReplaceCode(string html)
		{
			var singleLineCodeBlocks = new Regex(@"<code>([^\n]*?)</code>").Matches(html);
			singleLineCodeBlocks.Cast<Match>().Each(block =>
				{
					var code = block.Value;
					var content = GetCodeContent(code);
					html = html.Replace(code, string.Format("`{0}`", content));
				});

			var multiLineCodeBlocks = new Regex(@"<code>([^>]*?)</code>").Matches(html);
			multiLineCodeBlocks.Cast<Match>().Each(block =>
				{
					var code = block.Value;
					var content = GetCodeContent(code);
					content = IndentLines(content).TrimEnd() + Environment.NewLine + Environment.NewLine;
					html = html.Replace(code, string.Format("{0}    {1}", Environment.NewLine, TabsToSpaces(content)));
				});

			return html;
		}

		private static string IndentLines(string content)
		{
			var lines = content.Split(new[] {Environment.NewLine}, StringSplitOptions.None);

			return lines.Aggregate("", (current, line) => current + IndentLine(line));
		}

		private static string IndentLine(string line)
		{
			if (line.Trim().Equals(string.Empty)) return "";
			return line + Environment.NewLine + "    ";
		}

		private static string GetCodeContent(string code)
		{
			var match = Regex.Match(code, @"<code[^>]*?>([^<]*?)</code>");
			var groups = match.Groups;
			return groups[1].Value;
		}
	}
}
