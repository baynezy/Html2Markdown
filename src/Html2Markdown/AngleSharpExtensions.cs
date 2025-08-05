using AngleSharp.Dom;

namespace Html2Markdown;

internal static class AngleSharpExtensions
{
    public static string GetAttributeOrEmpty(this IElement element, string attributeName)
    {
        return element.GetAttribute(attributeName) ?? "";
    }
    
    public static void ReplaceNodeWithString(this INode node, string content)
    {
        var document = node.Owner;
        var tempDiv = document.CreateElement("div");
        tempDiv.InnerHtml = content;
        
        var parent = node.Parent;
        if (parent is null) return;
        
        // Insert all child nodes from the temporary div
        while (tempDiv.FirstChild is not null)
        {
            parent.InsertBefore(tempDiv.FirstChild, node);
        }
        
        // Remove the original node
        parent.RemoveChild(node);
    }
}