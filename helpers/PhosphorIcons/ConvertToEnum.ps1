# Path to the Phosphor Icons
$iconPath = "$PSScriptRoot/../../src/Stravaig.Avalonia.Controls.Icons/Assets/PhosphorIcons"

# Output C# file path
$outputPath = Join-Path $PSScriptRoot "../../src/Stravaig.Avalonia.Controls.Icons/PhosphorIcon" | Convert-Path 
$enumOutputPath = $outputPath + "/PhosphorIconName.cs"
$dictionaryOutputPath = $outputPath + "/PhosphorIconEnumToResourceMap-Dictionary.cs"

# Read and parse JSON
$svgFiles = Get-ChildItem -Path $iconPath -Filter "*.svg" -Recurse -File

$iconData = $svgFiles | ForEach-Object {
    $fileName = $_.BaseName
    $directory = $_.Directory.Name
    
    if ($directory -eq 'regular') {
        [PSCustomObject]@{
            BaseName = $fileName
            Style = 'regular'
        }
    }
    elseif ($fileName -match '^(.+)-([^-]+)$') {
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
$enumEntries = $entries | Sort-Object -Property Name | ForEach-Object {
    "        /// <Summary>`n" +
    "        /// Phosphor icon '$($_.RawName)', available in $($_.Styles).`n" +
    "        /// </Summary>`n" +
    "        $($_.Name),`n"
}

$dictEntries = $entries | ForEach-Object {
    "            { PhosphorIconName.$($_.Name), `"$($_.RawName)`" },"
}

# Write C# file for enum
@"
// Auto-generated file. Do not modify directly.
// Use helpers/PhosphorIcons/ConvertToEnum.ps1 to regenerate.

namespace Stravaig.Avalonia.Controls.Icons;

/// <summary>
/// Represents the different icons available in the Phosphor library.
/// </summary>
public enum PhosphorIconName
{
$($enumEntries -join "`n")
}
"@ | Set-Content $enumOutputPath

Write-Host "C# file generated at $enumOutputPath"

# Write C# file for the helper class to convert the enum to the resource name.

@"
// Auto-generated file. Do not modify directly.
// Use helpers/PhosphorIcons/ConvertToEnum.ps1 to regenerate.

using System.Collections.Frozen;
using System.Collections.Generic;

namespace Stravaig.Avalonia.Controls.Icons;

internal static partial class PhosphorIconEnumToResourceMap
{
    private static readonly FrozenDictionary<PhosphorIconName, string> IconMap =
        new Dictionary<PhosphorIconName, string>
        {
$($dictEntries -join "`n")
        }.ToFrozenDictionary();
}
"@ | Set-Content $dictionaryOutputPath
Write-Host "C# file generated at $enumOutputPath"
