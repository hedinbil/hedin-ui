using System.Text.RegularExpressions;

namespace Hedin.UI.Demo.Services.MCP.Server;

public static partial class HedinUiExampleTools
{
    private static readonly Lazy<List<ComponentExampleDto>> CachedExamples = new(() =>
    {
        var assembly = typeof(HedinUiExampleTools).Assembly;
        var resourceNames = assembly.GetManifestResourceNames()
            .Where(n => n.EndsWith(".razor", StringComparison.OrdinalIgnoreCase))
            .ToList();

        return resourceNames.Select(resourceName =>
        {
            using var stream = assembly.GetManifestResourceStream(resourceName)!;
            using var reader = new StreamReader(stream);
            var razorCode = reader.ReadToEnd();

            // Resource name format: Hedin.UI.Demo.Pages.Components.AppBar.AppBar.razor
            var withoutExtension = resourceName[..^".razor".Length];
            var componentName = withoutExtension.Split('.').Last();

            var matches = MyRegex().Matches(razorCode);

            var usedComponents = matches
                                 .Select(m => m.Groups["tag"].Value)
                                 .Distinct()
                                 .ToList();

            return new ComponentExampleDto
            {
                ComponentName = componentName,
                RazorCode = razorCode,
                RelativePath = resourceName,
                UsedComponents = usedComponents
            };
        }).ToList();
    });

    public static List<ComponentExampleDto> GetAllExamples() => CachedExamples.Value;

    public static List<ComponentExampleDto> GetExamplesByComponent(string componentName)
    {
        return CachedExamples.Value
            .Where(x =>
                x.ComponentName.Contains(componentName, StringComparison.OrdinalIgnoreCase) ||
                x.UsedComponents.Any(u =>
                    u.Contains(componentName, StringComparison.OrdinalIgnoreCase)))
            .ToList();
    }

    public static List<string> ListComponentNames()
    {
        return CachedExamples.Value
            .SelectMany(e => e.UsedComponents)
            .Distinct()
            .OrderBy(x => x)
            .ToList();
    }

    [GeneratedRegex(@"<(?<tag>HUI[A-Za-z0-9]+)")]
    private static partial Regex MyRegex();
}