using HtmlAgilityPack;

namespace Html2Markdown;

internal static class HtmlAgilityPackExtensions
{
	public static string GetAttributeOrEmpty(this HtmlAttributeCollection collection, string attributeName)
	{
		return (collection[attributeName] == null) ? "" : collection[attributeName].Value;
	}
		
	public static void ReplaceNodeWithString(this HtmlNode node, string content) {
		var temp = HtmlNode.CreateNode("<p></p>");
		temp.InnerHtml = content;
		var current = node;

		foreach (var child in temp.ChildNodes)
		{
			node.ParentNode.InsertAfter(child, current);
			current = child;
		}
		node.Remove();
	} 
}