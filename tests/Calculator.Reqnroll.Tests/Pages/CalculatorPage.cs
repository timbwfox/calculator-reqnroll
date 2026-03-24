using Microsoft.Playwright;
using NUnit.Framework;

namespace Calculator.Reqnroll.Tests.Pages;

public sealed class CalculatorPage
{
    private readonly IPage _page;

    public CalculatorPage(IPage page)
    {
        _page = page;
    }

    public Task GotoAsync()
    {
        return _page.GotoAsync("/");
    }

    public Task PressButtonAsync(string button)
    {
        return _page.Locator($"[data-button=\"{button}\"]").ClickAsync();
    }

    public async Task AssertResultAsync(string expected)
    {
        var actual = await _page.Locator("#result").InnerTextAsync();
        Assert.That(actual, Is.EqualTo(expected));
    }
}
