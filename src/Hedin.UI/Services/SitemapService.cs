using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Hedin.UI.Services
{
    /// <summary>
    /// Service for generating dynamic sitemap.xml using reflection
    /// </summary>
    public class SitemapService : ISitemapService
    {
        private readonly IHUIPageHelper _pageHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TitleService _titleService;
        
        public SitemapService(
            IHUIPageHelper pageHelper, 
            IHttpContextAccessor httpContextAccessor,
            IOptions<TitleService> titleService)
        {
            _pageHelper = pageHelper;
            _httpContextAccessor = httpContextAccessor;
            _titleService = titleService.Value;
        }
        
        public async Task<string> GenerateSitemapXmlAsync()
        {
            var entries = await GetSitemapEntriesAsync();
            
            var sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");
            
            foreach (var entry in entries)
            {
                sb.AppendLine("  <url>");
                sb.AppendLine($"    <loc>{entry.Url}</loc>");
                
                if (entry.LastModified.HasValue)
                {
                    sb.AppendLine($"    <lastmod>{entry.LastModified.Value:yyyy-MM-dd}</lastmod>");
                }
                
                if (entry.ChangeFrequency.HasValue)
                {
                    sb.AppendLine($"    <changefreq>{entry.ChangeFrequency.Value.ToString().ToLower()}</changefreq>");
                }
                
                if (entry.Priority.HasValue)
                {
                    sb.AppendLine($"    <priority>{entry.Priority.Value:F1}</priority>");
                }
                
                sb.AppendLine("  </url>");
            }
            
            sb.AppendLine("</urlset>");
            return sb.ToString();
        }
        
        public async Task<List<SitemapEntry>> GetSitemapEntriesAsync()
        {
            var entries = new List<SitemapEntry>();
            var baseUrl = GetBaseUrl();
            
            // Get all menu items (which represent routable pages)
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.GetName().Name?.Contains("Hedin.UI") == true)
                .ToArray();
            var menuItems = await _pageHelper.GetMenuItems(assemblies);
            
            // Add main pages with higher priority
            AddMainPageEntries(entries, baseUrl);
            
            // Add discovered component/demo pages
            AddMenuItemEntries(entries, baseUrl, menuItems);
            
            return entries.OrderBy(e => e.Priority).ThenBy(e => e.Url).ToList();
        }
        
        private void AddMainPageEntries(List<SitemapEntry> entries, string baseUrl)
        {
            // Home page - highest priority
            entries.Add(new SitemapEntry(
                baseUrl, 
                DateTime.Now, 
                SitemapChangeFrequency.Weekly, 
                1.0));
            
            // Main sections - high priority
            entries.Add(new SitemapEntry(
                $"{baseUrl}/getting-started", 
                DateTime.Now, 
                SitemapChangeFrequency.Monthly, 
                0.9));
                
            entries.Add(new SitemapEntry(
                $"{baseUrl}/components", 
                DateTime.Now, 
                SitemapChangeFrequency.Weekly, 
                0.8));
                
            entries.Add(new SitemapEntry(
                $"{baseUrl}/guidelines", 
                DateTime.Now, 
                SitemapChangeFrequency.Monthly, 
                0.7));
        }
        
        private void AddMenuItemEntries(List<SitemapEntry> entries, string baseUrl, List<HUIMenuItem> menuItems)
        {
            foreach (var menuItem in menuItems)
            {
                AddMenuItemEntry(entries, baseUrl, menuItem);
            }
        }
        
        private void AddMenuItemEntry(List<SitemapEntry> entries, string baseUrl, HUIMenuItem menuItem)
        {
            if (!string.IsNullOrEmpty(menuItem.Url))
            {
                var fullUrl = $"{baseUrl}{menuItem.Url}";
                
                // Avoid duplicates
                if (entries.Any(e => e.Url == fullUrl))
                    return;
                
                // Determine priority based on URL depth and type
                var priority = CalculatePriority(menuItem.Url);
                var changeFreq = DetermineChangeFrequency(menuItem.Url);
                
                entries.Add(new SitemapEntry(
                    fullUrl,
                    DateTime.Now,
                    changeFreq,
                    priority));
            }
            
            // Process sub-items recursively
            foreach (var subItem in menuItem.SubItems)
            {
                AddMenuItemEntry(entries, baseUrl, subItem);
            }
        }
        
        private double CalculatePriority(string url)
        {
            // Home page gets highest priority (handled separately)
            if (url == "/" || string.IsNullOrEmpty(url))
                return 1.0;
            
            // Count URL segments to determine depth
            var segments = url.Split('/', StringSplitOptions.RemoveEmptyEntries);
            
            return segments.Length switch
            {
                1 => 0.8,  // Top-level sections like /components
                2 => 0.6,  // Component categories like /components/buttons
                3 => 0.4,  // Specific components like /components/buttons/button
                _ => 0.3   // Deep nested pages
            };
        }
        
        private SitemapChangeFrequency DetermineChangeFrequency(string url)
        {
            if (string.IsNullOrEmpty(url) || url == "/")
                return SitemapChangeFrequency.Weekly;
            
            if (url.Contains("/components/"))
                return SitemapChangeFrequency.Monthly;
            
            if (url.Contains("/getting-started") || url.Contains("/guidelines"))
                return SitemapChangeFrequency.Monthly;
            
            return SitemapChangeFrequency.Weekly;
        }
        
        private string GetBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null)
                return "https://hedin-ui.hedinit.io"; // Fallback URL
            
            var scheme = request.Scheme;
            var host = request.Host.Value;
            
            return $"{scheme}://{host}";
        }
    }
}