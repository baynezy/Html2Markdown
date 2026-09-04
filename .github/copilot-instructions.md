# GitHub Copilot Instructions

This document contains guidelines that GitHub Copilot should follow when assisting with this repository.

## General

* Make only high confidence suggestions when reviewing code changes.
* Infer the naming conventions and coding standards from the existing codebase.
* Prefer UK English spelling for all text, including comments and documentation.

## Formatting

* Apply code-formatting style defined in `.editorconfig`.
* Prefer file-scoped namespace declarations and single-line using directives.
* Ensure that the final return statement of a method is on its own line.
* Use pattern matching and switch expressions wherever possible.
* Use `nameof` instead of string literals when referring to member names.

### Nullable Reference Types

* Declare variables non-nullable, and check for `null` at entry points.
* Always use `is null` or `is not null` instead of `== null` or `!= null`.
* Trust the C# null annotations and don't add null checks when the type system says a value cannot be null.

### Testing

* Write unit tests for all new features and bug fixes.
* Add `arrange`, `act`, and `assert` comments to unit tests to clarify the structure.
* We use the following testing frameworks:
  - xUnit for unit tests
  - NSubstitute for mocking dependencies
  - AwesomeAssertions for assertions
  - Verify for snapshot testing
  - FsCheck for Property-based testing
* Copy existing style in nearby files for test method names and capitalisation.

## Running Tests

1. Install the latest version of Cake `dotnet tool install Cake.Tool`.
2. Build using `dotnet cake --target=Build`.
3. If that produces errors, fix those errors and build again. Repeat until the build is successful.
4. Run tests using the Cake build script with `dotnet cake --testFilter="Category!=LocalTest"`.

## Mutation Testing

All changes must maintain a mutation score of at least 80% (the break threshold in `stryker-config.json`). This is enforced by the `step-mutation-testing.yml` workflow, so it must be verified locally before opening a Pull Request.

1. Install Stryker `dotnet tool install -g dotnet-stryker --version 4.8.1`.
2. Run mutation testing against the target branch with `dotnet stryker --since:develop` (use the branch the work will be merged into).
3. If the score is below the break threshold, open the generated report in `StrykerOutput/<timestamp>/reports/mutation-report.html` and review the surviving mutants.
4. Add or improve unit tests to kill the surviving mutants. Never weaken the thresholds or exclude code to pass the check.
5. Repeat until the run succeeds, and delete the `StrykerOutput` folder before committing.

Note that mutants in constructors invoked from static initialisers (for example the renderers in `HtmlTagRenderers.Defaults`) are only attributed to the first test that runs. Construct such types directly within a test to make them reliably testable.

## Changelog Updates

For all Pull Requests created:

1. **An entry must be added to CHANGELOG.md** under the `[Unreleased]` section.
2. Entries should follow the [Keep a Changelog](https://keepachangelog.com/en/1.0.0/) format.
3. Group changes under the appropriate headings:
   - `Added` for new features
   - `Changed` for changes in existing functionality
   - `Deprecated` for soon-to-be removed features
   - `Removed` for now removed features
   - `Fixed` for any bug fixes
   - `Security` in case of vulnerabilities
4. Be concise but descriptive in the changelog entries.
5. Make sure the entry clearly communicates the purpose and impact of the change.
6. Add a reference to the issue number if applicable.

This changelog update is required for all PRs to maintain a comprehensive history of changes to the project.
