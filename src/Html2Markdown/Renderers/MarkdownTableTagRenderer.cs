namespace Html2Markdown.Renderers;

/// <summary>
/// Renders HTML table elements into Markdown table syntax.
/// </summary>
public sealed class MarkdownTableTagRenderer : IHtmlTagRenderer
{
    private static readonly string Empty = new('\0', 0);

    /// <inheritdoc/>
    public string TagName => "table";

    /// <inheritdoc/>
    public string Render(IElement element, HtmlTagRenderingContext context)
    {
        var rows = element.QuerySelectorAll("tr")
            .ToList();
        return rows.Count switch
        {
            0 => RenderCaption(element, context),
            _ => RenderRows(element, context, rows)
        };
    }

    private static string RenderRows(IElement element, HtmlTagRenderingContext context, List<IElement> rows)
    {
        var header = FindHeader(element, rows);
        var renderedRows = Enumerable.Range(0, rows.Count + GetSyntheticHeaderRowCount(header))
            .Select(_ => new List<string>())
            .ToList();
        var alignments = new List<string>();

        foreach (var (row, rowIndex) in rows.Select((row, index) => (row, index)))
        {
            var cells = row.Children
                .Where(cell => cell.LocalName is "th" or "td")
                .Select(cell => RenderCell(cell, context))
                .ToList();
            if (cells.Any(cell => cell is null))
            {
                return element.OuterHtml;
            }

            AddRow(
                renderedRows,
                cells.Select(cell => cell!)
                    .ToList(),
                alignments,
                rowIndex + (header is null ? 1 : 0),
                row == header || (header is null && row == rows[0]));
        }

        var columnCount = renderedRows.Max(row => row.Count);
        if (columnCount == 0)
        {
            return RenderCaption(element, context);
        }

        foreach (var row in renderedRows)
        {
            row.AddRange(Enumerable.Repeat(Empty, columnCount - row.Count));
        }

        alignments.AddRange(Enumerable.Repeat(Empty, columnCount - alignments.Count));

        var table = string.Join(
            Environment.NewLine,
            [
                RenderRow(renderedRows[0]), RenderSeparator(alignments, columnCount), .. renderedRows.Skip(1)
                    .Select(RenderRow)
            ]);
        var content = string.Join(
            $"{Environment.NewLine}{Environment.NewLine}",
            new[] {table, RenderCaption(element, context)}
                .Where(part => part is not null));

        return MarkdownFormatting.Block(content);
    }

    private static TableCell RenderCell(IElement element, HtmlTagRenderingContext context)
    {
        var content = context.RenderChildren(element)
            .Trim();
        if (content.Contains('\n'))
        {
            return null;
        }

        return new TableCell(
            MarkdownFormatting.CollapseWhitespace(content)
                .Replace("|", "\\|"),
            GetSpan(element, "colspan"),
            GetSpan(element, "rowspan"),
            element.GetAttribute("align") ?? Empty);
    }

    private static void AddRow(
        List<List<string>> rows,
        IReadOnlyCollection<TableCell> cells,
        List<string> alignments,
        int rowIndex,
        bool collectAlignments)
    {
        var columnIndex = 0;

        foreach (var cell in cells)
        {
            columnIndex = FindAvailableColumnIndex(rows[rowIndex]);

            SetCell(rows, rowIndex, columnIndex, cell.Content);
            if (collectAlignments)
            {
                EnsureAlignment(alignments, columnIndex);
                alignments[columnIndex] = cell.Alignment;
            }

            for (var rowOffset = 0; rowOffset < cell.RowSpan; rowOffset++)
            {
                for (var columnOffset = 0; columnOffset < cell.ColumnSpan; columnOffset++)
                {
                    if (rowOffset != 0 || columnOffset != 0)
                    {
                        SetCell(rows, rowIndex + rowOffset, columnIndex + columnOffset, string.Empty);
                    }
                }
            }

        }
    }

    private static string RenderCaption(IElement table, HtmlTagRenderingContext context)
    {
        var caption = table.Children.FirstOrDefault(child => child.LocalName == "caption");
        return caption is null
            ? null
            : MarkdownFormatting.CollapseWhitespace(context.RenderChildren(caption));
    }

    private static string RenderRow(IEnumerable<string> cells) =>
        $"| {string.Join(" | ", cells.Select(cell => cell ?? Empty))} |";

    private static string RenderSeparator(IReadOnlyList<string> alignments, int columnCount) =>
        $"| {string.Join(" | ", Enumerable.Range(0, columnCount).Select(index => RenderAlignment(alignments[index])))} |";

    private static string RenderAlignment(string alignment) =>
        alignment.ToLowerInvariant() switch
        {
            "left" => ":---",
            "center" => ":--:",
            "right" => "---:",
            _ => "---"
        };

    private static int GetSpan(IElement element, string attributeName)
    {
        return int.TryParse(element.GetAttribute(attributeName), out var span)
            ? Math.Max(span, 1)
            : 1;
    }

    private static string GetCell(IReadOnlyList<List<string>> rows, int rowIndex, int columnIndex) =>
        rowIndex < rows.Count && columnIndex < rows[rowIndex].Count ? rows[rowIndex][columnIndex] : null;

    private static void SetCell(List<List<string>> rows, int rowIndex, int columnIndex, string content)
    {
        EnsureRow(rows, rowIndex);
        var cells = rows[rowIndex];
        if (cells.Count <= columnIndex)
        {
            cells.AddRange(Enumerable.Repeat<string>(null, columnIndex - cells.Count + 1));
        }

        cells[columnIndex] = content;
    }

    private static void EnsureRow(List<List<string>> rows, int rowIndex)
    {
        if (rows.Count <= rowIndex)
        {
            rows.AddRange(Enumerable.Range(rows.Count, rowIndex - rows.Count + 1)
                .Select(_ => new List<string>()));
        }
    }

    private static void EnsureAlignment(List<string> alignments, int columnIndex)
    {
        if (alignments.Count <= columnIndex)
        {
            alignments.AddRange(Enumerable.Repeat(Empty, columnIndex - alignments.Count + 1));
        }
    }

    private static IElement FindHeader(IElement table, IReadOnlyCollection<IElement> rows)
        => table.QuerySelectorAll("thead > tr")
            .Concat(rows.Where(row => row.Children.Any(cell => cell.LocalName == "th")))
            .FirstOrDefault();

    private static int FindAvailableColumnIndex(List<string> cells)
    {
        return cells.TakeWhile(cell => cell is not null)
            .Count();
    }

    private static int GetSyntheticHeaderRowCount(IElement header) =>
        header is null ? 1 : 0;

    private sealed record TableCell(string Content, int ColumnSpan, int RowSpan, string Alignment);
}