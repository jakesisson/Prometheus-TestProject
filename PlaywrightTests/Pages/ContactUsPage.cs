using Microsoft.Playwright;
using System.Threading.Tasks;

public class ContactUsPage
{
    private readonly IPage _page;

    public ContactUsPage(IPage page) => _page = page;

    public async Task FillFirstAndLastNameAsync(string firstName, string lastName)
    {
        await _page.FillAsync("xpath=//input[@name='firstname']", firstName);
        await _page.FillAsync("xpath=//input[@name='lastname']", lastName);
    }

    public async Task ClickSubmitAsync()
    {
        var submitButton = _page.Locator("xpath=//input[@type='submit']");
        await submitButton.First.ClickAsync();
    }

    public async Task<int> CountRemainingRequiredFieldsAsync()
    {
        // Match all inputs/selects with `required` AND still empty OR invalid state
        var missingFields = _page.Locator(
            "xpath=//*[@required and " +
            "((self::input or self::select) and " +
            "((not(@value) or @value='') or contains(@class, 'invalid') or contains(@class, 'error')))]"
        );

        return await missingFields.CountAsync();
    }
}
