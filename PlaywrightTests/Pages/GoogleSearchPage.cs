using Microsoft.Playwright;
using System.Threading.Tasks;

public class GoogleSearchPage
{
    private readonly IPage _page;

    public GoogleSearchPage(IPage page) => _page = page;

    public async Task SearchAsync(string query)
    {
        await _page.GotoAsync("https://www.google.com", new() { Timeout = 10000 });

        var agreeButton = _page.Locator("xpath=//button[text()='I agree'] | //div[@role='none']//button[text()='Accept']");
        if (await agreeButton.CountAsync() > 0)
            await agreeButton.First.ClickAsync();

        var searchBox = _page.Locator("xpath=//textarea[@name='q']");
        await searchBox.WaitForAsync();
        await searchBox.FillAsync(query);
        await _page.Keyboard.PressAsync("Enter");

        // Wait for results page to load
        await _page.WaitForURLAsync(url => url.Contains("/search"), new() { Timeout = 10000 });
    }

    public async Task ClickContactUsAsync()
    {
        var contactLink = _page.Locator("xpath=//a[normalize-space()='Contact Us']");
        await contactLink.First.ClickAsync();
    }
}
