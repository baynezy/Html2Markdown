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
					CustomAction = HtmlParser.ReplaceImg
				},
				new CustomReplacer
				{
					CustomAction = HtmlParser.ReplacePre
				},
				new CustomReplacer
				{
					CustomAction = HtmlParser.ReplaceLists
				},
				new CustomReplacer
				{
					CustomAction = HtmlParser.ReplaceAnchor
				}
			};

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

	internal static class HtmlParser
	{
		private static readonly Regex NoChildren = new Regex(@"<(ul|ol)\b[^>]*>(?:(?!<ul|<ol)[\s\S])*?<\/\1>");

		internal static string ReplaceLists(string html)
		{
			while (HasNoChildLists(html))
			{
				var listToReplace = NoChildren.Match(html).Value;
				var formattedList = ReplaceList(listToReplace);
				html = html.Replace(listToReplace, formattedList);
			}

			return html;
		}

		private static string ReplaceList(string html)
		{
			var list = Regex.Match(html, @"<(ul|ol)\b[^>]*>([\s\S]*?)<\/\1>");
			var type = list.Groups[1].Value;
			var lis = list.Groups[2].Value.Split(new[] {"</li>"}, StringSplitOptions.None);

			var counter = 0;
			var markdownList = new List<string>();
			foreach (var li in lis)
			{
				var prefix = (type.Equals("ol")) ? string.Format("{0}.  ", ++counter) : "*   ";
				var final = Regex.Replace(li, @"<li[^>]*>", string.Empty);

				if (final.Trim().Length == 0) continue;

				final = Regex.Replace(final, @"^\s+", string.Empty);
				final = Regex.Replace(final, @"\n{2}", string.Format("{0}{1}    ", Environment.NewLine, Environment.NewLine));
				// indent nested lists
				final = Regex.Replace(final, @"\n([ ]*)+(\*|\d+\.)", string.Format("{0}$1    $2", "\n"));
				markdownList.Add(string.Format("{0}{1}", prefix, final));
			}

			return Environment.NewLine + Environment.NewLine + markdownList.Aggregate((current, item) => current + Environment.NewLine + item);
		}

		private static bool HasNoChildLists(string html)
		{
			return NoChildren.Match(html).Success;
		}

		internal static string ReplacePre(string html)
		{
			var preTags = new Regex(@"<pre\b[^>]*>([\s\S]*)<\/pre>").Matches(html);

			return preTags.Cast<Match>().Aggregate(html, ConvertPre);
		}

		private static string ConvertPre(string html, Match preTag)
		{
			var tag = preTag.Groups[1].Value;
			tag = TabsToSpaces(tag);
			tag = IndentNewLines(tag);
			html = html.Replace(preTag.Value, Environment.NewLine + Environment.NewLine + tag + Environment.NewLine);
			return html;
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
			var originalImages = new Regex(@"<img([^>]+)>").Matches(html);

			foreach (Match image in originalImages)
			{
				var img = image.Value;
				var src = AttributeParser(img, "src");
				var alt = AttributeParser(img, "alt");
				var title = AttributeParser(img, "title");

				html = html.Replace(img, string.Format(@"![{0}]({1}{2})", alt, src, (title.Length > 0) ? string.Format(" \"{0}\"", (object) title) : ""));
			}

			return html;
		}

		public static string ReplaceAnchor(string html)
		{
			var originalAnchors = new Regex(@"<a[^>]+>[^<]+</a>").Matches(html);

			foreach (Match anchor in originalAnchors)
			{
				var a = anchor.Value;
				var linkText = GetLinkText(a);
				var href = AttributeParser(a, "href");
				var title = AttributeParser(a, "title");

                html = html.Replace(a, string.Format(@"[{0}]({1}{2})", linkText, href, (title.Length > 0) ? string.Format(" \"{0}\"", (object) title) : ""));
			}

            return html;
		}

        private static string GetLinkText(string link)
        {
            var match = Regex.Match(link, @"<a[^>]+>([^<]+)</a>");
            var groups = match.Groups;
            return groups[1].Value;
        }

		private static string AttributeParser(string html, string attribute)
		{
			var match = Regex.Match(html, string.Format(@"{0}\s*=\s*[""\']?([^""\']*)[""\']?", attribute));
			var groups = match.Groups;
			return groups[1].Value;
		}
	}
}
