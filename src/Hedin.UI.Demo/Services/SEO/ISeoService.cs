using Hedin.UI.Demo.Services.SEO.Models;

namespace Hedin.UI.Demo.Services.SEO;

/// <summary>
/// Service for managing SEO meta tags and page data
/// </summary>
public interface ISeoService
{
    /// <summary>
    /// Sets the current page SEO data
    /// </summary>
    /// <param name="seoData">The SEO data for the current page</param>
    void SetPageSeoData(SeoPageData seoData);

    /// <summary>
    /// Gets the current page SEO data
    /// </summary>
    /// <returns>Current SEO data or default values</returns>
    SeoPageData GetCurrentSeoData();

    /// <summary>
    /// Generates HTML meta tags based on current SEO data
    /// </summary>
    /// <returns>HTML string containing all meta tags</returns>
    string GenerateMetaTagsHtml();

    /// <summary>
    /// Sets just the page title
    /// </summary>
    /// <param name="title">Page title</param>
    void SetTitle(string title);

    /// <summary>
    /// Sets just the page description
    /// </summary>
    /// <param name="description">Page description</param>
    void SetDescription(string description);

    /// <summary>
    /// Sets just the page keywords
    /// </summary>
    /// <param name="keywords">Comma-separated keywords</param>
    void SetKeywords(string keywords);

    /// <summary>
    /// Sets the canonical URL for the page
    /// </summary>
    /// <param name="url">Canonical URL</param>
    void SetCanonicalUrl(string url);

    /// <summary>
    /// Sets the image for social media sharing
    /// </summary>
    /// <param name="imageUrl">Image URL</param>
    /// <param name="imageAlt">Alt text for the image</param>
    void SetImage(string imageUrl, string? imageAlt = null);

    /// <summary>
    /// Sets Open Graph data
    /// </summary>
    /// <param name="openGraphData">Open Graph data</param>
    void SetOpenGraphData(OpenGraphData openGraphData);

    /// <summary>
    /// Sets Twitter Card data
    /// </summary>
    /// <param name="twitterCardData">Twitter Card data</param>
    void SetTwitterCardData(TwitterCardData twitterCardData);

    /// <summary>
    /// Resets SEO data to defaults
    /// </summary>
    void ResetToDefaults();

    /// <summary>
    /// Gets the current page title
    /// </summary>
    /// <returns>Current page title</returns>
    string GetCurrentTitle();

    /// <summary>
    /// Event triggered when SEO data changes
    /// </summary>
    event Action? SeoDataChanged;
}