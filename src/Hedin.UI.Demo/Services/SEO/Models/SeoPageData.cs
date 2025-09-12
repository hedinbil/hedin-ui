namespace Hedin.UI.Demo.Services.SEO.Models;

/// <summary>
/// Represents comprehensive SEO data for a page
/// </summary>
public record SeoPageData
{
    /// <summary>
    /// Page title (will be used in &lt;title&gt; tag and og:title)
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// Page description for meta description and og:description
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Keywords for the page (comma-separated)
    /// </summary>
    public string? Keywords { get; init; }

    /// <summary>
    /// Canonical URL for the page
    /// </summary>
    public string? CanonicalUrl { get; init; }

    /// <summary>
    /// Image URL for social media sharing
    /// </summary>
    public string? ImageUrl { get; init; }

    /// <summary>
    /// Author of the page/content
    /// </summary>
    public string? Author { get; init; }

    /// <summary>
    /// Page type (article, website, product, etc.)
    /// </summary>
    public string? Type { get; init; } = "website";

    /// <summary>
    /// Language of the content
    /// </summary>
    public string? Language { get; init; } = "en";

    /// <summary>
    /// Robots directive (index,follow / noindex,nofollow, etc.)
    /// </summary>
    public string? Robots { get; init; } = "index,follow";

    /// <summary>
    /// Open Graph specific data
    /// </summary>
    public OpenGraphData? OpenGraph { get; init; }

    /// <summary>
    /// Twitter Card specific data
    /// </summary>
    public TwitterCardData? TwitterCard { get; init; }

    /// <summary>
    /// Additional custom meta tags
    /// </summary>
    public Dictionary<string, string>? CustomMetaTags { get; init; }

    /// <summary>
    /// Last modified date for sitemap
    /// </summary>
    public DateTime? LastModified { get; init; }

    /// <summary>
    /// Change frequency for sitemap (always, hourly, daily, weekly, monthly, yearly, never)
    /// </summary>
    public string? ChangeFrequency { get; init; } = "weekly";

    /// <summary>
    /// Priority for sitemap (0.0 to 1.0)
    /// </summary>
    public double Priority { get; init; } = 0.5;
}