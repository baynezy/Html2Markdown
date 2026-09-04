using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Html2Markdown.Observability;

/// <summary>
/// Provides configuration for observability, including activity source and metrics for the Html2Markdown service.
/// </summary>
public static class ActivityConfig
{
    /// <summary>
    /// The name of the service for observability purposes.
    /// </summary>
    public const string ServiceName = "Html2Markdown";

    /// <summary>
    /// The activity source used for tracing operations within the Html2Markdown service.
    /// </summary>
    public static readonly ActivitySource ActivitySource = new(ServiceName);

    private static readonly Meter Meter = new(ServiceName);

    /// <summary>
    /// A counter that tracks the number of rendered HTML elements.
    /// </summary>
    public static readonly Counter<int> RenderedElementsCounter =
        Meter.CreateCounter<int>("html.elements.rendered", description: "Counts the number of rendered HTML elements");
}