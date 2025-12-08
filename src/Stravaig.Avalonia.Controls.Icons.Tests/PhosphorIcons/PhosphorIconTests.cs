using Avalonia.Headless.XUnit;
using Avalonia.Media;
using Shouldly;
using System.Diagnostics.CodeAnalysis;

namespace Stravaig.Avalonia.Controls.Icons.Tests.PhosphorIcons;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores")]
public class PhosphorIconTests
{
    [AvaloniaFact]
    public void DefaultValuesAreSet()
    {
        var icon = new PhosphorIcon();
        icon.IconName.ShouldBe(PhosphorIconName.Acorn);
        icon.IconType.ShouldBe(PhosphorIconType.Regular);
        icon.Foreground.ShouldBeOfType<SolidColorBrush>()
            .Color.ShouldBe(Colors.Black);
        icon.IsVisible.ShouldBeTrue();
        icon.Source.ShouldNotBeNull();
    }


    [AvaloniaFact]
    public void IconType_WhenChanged_UpdatesSource()
    {
        var icon = new PhosphorIcon();
        icon.IconName = PhosphorIconName.Heart;
        icon.IconType = PhosphorIconType.Fill;
        icon.Source.ShouldNotBeNull();
    }
}
