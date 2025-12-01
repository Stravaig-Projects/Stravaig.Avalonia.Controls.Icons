using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Svg.Skia;
using Svg.Model;

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

    public static readonly StyledProperty<HsvColor> ColorProperty =
        AvaloniaProperty.Register<PhosphorIcon, HsvColor>(
            nameof(Color));

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

    public HsvColor Color
    {
        get => GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    static PhosphorIcon()
    {
        IconTypeProperty.Changed.AddClassHandler<PhosphorIcon>((x, _) => x.UpdateImageSource());
        IconNameProperty.Changed.AddClassHandler<PhosphorIcon>((x, _) => x.UpdateImageSource());
        ColorProperty.Changed.AddClassHandler<PhosphorIcon>((x, _) => x.UpdateImageSource());
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

        try
        {
            var css = BuildCss();
            Console.WriteLine($"Loading {resource}\n    with CSS: {css}");
            var sourceImage = SvgSource.Load(resource);
            var svg = new SvgImage
            {
                Source = sourceImage,
                Css = css,
                //CurrentCss = css,
            };
            Source = svg;
        }
        catch
        {
            Source = null;
        }
    }

    private string BuildCss()
    {
        var rgb = Color.ToRgb();
        var css = $"svg {{ fill: rgb({rgb.R}, {rgb.G}, {rgb.B}); }}";
        return css;
    }
}
