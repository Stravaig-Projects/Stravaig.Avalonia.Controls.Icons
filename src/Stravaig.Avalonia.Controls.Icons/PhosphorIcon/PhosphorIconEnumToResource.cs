using System.Diagnostics;

namespace Stravaig.Avalonia.Controls.Icons;

internal static partial class PhosphorIconEnumToResourceMap
{
    internal static string? GetResourceName(PhosphorIconName name, PhosphorIconType type)
    {
        if (IconMap.TryGetValue(name, out var resourceNameFragment))
        {
            var pathFragment = type.ToString().ToLowerInvariant();
            var resourceFile = resourceNameFragment +
                               (type == PhosphorIconType.Regular
                                   ? string.Empty
                                   : $"-{pathFragment}");
            var fullResourcePath =
                $"avares://Stravaig.Avalonia.Controls.Icons/Assets/PhosphorIcons/{pathFragment}/{resourceFile}.svg";
            return fullResourcePath;
        }

        // Should never happen as the Enum and Dictionary are generated from the same source.
        Console.WriteLine($"Could not find resource for icon. {name} - {type}.");
        Debug.Assert(false, $"Could not find resource for icon. {name} - {type}.");
        return null;
    }
}
