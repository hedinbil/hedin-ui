using Hedin.UI.Demo.Services.SEO.Models;
using Microsoft.AspNetCore.Components;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace Hedin.UI.Demo.Services.SEO;

/// <summary>
/// Implementation of sitemap generation service
/// </summary>
public class SitemapService : ISitemapService
{
    private readonly ILogger<SitemapService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly List<SitemapEntry> _customEntries = new();
    
    // Base URL - will be determined at runtime
    private string? _baseUrl;

    public SitemapService(ILogger<SitemapService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task<string> GenerateSitemapXmlAsync()
    {
        try
        {
            var entries = await GetSitemapEntriesAsync();
            
            var sitemap = new XDocument(
                new XDeclaration("1.0", "UTF-8", null),
                new XElement("urlset",
                    new XAttribute("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9"),
                    entries.Select(CreateUrlElement)
                )
            );

            return sitemap.ToString();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating sitemap XML");
            throw;
        }
    }

    public async Task<IEnumerable<SitemapEntry>> GetSitemapEntriesAsync()
    {
        var entries = new List<SitemapEntry>();
        
        try
        {
            // Get base URL
            var baseUrl = await GetBaseUrlAsync();
            
            // Discover routes from assemblies
            var discoveredRoutes = DiscoverRoutes();
            
            foreach (var route in discoveredRoutes)
            {
                entries.Add(new SitemapEntry
                {
                    Url = $"{baseUrl.TrimEnd('/')}{route.Url}",
                    LastModified = route.LastModified ?? DateTime.UtcNow,
                    ChangeFrequency = route.ChangeFrequency ?? "weekly",
                    Priority = route.Priority > 0 ? route.Priority : GetRoutePriority(route.Url)
                });
            }

            // Add custom entries
            entries.AddRange(_customEntries.Select(e => e with 
            { 
                Url = e.Url.StartsWith("http") ? e.Url : $"{baseUrl.TrimEnd('/')}{e.Url}" 
            }));

            return entries.OrderByDescending(e => e.Priority).ThenBy(e => e.Url);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting sitemap entries");
            return entries;
        }
    }

    public void AddCustomEntry(SitemapEntry entry)
    {
        _customEntries.RemoveAll(e => e.Url == entry.Url);
        _customEntries.Add(entry);
    }

    public void RemoveCustomEntry(string url)
    {
        _customEntries.RemoveAll(e => e.Url == url);
    }

    public void ClearCustomEntries()
    {
        _customEntries.Clear();
    }

    private async Task<string> GetBaseUrlAsync()
    {
        if (_baseUrl != null) return _baseUrl;

        try
        {
            // Try to get from HttpContext
            using var scope = _serviceProvider.CreateScope();
            var httpContextAccessor = scope.ServiceProvider.GetService<IHttpContextAccessor>();
            
            if (httpContextAccessor?.HttpContext != null)
            {
                var request = httpContextAccessor.HttpContext.Request;
                _baseUrl = $"{request.Scheme}://{request.Host}";
                return _baseUrl;
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Could not determine base URL from HttpContext");
        }

        // Fallback to configuration or default
        _baseUrl = "https://hedin-ui.hedinit.io"; // Default from project configuration
        return _baseUrl;
    }

    private IEnumerable<SitemapEntry> DiscoverRoutes()
    {
        var routes = new List<SitemapEntry>();
        
        try
        {
            // Get all assemblies that might contain Blazor pages
            var assemblies = new[]
            {
                typeof(App).Assembly, // Current demo assembly
                Assembly.GetExecutingAssembly()
            }.Distinct();

            foreach (var assembly in assemblies)
            {
                var componentTypes = assembly.GetTypes()
                    .Where(type => type.IsClass && !type.IsAbstract)
                    .Where(type => typeof(ComponentBase).IsAssignableFrom(type))
                    .ToList();

                foreach (var componentType in componentTypes)
                {
                    var routeAttributes = componentType.GetCustomAttributes<RouteAttribute>();
                    
                    foreach (var routeAttribute in routeAttributes)
                    {
                        if (!string.IsNullOrEmpty(routeAttribute.Template))
                        {
                            var route = routeAttribute.Template;
                            
                            // Skip routes with parameters for now (e.g., /user/{id})
                            if (route.Contains('{')) continue;
                            
                            // Get additional metadata from component
                            var lastModified = GetComponentLastModified(componentType);
                            var changeFreq = GetComponentChangeFrequency(componentType);
                            
                            routes.Add(new SitemapEntry
                            {
                                Url = route.StartsWith('/') ? route : $"/{route}",
                                LastModified = lastModified,
                                ChangeFrequency = changeFreq,
                                Priority = GetRoutePriority(route)
                            });
                        }
                    }
                }
            }

            // Add some standard routes
            AddStandardRoutes(routes);
            
            _logger.LogInformation($"Discovered {routes.Count} routes for sitemap");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error discovering routes");
        }

        return routes.DistinctBy(r => r.Url);
    }

    private void AddStandardRoutes(List<SitemapEntry> routes)
    {
        // Add home page if not already present
        if (!routes.Any(r => r.Url == "/" || r.Url == ""))
        {
            routes.Add(new SitemapEntry
            {
                Url = "/",
                LastModified = DateTime.UtcNow,
                ChangeFrequency = "daily",
                Priority = 1.0
            });
        }
    }

    private DateTime GetComponentLastModified(Type componentType)
    {
        try
        {
            // Try to get file modification time
            var assemblyLocation = componentType.Assembly.Location;
            if (File.Exists(assemblyLocation))
            {
                return File.GetLastWriteTimeUtc(assemblyLocation);
            }
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex, $"Could not get last modified time for {componentType.Name}");
        }

        return DateTime.UtcNow.Date; // Default to today
    }

    private string GetComponentChangeFrequency(Type componentType)
    {
        // You could add custom attributes to components to specify this
        var name = componentType.Name.ToLowerInvariant();
        
        return name switch
        {
            var n when n.Contains("index") || n.Contains("home") => "daily",
            var n when n.Contains("component") || n.Contains("example") => "weekly",
            var n when n.Contains("guideline") || n.Contains("doc") => "monthly",
            _ => "weekly"
        };
    }

    private double GetRoutePriority(string route)
    {
        // Assign priority based on route importance
        return route.ToLowerInvariant() switch
        {
            "/" => 1.0,
            "/components" => 0.9,
            "/getting-started" => 0.9,
            var r when r.StartsWith("/components/") => 0.8,
            var r when r.Contains("/example") => 0.6,
            var r when r.Contains("/demo") => 0.5,
            _ => 0.5
        };
    }

    private XElement CreateUrlElement(SitemapEntry entry)
    {
        var urlElement = new XElement("url",
            new XElement("loc", entry.Url)
        );

        if (entry.LastModified.HasValue)
        {
            urlElement.Add(new XElement("lastmod", entry.LastModified.Value.ToString("yyyy-MM-dd")));
        }

        if (!string.IsNullOrEmpty(entry.ChangeFrequency))
        {
            urlElement.Add(new XElement("changefreq", entry.ChangeFrequency));
        }

        if (entry.Priority > 0)
        {
            urlElement.Add(new XElement("priority", entry.Priority.ToString("F1")));
        }

        return urlElement;
    }
}