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

    # Add the class to the SVG element
    $svgElement = $svgXml.DocumentElement
    if (-not $svgElement.HasAttribute("class")) {
        $svgElement.SetAttribute("class", "Stravaig")
        Write-Host "Added <svg ... class=`"Stravaig`"> attribute to: $($file.FullName)"
    }

    # All circle elements should get the fill="currentColor" attribute
    $circleElements = $svgXml.GetElementsByTagName("circle")
    foreach ($circle in $circleElements) {
        if (-not $circle.HasAttribute("fill")) {
            $circle.SetAttribute("fill", "currentColor")
            Write-Host "Added <circle ... fill=`"currentColor`"> in: $($file.FullName)"
        }
    }

    # All ellipse elements should get the fill="currentColor" attribute
    $ellipseElements = $svgXml.GetElementsByTagName("ellipse")
    foreach ($ellipse in $ellipseElements) {
        if (-not $ellipse.HasAttribute("fill")) {
            $ellipse.SetAttribute("fill", "currentColor")
            Write-Host "Added <ellipse ... fill=`"currentColor`"> in: $($file.FullName)"
        }
    }

    # All path elements with an opacity attribute should get fill="currentColor"
    $pathElements = $svgXml.GetElementsByTagName("path")
    foreach ($path in $pathElements) {
        if ($path.HasAttribute("opacity") -and -not $path.HasAttribute("fill")) {
            $path.SetAttribute("fill", "currentColor")
            Write-Host "Added <path ... fill=`"currentColor`"> in: $($file.FullName)"
        }
    }

    # All g elements with an opacity attribute should get fill="currentColor"
    $gElements = $svgXml.GetElementsByTagName("g")
    foreach ($g in $gElements) {
        if ($g.HasAttribute("opacity") -and -not $g.HasAttribute("fill")) {
            $g.SetAttribute("fill", "currentColor")
            Write-Host "Added <g ... fill=`"currentColor`"> in: $($file.FullName)"
        }
    }

    # All rect elements with an opacity attribute should get fill="currentColor"
    $rectElements = $svgXml.GetElementsByTagName("rect")
    foreach ($rect in $rectElements) {
        if ($rect.HasAttribute("opacity") -and -not $rect.HasAttribute("fill")) {
            $rect.SetAttribute("fill", "currentColor")
            Write-Host "Added <rect ... fill=`"currentColor`"> in: $($file.FullName)"
        }
    }

    # All polygon elements with an opacity attribute should get fill="currentColor"
    $polygonElements = $svgXml.GetElementsByTagName("polygon")
    foreach ($polygon in $polygonElements) {
        if ($polygon.HasAttribute("opacity") -and -not $polygon.HasAttribute("fill")) {
            $polygon.SetAttribute("fill", "currentColor")
            Write-Host "Added <polygon ... fill=`"currentColor`"> in: $($file.FullName)"
        }
    }

    # For any file where the name ends with -fill.svg...
    if ($file.Name -like "*-fill.svg") {
        # ... ensure all path elements have fill="currentColor"
        $pathElements = $svgXml.GetElementsByTagName("path")
        foreach ($path in $pathElements) {
            if (-not $path.HasAttribute("fill")) {
                $path.SetAttribute("fill", "currentColor")
                Write-Host "Added <path ... fill=`"currentColor`"> in: $($file.FullName)"
            }
        }
        # ensure rect elements have fill="currentColor"
        $rectElements = $svgXml.GetElementsByTagName("rect")
        foreach ($rect in $rectElements) {
            if (-not $rect.HasAttribute("fill")) {
                $rect.SetAttribute("fill", "currentColor")
                Write-Host "Added <rect ... fill=`"currentColor`"> in: $($file.FullName)"
            }
        }
    }

    $svgXml.Save($file.FullName)
    Write-Host "Updated file: $($file.FullName)"
}

Write-Host "Completed updating SVG files."