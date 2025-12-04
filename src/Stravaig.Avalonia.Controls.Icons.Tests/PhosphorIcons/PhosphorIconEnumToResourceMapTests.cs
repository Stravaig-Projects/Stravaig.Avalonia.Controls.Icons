using Avalonia;
using Avalonia.Headless.XUnit;
using Avalonia.Platform;
using Shouldly;

namespace Stravaig.Avalonia.Controls.Icons.Tests.PhosphorIcons;

public class PhosphorIconEnumToResourceMapTests
{
    private static readonly string AssemblyName = typeof(PhosphorIcon).Assembly.GetName().Name!;

    [Theory]
    [InlineData(PhosphorIconName.Acorn, PhosphorIconType.Bold, "bold/acorn-bold")]
    public void MapsToResourceCorrectly(PhosphorIconName name, PhosphorIconType type, string expectedResourceFragment)
    {
        var expectedResource = $"avares://{AssemblyName}/Assets/PhosphorIcons/{expectedResourceFragment}.svg";
        var resourceName = PhosphorIconEnumToResourceMap.GetResourceName(name, type);

        resourceName.ShouldBe(expectedResource);
    }

    [AvaloniaFact]
    public async Task TrackPhosphorIconResourcesAsync()
    {
        var prefix = "avares://Stravaig.Avalonia.Controls.Icons/Assets/PhosphorIcons/";
        var iconResources = AssetLoader.GetAssets(new Uri(prefix), null)
            .Select(uri =>
            {
                var parts = uri.ToString().Substring(prefix.Length).Split('/');
                return new
                {
                    Type = parts[0],
                    Name = parts[1].Replace($"-{parts[0]}.svg", string.Empty, StringComparison.OrdinalIgnoreCase)
                };
            })
            .GroupBy(a => a.Name)
            .OrderBy(g => g.Key)
            .ToArray();



        await Verify(iconResources);
    }
}
