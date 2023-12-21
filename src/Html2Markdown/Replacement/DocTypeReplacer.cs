namespace Html2Markdown.Replacement;
/// <summary>
/// Removes the doctype tag.
/// </summary>
public class DocTypeReplacer : PatternReplacer
{
    public DocTypeReplacer()
    {
        Pattern = "<!DOCTYPE[^>]*>";
        Replacement = "";
    }
}