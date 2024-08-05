using System.Diagnostics.CodeAnalysis;

namespace SkiaSharpControls.Models;

public class NavigationButtonItem
{
    public NavigationButtonItem()
    {
    }

    [SetsRequiredMembers]
    public NavigationButtonItem(string name, string path)
    {
        Name = name;
        Path = path;
    }

    public required string Name { get; init; }

    public required string Path { get; init; }

}