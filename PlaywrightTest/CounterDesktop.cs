using Microsoft.Playwright;

namespace PlaywrightTest;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class CounterDesktop : BlazorTest
{
    [Test]
    public async Task NavigateToCounterPage()
    {
        await Page.GotoAsync(RootUri.AbsoluteUri);

        // Go to counter page
        await Page.GetByRole(AriaRole.Link, new() { Name = "Counter" }).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Status)).ToContainTextAsync("Current count: 0");
    }

    [Test]
    public async Task UseCounterPage()
    {
        // Go to counter page
        await Page.GotoAsync(RootUri.AbsoluteUri + "Counter");

        // Click button and verify count three times.
        await Page.GetByRole(AriaRole.Button, new() { Name = "Click me" }).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Status)).ToContainTextAsync("Current count: 1");

        await Page.GetByRole(AriaRole.Button, new() { Name = "Click me" }).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Status)).ToContainTextAsync("Current count: 2");

        await Page.GetByRole(AriaRole.Button, new() { Name = "Click me" }).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Status)).ToContainTextAsync("Current count: 3");
    }
}
