﻿using System;

namespace Html2Markdown.Uwp.Replacement
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
