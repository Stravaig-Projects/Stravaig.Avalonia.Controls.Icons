using Avalonia;
using Avalonia.Headless.XUnit;
using Avalonia.Platform;
using Shouldly;
using System.Text;
using System.IO;

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
                var type = parts[0];
                var name = parts[1];
                name = name.Replace(
                    string.Equals(type, "regular", StringComparison.OrdinalIgnoreCase)
                        ? ".svg"
                        : $"-{type}.svg",
                    string.Empty,
                    StringComparison.OrdinalIgnoreCase);

                return new
                {
                    Type = type,
                    Name = name
                };
            })
            .GroupBy(a => a.Name)
            .OrderBy(g => g.Key)
            .ToArray();

        // Render a Markdown table of the resources, with the types along
        // the top and the names down the side. Cells contain a ✅ if there is a
        // resource with that type and name and a ❌ if not. The table should be
        // space padded so that it looks readable as plain text too.
        // The verify method will check that the table is correct.
        StringBuilder md = new StringBuilder();

        // Work out the set of types and names
        var allTypes = iconResources
            .SelectMany(g => g.Select(x => x.Type))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(t => t, StringComparer.OrdinalIgnoreCase)
            .ToArray();

        var nameToTypes = iconResources
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.Type).ToHashSet(StringComparer.OrdinalIgnoreCase),
                StringComparer.OrdinalIgnoreCase);

        // Compute column widths for nice plain-text readability
        int nameColWidth = Math.Max("Name".Length, nameToTypes.Keys.Max(n => n.Length));
        var typeColWidths = allTypes
            .Select(t => Math.Max(t.Length, 1))
            .ToArray();

        // Helper to pad a cell with surrounding spaces inside the pipe
        static string Pad(string content, int width)
            => " " + content.PadRight(width) + " ";

        // Header
        md.Append('|').Append(Pad("Name", nameColWidth));
        for (int i = 0; i < allTypes.Length; i++)
            md.Append('|').Append(Pad(allTypes[i], typeColWidths[i]));
        md.Append('|').AppendLine();

        // Separator row per Markdown table syntax
        md.Append('|').Append(' ').Append(new string('-', nameColWidth)).Append(' ');
        for (int i = 0; i < typeColWidths.Length; i++)
        {
            md.Append('|').Append(' ').Append(new string('-', typeColWidths[i])).Append(' ');
        }
        md.Append('|').AppendLine();

        // Rows per name
        foreach (var name in nameToTypes.Keys.OrderBy(n => n, StringComparer.OrdinalIgnoreCase))
        {
            md.Append('|').Append(Pad(name, nameColWidth));
            var presentTypes = nameToTypes[name];
            for (int i = 0; i < allTypes.Length; i++)
            {
                var has = presentTypes.Contains(allTypes[i]) ? "✅" : "❌";
                md.Append('|').Append(Pad(has, typeColWidths[i]));
            }
            md.Append('|').AppendLine();
        }

        await Verify(md.ToString());
    }
}
