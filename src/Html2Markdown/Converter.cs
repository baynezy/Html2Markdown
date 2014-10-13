using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Html2Markdown
{
	public class Converter
	{
		private readonly IList<IReplacer> _replacers = new List<IReplacer>
			{
				new Element
				{
					Pattern = @"<a.+?href\s*=\s*['""]([^'""]+)['""]>([^<]+)</a>",
					Replacement = @"[$2]($1)"
				},
				new Element
				{
					Pattern = @"</?(strong|b)>",
					Replacement = @"**"
				},
				new Element
				{
					Pattern = @"</?(em|i)>",
					Replacement = @"*"
				},
				new Element
				{
					Pattern = @"<br\s/>",
					Replacement = @"  " + Environment.NewLine
				},
				new Element
				{
					Pattern = @"</?code>",
					Replacement = @"`"
				},
				new Element
				{
					Pattern = @"</h[1-6]>",
					Replacement = Environment.NewLine + Environment.NewLine
				},
				new Element
				{
					Pattern = @"<h1>",
					Replacement = Environment.NewLine + Environment.NewLine + "# "
				},
				new Element
				{
					Pattern = @"<h2>",
					Replacement = Environment.NewLine + Environment.NewLine + "## "
				},
				new Element
				{
					Pattern = @"<h3>",
					Replacement = Environment.NewLine + Environment.NewLine + "### "
				},
				new Element
				{
					Pattern = @"<h4>",
					Replacement = Environment.NewLine + Environment.NewLine + "#### "
				},
				new Element
				{
					Pattern = @"<h5>",
					Replacement = Environment.NewLine + Environment.NewLine + "##### "
				},
				new Element
				{
					Pattern = @"<h6>",
					Replacement = Environment.NewLine + Environment.NewLine + "###### "
				},
                new Element
				{
                    Pattern = @"<blockquote>",
                    Replacement = Environment.NewLine + Environment.NewLine + @">"
                },
                new Element
				{
                    Pattern = @"</blockquote>",
                    Replacement = Environment.NewLine + Environment.NewLine
                },
				new Element
				{
					Pattern = @"<p>",
					Replacement = Environment.NewLine + Environment.NewLine
				},
				new Element
				{
					Pattern = @"</p>",
					Replacement = Environment.NewLine
				},
				new Element
				{
					Pattern = @"<hr/>",
					Replacement = Environment.NewLine + Environment.NewLine + "* * *" + Environment.NewLine
				},
				new CustomReplacer
				{
					CustomAction = FormatImage
				}
			};

		private static string FormatImage(string html)
		{
			var originalImages = new Regex(@"<img([^>]+)>").Matches(html);

			foreach (Match image in originalImages)
			{
				var img = image.Value;
				var src = AttributeParser(img, "src");
				var alt = AttributeParser(img, "alt");
				var title = AttributeParser(img, "title");

				html = html.Replace(img, string.Format(@"![{0}]({1}{2})", alt, src, (title.Length > 0) ? string.Format(" \"{0}\"", title) : ""));
			}

			return html;
		}

		private static string AttributeParser(string html, string attribute)
		{
			var match = Regex.Match(html, string.Format(@"{0}\s*=\s*[""\']?([^""\']*)[""\']?", attribute));
			var groups = match.Groups;
			return groups[1].Value;
		}

		public string Convert(string html)
		{
			return _replacers.Aggregate(html, (current, element) => element.Replace(current));
		}
	}

	internal class CustomReplacer : IReplacer
	{
		public string Replace(string html)
		{
			return CustomAction.Invoke(html);
		}

		public Func<string, string> CustomAction { get; set; }
	}

	internal class Element : IReplacer
	{
		public string Pattern { get; set; }

		public string Replacement { get; set; }
		public string Replace(string html)
		{
			var regex = new Regex(Pattern);

			return regex.Replace(html, Replacement);
		}
	}

	internal interface IReplacer
	{
		string Replace(string html);
	}
}
