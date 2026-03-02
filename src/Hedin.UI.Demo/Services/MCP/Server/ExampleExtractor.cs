using System.Reflection;

namespace Hedin.UI.Demo.Services.MCP.Server;

public static class ExampleExtractor
{
    public static Task<List<ComponentExampleDto>> GetAllExamples()
    {
        var assembly = typeof(ExampleExtractor).Assembly;
        var resourceNames = assembly.GetManifestResourceNames()
            .Where(n => n.EndsWith(".razor", StringComparison.OrdinalIgnoreCase))
            .ToList();

        var result = resourceNames.Select(resourceName =>
        {
            using var stream = assembly.GetManifestResourceStream(resourceName)!;
            using var reader = new StreamReader(stream);
            var withoutExtension = resourceName[..^".razor".Length];

            return new ComponentExampleDto
            {
                ComponentName = withoutExtension.Split('.').Last(),
                RazorCode = reader.ReadToEnd(),
                RelativePath = resourceName
            };
        }).ToList();

        return Task.FromResult(result);
    }
}