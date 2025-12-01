#!/usr/bin/env pwsh
# Script to add a class attribute to all SVG files in the Phosphor Icons folder
# Ensure you have a backup of the SVG files before running this script,
# e.g. via source control.

# Path to the Phosphor Icons
$iconPath = "$PSScriptRoot/../../src/Stravaig.Avalonia.Controls.Icons/Assets/PhosphorIcons"

$svgFiles = Get-ChildItem -Path $iconPath -Filter "*.svg" -Recurse -File

foreach ($file in $svgFiles) {
# Open the file as XML
# Add a class="Stravaig" attribute to the <svg> element, if one does not already exist
# Save the file back to disk
    [xml]$svgXml = Get-Content -Path $file.FullName
    $svgElement = $svgXml.DocumentElement

    if (-not $svgElement.HasAttribute("class")) {
        $svgElement.SetAttribute("class", "Stravaig")
        $svgXml.Save($file.FullName)
        Write-Host "Updated file: $($file.FullName)"
    } else {
        Write-Host "File already has class attribute: $($file.FullName)"
    }
}

Write-Host "Completed updating SVG files."