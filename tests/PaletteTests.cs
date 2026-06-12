using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Styling;
using FluentAssertions;
using SoundBoard.PluginApi;
using Xunit;

namespace NordAtomThemePlugin.Tests;

public class PaletteTests
{
    // The 25 semantic brush keys the host expects every palette to define.
    // A missing key means an unstyled control the first time a user selects
    // the theme, so the completeness theory below guards the whole set.
    public static readonly string[] SemanticKeys =
    {
        "SidebarBackground", "ContentBackground",
        "PanelBackground1", "PanelBackground2", "PanelBackground3", "SubtleBorder",
        "PrimaryAccent", "PrimaryAccentHover", "OnPrimaryAccent", "SecondaryAccent",
        "TextPrimary", "TextSecondary",
        "SuccessBackground", "SuccessForeground",
        "DangerBackground", "DangerForeground",
        "InfoBackground", "InfoForeground",
        "WarningBackground", "WarningForeground",
        "DropZoneHighlight", "WaveformBrush",
        "LoopInheritForeground", "LoopForceOnForeground", "LoopForceOffForeground",
    };

    private static readonly (string Id, string Name, string Uri)[] ExpectedPalettes =
    {
        ("nord", "Nord", "avares://NordAtomThemePlugin/Themes/Nord.axaml"),
        ("atom-one-dark", "Atom One Dark", "avares://NordAtomThemePlugin/Themes/AtomOneDark.axaml"),
    };

    private static IEnumerable<ThemePalette> Palettes => new NordAtomThemePlugin().GetPalettes();

    // ── Palette catalog ──────────────────────────────────────────────
    // GetPalettes() ships exactly the id / name / URI set this pack
    // advertises in its docs and plugin.json.

    [Fact]
    public void GetPalettes_ReturnsExpectedCatalog()
    {
        var actual = Palettes
            .Select(p => (p.Id, p.Name, Uri: p.ResourceUris.Single()))
            .ToArray();

        actual.Should().BeEquivalentTo(ExpectedPalettes);
    }

    [Fact]
    public void Plugin_IdentityMatchesManifest()
    {
        var plugin = new NordAtomThemePlugin();

        plugin.Id.Should().Be("theme.nord-atom");
        plugin.Name.Should().Be("Nord & Atom One Dark");
        plugin.Author.Should().Be("Devin Sanders");
        plugin.Version.Should().NotBeNullOrWhiteSpace();
    }

    // ── Resources resolve ────────────────────────────────────────────
    // Each advertised avares:// URI loads to a non-empty dictionary.

    [AvaloniaTheory]
    [MemberData(nameof(PaletteUris))]
    public void PaletteUri_LoadsNonEmptyDictionary(string uri)
    {
        var dict = Load(uri);

        dict.Count.Should().BeGreaterThan(0);
    }

    // ── Semantic-key completeness (the important test) ───────────────
    // Every (palette × key) pair resolves to a SolidColorBrush.

    [AvaloniaTheory]
    [MemberData(nameof(PaletteKeyMatrix))]
    public void Palette_DefinesSemanticKeyAsBrush(string uri, string key)
    {
        var dict = Load(uri);

        dict.TryGetResource(key, ThemeVariant.Default, out var value)
            .Should().BeTrue($"palette '{uri}' must define '{key}'");
        value.Should().BeOfType<SolidColorBrush>();
    }

    // ── Flatness guard ───────────────────────────────────────────────
    // A regression to the old Dark/Light variant model would populate
    // ThemeDictionaries; a flat palette never does.

    [AvaloniaTheory]
    [MemberData(nameof(PaletteUris))]
    public void Palette_HasNoThemeVariants(string uri)
    {
        Load(uri).ThemeDictionaries.Should().BeEmpty();
    }

    // ── Data sources ─────────────────────────────────────────────────

    public static IEnumerable<object[]> PaletteUris() =>
        ExpectedPalettes.Select(p => new object[] { p.Uri });

    public static IEnumerable<object[]> PaletteKeyMatrix() =>
        from p in ExpectedPalettes
        from key in SemanticKeys
        select new object[] { p.Uri, key };

    private static ResourceDictionary Load(string uri)
    {
        var source = new Uri(uri);
        var include = new ResourceInclude(source) { Source = source };
        return (ResourceDictionary)include.Loaded;
    }
}
