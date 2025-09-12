using Hedin.UI.Demo.Services.SEO;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Hedin.UI.Demo.Controllers;

/// <summary>
/// Controller for serving sitemap.xml
/// </summary>
[ApiController]
[Route("")]
public class SitemapController : ControllerBase
{
    private readonly ISitemapService _sitemapService;
    private readonly ILogger<SitemapController> _logger;

    public SitemapController(ISitemapService sitemapService, ILogger<SitemapController> logger)
    {
        _sitemapService = sitemapService;
        _logger = logger;
    }

    /// <summary>
    /// Serves the dynamic sitemap.xml
    /// </summary>
    /// <returns>XML sitemap</returns>
    [HttpGet("sitemap.xml")]
    [Produces("application/xml")]
    [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Client)] // Cache for 1 hour
    public async Task<IActionResult> GetSitemap()
    {
        try
        {
            _logger.LogInformation("Generating sitemap.xml");
            
            var sitemapXml = await _sitemapService.GenerateSitemapXmlAsync();
            
            return Content(sitemapXml, "application/xml", Encoding.UTF8);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating sitemap.xml");
            return StatusCode(500, "Error generating sitemap");
        }
    }

    /// <summary>
    /// Serves robots.txt with sitemap reference
    /// </summary>
    /// <returns>robots.txt content</returns>
    [HttpGet("robots.txt")]
    [Produces("text/plain")]
    [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Client)] // Cache for 24 hours
    public IActionResult GetRobots()
    {
        try
        {
            _logger.LogInformation("Serving robots.txt");
            
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var robotsTxt = GenerateRobotsTxt(baseUrl);
            
            return Content(robotsTxt, "text/plain", Encoding.UTF8);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error serving robots.txt");
            return StatusCode(500, "Error serving robots.txt");
        }
    }

    private static string GenerateRobotsTxt(string baseUrl)
    {
        var robots = new StringBuilder();
        
        // Allow all robots to access everything by default
        robots.AppendLine("User-agent: *");
        robots.AppendLine("Allow: /");
        robots.AppendLine();
        
        // Disallow common areas that shouldn't be indexed
        robots.AppendLine("# Disallow specific paths that shouldn't be indexed");
        robots.AppendLine("Disallow: /api/");
        robots.AppendLine("Disallow: /_framework/");
        robots.AppendLine("Disallow: /_content/");
        robots.AppendLine("Disallow: /css/");
        robots.AppendLine("Disallow: /js/");
        robots.AppendLine("Disallow: /lib/");
        robots.AppendLine();
        
        // Add sitemap reference
        robots.AppendLine($"Sitemap: {baseUrl}/sitemap.xml");
        robots.AppendLine();
        
        // Add crawl delay to be respectful
        robots.AppendLine("# Crawl-delay: 1");
        robots.AppendLine();
        
        // Add information about the site
        robots.AppendLine("# Hedin UI - Modern Blazor Component Library");
        robots.AppendLine("# https://hedin-ui.hedinit.io/");
        
        return robots.ToString();
    }
}