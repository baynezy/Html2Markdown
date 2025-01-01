# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [7.0.2.10] - 2025-01-01

## [7.0.1.9] - 2024-09-06

### Added

- Added support extending existing Schemes for customisation
- Migrated to using xUnit and FluentAssertions for testing
- Added code coverage to static analysis

## [7.0.0.8] - 2024-08-15

### Added

- Reverted doxygen publishing action back to original as they have taken
  [my contribution](https://github.com/DenverCoder1/doxygen-github-pages-action/pull/30) and 
  [implemented it](https://github.com/DenverCoder1/doxygen-github-pages-action/releases/tag/v2.0.0) in the main repo.
- Dropped support for .Net 7

## [6.2.5.7] - 2024-08-07

### Fixed

- Fixed issue where empty HTML lists would cause a `System.InvalidOperationException`

## [6.2.4.6] - 2024-08-02

### Added

- Added link to doxygen docs in GitHub Pages

### Fixed

- Fixed issue where HTML was allowed in headings

## [6.2.3.6] - 2024-08-02

### Fixed

- Fixed issue with documentation not being published

## [6.2.2.5] - 2024-08-02

### Removed

- Removed old files that are no longer needed

### Added

- Added missing XML docs for classes and methods
- Pushing beta packages to NuGet from `develop` branch
- Added README to NuGet package 
- Reinstate document publishing on successful merge to `master`

## [6.2.1.4] - 2024-05-07

### Added

- Upgraded release pipeline to use GitHub Actions

### Fixed

- Fixed recommendations from SonarQube
- Fixed issue with additional empty lists breaking

## [6.2.0.3] - 2023-12-21

### Added

- Added stale issue workflow
- Added allowing strong and b tags to have properties
- Modified all `IReplacers` to have their own concrete implementation

### Fixed

- Remove extra whitespace on lists containing p tags

## [6.1.0.2] - 2023-12-11

### Added

- Support for syntax highlighting on code blocks
- Added CommonMark schema

### Fixed

- Fixed issue with image src for master build status

## [6.0.0.1] - 2023-12-07

### Added

- Upgrade to .Net 8
- Added community labels
- Added support for GitStream
- Switched to NSubstitute from Moq

### Fixed

- Fixed issue with empty lists
- Fixed issues in README
- Fixed NuGet license issue

## [5.1.0] - 2022-09-26

### Added

- Upgraded AppVeyor tests to work with Linux

## [5.0.2] - 2022-02-09

## [5.0.1] - 2021-12-31

### Added

- Signed the assembly with a strong name

### Fixed

- Removed unused methods
- Moved HAP fix to extension method
- Resolved typos
- Update NuGet API key

## [5.0.0] - 2021-11-18

### Added

- Converted unit tests to use Verify.Net
- Upgraded to .Net 6
- Support for dependabot

### Fixed

- Upgraded HAP to latest version

## [4.0.0] - 2021-01-20

### Added

- Started replacing code blocks with triple backticks

### Fixed

- Updated NuGet API key

## [3.4.0] - 2020-10-19

### Added

- Grouped `IReplacer` for easier customisation

## [3.3.1] - 2020-02-23

### Fixed

- Swap incorrect whitespace for closing emphasis

## [3.3.0] - 2020-02-17

### Fixed

- Remove references to Waffle.io

## [3.2.3] - 2019-12-11

### Fixed

- Updated NuGet API key

## [3.2.2] - 2019-12-11

### Added

- Changed list parsing to work with `li`s with no closing tag

### Fixed

- Resolved issue with linter failing on build server
- Removed AppVeyor Slack Integration

## [3.2.1] - 2018-01-26

### Fixed

- Fixed code quality errors from Codacy

## [3.2.0] - 2018-01-24

### Added

- Add support for removing `<script>` tags

## [3.1.1.325] - 2017-12-07

### Fixed

- Fixed bug with test uploading

## [3.1.1] - 2017-12-07

### Added

- Expanded framework support

### Fixed

- Fix markdown linting errors
- Make sure test upload runs even on failure

## [3.1.0] - 2017-09-30

### Added

- Updated docs pertaining to customer conversion
- Added Gitter badge to README
- Configured AppVeyor to run markdown linter
- Updated doxygen to ignore binary files
- Swapped HtmlAgilityPack.Core for HtmlAgilityPack

### Fixed

- Added missing documentation

## [3.0.1] - 2017-09-17

### Added

- Improved validity of Markdown documentation
- Allowed different conversion schemes

## [3.0.0.1] - 2017-06-25

### Fixed

- Fixed NuGet publishing

## [3.0.0] - 2017-06-25

### Added

- Added a width hint to the commit template
- Configured tests to run with NUnit
- Configured GitHub contributing

### Fixed

- Made the tests pass on AppVeyor
- Made build push test results to AppVeyor
- Changed project configuration for NuGet to work
- Stopped doxygen process happening on non-master branches
- Updated configuration to be RELEASE in NuGet pack

## [2.1.3] - 2017-04-30

### Added

- Updated CONTRIBUTING.md
- Added a template for commit messages to the source
- Updated the contributing guidelines to include how to work with the new commit template
- Tidied up the Contributing Guidelines

### Fixed

- Fixed the issue that Doxygen was having creating the documentation

## [2.1.2] - 2016-04-22

### Fixed

- Stopped extra whitespace being created in paragraph tags

## [2.1.1] - 2016-04-13

### Fixed

- Changed build configuration to be release

## [2.1.0] - 2016-04-13

### Added

- Added Slack integration

## [2.0.15] - 2016-03-04

### Added

- Set up auto publishing of documentation on successful build

### Fixed

- Removed incompatible logo

## [2.0.14] - 2016-03-01

### Added

- Added Waffle.io badge to README
- Added licensing information

## [2.0.13] - 2016-02-18

### Fixed

- Configured AppVeyor via yml file
- Added .gitignore to handle NuGet properly
- Allowed environment variable fallback for test path
- Refactored to set test path on setup

## [2.0.12] - 2016-01-14

### Fixed

- Removed dependency on LinqExtensions

## [2.0.11] - 2015-12-22

### Added

- Updated README to properly show build status

## [2.0.9] - 2015-12-07

### Fixed

- Actually fixed whitespace issue
- Fixed broken unit tests

## [2.0.8] - 2015-12-07

### Added

- Removed excessive whitespace for `<blockquote>`

## [2.0.7] - 2015-12-06

### Fixed

- Fixed `<blockquote>` implementation as it was badly flawed

## [2.0.6] - 2015-12-04

### Added

- Added HTML Entity parsing

### Fixed

- Fixed issue with `<p>` tags with attributes
- Updated `<blockquote>` matching
- Fixed `<hr>` issues
- Tightened Regex

## [2.0.5] - 2015-12-03

### Added

- Added support form `DOCTYPE`
- Added support from `<html>` tags
- Added comment removal
- Added support from `<head>` tags
- Added support for `<meta>` tags
- Added support for `<title>` tags
- Added support for `<link>` tags
- Added support for `<body>` tags
- Added support for empty anchors

### Fixed

- Documentation improvements

## [2.0.4] - 2015-12-01

### Added

- Updated README to include links to the demo site

## [2.0.3] - 2015-11-26

### Added

- Removed extra whitespace

## [2.0.2] - 2015-11-22

### Fixed

- Fixed issue with titles with attributes

## [2.0.1.40780] - 2015-11-16

### Fixed

- Fixed whitespace issue

## [2.0.1] - 2015-11-16

### Added

- Added support for converting html files to markdown
- Updated documentation to reflect new file reading capability

## [2.0.0] - 2015-11-16

### Added

- Updated anchor conversion to use `HtmlAgilityPack`
- Added docs to public classes and methods
- Updated `<pre>` conversion to use `HtmlAgilityPack`

### Fixed

- Cleaned up redundant code

## [1.0.7] - 2015-11-14

### Added

- Added whitespace removal

## [1.0.6] - 2015-11-11

### Fixed

- Refactored `<pre>` replacement

## [1.0.5] - 2015-11-10

### Added

- Added NuGet installation instructions
- Added link to AppVeyor public page for project
- Added support for multiline `<code>` blocks

## [1.0.4] - 2015-11-06

### Fixed

- Fixed versioning issue with NuGet dependencies

## [1.0.3] - 2015-10-13

### Added

- Updated documentation
- Updated to use LinqExtensions
- Setup NuGet package restore

### Fixed

- Improved variable naming

## [1.0.2] - 2015-10-10

### Fixed

- Moved replacers into their own namespace

## [1.0.1] - 2015-10-09

### Added

- Added badges to README
- Added support for other attributes in `<a>` tags

## [1.0.0] - 2014-10-13

### Added

- Update README.md
- Added support for pre tags
- Added support for lists
- Updated documentation to include all supported tags
-

## [0.0.2] - 2014-10-13

### Added

- Added `blockquote` support
- Added support for `paragraph` tags
- Added support for `horizontal rule`
- Refactored Elements to be `IReplacers`
- Added support for image tags

## [0.0.1] - 2013-07-04

[unreleased]: https://github.com/baynezy/Html2Markdown/compare/7.0.2.10...HEAD
[7.0.2.10]: https://github.com/baynezy/Html2Markdown/compare/7.0.1.9...7.0.2.10
[7.0.1.9]: https://github.com/baynezy/Html2Markdown/compare/7.0.0.8...7.0.1.9
[7.0.0.8]: https://github.com/baynezy/Html2Markdown/compare/6.2.5.7...7.0.0.8
[6.2.5.7]: https://github.com/baynezy/Html2Markdown/compare/6.2.4.6...6.2.5.7
[6.2.4.6]: https://github.com/baynezy/Html2Markdown/compare/6.2.3.6...6.2.4.6
[6.2.3.6]: https://github.com/baynezy/Html2Markdown/compare/6.2.2.5...6.2.3.6
[6.2.2.5]: https://github.com/baynezy/Html2Markdown/compare/6.2.1.4...6.2.2.5
[6.2.1.4]: https://github.com/baynezy/Html2Markdown/compare/6.2.0.3...6.2.1.4
[6.2.0.3]: https://github.com/baynezy/Html2Markdown/compare/6.1.0.2...6.2.0.3
[6.1.0.2]: https://github.com/baynezy/Html2Markdown/compare/6.0.0.1...6.1.0.2
[6.0.0.1]: https://github.com/baynezy/Html2Markdown/compare/5.1.0...6.0.0.1
[5.1.0]: https://github.com/baynezy/Html2Markdown/compare/5.0.2...5.1.0
[5.0.2]: https://github.com/baynezy/Html2Markdown/compare/5.0.1...5.0.2
[5.0.1]: https://github.com/baynezy/Html2Markdown/compare/5.0.0...5.0.1
[5.0.0]: https://github.com/baynezy/Html2Markdown/compare/4.0.0...5.0.0
[4.0.0]: https://github.com/baynezy/Html2Markdown/compare/3.4.0...4.0.0
[3.4.0]: https://github.com/baynezy/Html2Markdown/compare/3.3.1...3.4.0
[3.3.1]: https://github.com/baynezy/Html2Markdown/compare/3.3.0...3.3.1
[3.3.0]: https://github.com/baynezy/Html2Markdown/compare/3.2.3...3.3.0
[3.2.3]: https://github.com/baynezy/Html2Markdown/compare/3.2.2...3.2.3
[3.2.2]: https://github.com/baynezy/Html2Markdown/compare/3.2.1...3.2.2
[3.2.1]: https://github.com/baynezy/Html2Markdown/compare/3.2.0...3.2.1
[3.2.0]: https://github.com/baynezy/Html2Markdown/compare/3.1.1.325...3.2.0
[3.1.1.325]: https://github.com/baynezy/Html2Markdown/compare/3.1.1...3.1.1.325
[3.1.1]: https://github.com/baynezy/Html2Markdown/compare/3.1.0...3.1.1
[3.1.0]: https://github.com/baynezy/Html2Markdown/compare/3.0.1...3.1.0
[3.0.1]: https://github.com/baynezy/Html2Markdown/compare/3.0.0.1...3.0.1
[3.0.0.1]: https://github.com/baynezy/Html2Markdown/compare/3.0.0...3.0.0.1
[3.0.0]: https://github.com/baynezy/Html2Markdown/compare/2.1.3...3.0.0
[2.1.3]: https://github.com/baynezy/Html2Markdown/compare/2.1.2...2.1.3
[2.1.2]: https://github.com/baynezy/Html2Markdown/compare/2.1.1...2.1.2
[2.1.1]: https://github.com/baynezy/Html2Markdown/compare/2.1.0...2.1.1
[2.1.0]: https://github.com/baynezy/Html2Markdown/compare/2.0.15...2.1.0
[2.0.15]: https://github.com/baynezy/Html2Markdown/compare/2.0.14...2.0.15
[2.0.14]: https://github.com/baynezy/Html2Markdown/compare/2.0.13...2.0.14
[2.0.13]: https://github.com/baynezy/Html2Markdown/compare/2.0.12...2.0.13
[2.0.12]: https://github.com/baynezy/Html2Markdown/compare/2.0.11...2.0.12
[2.0.11]: https://github.com/baynezy/Html2Markdown/compare/2.0.9...2.0.11
[2.0.9]: https://github.com/baynezy/Html2Markdown/compare/2.0.8...2.0.9
[2.0.8]: https://github.com/baynezy/Html2Markdown/compare/2.0.7...2.0.8
[2.0.7]: https://github.com/baynezy/Html2Markdown/compare/2.0.6...2.0.7
[2.0.6]: https://github.com/baynezy/Html2Markdown/compare/2.0.5...2.0.6
[2.0.5]: https://github.com/baynezy/Html2Markdown/compare/2.0.4...2.0.5
[2.0.4]: https://github.com/baynezy/Html2Markdown/compare/2.0.3...2.0.4
[2.0.3]: https://github.com/baynezy/Html2Markdown/compare/2.0.2...2.0.3
[2.0.2]: https://github.com/baynezy/Html2Markdown/compare/2.0.1.40780...2.0.2
[2.0.1.40780]: https://github.com/baynezy/Html2Markdown/compare/2.0.1...2.0.1.40780
[2.0.1]: https://github.com/baynezy/Html2Markdown/compare/2.0.0...2.0.1
[2.0.0]: https://github.com/baynezy/Html2Markdown/compare/1.0.7...2.0.0
[1.0.7]: https://github.com/baynezy/Html2Markdown/compare/1.0.6...1.0.7
[1.0.6]: https://github.com/baynezy/Html2Markdown/compare/1.0.5...1.0.6
[1.0.5]: https://github.com/baynezy/Html2Markdown/compare/1.0.4...1.0.5
[1.0.4]: https://github.com/baynezy/Html2Markdown/compare/1.0.3...1.0.4
[1.0.3]: https://github.com/baynezy/Html2Markdown/compare/1.0.2...1.0.3
[1.0.2]: https://github.com/baynezy/Html2Markdown/compare/1.0.1...1.0.2
[1.0.1]: https://github.com/baynezy/Html2Markdown/compare/1.0.0...1.0.1
[1.0.0]: https://github.com/baynezy/Html2Markdown/compare/0.0.2...1.0.0
[0.0.2]: https://github.com/baynezy/Html2Markdown/compare/0.0.1...0.0.2
[0.0.1]: https://github.com/baynezy/Html2Markdown/compare/6b4b488efd44ded4bcf0af7b3b44167591ff61cd...0.0.1
