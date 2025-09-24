using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml;
using Hedin.UI.Services;
using Hedin.UI.Demo.Services;

namespace Hedin.UI.Demo.Controllers;

[ApiController]
public class SitemapController : ControllerBase
{
    private readonly IHUIPageHelper _pageHelper;
    private readonly ComponentCatalogService _componentCatalog;
    private readonly ILogger<SitemapController> _logger;

    public SitemapController(
        IHUIPageHelper pageHelper, 
        ComponentCatalogService componentCatalog,
        ILogger<SitemapController> logger)
    {
        _pageHelper = pageHelper;
        _componentCatalog = componentCatalog;
        _logger = logger;
    }

    [HttpGet("sitemap.xml")]
    [Produces("application/xml")]
    public async Task<IActionResult> GetSitemap()
    {
        try
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var sitemap = await GenerateSitemap(baseUrl);
            
            return Content(sitemap, "application/xml", Encoding.UTF8);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating sitemap");
            return StatusCode(500, "Error generating sitemap");
        }
    }

    private async Task<string> GenerateSitemap(string baseUrl)
    {
        var settings = new XmlWriterSettings
        {
            Indent = true,
            Encoding = Encoding.UTF8,
            OmitXmlDeclaration = false
        };

        using var stream = new MemoryStream();
        using var writer = XmlWriter.Create(stream, settings);

        writer.WriteStartDocument();
        writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

        // Get all routable components using the existing HUIPageHelper
        var assemblies = new[] { 
            typeof(Program).Assembly, // Demo assembly
            typeof(HUIPage).Assembly  // UI assembly
        };
        
        var menuItems = await _pageHelper.GetMenuItems(assemblies);
        var allUrls = ExtractAllUrls(menuItems);

        // Add main pages with high priority
        await AddMainPages(writer, baseUrl);

        // Add component pages
        await AddComponentPages(writer, baseUrl, allUrls);

        // Add component catalog pages
        await AddCatalogPages(writer, baseUrl);

        writer.WriteEndElement(); // urlset
        writer.WriteEndDocument();
        writer.Flush();

        return Encoding.UTF8.GetString(stream.ToArray());
    }

    private async Task AddMainPages(XmlWriter writer, string baseUrl)
    {
        var mainPages = new[]
        {
            new { Url = "/", Priority = "1.0", ChangeFreq = "weekly" },
            new { Url = "/getting-started", Priority = "0.9", ChangeFreq = "monthly" },
            new { Url = "/components", Priority = "0.9", ChangeFreq = "weekly" },
            new { Url = "/guidelines", Priority = "0.7", ChangeFreq = "monthly" },
            new { Url = "/why-open-source", Priority = "0.6", ChangeFreq = "monthly" }
        };

        foreach (var page in mainPages)
        {
            await WriteUrlElement(writer, baseUrl, page.Url, page.Priority, page.ChangeFreq, DateTime.UtcNow);
        }
    }

    private async Task AddComponentPages(XmlWriter writer, string baseUrl, List<string> urls)
    {
        foreach (var url in urls.Where(u => !string.IsNullOrEmpty(u)))
        {
            var priority = GetPriority(url);
            var changeFreq = GetChangeFrequency(url);
            
            await WriteUrlElement(writer, baseUrl, url, priority, changeFreq, DateTime.UtcNow);
        }
    }

    private async Task AddCatalogPages(XmlWriter writer, string baseUrl)
    {
        var componentSummaries = _componentCatalog.GetSummaries();
        
        foreach (var summary in componentSummaries)
        {
            var priority = "0.8"; // Component pages are important
            var changeFreq = "weekly"; // Components may be updated regularly
            
            await WriteUrlElement(writer, baseUrl, summary.Url, priority, changeFreq, DateTime.UtcNow);
        }
    }

    private async Task WriteUrlElement(XmlWriter writer, string baseUrl, string url, string priority, string changeFreq, DateTime lastMod)
    {
        writer.WriteStartElement("url");
        
        writer.WriteElementString("loc", $"{baseUrl}{url}");
        writer.WriteElementString("lastmod", lastMod.ToString("yyyy-MM-ddTHH:mm:ssZ"));
        writer.WriteElementString("changefreq", changeFreq);
        writer.WriteElementString("priority", priority);
        
        writer.WriteEndElement();
    }

    private List<string> ExtractAllUrls(List<HUIMenuItem> menuItems)
    {
        var urls = new List<string>();
        
        foreach (var item in menuItems)
        {
            if (!string.IsNullOrEmpty(item.Url))
            {
                urls.Add(item.Url);
            }
            
            if (item.SubItems.Any())
            {
                urls.AddRange(ExtractAllUrls(item.SubItems));
            }
        }
        
        return urls.Distinct().ToList();
    }

    private string GetPriority(string url)
    {
        if (url == "/") return "1.0";
        if (url.Contains("/getting-started")) return "0.9";
        if (url.Contains("/components")) return "0.8";
        if (url.Contains("/guidelines")) return "0.7";
        if (url.Contains("/examples")) return "0.6";
        
        return "0.5"; // Default priority
    }

    private string GetChangeFrequency(string url)
    {
        if (url == "/") return "weekly";
        if (url.Contains("/getting-started")) return "monthly";
        if (url.Contains("/components")) return "weekly";
        if (url.Contains("/guidelines")) return "monthly";
        if (url.Contains("/examples")) return "weekly";
        
        return "monthly"; // Default frequency
    }
}