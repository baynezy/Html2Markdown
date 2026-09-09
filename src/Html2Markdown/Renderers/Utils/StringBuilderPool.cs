namespace Html2Markdown.Renderers.Utils;

internal static class StringBuilderPool
{
    private const int MaximumRetainedCapacity = 16 * 1024;

    [ThreadStatic]
    private static StringBuilder _cachedInstance;

    internal static StringBuilder Rent()
    {
        var builder = _cachedInstance;

        if (builder is null)
        {
            return new StringBuilder();
        }

        _cachedInstance = null;
        builder.Clear();
        return builder;
    }

    internal static string ReturnAndToString(StringBuilder builder)
    {
        var value = builder.ToString();
        Return(builder);
        return value;
    }

    internal static void Return(StringBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        if (builder.Capacity > MaximumRetainedCapacity)
        {
            return;
        }

        builder.Clear();
        _cachedInstance = builder;
    }
}
