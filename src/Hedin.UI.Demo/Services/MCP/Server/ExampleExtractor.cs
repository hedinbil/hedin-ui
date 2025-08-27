namespace Hedin.UI.Demo.Services.MCP.Server;

public static class ExampleExtractor
{
    public static Task<List<ComponentExampleDto>> GetAllExamples()
    {
        var allRazorFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.razor", SearchOption.AllDirectories);

        var result = allRazorFiles.Select(path => new ComponentExampleDto
        {
            ComponentName = Path.GetFileNameWithoutExtension(path),
            RazorCode = File.ReadAllText(path),
            RelativePath = Path.GetRelativePath(AppContext.BaseDirectory, path)
        }).ToList();

        return Task.FromResult(result);
    }
}