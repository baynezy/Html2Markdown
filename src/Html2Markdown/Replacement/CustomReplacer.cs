namespace Html2Markdown.Replacement;
/// <summary>
/// Allows custom replacement of HTML tags utilising external functions.
/// </summary>
public class CustomReplacer : IReplacer
{
	public string Replace(string html)
	{
		return CustomAction.Invoke(html);
	}

	protected Func<string, string> CustomAction { get; init; }
}