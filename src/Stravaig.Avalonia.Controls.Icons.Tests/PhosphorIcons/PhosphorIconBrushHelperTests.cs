using Avalonia.Headless.XUnit;
using Avalonia.Media;
using Shouldly;
using System.Diagnostics.CodeAnalysis;
using Xunit.Abstractions;

namespace Stravaig.Avalonia.Controls.Icons.Tests.PhosphorIcons;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores")]
public class PhosphorIconBrushHelperTests
{
    private readonly ITestOutputHelper _output;

    public PhosphorIconBrushHelperTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [AvaloniaTheory]
    [InlineData(0, 0, 0)]
    [InlineData(255, 0, 0)]
    [InlineData(0, 255, 0)]
    [InlineData(0, 0, 255)]
    [InlineData(255, 255, 255)]
    public void ToCss_WhenBrushIsSolid_ReturnsColourInCssFragment(byte r, byte g, byte b)
    {
        var solidColorBrush = new SolidColorBrush(Color.FromRgb(r, g, b));
        var css = PhosphorIconBrushHelper.ToCss(solidColorBrush);
        _output.WriteLine(css);
        css.ShouldContain($"rgb({r}, {g}, {b})");
    }

    [AvaloniaTheory]
    [InlineData(0, 0, 0)]
    [InlineData(255, 0, 0)]
    [InlineData(0, 255, 0)]
    [InlineData(0, 0, 255)]
    [InlineData(255, 255, 255)]
    public void ToCss_WhenBrushIsGradient_ReturnsFirstColourInCssFragment(byte r, byte g, byte b)
    {
        var gradientBrush = new LinearGradientBrush();
        gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(r, g, b), 0));
        gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(0, 0, 0), 1));
        var css = PhosphorIconBrushHelper.ToCss(gradientBrush);
        _output.WriteLine(css);
        css.ShouldContain($"rgb({r}, {g}, {b})");
    }

    [AvaloniaFact]
    public void ToCss_WhenBrushIsNotSupported_ReturnsBlackCssFragment()
    {
        var brush = new DrawingBrush();
        var css = PhosphorIconBrushHelper.ToCss(brush);
        _output.WriteLine(css);
        css.ShouldContain($"rgb(0, 0, 0)");
    }
}
