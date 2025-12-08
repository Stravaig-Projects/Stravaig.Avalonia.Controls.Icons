# Stravaig Icon Controls

This package contains a number of icon controls for use with Avalonia 11.3.x

## Phosphor Icons

These icons come from the [Phosphor Icons](https://phosphoricons.com) collection.

For an example of how to use this control see the `Stravaig.Icons.Example` demo project.

![Phosphor Icons Example](https://raw.githubusercontent.com/Stravaig-Projects/Stravaig.Avalonia.Controls.Icons/main/docs/docs/PhosphorIconsExample.png)

### Usage

```xml
<!-- Import the namespace in your user control or window -->
<UserControl
    xmlns:icons="clr-namespace:Stravaig.Avalonia.Controls.Icons;assembly=Stravaig.Avalonia.Controls.Icons">

<!-- In the main body of your XAML -->
<icons:PhosphorIcon
    Color="Gray"
    IconName="Acorn"
    IconType="Duotone"/>
```

### Properties

* `IconType`: `enum PhosphorIconType` The type of icon to display. One of `Thin`, `Light`, `Regular`, `Bold`, `Fill`, or `Duotone`.
* `IconName`: `enum PhosphorIconName` - The name of the icon to display. The enum values are based on the pascal cased version of the icon name.
* `Color` - The HsvColor to display the icon.

---

## Contributing / Getting Started

* Ensure you have PowerShell 7.1.x or higher installed
* At a PowerShell prompt
    * Navigate to the root of this repository
    * Run `./Install-GitHooks.ps1`
