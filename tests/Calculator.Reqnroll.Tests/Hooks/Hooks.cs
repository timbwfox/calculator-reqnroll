using Microsoft.Playwright;
using Reqnroll;

namespace Calculator.Reqnroll.Tests.Hooks;

[Binding]
public sealed class Hooks
{
    [BeforeScenario]
    public async Task BeforeScenarioAsync(ScenarioContext scenarioContext)
    {
        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = !IsHeaded(),
            SlowMo = GetSlowMoMs()
        });

        var context = await browser.NewContextAsync(new BrowserNewContextOptions
        {
            BaseURL = Environment.GetEnvironmentVariable("E2E_BASE_URL") ?? "http://127.0.0.1:4173"
        });

        var page = await context.NewPageAsync();

        scenarioContext.Set(playwright, nameof(IPlaywright));
        scenarioContext.Set(browser, nameof(IBrowser));
        scenarioContext.Set(context, nameof(IBrowserContext));
        scenarioContext.Set(page, nameof(IPage));
    }

    [AfterScenario]
    public async Task AfterScenarioAsync(ScenarioContext scenarioContext)
    {
        if (scenarioContext.TryGetValue(nameof(IPage), out IPage? page) && page is not null)
        {
            await page.CloseAsync();
        }

        if (scenarioContext.TryGetValue(nameof(IBrowserContext), out IBrowserContext? context) && context is not null)
        {
            await context.CloseAsync();
        }

        if (scenarioContext.TryGetValue(nameof(IBrowser), out IBrowser? browser) && browser is not null)
        {
            await browser.CloseAsync();
        }

        if (scenarioContext.TryGetValue(nameof(IPlaywright), out IPlaywright? playwright) && playwright is not null)
        {
            playwright.Dispose();
        }
    }

    private static bool IsHeaded()
    {
        var headedValue = Environment.GetEnvironmentVariable("E2E_HEADED");
        return string.Equals(headedValue, "1", StringComparison.OrdinalIgnoreCase)
            || string.Equals(headedValue, "true", StringComparison.OrdinalIgnoreCase);
    }

    private static float? GetSlowMoMs()
    {
        var value = Environment.GetEnvironmentVariable("E2E_SLOWMO_MS");
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (float.TryParse(value, out var parsed) && parsed > 0)
        {
            return parsed;
        }

        return null;
    }
}
