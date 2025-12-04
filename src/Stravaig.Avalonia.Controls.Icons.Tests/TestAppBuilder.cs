using Avalonia;
using Avalonia.Headless;

[assembly: AvaloniaTestApplication(typeof(Stravaig.Avalonia.Controls.Icons.Tests.TestAppBuilder))]

namespace Stravaig.Avalonia.Controls.Icons.Tests;

public class TestAppBuilder
{
    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>()
        .UseHeadless(new AvaloniaHeadlessPlatformOptions());
}
