var target = Argument("target", "Test");
var configuration = Argument("configuration", "Release");
var versionNumber = Argument("versionNumber", "0.1.0");
var projectName = "Html2Markdown";
var solutionFolder = "./";

Task("Clean")
    .Does(() =>
    {
        // Clean solution
        DotNetClean(solutionFolder);
    });

Task("Restore")
	.Does(() =>
	{
		// Restore NuGet packages
		DotNetRestore(solutionFolder);
	});

Task("Build")
	.Does(() =>
	{
		// Build solution
		DotNetBuild(solutionFolder, new DotNetBuildSettings
		{
			NoRestore = true,
			Configuration = configuration,
            ArgumentCustomization = args => args.Append("/p:Version=" + versionNumber)
		});
	});

Task("Test")
	.Does(() =>
	{
		// Run tests
		DotNetTest(solutionFolder, new DotNetTestSettings
		{
			NoRestore = true,
            NoBuild = true,
			Configuration = configuration,
            Loggers = new string[] { "junit;LogFileName=results.xml" }
		});
	});

Task("Pack")
    .Does(() =>
    {
        // Publish solution
        DotNetPack(solutionFolder, new DotNetPackSettings
        {
            NoRestore = true,
            NoBuild = true,
            Configuration = configuration,
            ArgumentCustomization = args => args.Append("/p:PackageVersion=" + versionNumber)
        });
    });

RunTarget(target);