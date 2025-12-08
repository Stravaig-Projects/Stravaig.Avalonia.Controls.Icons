using Avalonia.Media;

namespace Stravaig.Avalonia.Controls.Icons;

internal static class PhosphorIconBrushHelper
{
    public static string ToCss(IBrush brush)
    {
        var rgb = new Color(255, 0, 0, 0);
        if (brush is SolidColorBrush solidColorBrush)
        {
            // Use the colour of the brush.
            rgb = solidColorBrush.Color;
        }
        else if (brush is GradientBrush gradientBrush)
        {
            // TODO: Support gradients. For the moment, just use the first colour.
            var firstColor = gradientBrush.GradientStops.FirstOrDefault();
            if (firstColor != null)
                rgb = firstColor.Color;
        }

        var css = $".Stravaig {{ color: rgb({rgb.R}, {rgb.G}, {rgb.B}); }}";
        return css;
    }
}