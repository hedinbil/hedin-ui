using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Hedin.UI.Demo.Services.SEO;

/// <summary>
/// Service for dynamically updating HTML head content for SEO
/// </summary>
public class SeoHeadContentService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly ISeoService _seoService;

    public SeoHeadContentService(IJSRuntime jsRuntime, ISeoService seoService)
    {
        _jsRuntime = jsRuntime;
        _seoService = seoService;
        
        // Subscribe to SEO data changes
        _seoService.SeoDataChanged += UpdateHeadContent;
    }

    /// <summary>
    /// Updates the HTML head content with current SEO data
    /// </summary>
    public async Task UpdateHeadContentAsync()
    {
        try
        {
            var seoData = _seoService.GetCurrentSeoData();
            
            // Update page title
            if (!string.IsNullOrEmpty(seoData.Title))
            {
                await _jsRuntime.InvokeVoidAsync("document.title = arguments[0]", seoData.Title);
            }

            // Update meta description
            if (!string.IsNullOrEmpty(seoData.Description))
            {
                await UpdateMetaTag("description", seoData.Description);
            }

            // Update meta keywords
            if (!string.IsNullOrEmpty(seoData.Keywords))
            {
                await UpdateMetaTag("keywords", seoData.Keywords);
            }

            // Update canonical URL
            if (!string.IsNullOrEmpty(seoData.CanonicalUrl))
            {
                await UpdateLinkTag("canonical", seoData.CanonicalUrl);
            }

            // Update Open Graph tags
            if (seoData.OpenGraph != null)
            {
                var og = seoData.OpenGraph;
                if (!string.IsNullOrEmpty(og.Title ?? seoData.Title))
                    await UpdateMetaProperty("og:title", og.Title ?? seoData.Title!);
                
                if (!string.IsNullOrEmpty(og.Description ?? seoData.Description))
                    await UpdateMetaProperty("og:description", og.Description ?? seoData.Description!);
                
                await UpdateMetaProperty("og:type", og.Type ?? "website");
                
                if (!string.IsNullOrEmpty(og.Image ?? seoData.ImageUrl))
                    await UpdateMetaProperty("og:image", og.Image ?? seoData.ImageUrl!);
                
                if (!string.IsNullOrEmpty(og.Url ?? seoData.CanonicalUrl))
                    await UpdateMetaProperty("og:url", og.Url ?? seoData.CanonicalUrl!);
            }

            // Update Twitter Card tags
            if (seoData.TwitterCard != null)
            {
                var twitter = seoData.TwitterCard;
                await UpdateMetaTag("twitter:card", twitter.Card);
                
                if (!string.IsNullOrEmpty(twitter.Title ?? seoData.Title))
                    await UpdateMetaTag("twitter:title", twitter.Title ?? seoData.Title!);
                
                if (!string.IsNullOrEmpty(twitter.Description ?? seoData.Description))
                    await UpdateMetaTag("twitter:description", twitter.Description ?? seoData.Description!);
                
                if (!string.IsNullOrEmpty(twitter.Image ?? seoData.ImageUrl))
                    await UpdateMetaTag("twitter:image", twitter.Image ?? seoData.ImageUrl!);
            }
        }
        catch (Exception)
        {
            // Silently ignore JS interop errors (e.g., during prerendering)
        }
    }

    private async Task UpdateMetaTag(string name, string content)
    {
        var script = $@"
            var meta = document.querySelector('meta[name=""{name}""]');
            if (meta) {{
                meta.content = '{EscapeJavaScript(content)}';
            }} else {{
                meta = document.createElement('meta');
                meta.name = '{name}';
                meta.content = '{EscapeJavaScript(content)}';
                document.head.appendChild(meta);
            }}";
        
        await _jsRuntime.InvokeVoidAsync("eval", script);
    }

    private async Task UpdateMetaProperty(string property, string content)
    {
        var script = $@"
            var meta = document.querySelector('meta[property=""{property}""]');
            if (meta) {{
                meta.content = '{EscapeJavaScript(content)}';
            }} else {{
                meta = document.createElement('meta');
                meta.setAttribute('property', '{property}');
                meta.content = '{EscapeJavaScript(content)}';
                document.head.appendChild(meta);
            }}";
        
        await _jsRuntime.InvokeVoidAsync("eval", script);
    }

    private async Task UpdateLinkTag(string rel, string href)
    {
        var script = $@"
            var link = document.querySelector('link[rel=""{rel}""]');
            if (link) {{
                link.href = '{EscapeJavaScript(href)}';
            }} else {{
                link = document.createElement('link');
                link.rel = '{rel}';
                link.href = '{EscapeJavaScript(href)}';
                document.head.appendChild(link);
            }}";
        
        await _jsRuntime.InvokeVoidAsync("eval", script);
    }

    private void UpdateHeadContent()
    {
        // Fire and forget - don't await to avoid blocking
        Task.Run(async () => await UpdateHeadContentAsync());
    }

    private static string EscapeJavaScript(string input)
    {
        return input.Replace("'", "\\'")
                   .Replace("\"", "\\\"")
                   .Replace("\r", "\\r")
                   .Replace("\n", "\\n");
    }
}