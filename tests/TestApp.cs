using Avalonia;
using Avalonia.Headless;
using NordAtomThemePlugin.Tests;

// Registers the headless Avalonia application that backs every
// [AvaloniaFact]/[AvaloniaTheory] in this assembly. Without an initialized
// runtime, loading the embedded avares:// palette dictionaries throws.
[assembly: AvaloniaTestApplication(typeof(TestApp))]

namespace NordAtomThemePlugin.Tests;

public sealed class TestApp : Application
{
    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<TestApp>()
            .UseHeadless(new AvaloniaHeadlessPlatformOptions());
}
