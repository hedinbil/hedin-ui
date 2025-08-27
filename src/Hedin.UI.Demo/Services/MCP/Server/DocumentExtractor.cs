namespace Hedin.UI.Demo.Services.MCP.Server;

public static class DocumentationExtractor
{
    public static Task<List<ComponentDocDto>> GetAllDocs()
    {
        var basePath = AppContext.BaseDirectory;

        var allMarkdownFiles = Directory.GetFiles(basePath, "*.md", SearchOption.AllDirectories);

        var result = allMarkdownFiles.Select(path =>
        {
            var markdownText = File.ReadAllText(path);
            return new ComponentDocDto
            {
                FileName = Path.GetFileNameWithoutExtension(path),
                MarkdownText = markdownText
            };
        }).ToList();

        return Task.FromResult(result);
    }
}
