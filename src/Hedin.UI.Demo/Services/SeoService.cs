using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace Hedin.UI.Demo.Services;

public class SeoService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private SeoMetadata _currentMetadata = new();

    public SeoService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public SeoMetadata CurrentMetadata => _currentMetadata;

    public void SetMetadata(SeoMetadata metadata)
    {
        _currentMetadata = metadata;
    }

    public void SetMetadata(string? title = null, string? description = null, string? keywords = null, 
        string? image = null, string? url = null, string? type = "website")
    {
        _currentMetadata = new SeoMetadata
        {
            Title = title ?? _currentMetadata.Title,
            Description = description ?? _currentMetadata.Description,
            Keywords = keywords ?? _currentMetadata.Keywords,
            Image = image ?? _currentMetadata.Image,
            Url = url ?? GetCurrentUrl(),
            Type = type
        };
    }

    public string GetCurrentUrl()
    {
        var request = _httpContextAccessor.HttpContext?.Request;
        if (request == null) return string.Empty;

        var scheme = request.Scheme;
        var host = request.Host;
        var path = request.Path;
        var query = request.QueryString;

        // Ensure we have a valid URL
        var url = $"{scheme}://{host}{path}{query}";
        return !string.IsNullOrEmpty(url) ? url : GetBaseUrl();
    }

    public string GetBaseUrl()
    {
        var request = _httpContextAccessor.HttpContext?.Request;
        if (request == null) return string.Empty;

        var scheme = request.Scheme;
        var host = request.Host;

        return $"{scheme}://{host}";
    }

    public List<RouteInfo> GetAllRoutableComponents()
    {
        var routes = new List<RouteInfo>();
        var assembly = Assembly.GetExecutingAssembly();
        
        var routableTypes = assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(ComponentBase)) && 
                       t.GetCustomAttributes<RouteAttribute>().Any())
            .ToList();

        foreach (var type in routableTypes)
        {
            var routeAttributes = type.GetCustomAttributes<RouteAttribute>();
            foreach (var routeAttr in routeAttributes)
            {
                routes.Add(new RouteInfo
                {
                    Type = type,
                    Route = routeAttr.Template,
                    Name = type.Name,
                    Description = GetComponentDescription(type)
                });
            }
        }

        return routes;
    }

    private string GetComponentDescription(Type componentType)
    {
        // Try to get description from XML documentation or attributes
        var descriptionAttr = componentType.GetCustomAttribute<System.ComponentModel.DescriptionAttribute>();
        if (descriptionAttr != null)
            return descriptionAttr.Description;

        // Generate a generic description - pages should set their own specific descriptions
        return $"Hedin UI {componentType.Name} - Component documentation and examples";
    }
}

public class SeoMetadata
{
    public string Title { get; set; } = "Hedin UI - Build Blazor Apps in Minutes";
    public string Description { get; set; } = "Hedin UI gives you a polished suite of MudBlazor-based components, themes, and patterns so you can ship UIs faster.";
    public string Keywords { get; set; } = "Blazor, MudBlazor, UI Components, C#, .NET, Web Development, Hedin UI";
    public string Image { get; set; } = "/HedinUI.svg";
    public string Url { get; set; } = "";
    public string Type { get; set; } = "website";
    public string Author { get; set; } = "Hedin IT";
    public string SiteName { get; set; } = "Hedin UI";
    public string Locale { get; set; } = "en_US";
}

public class RouteInfo
{
    public Type Type { get; set; } = null!;
    public string Route { get; set; } = "";
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
}
