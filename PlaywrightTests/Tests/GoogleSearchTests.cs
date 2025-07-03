using Xunit;
using FluentAssertions;
using Microsoft.Playwright;
using System.Threading.Tasks;
using System;

public class GoogleSearchTests : IAsyncLifetime
{
    private IPlaywright _playwright = null!;
    private IBrowser _browser = null!;
    private IPage _page = null!;

    public async Task InitializeAsync()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new()
        {
            Headless = false,
            SlowMo = 100
        });
        _page = await _browser.NewPageAsync();
    }

    public async Task DisposeAsync()
    {
        await _browser.CloseAsync();
        _playwright.Dispose();
    }

    [Fact]
    public async Task ContactForm_Submission_MissingFourFields()
    {
        await _page.GotoAsync("https://www.google.com");
        var searchPage = new GoogleSearchPage(_page);
        await searchPage.SearchAsync("Prometheus Group");

        var contactUsPage = new ContactUsPage(_page);
        await searchPage.ClickContactUsAsync();

        await contactUsPage.FillFirstAndLastNameAsync("Jacob", "Sisson");
        await contactUsPage.ClickSubmitAsync();

        var requiredErrorCount = await contactUsPage.CountRemainingRequiredFieldsAsync();

        if (requiredErrorCount < 4)
        {
            Console.WriteLine($"Expected at least 4 validation errors but found {requiredErrorCount}");
        }
        else
        {
            Console.WriteLine("Correct number of validation errors triggered.");
        }
    }
}