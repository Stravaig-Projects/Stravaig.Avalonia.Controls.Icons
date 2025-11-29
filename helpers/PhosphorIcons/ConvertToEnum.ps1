# Path to the selection.json from the Phosphor Icons repository
$iconPath = "$PSScriptRoot/../../src/Stravaig.Avalonia.Controls.Icons/Assets/PhosphorIcons"

# Output C# file path
$outputPath = Join-Path $PSScriptRoot "../../src/Stravaig.Avalonia.Controls.Icons/PhosphorIcons" | Convert-Path 
$outputPath += "/PhosphorIconName.cs"

# Read and parse JSON
$svgFiles = Get-ChildItem -Path $iconPath -Filter "*.svg" -Recurse -File

$iconData = $svgFiles | ForEach-Object {
    $fileName = $_.BaseName
    if ($fileName -match '^(.+)-([^-]+)$') {
        [PSCustomObject]@{
            BaseName = $matches[1]
            Style = $matches[2]
        }
    }
} | Group-Object -Property BaseName | ForEach-Object {
    [PSCustomObject]@{
        Name = $_.Name
        Styles = ($_.Group | Select-Object -ExpandProperty Style | Sort-Object -Unique)
    }
}

# $json = @{
#     icons = $iconData | ForEach-Object {
#         @{
#             properties = @{
#                 name = $_.Name
#                 styles = $_.Styles
#             }
#         }
#     }
# }

# Extract icon name and code, format as required
$entries = foreach ($icon in $iconData) {
    $rawName = $icon.Name;
    $styles = if ($icon.Styles.Count -gt 1) {
        ($icon.Styles[0..($icon.Styles.Count-2)] -join ', ') + ', and ' + $icon.Styles[-1]
    } else {
        $icon.Styles -join ''
    }
    
    # Split by comma to handle multiple names
    $names = $rawName -split ',' | ForEach-Object { $_.Trim() }
    
    foreach ($singleName in $names) {
        $name = (($singleName -split '-') | ForEach-Object { $_.Substring(0,1).ToUpperInvariant() + $_.Substring(1) }) -join ''
        [PSCustomObject]@{ Name = $name; RawName = $rawName; Styles = $styles }
    }
}

# Generate C# dictionary entries
$dictEntries = $entries | Sort-Object -Property Name | ForEach-Object {
    "        /// <Summary>`n" +
    "        /// Phosphor icon '$($_.RawName)', available in styles $($_.Styles).`n" +
    "        /// </Summary>`n" +
    "        $($_.Name) = $($_.Value),`n"
}

# Write C# file
@"
// Auto-generated file. Do not modify directly.
// Use helpers/Phosphor-Icons/ConvertToEnum.ps1 to regenerate.

// Some icon codes have multiple names, leading to duplicate enum values which triggers CA1069 warning.
#pragma warning disable CA1069

namespace Stravaig.Controls.Icons;

public enum PhosphorIconName
{
$($dictEntries -join "`n")
}
"@ | Set-Content $outputPath

Write-Host "C# file generated at $outputPath"