using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace Html2Markdown;

internal static class AngleSharpExtensions
{
	public static string GetAttributeOrEmpty(this IElement element, string attributeName)
	{
		return element.GetAttribute(attributeName) ?? string.Empty;
	}
		
	public static void ReplaceNodeWithString(this INode node, string content) 
	{
		if (node.Parent is null) return;
		
		// Create a temporary container to parse the content
		var document = node.Owner ?? node as IDocument;
		if (document is null) return;
		
		var tempDiv = document.CreateElement("div");
		tempDiv.InnerHtml = content;
		
		var parent = node.Parent;
		var nextSibling = node.NextSibling;
		
		// Insert all child nodes from the temporary container
		while (tempDiv.FirstChild is not null)
		{
			var child = tempDiv.FirstChild;
			tempDiv.RemoveChild(child);
			
			if (nextSibling is not null)
			{
				parent.InsertBefore(child, nextSibling);
			}
			else
			{
				parent.AppendChild(child);
			}
		}
		
		// Remove the original node
		parent.RemoveChild(node);
	} 
}