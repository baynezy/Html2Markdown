﻿using HtmlAgilityPack;

namespace Html2Markdown.Uwp
{
	internal static class HtmlAgilityPackExtensions
	{
		public static string GetAttributeOrEmpty(this HtmlAttributeCollection collection, string attributeName)
		{
			return (collection[attributeName] == null) ? "" : collection[attributeName].Value;
		}
	}
}
