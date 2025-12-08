using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Svg.Skia;
using Svg.Model;
using System.Diagnostics;

namespace Stravaig.Avalonia.Controls.Icons;

/// <summary>
/// This is a control that displays a Phosphor icon.
/// </summary>
public class PhosphorIcon : Image
{
    /// <summary>
    /// Defines the <see cref="IconType"/> property.
    /// </summary>
    public static readonly StyledProperty<PhosphorIconType> IconTypeProperty =
        AvaloniaProperty.Register<PhosphorIcon, PhosphorIconType>(
            nameof(IconType),
            defaultValue: PhosphorIconType.Regular);

    /// <summary>
    /// Defines the <see cref="IconName"/> property.
    /// </summary>
    public static readonly StyledProperty<PhosphorIconName> IconNameProperty =
        AvaloniaProperty.Register<PhosphorIcon, PhosphorIconName>(
            nameof(IconName));

    /// <summary>
    /// Defines the <see cref="Color"/> property.
    /// </summary>
    public static readonly StyledProperty<HsvColor> ColorProperty =
        AvaloniaProperty.Register<PhosphorIcon, HsvColor>(
            nameof(Color));

    /// <summary>
    /// Gets or sets the type of icon.
    /// </summary>
    public PhosphorIconType IconType
    {
        get => GetValue(IconTypeProperty);
        set => SetValue(IconTypeProperty, value);
    }

    /// <summary>
    /// Gets or sets the name of the icon.
    /// </summary>
    public PhosphorIconName IconName
    {
        get => GetValue(IconNameProperty);
        set => SetValue(IconNameProperty, value);
    }

    /// <summary>
    /// Gets or sets the color of the icon.
    /// </summary>
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

    /// <summary>
    /// Creates a new instance of the control.
    /// </summary>
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
            Debug.WriteLine("Could not generate resource name. Setting source to null.");
            Source = null;
            return;
        }

        try
        {
            var css = BuildCss();
            var sourceImage = SvgSource.Load(resource);
            var svg = new SvgImage
            {
                Source = sourceImage,
                Css = css,
            };
            Source = svg;
        }
        catch(Exception ex)
        {
            Debug.WriteLine($"Failed to load resource: {resource}");
            Debug.WriteLine(ex);
            Debug.WriteLine("Setting source to null.");
            Source = null;
        }
    }

    private string BuildCss()
    {
        var rgb = Color.ToRgb();
        var css = $".Stravaig {{ color: rgb({rgb.R}, {rgb.G}, {rgb.B}); }}";
        return css;
    }
}
