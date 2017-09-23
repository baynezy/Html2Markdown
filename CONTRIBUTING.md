# Contributing

## Pull Requests

After forking the repository please create a pull request before creating the
fix. This way we can talk about how the fix will be implemeted. This will
greatly increase your chance of your patch getting merged into the code base.

Please create all pull requests from the `develop` branch.

### AppVeyor

[AppVeyor](https://www.appveyor.com/) is a Continous Delivery system for this
project. It is configured in `appveyor.yml`. All PRs and commits to the project
are checked on AppVeyor. You can setup AppVeyor for your own fork which means
you can test your code prior to raising a PR.

### Commit Template

Please run the following to make sure you commit messages conform to the project
standards.

```bash
git config --local commit.template .gitmessage
```

## Markdown

There are linting rules for the markdown documentation in this project. So
please adhere to them. This can be achieved by installing the node module
`markdownlint-cli`.

```bash
npm install -g markdownlint-cli
```

Then to check your Markdown run.

```bash
markdownlint .
```
