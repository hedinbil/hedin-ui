using Microsoft.AspNetCore.Mvc;
using Hedin.UI.Demo.Services;

namespace Hedin.UI.Demo.Controllers;

[ApiController]
public class SeoController : ControllerBase
{
    private readonly SitemapService _sitemapService;

    public SeoController(SitemapService sitemapService)
    {
        _sitemapService = sitemapService;
    }

    [HttpGet("sitemap.xml")]
    public IActionResult GetSitemap()
    {
        var sitemapXml = _sitemapService.GenerateSitemapXml();
        return Content(sitemapXml, "application/xml");
    }

    [HttpGet("robots.txt")]
    public IActionResult GetRobots()
    {
        var robotsTxt = _sitemapService.GenerateRobotsTxt();
        return Content(robotsTxt, "text/plain");
    }
}
