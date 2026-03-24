# Calculator

A calculator web app built with Vite and tested end-to-end with C#, Reqnroll, and Playwright.

This `calculator-reqnroll` repository was first created as a copy of the `calculator` repository template.

At that point, the project used a JavaScript/Node calculator app and BDD-based E2E tests written in JavaScript using Playwright and BDD tooling.

GitHub Copilot was then used to migrate the E2E test stack from JavaScript-based BDD to C# with Reqnroll, while keeping the same calculator app behavior and scenario coverage.

The main purpose of this project is to demonstrate behavior-driven testing of a UI app using Gherkin Feature files with Reqnroll step definitions and a Playwright Page Object Model.

All application code and all tests in this repository were written 100% by GitHub Copilot.

GitHub Copilot was also used to pull in and configure all required technology modules for this project, including Playwright, Reqnroll, and the CI/CD pipeline.

This entire README file was created and is maintained via GitHub Copilot.

## Prerequisites

- Node.js 24+
- npm 11+
- .NET SDK 8+

## Install

```bash
npm install
```

## Run Locally

```bash
npm run dev
```

## Build

```bash
npm run build
```

## Test Commands

Run unit tests:

```bash
npm test
```

Run E2E tests (Reqnroll + Playwright + C#):

```bash
npm run test:e2e
```

Run E2E tests in headed mode:

```bash
npm run test:e2e:headed
```

## E2E Setup (first time)

Build the .NET test project and install Playwright browser dependencies:

```bash
dotnet build tests/Calculator.Reqnroll.Tests/Calculator.Reqnroll.Tests.csproj
pwsh tests/Calculator.Reqnroll.Tests/bin/Debug/net8.0/playwright.ps1 install --with-deps chromium
```

Optional: open and run C# tests through the root solution file:

```bash
dotnet sln calculator-reqnroll.slnx list
```

Run the app and E2E tests in separate terminals:

```bash
# terminal 1
npm run dev -- --host 127.0.0.1 --port 4173

# terminal 2
dotnet test tests/Calculator.Reqnroll.Tests/Calculator.Reqnroll.Tests.csproj
```

## CI/CD

GitHub Actions workflow:

- Builds the app
- Runs all unit tests
- Runs all Reqnroll E2E tests
- Uploads the build artifact (`dist`)
- Uploads test artifacts for each run (`test-artifacts`), including:
	- unit test log: `artifacts/logs/unit-tests.log`
	- e2e test log: `artifacts/logs/e2e-tests.log`
	- Playwright runtime output: `test-results/`

Workflow file: `.github/workflows/ci-cd.yml`

## Project Structure

```text
calculator/
	.github/
		workflows/
			ci-cd.yml
	public/
	src/
		styles/
			main.css
		bootstrap.js
		calculator.js
		calculator-app.js
		calculator-engine.js
		calculator.test.js
		calculator.unit.test.js
		calculator.buttons.test.js
		main.js
		run-tests.js
	tests/
			Calculator.Reqnroll.Tests/
				Calculator.Reqnroll.Tests.csproj
				reqnroll.json
				Features/
					Calculator.feature
				Steps/
					CalculatorSteps.cs
				Pages/
					CalculatorPage.cs
				Hooks/
					Hooks.cs
	index.html
	package.json
	package-lock.json
	.gitignore
	README.md
```
