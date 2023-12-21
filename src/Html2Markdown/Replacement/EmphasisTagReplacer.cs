namespace Html2Markdown.Replacement;
/// <summary>
/// Replaces the HTML emphasis tag with its Markdown equivalent.
/// </summary>
public class EmphasisTagReplacer : CompositeReplacer
{
    public EmphasisTagReplacer()
    {
        AddReplacer(new PatternReplacer
        {
            Pattern = @"<(?:em|i)>(\s+)",
            Replacement = " *"
        });
        
        AddReplacer(new PatternReplacer
        {
            Pattern = "<(?:em|i)>",
            Replacement = "*"
        });
        
        AddReplacer(new PatternReplacer
        {
            Pattern = @"(\s+)</(em|i)>",
            Replacement = "* "
        });
        
        AddReplacer(new PatternReplacer
        {
            Pattern = "</(em|i)>",
            Replacement = "*"
        });
    }
}