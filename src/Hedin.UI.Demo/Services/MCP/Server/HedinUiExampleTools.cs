using Microsoft.AspNetCore.Components;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Hedin.UI.Demo.Services.MCP.Server;

public static class HedinUiExampleTools
{
    private static readonly Lazy<List<ComponentExampleDto>> _cachedExamples = new(() =>
    {
        var allRazorFiles = Directory.GetFiles(AppContext.BaseDirectory,
                                               "*.razor",
                                               SearchOption.AllDirectories);

        return allRazorFiles.Select(path =>
        {
            var razorCode = File.ReadAllText(path);

            // Hitta ALLA taggar som börjar med HUI* (t.ex. <HUIButton>, <HUIAppBar>)
            var matches = Regex.Matches(razorCode, @"<(?<tag>HUI[A-Za-z0-9]+)");

            var usedComponents = matches
                                 .Select(m => m.Groups["tag"].Value)
                                 .Distinct()
                                 .ToList();

            return new ComponentExampleDto
            {
                ComponentName = Path.GetFileNameWithoutExtension(path),
                RazorCode = razorCode,
                RelativePath = Path.GetRelativePath(AppContext.BaseDirectory, path),
                UsedComponents = usedComponents           // 🆕
            };
        }).ToList();
    });

    public static List<ComponentExampleDto> GetAllExamples() => _cachedExamples.Value;

    public static List<ComponentExampleDto> GetExamplesByComponent(string componentName)
    {
        // Filtrera på både filnamn OCH om koden faktiskt använder t.ex. <HUIButton>
        return _cachedExamples.Value
            .Where(x =>
                x.ComponentName.Contains(componentName, StringComparison.OrdinalIgnoreCase) ||
                x.UsedComponents.Any(u =>
                    u.Contains(componentName, StringComparison.OrdinalIgnoreCase)))
            .ToList();
    }

    public static List<string> ListComponentNames()
    {
        // Nu returnerar vi en lista på ALLA HUI-taggar som förekommer i exem­plen
        return _cachedExamples.Value
            .SelectMany(e => e.UsedComponents)
            .Distinct()
            .OrderBy(x => x)
            .ToList();
    }
}