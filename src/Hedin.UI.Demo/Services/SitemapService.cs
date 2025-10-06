using System.Text;
using System.Xml.Linq;
using Microsoft.AspNetCore.Components;

namespace Hedin.UI.Demo.Services;

public class SitemapService
{
    private readonly SeoService _seoService;

    public SitemapService(SeoService seoService)
    {
        _seoService = seoService;
    }

    public string GenerateSitemapXml()
    {
        var baseUrl = _seoService.GetBaseUrl();
        var routes = _seoService.GetAllRoutableComponents();
        
        var xNamespace = XNamespace.Get("http://www.sitemaps.org/schemas/sitemap/0.9");
        var xDoc = new XDocument(
            new XDeclaration("1.0", "utf-8", "yes"),
            new XElement(xNamespace + "urlset",
                from route in routes
                select new XElement(xNamespace + "url",
                    new XElement(xNamespace + "loc", $"{baseUrl}{route.Route}"),
                    new XElement(xNamespace + "lastmod", DateTime.UtcNow.ToString("yyyy-MM-dd")),
                    new XElement(xNamespace + "changefreq", GetChangeFrequency(route)),
                    new XElement(xNamespace + "priority", GetPriority(route))
                )
            )
        );

        return xDoc.ToString();
    }

    private string GetChangeFrequency(RouteInfo route)
    {
        return "weekly";
    }

    private string GetPriority(RouteInfo route)
    {
        return route.Name switch
        {
            "Index" => "1.0",
            "GettingStarted" => "0.9",
            "Guidelines" => "0.8",
            _ when route.Name.Contains("Example") => "0.7",
            _ => "0.6"
        };
    }

    public string GenerateRobotsTxt()
    {
        var baseUrl = _seoService.GetBaseUrl();
        
        var robotsContent = new StringBuilder();
        robotsContent.AppendLine("User-agent: *");
        robotsContent.AppendLine("Allow: /");
        robotsContent.AppendLine("Disallow: /mcp/");
        robotsContent.AppendLine("Disallow: /Error");
        robotsContent.AppendLine();
        robotsContent.AppendLine($"Sitemap: {baseUrl}/sitemap.xml");
        
        return robotsContent.ToString();
    }
}
