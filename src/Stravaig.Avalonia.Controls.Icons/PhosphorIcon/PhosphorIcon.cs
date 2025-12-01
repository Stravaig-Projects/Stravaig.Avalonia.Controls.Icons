using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Svg.Skia;

namespace Stravaig.Avalonia.Controls.Icons;

/// <summary>
/// This is a control that displays a Phosphor icon.
/// </summary>
public class PhosphorIcon : Image
{
    public static readonly StyledProperty<PhosphorIconType> IconTypeProperty =
        AvaloniaProperty.Register<PhosphorIcon, PhosphorIconType>(
            nameof(IconType),
            defaultValue: PhosphorIconType.Regular);

    public static readonly StyledProperty<PhosphorIconName> IconNameProperty =
        AvaloniaProperty.Register<PhosphorIcon, PhosphorIconName>(
            nameof(IconName));

    public PhosphorIconType IconType
    {
        get => GetValue(IconTypeProperty);
        set => SetValue(IconTypeProperty, value);
    }

    public PhosphorIconName IconName
    {
        get => GetValue(IconNameProperty);
        set => SetValue(IconNameProperty, value);
    }

    static PhosphorIcon()
    {
        IconTypeProperty.Changed.AddClassHandler<PhosphorIcon>((x, _) => x.UpdateImageSource());
        IconNameProperty.Changed.AddClassHandler<PhosphorIcon>((x, _) => x.UpdateImageSource());
    }

    public PhosphorIcon()
    {
        // Ensure initial Source is set based on default properties
        UpdateImageSource();
    }

    private string? GetResourceName()
        => PhosphorIconEnumToResourceMap.GetResourceName(IconName, IconType);

    private void UpdateImageSource()
    {
        var resource = GetResourceName();
        if (string.IsNullOrWhiteSpace(resource))
        {
            Source = null;
            return;
        }

        // Delegate actual SVG rendering to Avalonia's Image via SvgImage
        try
        {
            var svg = new SvgImage
            {
                Source = SvgSource.Load(resource),
            };
            Source = svg;
        }
        catch
        {
            Source = null;
        }
    }
}
