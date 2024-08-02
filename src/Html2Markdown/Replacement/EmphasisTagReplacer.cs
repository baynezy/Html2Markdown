namespace Html2Markdown.Replacement;

/// <summary>
/// Replaces the HTML emphasis tag with its Markdown equivalent.
/// </summary>
public class EmphasisTagReplacer : CompositeReplacer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmphasisTagReplacer"/> class.
    /// Sets up the patterns and replacements for converting HTML emphasis tags to Markdown.
    /// </summary>
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