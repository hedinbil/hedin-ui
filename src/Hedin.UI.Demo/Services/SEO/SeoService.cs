using Hedin.UI.Demo.Services.SEO.Models;
using Microsoft.AspNetCore.Components;
using System.Text;

namespace Hedin.UI.Demo.Services.SEO;

/// <summary>
/// Implementation of SEO service for managing meta tags and page data
/// </summary>
public class SeoService : ISeoService
{
    private SeoPageData _currentSeoData;
    private readonly SeoPageData _defaultSeoData;
    private readonly NavigationManager _navigationManager;

    /// <summary>
    /// Event triggered when SEO data changes
    /// </summary>
    public event Action? SeoDataChanged;

    public SeoService(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        
        // Default SEO configuration for Hedin UI
        _defaultSeoData = new SeoPageData
        {
            Title = "Hedin UI - Modern Blazor Component Library",
            Description = "A comprehensive Blazor component library built by Hedin IT. Features modern UI components, themes, and tools for building professional web applications.",
            Keywords = "Blazor, UI Components, .NET, C#, Web Development, Component Library, Hedin IT, MudBlazor",
            Author = "Hedin IT",
            Type = "website",
            Language = "en",
            Robots = "index,follow",
            ChangeFrequency = "weekly",
            Priority = 0.8,
            OpenGraph = new OpenGraphData
            {
                SiteName = "Hedin UI",
                Type = "website",
                Locale = "en_US"
            },
            TwitterCard = new TwitterCardData
            {
                Card = "summary_large_image",
                Site = "@HedinIT"
            }
        };

        _currentSeoData = _defaultSeoData;
    }

    public void SetPageSeoData(SeoPageData seoData)
    {
        // Merge with defaults for any null values
        _currentSeoData = new SeoPageData
        {
            Title = seoData.Title ?? _defaultSeoData.Title,
            Description = seoData.Description ?? _defaultSeoData.Description,
            Keywords = seoData.Keywords ?? _defaultSeoData.Keywords,
            CanonicalUrl = seoData.CanonicalUrl ?? GetCurrentUrl(),
            ImageUrl = seoData.ImageUrl ?? _defaultSeoData.ImageUrl,
            Author = seoData.Author ?? _defaultSeoData.Author,
            Type = seoData.Type ?? _defaultSeoData.Type,
            Language = seoData.Language ?? _defaultSeoData.Language,
            Robots = seoData.Robots ?? _defaultSeoData.Robots,
            LastModified = seoData.LastModified ?? DateTime.UtcNow,
            ChangeFrequency = seoData.ChangeFrequency ?? _defaultSeoData.ChangeFrequency,
            Priority = seoData.Priority != 0 ? seoData.Priority : _defaultSeoData.Priority,
            OpenGraph = MergeOpenGraphData(seoData.OpenGraph),
            TwitterCard = MergeTwitterCardData(seoData.TwitterCard),
            CustomMetaTags = seoData.CustomMetaTags ?? new Dictionary<string, string>()
        };

        SeoDataChanged?.Invoke();
    }

    public SeoPageData GetCurrentSeoData() => _currentSeoData;

    public string GenerateMetaTagsHtml()
    {
        var html = new StringBuilder();
        var currentUrl = GetCurrentUrl();
        var data = _currentSeoData;

        // Basic meta tags
        if (!string.IsNullOrEmpty(data.Description))
            html.AppendLine($"    <meta name=\"description\" content=\"{EscapeHtml(data.Description)}\" />");
        
        if (!string.IsNullOrEmpty(data.Keywords))
            html.AppendLine($"    <meta name=\"keywords\" content=\"{EscapeHtml(data.Keywords)}\" />");
        
        if (!string.IsNullOrEmpty(data.Author))
            html.AppendLine($"    <meta name=\"author\" content=\"{EscapeHtml(data.Author)}\" />");
        
        if (!string.IsNullOrEmpty(data.Robots))
            html.AppendLine($"    <meta name=\"robots\" content=\"{data.Robots}\" />");

        // Canonical URL
        var canonicalUrl = data.CanonicalUrl ?? currentUrl;
        html.AppendLine($"    <link rel=\"canonical\" href=\"{EscapeHtml(canonicalUrl)}\" />");

        // Open Graph tags
        if (data.OpenGraph != null)
        {
            var og = data.OpenGraph;
            if (!string.IsNullOrEmpty(og.Title ?? data.Title))
                html.AppendLine($"    <meta property=\"og:title\" content=\"{EscapeHtml(og.Title ?? data.Title!)}\" />");
            
            if (!string.IsNullOrEmpty(og.Description ?? data.Description))
                html.AppendLine($"    <meta property=\"og:description\" content=\"{EscapeHtml(og.Description ?? data.Description!)}\" />");
            
            html.AppendLine($"    <meta property=\"og:type\" content=\"{og.Type}\" />");
            html.AppendLine($"    <meta property=\"og:url\" content=\"{EscapeHtml(og.Url ?? currentUrl)}\" />");
            
            if (!string.IsNullOrEmpty(og.SiteName))
                html.AppendLine($"    <meta property=\"og:site_name\" content=\"{EscapeHtml(og.SiteName)}\" />");
            
            if (!string.IsNullOrEmpty(og.Image ?? data.ImageUrl))
            {
                html.AppendLine($"    <meta property=\"og:image\" content=\"{EscapeHtml(og.Image ?? data.ImageUrl!)}\" />");
                
                if (og.ImageWidth.HasValue)
                    html.AppendLine($"    <meta property=\"og:image:width\" content=\"{og.ImageWidth}\" />");
                
                if (og.ImageHeight.HasValue)
                    html.AppendLine($"    <meta property=\"og:image:height\" content=\"{og.ImageHeight}\" />");
                
                if (!string.IsNullOrEmpty(og.ImageType))
                    html.AppendLine($"    <meta property=\"og:image:type\" content=\"{og.ImageType}\" />");
                
                if (!string.IsNullOrEmpty(og.ImageAlt))
                    html.AppendLine($"    <meta property=\"og:image:alt\" content=\"{EscapeHtml(og.ImageAlt)}\" />");
            }
            
            html.AppendLine($"    <meta property=\"og:locale\" content=\"{og.Locale}\" />");
        }

        // Twitter Card tags
        if (data.TwitterCard != null)
        {
            var twitter = data.TwitterCard;
            html.AppendLine($"    <meta name=\"twitter:card\" content=\"{twitter.Card}\" />");
            
            if (!string.IsNullOrEmpty(twitter.Title ?? data.Title))
                html.AppendLine($"    <meta name=\"twitter:title\" content=\"{EscapeHtml(twitter.Title ?? data.Title!)}\" />");
            
            if (!string.IsNullOrEmpty(twitter.Description ?? data.Description))
                html.AppendLine($"    <meta name=\"twitter:description\" content=\"{EscapeHtml(twitter.Description ?? data.Description!)}\" />");
            
            if (!string.IsNullOrEmpty(twitter.Image ?? data.ImageUrl))
                html.AppendLine($"    <meta name=\"twitter:image\" content=\"{EscapeHtml(twitter.Image ?? data.ImageUrl!)}\" />");
            
            if (!string.IsNullOrEmpty(twitter.ImageAlt))
                html.AppendLine($"    <meta name=\"twitter:image:alt\" content=\"{EscapeHtml(twitter.ImageAlt)}\" />");
            
            if (!string.IsNullOrEmpty(twitter.Site))
                html.AppendLine($"    <meta name=\"twitter:site\" content=\"{twitter.Site}\" />");
            
            if (!string.IsNullOrEmpty(twitter.Creator))
                html.AppendLine($"    <meta name=\"twitter:creator\" content=\"{twitter.Creator}\" />");
        }

        // Custom meta tags
        if (data.CustomMetaTags?.Any() == true)
        {
            foreach (var (name, content) in data.CustomMetaTags)
            {
                html.AppendLine($"    <meta name=\"{EscapeHtml(name)}\" content=\"{EscapeHtml(content)}\" />");
            }
        }

        return html.ToString();
    }

    public void SetTitle(string title)
    {
        SetPageSeoData(_currentSeoData with { Title = title });
    }

    public void SetDescription(string description)
    {
        SetPageSeoData(_currentSeoData with { Description = description });
    }

    public void SetKeywords(string keywords)
    {
        SetPageSeoData(_currentSeoData with { Keywords = keywords });
    }

    public void SetCanonicalUrl(string url)
    {
        SetPageSeoData(_currentSeoData with { CanonicalUrl = url });
    }

    public void SetImage(string imageUrl, string? imageAlt = null)
    {
        var openGraph = _currentSeoData.OpenGraph with { Image = imageUrl, ImageAlt = imageAlt };
        var twitterCard = _currentSeoData.TwitterCard with { Image = imageUrl, ImageAlt = imageAlt };
        
        SetPageSeoData(_currentSeoData with 
        { 
            ImageUrl = imageUrl, 
            OpenGraph = openGraph,
            TwitterCard = twitterCard
        });
    }

    public void SetOpenGraphData(OpenGraphData openGraphData)
    {
        SetPageSeoData(_currentSeoData with { OpenGraph = openGraphData });
    }

    public void SetTwitterCardData(TwitterCardData twitterCardData)
    {
        SetPageSeoData(_currentSeoData with { TwitterCard = twitterCardData });
    }

    public void ResetToDefaults()
    {
        _currentSeoData = _defaultSeoData;
        SeoDataChanged?.Invoke();
    }

    public string GetCurrentTitle()
    {
        return _currentSeoData.Title ?? _defaultSeoData.Title ?? "Hedin UI";
    }

    private string GetCurrentUrl()
    {
        return _navigationManager.Uri;
    }

    private OpenGraphData MergeOpenGraphData(OpenGraphData? provided)
    {
        var defaultOg = _defaultSeoData.OpenGraph!;
        if (provided == null) return defaultOg;

        return new OpenGraphData
        {
            Title = provided.Title,
            Description = provided.Description,
            Type = provided.Type ?? defaultOg.Type,
            Image = provided.Image,
            Url = provided.Url,
            SiteName = provided.SiteName ?? defaultOg.SiteName,
            Locale = provided.Locale ?? defaultOg.Locale,
            ImageWidth = provided.ImageWidth,
            ImageHeight = provided.ImageHeight,
            ImageType = provided.ImageType,
            ImageAlt = provided.ImageAlt
        };
    }

    private TwitterCardData MergeTwitterCardData(TwitterCardData? provided)
    {
        var defaultTwitter = _defaultSeoData.TwitterCard!;
        if (provided == null) return defaultTwitter;

        return new TwitterCardData
        {
            Card = provided.Card ?? defaultTwitter.Card,
            Title = provided.Title,
            Description = provided.Description,
            Image = provided.Image,
            ImageAlt = provided.ImageAlt,
            Site = provided.Site ?? defaultTwitter.Site,
            Creator = provided.Creator
        };
    }

    private static string EscapeHtml(string input)
    {
        return input.Replace("&", "&amp;")
                   .Replace("<", "&lt;")
                   .Replace(">", "&gt;")
                   .Replace("\"", "&quot;")
                   .Replace("'", "&#39;");
    }
}