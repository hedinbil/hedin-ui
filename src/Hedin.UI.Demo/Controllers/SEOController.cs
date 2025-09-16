using Hedin.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hedin.UI.Demo.Controllers
{
    /// <summary>
    /// Controller for SEO-related endpoints like sitemap.xml and robots.txt
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SEOController : ControllerBase
    {
        private readonly ISitemapService _sitemapService;
        
        public SEOController(ISitemapService sitemapService)
        {
            _sitemapService = sitemapService;
        }
        
        /// <summary>
        /// Returns the dynamically generated sitemap.xml
        /// </summary>
        [HttpGet("sitemap.xml")]
        [ResponseCache(Duration = 3600)] // Cache for 1 hour
        public async Task<IActionResult> GetSitemapXml()
        {
            try
            {
                var sitemapXml = await _sitemapService.GenerateSitemapXmlAsync();
                return Content(sitemapXml, "application/xml");
            }
            catch (Exception ex)
            {
                // Log the error and return a basic sitemap
                Console.WriteLine($"Error generating sitemap: {ex}");
                var fallbackSitemap = GenerateFallbackSitemap();
                return Content(fallbackSitemap, "application/xml");
            }
        }
        
        /// <summary>
        /// Returns the robots.txt file
        /// </summary>
        [HttpGet("robots.txt")]
        [ResponseCache(Duration = 86400)] // Cache for 24 hours
        public IActionResult GetRobotsTxt()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var robotsTxt = $@"User-agent: *
Allow: /

# Sitemap
Sitemap: {baseUrl}/seo/sitemap.xml

# Performance - delay crawling for non-critical paths
Crawl-delay: 1

# Block crawling of certain paths if needed
# Disallow: /api/
# Disallow: /admin/";

            return Content(robotsTxt, "text/plain");
        }
        
        private string GenerateFallbackSitemap()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            
            return $@"<?xml version=""1.0"" encoding=""UTF-8""?>
<urlset xmlns=""http://www.sitemaps.org/schemas/sitemap/0.9"">
  <url>
    <loc>{baseUrl}</loc>
    <lastmod>{DateTime.Now:yyyy-MM-dd}</lastmod>
    <changefreq>weekly</changefreq>
    <priority>1.0</priority>
  </url>
  <url>
    <loc>{baseUrl}/getting-started</loc>
    <lastmod>{DateTime.Now:yyyy-MM-dd}</lastmod>
    <changefreq>monthly</changefreq>
    <priority>0.9</priority>
  </url>
  <url>
    <loc>{baseUrl}/components</loc>
    <lastmod>{DateTime.Now:yyyy-MM-dd}</lastmod>
    <changefreq>weekly</changefreq>
    <priority>0.8</priority>
  </url>
</urlset>";
        }
    }
}