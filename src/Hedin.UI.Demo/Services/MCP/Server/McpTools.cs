using Microsoft.AspNetCore.Components;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Reflection;

namespace Hedin.UI.Demo.Services.MCP.Server;

[McpServerToolType]
public class HedinUiMcpTools
{
    [McpServerTool, Description("Returns all .razor example files included in the demo site.")]
    public Task<List<ComponentExampleDto>> GetComponentExamples()
        => Task.FromResult(HedinUiExampleTools.GetAllExamples());

    [McpServerTool, Description("Returns example code for a specific component by name.")]
    public Task<List<ComponentExampleDto>> GetExamplesByComponent(string componentName)
        => Task.FromResult(HedinUiExampleTools.GetExamplesByComponent(componentName));

    [McpServerTool, Description("Lists the names of all components that have example files.")]
    public Task<List<string>> ListComponentNames()
        => Task.FromResult(HedinUiExampleTools.ListComponentNames());

    [McpServerTool, Description("Returns component documentation from Markdown or Razor comments.")]
    public Task<List<ComponentDocDto>> GetComponentDocs()
        => DocumentationExtractor.GetAllDocs();
}



public record UiComponentDto
{
    public string Name { get; set; }
    public string Namespace { get; set; }
    public List<ComponentParameterDto> Parameters { get; set; }
}

public record ComponentParameterDto
{
    public string Name { get; set; }
    public string Type { get; set; }
}

public record ComponentExampleDto
{
    public string ComponentName { get; set; }
    public string RazorCode { get; set; }
    public string RelativePath { get; set; }
    public List<string> UsedComponents { get; set; } = new();
}

public record ComponentDocDto
{
    public string FileName { get; set; }
    public string MarkdownText { get; set; }
}
