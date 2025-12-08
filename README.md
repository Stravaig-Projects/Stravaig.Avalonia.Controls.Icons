# Stravaig Icon Controls

This package contains a number of icon controls for use with Avalonia 11.3.x

* Stable releases:
    * ![Nuget](https://img.shields.io/nuget/v/Stravaig.Avalonia.Controls.Icons?color=004880&label=nuget%20stable&logo=nuget) [View Stravaig.Avalonia.Controls.Icons on NuGet](https://www.nuget.org/packages/Stravaig.Avalonia.Controls.Icons)
* Latest releases:
    * ![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Stravaig.Avalonia.Controls.Icons?color=ffffff&label=nuget%20latest&logo=nuget) [View Stravaig.Avalonia.Controls.Icons on NuGet](https://www.nuget.org/packages/Stravaig.Avalonia.Controls.Icons)

## Phosphor Icons

These icons come from the [Phosphor Icons](https://phosphoricons.com) collection.

For an example of how to use this control see the `Stravaig.Icons.Example` demo project.

```powershell
# To run the demo project:
# From the repository root
dotnet run --project ./src/Stravaig.Icons.Example/Stravaig.Icons.Example.csproj
```

![Phosphor Icons Example](https://raw.githubusercontent.com/Stravaig-Projects/Stravaig.Avalonia.Controls.Icons/main/docs/docs/PhosphorIconsExample.png)

### Usage

```xml
<!-- Import the namespace in your user control or window -->
<UserControl
    xmlns:icons="clr-namespace:Stravaig.Avalonia.Controls.Icons;assembly=Stravaig.Avalonia.Controls.Icons">

    <!-- In the main body of your XAML -->
    <icons:PhosphorIcon
        Foreground="Gray"
        IconName="Acorn"
        IconType="Duotone" />

</UserControl>
```

### Properties

* `IconType`: `enum PhosphorIconType` The type of icon to display. One of `Thin`, `Light`, `Regular`, `Bold`, `Fill`, or `Duotone`.
* `IconName`: `enum PhosphorIconName` The name of the icon to display. The enum values are based on the pascal cased version of the icon name.
* `Foreground`: IBrush - The Foreground Bruse to use to display the icon. Note: Only SolidColorBrush is currently supported.

---

## Contributing / Getting Started

* Ensure you have PowerShell 7.1.x or higher installed
* At a PowerShell prompt
    * Navigate to the root of this repository
    * Run `./Install-GitHooks.ps1`
