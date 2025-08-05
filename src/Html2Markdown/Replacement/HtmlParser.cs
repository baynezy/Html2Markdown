using System.Net;
using System.Text.RegularExpressions;
using AngleSharp.Html.Parser;

namespace Html2Markdown.Replacement;

internal static partial class HtmlParser
{
    internal static string ReplaceLists(string html)
    {
        var finalHtml = html;
        var lastRun = string.Empty;
        while (HasNoChildLists(finalHtml))
        {
            var listToReplace = HtmlListHasNoChildren()
                .Match(finalHtml)
                .Value;
            var formattedList = ReplaceList(listToReplace);

            // an empty string signifies that the HTML is malformed in some way.
            // so we should leave the final HTML as is
            if (string.IsNullOrEmpty(formattedList))
            {
                finalHtml = finalHtml.Replace(listToReplace, lastRun);
                break;
            }

            lastRun = formattedList;

            finalHtml = finalHtml.Replace(listToReplace, formattedList);
        }

        return finalHtml;
    }

    private static string ReplaceList(string html)
    {
        var list = FindHtmlList()
            .Match(html);
        var listType = list.Groups[1].Value;
        var listItems = FindHtmlListItems()
            .Split(list.Groups[2].Value);

        if (ListOnlyHasEmptyStringsForChildren(listItems)) return string.Empty;

        listItems = listItems.Skip(1)
            .ToArray();

        if (ListIsEmpty(listItems)) return string.Empty;

        // Check if this is an ordered list with potentially non-continuous numbers
        var isOrderedList = listType.Equals("ol");
        var counter = 0;
        var markdownList = new List<string>();

        if (isOrderedList && TryProcessOrderedList(html, counter, markdownList, out var orderedList))
        {
            return orderedList;
        }

        // Fall back to original implementation for unordered lists or if HTML parsing failed
        listItems.ToList()
            .ForEach(listItem =>
            {
                var listPrefix = isOrderedList ? $"{++counter}.  " : "*   ";
                //In case of multiline Html, a line can end with a new line. In this case we want to remove the closing tag as well as the new line
                //otherwise we may only keep the line breaks between tags and create a double line break in the markdown
                var closingTag = listItem.EndsWith($"</li>{Environment.NewLine}")
                    ? $"</li>{Environment.NewLine}"
                    : "</li>";
                var finalList = listItem.Replace(closingTag, string.Empty);

                if (finalList.Trim()
                        .Length == 0)
                {
                    return;
                }

                finalList = SpacesAtTheStartOfALine()
                    .Replace(finalList, string.Empty);
                finalList = TwoNewLines()
                    .Replace(finalList, $"{Environment.NewLine}{Environment.NewLine}");
                // indent nested lists
                finalList = NestedList()
                    .Replace(finalList, "\n$1    $2");
                // remove the indent from the first line
                if (listItem.StartsWith("<p>"))
                {
                    finalList = ReplaceParagraph(finalList, true);
                }

                markdownList.Add($"{listPrefix}{finalList.TrimEnd()}");
            });

        if (markdownList.Count == 0) return string.Empty;

        //If a new line is already ending the markdown item, then we don't need to add another one
        return Environment.NewLine + Environment.NewLine +
               markdownList.Aggregate((current, item) => current.EndsWith(Environment.NewLine)
                   ? current + item
                   : current + Environment.NewLine + item) + Environment.NewLine + Environment.NewLine;
    }

    private static bool TryProcessOrderedList(string html, int counter, List<string> markdownList,
        out string orderedList)
    {
        // Parse the HTML to get value attributes from li elements
        var doc = GetHtmlDocument(html);
        var listNode = doc.QuerySelector("ol");
        var liNodes = listNode?.QuerySelectorAll("li");
        orderedList = string.Empty;

        if (liNodes is null || !liNodes.Any()) return false;
        
        // Process list items with AngleSharp to access attributes
        foreach (var liNode in liNodes)
        {
            var element = liNode as IElement;
            if (element is null) continue;
            
            // Check if the li has a value attribute
            var valueAttr = element.GetAttributeOrEmpty("value");
            if (!string.IsNullOrEmpty(valueAttr) && int.TryParse(valueAttr, out var value))
            {
                counter = value - 1; // Subtract 1 because we increment before using it
            }

            var listPrefix = $"{++counter}.  ";
            var finalList = ProcessListItem(element.OuterHtml);

            if (!string.IsNullOrWhiteSpace(finalList))
            {
                markdownList.Add($"{listPrefix}{finalList.TrimEnd()}");
            }
        }

        if (markdownList.Count == 0)
        {
            orderedList = string.Empty;
        }

        // If a new line is already ending the markdown item, then we don't need to add another one
        orderedList = Environment.NewLine + Environment.NewLine + markdownList.Aggregate((current, item) =>
                          current.EndsWith(Environment.NewLine)
                              ? current + item
                              : current + Environment.NewLine + item) +
                      Environment.NewLine + Environment.NewLine;
        return true;

    }

    private static string ProcessListItem(string listItemHtml)
    {
        //In case of multiline Html, a line can end with a new line. In this case we want to remove the closing tag as well as the new line
        //otherwise we may only keep the line breaks between tags and create a double line break in the markdown
        var closingTag = listItemHtml.EndsWith($"</li>{Environment.NewLine}") ? $"</li>{Environment.NewLine}" : "</li>";
        var finalList = listItemHtml.Replace(closingTag, string.Empty)
            .Replace("<li>", string.Empty)
            .Replace("<li ", string.Empty);

        if (finalList.Contains('>'))
        {
            // Handle case where list item has attributes
            var startContentIndex = finalList.IndexOf('>') + 1;
            if (startContentIndex < finalList.Length)
            {
                finalList = finalList.Substring(startContentIndex);
            }
        }

        if (finalList.Trim()
                .Length == 0)
        {
            return string.Empty;
        }

        finalList = SpacesAtTheStartOfALine()
            .Replace(finalList, string.Empty);
        finalList = TwoNewLines()
            .Replace(finalList, $"{Environment.NewLine}{Environment.NewLine}");
        // indent nested lists
        finalList = NestedList()
            .Replace(finalList, "\n$1    $2");
        // remove the indent from the first line
        if (listItemHtml.Contains("<p>"))
        {
            finalList = ReplaceParagraph(finalList, true);
        }

        return finalList;
    }

    private static bool ListIsEmpty(string[] listItems)
    {
        return listItems.Length == 0;
    }

    private static bool ListOnlyHasEmptyStringsForChildren(IEnumerable<string> listItems)
    {
        return listItems.All(string.IsNullOrEmpty);
    }

    private static bool HasNoChildLists(string html)
    {
        return HtmlListHasNoChildren()
            .Match(html)
            .Success;
    }

    internal static string ReplacePre(string html)
    {
        var doc = GetHtmlDocument(html);
        var nodes = doc.QuerySelectorAll("pre");
        if (!nodes.Any())
        {
            return html;
        }

        // Process nodes in reverse order (bottom-up)
        foreach (var node in nodes.Reverse())
        {
            var element = node as IElement;
            if (element is null) continue;
            
            var tagContents = element.InnerHtml;
            var markdown = ConvertPre(tagContents);

            ReplaceNode(node, markdown);
        }

        return doc.DocumentElement?.OuterHtml ?? html;
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
        var nodes = doc.QuerySelectorAll("img");
        if (!nodes.Any())
        {
            return html;
        }

        // Process nodes in reverse order (bottom-up)
        foreach (var node in nodes.Reverse())
        {
            var element = node as IElement;
            if (element is null) continue;
            
            var src = element.GetAttributeOrEmpty("src");
            var alt = element.GetAttributeOrEmpty("alt");
            var title = element.GetAttributeOrEmpty("title");

            var markdown = $"![{alt}]({src}{(title.Length > 0 ? $" \"{title}\"" : "")})";

            ReplaceNode(node, markdown);
        }

        return GetModifiedHtml(doc, html);
    }

    internal static string ReplaceAnchor(string html)
    {
        var doc = GetHtmlDocument(html);
        var nodes = doc.QuerySelectorAll("a");
        if (!nodes.Any())
        {
            return html;
        }

        // Process nodes in reverse order (bottom-up)
        foreach (var node in nodes.Reverse())
        {
            var element = node as IElement;
            if (element is null) continue;
            
            var linkText = element.InnerHtml;
            var href = element.GetAttributeOrEmpty("href");
            var title = element.GetAttributeOrEmpty("title");

            var markdown = "";

            if (!IsEmptyLink(linkText, href))
            {
                markdown = $"[{linkText}]({href}{(title.Length > 0 ? $" \"{title}\"" : "")})";
            }

            ReplaceNode(node, markdown);
        }

        return doc.DocumentElement?.OuterHtml ?? html;
    }

    internal static string ReplaceCode(string html, bool supportSyntaxHighlighting)
    {
        var doc = GetHtmlDocument(html);
        var nodes = doc.QuerySelectorAll("code");

        if (!nodes.Any())
        {
            return html;
        }

        // Process nodes in reverse order (bottom-up)
        foreach (var node in nodes.Reverse())
        {
            var element = node as IElement;
            if (element is null) continue;
            
            var code = element.InnerHtml;
            var language = supportSyntaxHighlighting ? GetSyntaxHighlightLanguage(element) : "";

            string markdown;
            if (IsSingleLineCodeBlock(code))
            {
                markdown = "`" + code + "`";
            }
            else
            {
                markdown = ReplaceBreakTagsWithNewLines(code);
                markdown = InitialCrLf()
                    .Replace(markdown, "");
                markdown = FinalCrLf()
                    .Replace(markdown, "");
                markdown = "```" + language + Environment.NewLine + markdown + Environment.NewLine + "```";
            }

            ReplaceNode(node, markdown);
        }

        return doc.DocumentElement?.OuterHtml ?? html;
    }

    private static string ReplaceBreakTagsWithNewLines(string code)
    {
        return BreakTag()
            .Replace(code, "");
    }

    private static bool IsSingleLineCodeBlock(string code)
    {
        // single line code blocks do not have new line characters
        return !code.Contains(Environment.NewLine);
    }

    private static string GetSyntaxHighlightLanguage(IElement element)
    {
        // extract the language for syntax highlighting from a code tag
        // depending on the implementations, language can be declared in the tag as :
        // <code class="language-csharp">
        // <code class="lang-csharp">
        // <code class="csharp">
        var classAttributeValue = element.GetAttribute("class");

        if (string.IsNullOrEmpty(classAttributeValue))
        {
            return string.Empty;
        }

        if (!classAttributeValue.StartsWith("lang")) return classAttributeValue;
        var split = classAttributeValue.Split('-');

        return split
            [^1]; // PERFORMANCE: https://sonarcloud.io/organizations/baynezy/rules?open=csharpsquid%3AS6608&rule_key=csharpsquid%3AS6608
    }

    internal static string ReplaceBlockquote(string html)
    {
        var doc = GetHtmlDocument(html);
        var nodes = doc.QuerySelectorAll("blockquote");
        if (!nodes.Any())
        {
            return html;
        }

        // Process nodes in reverse order (bottom-up)
        foreach (var node in nodes.Reverse())
        {
            var element = node as IElement;
            if (element is null) continue;
            
            var quote = element.InnerHtml;
            var lines = quote.TrimStart()
                .Split([Environment.NewLine], StringSplitOptions.None);
            var markdown = "";

            foreach (var line in lines)
            {
                markdown += $"> {line.TrimEnd()}{Environment.NewLine}";
            }

            markdown = EmptyQuoteLines()
                .Replace(markdown, "");

            markdown = Environment.NewLine + Environment.NewLine + markdown + Environment.NewLine +
                       Environment.NewLine;

            ReplaceNode(node, markdown);
        }

        return doc.DocumentElement?.OuterHtml ?? html;
    }

    internal static string ReplaceEntities(string html)
    {
        return WebUtility.HtmlDecode(html);
    }

    internal static string ReplaceParagraph(string html) => ReplaceParagraph(html, false);

    internal static string ReplaceHeading(string html, int headingNumber)
    {
        var tag = $"h{headingNumber}";
        var doc = GetHtmlDocument(html);
        var nodes = doc.QuerySelectorAll(tag);

        if (!nodes.Any()) return html;

        // Process nodes in reverse order (bottom-up)
        foreach (var node in nodes.Reverse())
        {
            var element = node as IElement;
            if (element is null) continue;
            
            var text = element.InnerHtml;
            var htmlRemoved = HtmlTags()
                .Replace(text, "");
            var markdown = Spaces()
                .Replace(htmlRemoved, " ");
            markdown = markdown.Replace(Environment.NewLine, " ");
            markdown = Environment.NewLine + Environment.NewLine + new string('#', headingNumber) + " " + markdown +
                       Environment.NewLine + Environment.NewLine;
            ReplaceNode(node, markdown);
        }

        return doc.DocumentElement?.OuterHtml ?? html;
    }

    private static string ReplaceParagraph(string html, bool nestedIntoList)
    {
        var doc = GetHtmlDocument(html);
        var nodes = doc.QuerySelectorAll("p");
        if (!nodes.Any())
        {
            return html;
        }

        // Process nodes in reverse order (bottom-up)
        foreach (var node in nodes.Reverse())
        {
            var element = node as IElement;
            if (element is null) continue;
            
            var text = element.InnerHtml;
            var markdown = Spaces()
                .Replace(text, " ");
            markdown = markdown.Replace(Environment.NewLine, " ");

            //If a paragraph is contained in a list, we don't want to add new line characters
            var openingTag = nestedIntoList ? "" : Environment.NewLine + Environment.NewLine;
            var closingTag = nestedIntoList ? "" : Environment.NewLine;

            markdown = openingTag + markdown + closingTag;
            ReplaceNode(node, markdown);
        }

        return doc.DocumentElement?.OuterHtml ?? html;
    }

    private static bool IsEmptyLink(string linkText, string href)
    {
        var length = linkText.Length + href.Length;
        return length == 0;
    }

    private static IHtmlDocument GetHtmlDocument(string html)
    {
        var parser = new AngleSharp.Html.Parser.HtmlParser();
        var config = AngleSharp.Configuration.Default;
        
        // Parse as a fragment if it doesn't look like a complete document
        if (!html.TrimStart().StartsWith("<!DOCTYPE", StringComparison.OrdinalIgnoreCase) && 
            !html.TrimStart().StartsWith("<html", StringComparison.OrdinalIgnoreCase))
        {
            // Wrap in a temporary container for fragment parsing
            var wrappedHtml = $"<div>{html}</div>";
            var doc = parser.ParseDocument(wrappedHtml);
            return doc;
        }
        
        return parser.ParseDocument(html);
    }

    private static void ReplaceNode(INode node, string markdown)
    {
        if (string.IsNullOrEmpty(markdown))
        {
            node.Parent?.RemoveChild(node);
        }
        else
        {
            node.ReplaceNodeWithString(markdown);
        }
    }
    
    private static string GetModifiedHtml(IHtmlDocument doc, string originalHtml)
    {
        // If the original HTML was a fragment, return just the body content
        if (!originalHtml.TrimStart().StartsWith("<!DOCTYPE", StringComparison.OrdinalIgnoreCase) && 
            !originalHtml.TrimStart().StartsWith("<html", StringComparison.OrdinalIgnoreCase))
        {
            // Get the content from the temporary div we created
            var bodyContent = doc.Body?.FirstElementChild?.InnerHtml;
            return bodyContent ?? originalHtml;
        }
        
        return doc.DocumentElement?.OuterHtml ?? originalHtml;
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

    [GeneratedRegex(@"\n([ ]*)(\*|\d+\.)")]
    private static partial Regex NestedList();

    [GeneratedRegex("^\r?\n")]
    private static partial Regex InitialCrLf();

    [GeneratedRegex("\r?\n$")]
    private static partial Regex FinalCrLf();

    [GeneratedRegex(@"<\s*?/?\s*?br\s*?>")]
    private static partial Regex BreakTag();

    [GeneratedRegex("<[^>]+>")]
    private static partial Regex HtmlTags();
}