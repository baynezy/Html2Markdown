using AngleSharp.Dom;

namespace Html2Markdown
{
	internal static class AngleSharpExtensions
	{
		public static string GetAttributeOrEmpty(this IElement element, string attributeName)
		{
            return element.HasAttribute(attributeName) ? element.Attributes[attributeName].Value : "";
		}
	}
}
