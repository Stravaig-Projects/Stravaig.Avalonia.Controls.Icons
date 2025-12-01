using CommunityToolkit.Mvvm.ComponentModel;
using Stravaig.Avalonia.Controls.Icons;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stravaig.Icons.Example.ViewModels;

public partial class PhosphorIconDemoViewModel : ViewModelBase
{
    [ObservableProperty]
    private KeyValuePair<PhosphorIconType, string>? _selectedIconTypeName;

    public List<KeyValuePair<PhosphorIconName, string>> Icons { get; } = Enum.GetValues<PhosphorIconName>().Select(n => new KeyValuePair<PhosphorIconName, string>(n, n.ToString())).ToList();

    //public List<string> IconTypes { get; } = Enum.GetValues<PhosphorIconType>().Select(t => t.ToString()).ToList();

    public List<KeyValuePair<PhosphorIconType, string>> IconTypes { get; } = Enum.GetValues<PhosphorIconType>().Select(t => new KeyValuePair<PhosphorIconType, string>(t, t.ToString())).ToList();

}
