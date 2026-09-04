using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AngleSharp.Html.Parser;
using Html2Markdown.Observability;
using Microsoft.Extensions.Diagnostics.Metrics.Testing;

namespace Html2Markdown.Test.Renderers;

using Html2Markdown.Renderers;

[Collection(nameof(MarkdownRendererTests))]
public class MarkdownRendererTests
{
    private readonly HtmlParser _parser = new();

    [Theory]
    [InlineData("<strong>Hello World</strong>", "strong")]
    [InlineData("<p>Hello World</p>", "p")]
    [InlineData("<em>Hello World</em>", "em")]
    [InlineData("<a href=\"https://example.com\">Hello World</a>", "a")]
    [InlineData("<u>Hello World</u>", "u")]
    public void RenderChildren_WhenCalledWithSingleElement_ThenShouldRecordRenderTagActivity(string html,
        string expectedTagName)
    {
        // arrange
        MarkdownRenderer sut = new([], false);
        var exportedActivities = new List<Activity>();
        using var listener = CreateMessagingActivityListener(exportedActivities);
        using var parentActivity = CreateParentActivity();
        var document = _parser.ParseDocument(html);
        var parentActivityId = parentActivity.Id;

        // act
        sut.RenderChildren(document.Body, ConversionContext.Default);

        // assert
        exportedActivities.Should()
            .ContainSingle(activity =>
                activity.OperationName == $"Render {expectedTagName}" && activity.ParentId == parentActivityId);
    }

    [Fact]
    public void RenderChildren_WhenCalledWithMultipleElements_ThenShouldRecordRenderTagActivities()
    {
        // arrange
        MarkdownRenderer sut = new([], false);
        var exportedActivities = new List<Activity>();
        using var listener = CreateMessagingActivityListener(exportedActivities);
        using var parentActivity = CreateParentActivity();
        const string html = "<p>Hello World</p><strong>Hello World</strong><em>Hello World</em>";
        var document = _parser.ParseDocument(html);
        var parentActivityId = parentActivity.Id;

        // act
        sut.RenderChildren(document.Body, ConversionContext.Default);

        // assert
        exportedActivities.Should()
            .Contain(activity =>
                activity.OperationName == "Render p" && activity.ParentId == parentActivityId);

        exportedActivities.Should()
            .Contain(activity =>
                activity.OperationName == "Render strong" && activity.ParentId == parentActivityId);

        exportedActivities.Should()
            .Contain(activity =>
                activity.OperationName == "Render em" && activity.ParentId == parentActivityId);
    }

    [Fact]
    public void RenderChildren_WhenCalledWithMultipleNestedElements_ThenShouldRecordRenderTagActivities()
    {
        // arrange
        MarkdownRenderer sut = new([], false);
        var exportedActivities = new List<Activity>();
        using var listener = CreateMessagingActivityListener(exportedActivities);
        using var parentActivity = CreateParentActivity();
        const string html = "<p>Hello <strong>World</strong></p>";
        var document = _parser.ParseDocument(html);

        // act
        sut.RenderChildren(document.Body, ConversionContext.Default);

        // assert
        exportedActivities.Should()
            .Contain(activity =>
                activity.OperationName == "Render p");

        exportedActivities.Should()
            .Contain(activity =>
                activity.OperationName == "Render strong");
    }

    [Fact]
    public void RenderChildren_WhenCalledWithMultipleNestedElements_ThenParentIdShouldMatchTheParentInTheHierarchy()
    {
        // arrange
        MarkdownRenderer sut = new([], false);
        var exportedActivities = new List<Activity>();
        using var listener = CreateMessagingActivityListener(exportedActivities);
        using var parentActivity = CreateParentActivity();
        const string html = "<p>Hello <strong>World</strong></p>";
        var document = _parser.ParseDocument(html);

        // act
        sut.RenderChildren(document.Body, ConversionContext.Default);

        // assert
        var pActivity = exportedActivities.FirstOrDefault(activity => activity.OperationName == "Render p");
        var strongActivity = exportedActivities.FirstOrDefault(activity => activity.OperationName == "Render strong");

        pActivity.Should()
            .NotBeNull();
        strongActivity.Should()
            .NotBeNull();

        strongActivity!.ParentId.Should()
            .Be(pActivity!.Id);
    }

    [Theory]
    [InlineData("<strong>Hello World</strong>")]
    [InlineData("<p>Hello World</p>")]
    [InlineData("<em>Hello World</em>")]
    [InlineData("<a href=\"https://example.com\">Hello World</a>")]
    [InlineData("<u>Hello World</u>")]
    public void RenderChildren_WhenCalledWithSingleElement_ThenCounterShouldHaveSingleCount(string html)
    {
        // arrange
        MarkdownRenderer sut = new([], false);
        var document = _parser.ParseDocument(html);
        using var collector = new MetricCollector<int>(ActivityConfig.RenderedElementsCounter);

        // act
        sut.RenderChildren(document.Body, ConversionContext.Default);

        // assert
        var measurement = collector.GetMeasurementSnapshot();
        measurement.Count.Should()
            .Be(1);
    }

    [Fact]
    public void RenderChildren_WhenCalledWithMultipleElements_ThenCounterShouldHaveCorrectCount()
    {
        // arrange
        MarkdownRenderer sut = new([], false);
        const string html = "<p>Hello World</p><strong>Hello World</strong><em>Hello World</em>";
        var document = _parser.ParseDocument(html);
        using var collector = new MetricCollector<int>(ActivityConfig.RenderedElementsCounter);

        // act
        sut.RenderChildren(document.Body, ConversionContext.Default);

        // assert
        var measurement = collector.GetMeasurementSnapshot();
        measurement.Count.Should()
            .Be(3);
    }

    [Fact]
    public void RenderChildren_WhenCalledWithMultipleNestedElements_ThenCounterShouldHaveCorrectCount()
    {
        // arrange
        MarkdownRenderer sut = new([], false);
        const string html = "<p>Hello <strong>World</strong></p>";
        var document = _parser.ParseDocument(html);
        using var collector = new MetricCollector<int>(ActivityConfig.RenderedElementsCounter);

        // act
        sut.RenderChildren(document.Body, ConversionContext.Default);

        // assert
        var measurement = collector.GetMeasurementSnapshot();
        measurement.Count.Should()
            .Be(2);
    }

    private static ActivityListener CreateMessagingActivityListener(List<Activity> exportedActivities)
    {
        var listener = new ActivityListener
        {
            ShouldListenTo = static source => source.Name == ActivityConfig.ServiceName,
            Sample = static (ref ActivityCreationOptions<ActivityContext> _) =>
                ActivitySamplingResult.AllDataAndRecorded,
            SampleUsingParentId = static (ref ActivityCreationOptions<string> _) =>
                ActivitySamplingResult.AllDataAndRecorded,
            ActivityStopped = exportedActivities.Add
        };

        ActivitySource.AddActivityListener(listener);
        return listener;
    }

    private static Activity CreateParentActivity()
    {
        var activity = new Activity("Parent");
        activity.SetIdFormat(ActivityIdFormat.W3C);
        activity.Start();
        return activity;
    }
}