using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace Hedin.UI.Demo.Services;

public class SeoService
{
    private readonly NavigationManager _navigationManager;

    public SeoService(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    public SeoMetadata GetDefaultSeoData()
    {
        return new SeoMetadata
        {
            Title = "Hedin UI - Modern Blazor Component Library",
            Description = "A comprehensive Blazor component library built on MudBlazor, providing enterprise-ready UI components for modern web applications.",
            Keywords = "blazor, components, ui library, mudblazor, enterprise, web development, dotnet",
            Image = $"{_navigationManager.BaseUri}HedinUI.svg",
            Url = _navigationManager.Uri,
            SiteName = "Hedin UI",
            Type = "website",
            TwitterCard = "summary_large_image",
            TwitterSite = "@hedin",
            Author = "Hedin",
            Robots = "index, follow"
        };
    }

    public SeoMetadata GetComponentSeoData(string componentName, string? description = null)
    {
        var defaults = GetDefaultSeoData();
        return new SeoMetadata
        {
            Title = $"{componentName} - Hedin UI Component Library",
            Description = description ?? $"Learn how to use the {componentName} component in Hedin UI. View examples, API documentation, and implementation guides.",
            Keywords = $"{componentName.ToLower()}, blazor component, hedin ui, {defaults.Keywords}",
            Image = defaults.Image,
            Url = _navigationManager.Uri,
            SiteName = defaults.SiteName,
            Type = "article",
            TwitterCard = defaults.TwitterCard,
            TwitterSite = defaults.TwitterSite,
            Author = defaults.Author,
            Robots = defaults.Robots
        };
    }

    public SeoMetadata GetPageSeoData(string title, string description, string? keywords = null)
    {
        var defaults = GetDefaultSeoData();
        return new SeoMetadata
        {
            Title = $"{title} - Hedin UI",
            Description = description,
            Keywords = keywords ?? defaults.Keywords,
            Image = defaults.Image,
            Url = _navigationManager.Uri,
            SiteName = defaults.SiteName,
            Type = "article",
            TwitterCard = defaults.TwitterCard,
            TwitterSite = defaults.TwitterSite,
            Author = defaults.Author,
            Robots = defaults.Robots
        };
    }

    public string GenerateStructuredData(SeoMetadata seoData)
    {
        var structuredData = new
        {
            context = "https://schema.org",
            type = "WebSite",
            name = seoData.SiteName,
            description = seoData.Description,
            url = seoData.Url,
            author = new
            {
                type = "Organization",
                name = seoData.Author
            },
            publisher = new
            {
                type = "Organization",
                name = seoData.Author,
                logo = new
                {
                    type = "ImageObject",
                    url = seoData.Image
                }
            }
        };

        return JsonSerializer.Serialize(structuredData, new JsonSerializerOptions 
        { 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        });
    }

    public string GenerateArticleStructuredData(SeoMetadata seoData, DateTime? publishedDate = null, DateTime? modifiedDate = null)
    {
        var structuredData = new
        {
            context = "https://schema.org",
            type = "Article",
            headline = seoData.Title,
            description = seoData.Description,
            url = seoData.Url,
            image = seoData.Image,
            datePublished = publishedDate?.ToString("yyyy-MM-ddTHH:mm:ssZ") ?? DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
            dateModified = modifiedDate?.ToString("yyyy-MM-ddTHH:mm:ssZ") ?? DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
            author = new
            {
                type = "Organization",
                name = seoData.Author
            },
            publisher = new
            {
                type = "Organization",
                name = seoData.Author,
                logo = new
                {
                    type = "ImageObject",
                    url = seoData.Image
                }
            }
        };

        return JsonSerializer.Serialize(structuredData, new JsonSerializerOptions 
        { 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        });
    }
}

public class SeoMetadata
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Keywords { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string SiteName { get; set; } = string.Empty;
    public string Type { get; set; } = "website";
    public string TwitterCard { get; set; } = "summary_large_image";
    public string TwitterSite { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Robots { get; set; } = "index, follow";
    public string? CanonicalUrl { get; set; }
}