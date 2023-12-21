namespace Html2Markdown.Replacement;
/// <summary>
/// Replaces the HTML heading tag with its Markdown equivalent.
/// </summary>
public class HeadingTagReplacer : CompositeReplacer
{
    public HeadingTagReplacer(Heading heading)
    {
        var headingNumber = (int) heading;
        AddReplacer(new PatternReplacer
        {
            Pattern = $"</h{headingNumber}>",
            Replacement = Environment.NewLine + Environment.NewLine
        });
        
        AddReplacer(new PatternReplacer
        {
            Pattern = $"<h{headingNumber}[^>]*>",
            Replacement = Environment.NewLine + Environment.NewLine + new string('#', headingNumber) + " "
        });
    }
}