namespace Html2Markdown.Replacement;
/// <summary>
/// Replace the strong tag with its Markdown equivalent.
/// </summary>
public class StrongTagReplacer : CompositeReplacer
{
    public StrongTagReplacer()
    {
        AddReplacer(new PatternReplacer
        {
            Pattern = @"<(?:strong|b)>(\s+)",
            Replacement = " **"
        });
        
        AddReplacer(new PatternReplacer
        {
            Pattern = @"<(?:strong|b)(:?\s[^>]*)?>",
            Replacement = "**"
        });
        
        AddReplacer(new PatternReplacer
        {
            Pattern = @"(\s+)</(strong|b)>",
            Replacement = "** "
        });
        
        AddReplacer(new PatternReplacer
        {
            Pattern = "</(strong|b)>",
            Replacement = "**"
        });
    }
}