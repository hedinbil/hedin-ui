using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace Hedin.UI.Demo.Services.MCP.Server;

public static class ComponentExtractor
{
    public static List<UiComponentDto> GetAllComponents()
    {
        var assembly = typeof(HUIDialog).Assembly;

        var components = assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(ComponentBase)))
            .Select(t => new UiComponentDto
            {
                Name = t.Name,
                Namespace = t.Namespace,
                Parameters = t.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                              .Where(p => p.CustomAttributes.Any(a => a.AttributeType.Name == "ParameterAttribute"))
                              .Select(p => new ComponentParameterDto
                              {
                                  Name = p.Name,
                                  Type = p.PropertyType.Name
                              }).ToList()
            })
            .ToList();

        return components;
    }
}