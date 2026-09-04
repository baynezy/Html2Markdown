using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder
    .AddProject<TestWebApp>("TestHarness");

await builder.Build()
    .RunAsync();