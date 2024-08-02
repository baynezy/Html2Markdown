namespace Html2Markdown.Replacement;

/// <summary>
/// Replace the strong tag with its Markdown equivalent.
/// </summary>
public class StrongTagReplacer : CompositeReplacer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StrongTagReplacer"/> class.
    /// Sets up the replacers for converting strong and b tags to Markdown.
    /// </summary>
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