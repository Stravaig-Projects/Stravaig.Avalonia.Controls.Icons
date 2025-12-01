using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Humanizer;
using Stravaig.Avalonia.Controls.Icons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Stravaig.Icons.Example.ViewModels;

public partial class PhosphorIconDemoViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isExpanded;

    [ObservableProperty]
    private int? _selectedIconTypeIndex;

    [ObservableProperty]
    private PhosphorIconType _selectedIconType;

    [ObservableProperty]
    private int _iconSize;

    [ObservableProperty]
    private HsvColor _colour;

    [ObservableProperty]
    private string _rgbColour;

    [ObservableProperty]
    private string? _filter;

    [ObservableProperty]
    private int _filterCount;

    public PhosphorIconDemoViewModel()
    {
        // In design mode, expand the tree by default so that is open in the designer.
        _isExpanded = Design.IsDesignMode;
        _selectedIconTypeIndex = 2;
        _selectedIconType = IconTypes[2].Key;
        _iconSize = 24;
        _colour = new HsvColor(Color.FromRgb(0xFF, 0x00, 0x00));
        _rgbColour = _colour.ToRgb().ToString();
        _filterCount = Icons.Count;
    }

    public List<IconViewModel> Icons { get; } = Enum.GetValues<PhosphorIconName>().Select(n => new IconViewModel(n)).ToList();

    public List<KeyValuePair<PhosphorIconType, string>> IconTypes { get; } = Enum.GetValues<PhosphorIconType>().Select(t => new KeyValuePair<PhosphorIconType, string>(t, t.ToString())).ToList();

    partial void OnColourChanged(HsvColor value)
    {
        RgbColour = value.ToRgb().ToString();
    }

    partial void OnFilterChanged(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            // No filter, everything visible.
            foreach (var icon in Icons.Where(i => !i.IsVisible))
                icon.IsVisible = true;

            FilterCount = Icons.Count;
            return;
        }

        value = new string(value.Where(c => char.IsLetterOrDigit(c) || c == '-').ToArray());

        var regExValue = $"^.*{value.Kebaberize().Replace("-", ".*")}.*$";

        var regEx = new Regex(regExValue, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        var newCount = FilterCount;
        foreach (var icon in Icons)
        {
            if (regEx.IsMatch(icon.Value))
            {
                if (!icon.IsVisible)
                {
                    icon.IsVisible = true;
                    newCount++;
                }
            }
            else
            {
                if (icon.IsVisible)
                {
                    icon.IsVisible = false;
                    newCount--;
                }
            }
        }

        FilterCount = newCount;
    }

    partial void OnSelectedIconTypeIndexChanged(int? value)
    {
        if (value.HasValue)
            SelectedIconType = IconTypes[value.Value].Key;
        Trace.WriteLine($"Selected Icon Type Index Changed to {value}. Type is now {SelectedIconType}.");
    }

    public partial class IconViewModel : ViewModelBase
    {
        [ObservableProperty]
        private bool _isVisible;

        public IconViewModel(PhosphorIconName key)
        {
            Key = key;
            Value = key.ToString().Kebaberize();
            IsVisible = true;
        }

        public PhosphorIconName Key { get; }

        public string Value { get; }
    }
}
