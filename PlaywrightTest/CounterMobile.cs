using Microsoft.Playwright;

namespace PlaywrightTest;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class CounterMobile : BlazorTest
{
    public override bool IsMobileTest => true;

    [Test]
    public async Task NavigateToCounterPageMobile()
    {
        if (!IsMobileTestSupported)
        {
            return;
        }

        var page = Page;

        await page.GotoAsync(RootUri.AbsoluteUri);

        // Go to counter page
        await page.GetByRole(AriaRole.Button, new() { Name = "Navigation menu" }).ClickAsync();
        await page.GetByRole(AriaRole.Link, new() { Name = "Counter" }).ClickAsync();
        await Expect(page.GetByRole(AriaRole.Status)).ToContainTextAsync("Current count: 0");
    }

    [Test]
    public async Task UseCounterPageMobile()
    {
        if (!IsMobileTestSupported)
        {
            return;
        }

        await Page.GotoAsync(RootUri.AbsoluteUri + "Counter");

        // Go to counter page
        await Expect(Page.GetByRole(AriaRole.Status)).ToContainTextAsync("Current count: 0");

        // Click button and verify count three times.
        await Page.GetByRole(AriaRole.Button, new() { Name = "Click me" }).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Status)).ToContainTextAsync("Current count: 1");

        await Page.GetByRole(AriaRole.Button, new() { Name = "Click me" }).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Status)).ToContainTextAsync("Current count: 2");

        await Page.GetByRole(AriaRole.Button, new() { Name = "Click me" }).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Status)).ToContainTextAsync("Current count: 3");
    }
}
