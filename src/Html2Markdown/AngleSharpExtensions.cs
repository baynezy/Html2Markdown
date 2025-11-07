using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp;
using AngleSharp.Html.Parser;

namespace Html2Markdown;

internal static class AngleSharpExtensions
{
	public static string GetAttributeOrEmpty(this IElement element, string attributeName)
	{
		return element.GetAttribute(attributeName) ?? "";
	}
		
	public static void ReplaceNodeWithString(this IElement node, string content) 
	{
		// Parse the content as HTML
		var context = node.Owner?.Context ?? BrowsingContext.New(Configuration.Default);
		var parser = context.GetService<IHtmlParser>();
		if (parser is null)
		{
			node.Remove();
			return;
		}
		
		var tempDoc = parser.ParseDocument($"<p>{content}</p>");
		var tempElement = tempDoc.Body?.FirstElementChild;
		
		if (tempElement is null)
		{
			node.Remove();
			return;
		}
		
		// Insert all child nodes before the current node
		var parent = node.Parent;
		var current = node as INode;
		
		foreach (var child in tempElement.ChildNodes.ToList())
		{
			parent?.InsertBefore(child, current.NextSibling);
		}
		
		// Remove the original node
		node.Remove();
	}
}
