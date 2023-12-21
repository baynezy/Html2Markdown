namespace Html2Markdown.Replacement;
/// <summary>
/// Allows for multiple replacements to be applied to the HTML.
/// </summary>
public abstract class CompositeReplacer : IReplacer
{
    private readonly IList<IReplacer> _replacements = new List<IReplacer>();
    
    protected void AddReplacer(IReplacer replacer)
    {
        _replacements.Add(replacer);
    }
    
    public string Replace(string html)
    {
        return _replacements.Aggregate(html, (current, replacer) => replacer.Replace(current));
    }
}