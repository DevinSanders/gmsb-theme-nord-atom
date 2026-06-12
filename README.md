# gmsb-theme-nord-atom

Nord + Atom One Dark theme pack for [Game Master Sound Board](https://github.com/DevinSanders/game-master-soundboard).

Two iconic dark colour schemes — opposite ends of the "cool dark" axis. Two independent, selectable palettes:

| Palette         | Notes |
|-----------------|-------|
| Nord            | arcticicestudio's frosty low-contrast palette. Cool Polar Night greys with desaturated Frost (cyan/blue) accents and muted Aurora (red/orange/yellow/green/purple) status hues. Excellent for long sessions. |
| Atom One Dark   | The warmer, softer dark scheme from GitHub's Atom editor. Slightly higher saturation than Nord, with a recognizable syntax-highlight accent vocabulary (cursor-blue, syntax-red, syntax-green, etc.). |

Each palette is a flat set of colours — one selectable look in the host's theme dropdown (shown as "Nord & Atom One Dark: Nord" / "…: Atom One Dark"). There is no Dark/Light variant: the host applies the palette regardless of the active Avalonia variant and infers light/dark Fluent chrome (scrollbars, popups, focus rings) from the background luminance on its own. Both palettes happen to be dark-looking; users who want light surfaces should pick a light-friendly pack (Catppuccin Latte, Solarized Light, Dracula Alabaster).

## Install

Drop the released `.zip` onto Settings → Plugin Manager. Themes activate live — no restart needed. Pick the palette from Settings → Appearance → Theme.

Pre-built zips are attached to each [GitHub Release](../../releases).

## Build

```powershell
dotnet build src/NordAtomThemePlugin.csproj
pwsh scripts/package.ps1
# → dist/github.DevinSanders-theme.nord-atom-1.0.0.zip
```

Requires .NET 10 SDK. `SoundBoard.PluginApi` is restored from NuGet automatically — no sibling checkout needed.

## Plugin manifest

| Field     | Value                          |
|-----------|--------------------------------|
| publisher | `github.DevinSanders`          |
| id        | `theme.nord-atom`              |
| entryDll  | `NordAtomThemePlugin.dll`      |
| isTheme   | `true`                         |

## Attribution

- **Nord** color values from https://www.nordtheme.com — © Arctic Ice Studio + Sven Greb, MIT licensed.
- **Atom One Dark** color values from https://github.com/atom/atom/tree/master/packages/one-dark-ui — © GitHub Inc., MIT licensed.

This pack adapts both palettes to Game Master Sound Board's semantic key vocabulary; it is not an official port of either.

## License

Released under the [MIT License](LICENSE).

Nord colors are © Arctic Ice Studio + Sven Greb, licensed under MIT — see https://github.com/nordtheme/nord/blob/main/LICENSE.md.

Atom One Dark colors are © GitHub Inc., licensed under MIT — see https://github.com/atom/atom/blob/master/LICENSE.md.
