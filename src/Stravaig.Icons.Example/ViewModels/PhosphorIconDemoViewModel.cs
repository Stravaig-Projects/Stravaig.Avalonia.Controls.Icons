using Stravaig.Avalonia.Controls.Icons;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stravaig.Icons.Example.ViewModels;

public class PhosphorIconDemoViewModel
{
    public List<KeyValuePair<PhosphorIconName, string>> Icons { get; } = Enum.GetValues<PhosphorIconName>().Select(n => new KeyValuePair<PhosphorIconName, string>(n, n.ToString())).ToList();
}
