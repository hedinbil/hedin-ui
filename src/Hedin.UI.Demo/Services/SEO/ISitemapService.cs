using Hedin.UI.Demo.Services.SEO.Models;

namespace Hedin.UI.Demo.Services.SEO;

/// <summary>
/// Service for generating dynamic sitemaps
/// </summary>
public interface ISitemapService
{
    /// <summary>
    /// Generates the complete sitemap as XML string
    /// </summary>
    /// <returns>XML string containing the sitemap</returns>
    Task<string> GenerateSitemapXmlAsync();

    /// <summary>
    /// Gets all discoverable routes in the application
    /// </summary>
    /// <returns>Collection of sitemap entries</returns>
    Task<IEnumerable<SitemapEntry>> GetSitemapEntriesAsync();

    /// <summary>
    /// Adds a custom sitemap entry
    /// </summary>
    /// <param name="entry">The sitemap entry to add</param>
    void AddCustomEntry(SitemapEntry entry);

    /// <summary>
    /// Removes a custom sitemap entry by URL
    /// </summary>
    /// <param name="url">The URL to remove</param>
    void RemoveCustomEntry(string url);

    /// <summary>
    /// Clears all custom entries
    /// </summary>
    void ClearCustomEntries();
}