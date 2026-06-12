using System.Collections.Generic;
using SoundBoard.PluginApi;

namespace NordAtomThemePlugin;

/// <summary>
/// A pack of two iconic dark colour schemes that occupy opposite ends of
/// the "cool dark" axis:
///
/// <list type="bullet">
///   <item><b>Nord</b> (https://www.nordtheme.com) — arcticicestudio's
///   low-contrast frosty palette. Cool greys with desaturated cyan/blue
///   accents (Frost) and muted nature-named accents (Aurora). The
///   canonical "easy on the eyes for long sessions" dark scheme.</item>
///   <item><b>Atom One Dark</b> (https://github.com/atom/atom/tree/master/packages/one-dark-ui) —
///   the warmer, softer dark scheme from GitHub's Atom editor.
///   Slightly higher saturation than Nord, with a recognizable
///   syntax-highlight accent vocabulary (red/orange/yellow/green/cyan/
///   blue/purple).</item>
/// </list>
///
/// <para>Nord and Atom One Dark are two independent flat palettes —
/// each a single selectable look in the host's theme dropdown. Both
/// happen to be dark-looking; theme authors declare nothing about
/// light/dark. The host applies the chosen palette regardless of the
/// active Avalonia variant and infers light/dark Fluent chrome from
/// the background luminance on its own. Users who prefer light surfaces
/// should pick a different pack (Catppuccin Latte, Solarized Light,
/// Dracula Alabaster).</para>
/// </summary>
public sealed class NordAtomThemePlugin : IThemePlugin
{
    public string Id => "theme.nord-atom";
    public string Name => "Nord & Atom One Dark";
    public string Version => PluginVersion.OfAssembly(typeof(NordAtomThemePlugin));
    public string Author => "Devin Sanders";
    public string Description => "Two cool dark schemes: Nord (frosty low-contrast blue) and Atom One Dark (warmer softer dark).";

    public void Initialize(IPluginContext context) { }
    public void Shutdown() { }

    public IEnumerable<ThemePalette> GetPalettes() => new[]
    {
        new ThemePalette("nord",          "Nord",
            new[] { "avares://NordAtomThemePlugin/Themes/Nord.axaml" }),
        new ThemePalette("atom-one-dark", "Atom One Dark",
            new[] { "avares://NordAtomThemePlugin/Themes/AtomOneDark.axaml" }),
    };
}
