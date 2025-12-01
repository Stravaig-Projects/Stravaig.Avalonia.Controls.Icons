using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Stravaig.Avalonia.Controls.Icons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

    partial void OnSelectedIconTypeIndexChanged(int? value)
    {
        if (value.HasValue)
            SelectedIconType = IconTypes[value.Value].Key;
        Trace.WriteLine($"Selected Icon Type Index Changed to {value}. Type is now {SelectedIconType}.");
    }

    public PhosphorIconDemoViewModel()
    {
        // In design mode, expand the tree by default so that is open in the designer.
        _isExpanded = Design.IsDesignMode;
        _selectedIconTypeIndex = 0;
        _selectedIconType = IconTypes[0].Key;
        _iconSize = 24;
    }

    public List<KeyValuePair<PhosphorIconName, string>> Icons { get; } = Enum.GetValues<PhosphorIconName>().Select(n => new KeyValuePair<PhosphorIconName, string>(n, n.ToString())).ToList();

    public List<KeyValuePair<PhosphorIconType, string>> IconTypes { get; } = Enum.GetValues<PhosphorIconType>().Select(t => new KeyValuePair<PhosphorIconType, string>(t, t.ToString())).ToList();


}
