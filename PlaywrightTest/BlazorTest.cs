using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTest;
[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class BlazorTest : PageTest
{
    // private static protected Uri DefaultUrl { get; private set; } = new Uri("https://localhost:7154/");
    private static protected Uri DefaultUrl { get; private set; } = new Uri("http://localhost:5097/");

    public virtual bool IsMobileTest => false;

    public bool IsMobileTestSupported => BrowserName == "chromium";

    public override BrowserNewContextOptions ContextOptions()
    {
        if (IsMobileTest && IsMobileTestSupported)
        {
            return Playwright.Devices["Pixel 7"];
        }

        return base.ContextOptions();
    }

    protected Uri RootUri
    {
        get
        {
            var baseUrl = Environment.GetEnvironmentVariable("BASE_URL");

            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                return DefaultUrl;
            }

            return new Uri(baseUrl);
        }
    }

    [SetUp]
    public async Task Setup()
    {
        if (IsMobileTest && !IsMobileTestSupported)
        {
            return;
        }

        await Context.Tracing.StartAsync(new()
        {
            Title = $"{(IsMobileTest ? "Mobile" : "Desktop")} - {BrowserName} - {TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}",
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
    }

    [TearDown]
    public async Task TearDown()
    {
        if (IsMobileTest && !IsMobileTestSupported)
        {
            return;
        }

        // Do not store anything if the test succeeded.
        if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed)
        {
            return;
        }

        // This will produce e.g.:
        // bin/Debug/net8.0/playwright-traces/PlaywrightTests.Tests.Test1.zip
        await Context.Tracing.StopAsync(new()
        {
            Path = Path.Combine(
                TestContext.CurrentContext.WorkDirectory,
                "playwright-traces",
                $"{(IsMobileTest ? "Mobile" : "Desktop")} - {BrowserName} - {TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip"
            )
        });
    }
}
