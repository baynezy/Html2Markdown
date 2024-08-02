namespace Html2Markdown.Replacement
{
    /// <summary>
    /// Replaces an anchor tag with the link text and the link URL in Markdown format.
    /// </summary>
    public class AnchorTagReplacer : CustomReplacer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnchorTagReplacer"/> class.
        /// Sets the custom action to replace anchor tags with Markdown formatted links.
        /// </summary>
        public AnchorTagReplacer()
        {
            CustomAction = HtmlParser.ReplaceAnchor;
        }
    }
}