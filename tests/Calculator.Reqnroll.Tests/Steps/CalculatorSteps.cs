using Calculator.Reqnroll.Tests.Pages;
using Microsoft.Playwright;
using Reqnroll;

namespace Calculator.Reqnroll.Tests.Steps;

[Binding]
public sealed class CalculatorSteps
{
    private readonly CalculatorPage _calculator;

    public CalculatorSteps(ScenarioContext scenarioContext)
    {
        var page = scenarioContext.Get<IPage>(nameof(IPage));
        _calculator = new CalculatorPage(page);
    }

    [Given("the calculator page is open")]
    public Task GivenTheCalculatorPageIsOpen()
    {
        return _calculator.GotoAsync();
    }

    [When("I press {string}")]
    public async Task WhenIPress(string buttonSequence)
    {
        var buttons = new List<string>();
        for (var i = 0; i < buttonSequence.Length; i++)
        {
            if (i + 2 < buttonSequence.Length && buttonSequence.Substring(i, 3) == "+/-")
            {
                buttons.Add("+/-");
                i += 2;
            }
            else
            {
                buttons.Add(buttonSequence[i].ToString());
            }
        }

        foreach (var button in buttons)
        {
            await _calculator.PressButtonAsync(button);
        }
    }

    [Then("the display shows {string}")]
    public Task ThenTheDisplayShows(string result)
    {
        return _calculator.AssertResultAsync(result);
    }
}
