# Stravaig Icon Controls

This package contains a number of icon controls for use with Avalonia 11.3.x

## Phosphor Icons

These icons come from the [Phosphor Icons](https://phosphoricons.com) collection.

For an example of how to use this control see the `Stravaig.Icons.Example` demo project.

### Usage

```xml
<!-- Import the namespace in your user control or window -->
<UserControl
    xmlns:icons="clr-namespace:Stravaig.Avalonia.Controls.Icons;assembly=Stravaig.Avalonia.Controls.Icons">

<!-- In the main body of your XAML -->
<icons:PhosphorIcon
    Foreground="Gray"
    IconName="Acorn"
    IconType="Duotone"/>
```

### Properties

* `IconType`: `enum PhosphorIconType` The type of icon to display. One of `Thin`, `Light`, `Regular`, `Bold`, `Fill`, or `Duotone`.
* `IconName`: `enum PhosphorIconName` The name of the icon to display. The enum values are based on the pascal cased version of the icon name.
* `Foreground`: `IBrush` The brush to use to display the icon. Note: Only SolidColorBrush is supported.
