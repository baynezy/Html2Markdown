using System;

namespace Html2Markdown.Replacement
{
	internal class CustomReplacer : IReplacer
	{
		public string Replace(string html)
		{
			return CustomAction.Invoke(html);
		}

		public Func<string, string> CustomAction { get; set; }
	}
}