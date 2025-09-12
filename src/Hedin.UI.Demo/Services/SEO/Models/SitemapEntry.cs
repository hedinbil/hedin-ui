namespace Hedin.UI.Demo.Services.SEO.Models;

/// <summary>
/// Represents an entry in the sitemap.xml
/// </summary>
public record SitemapEntry
{
    /// <summary>
    /// The URL of the page
    /// </summary>
    public string Url { get; init; } = string.Empty;

    /// <summary>
    /// Last modified date
    /// </summary>
    public DateTime? LastModified { get; init; }

    /// <summary>
    /// Change frequency (always, hourly, daily, weekly, monthly, yearly, never)
    /// </summary>
    public string? ChangeFrequency { get; init; }

    /// <summary>
    /// Priority (0.0 to 1.0)
    /// </summary>
    public double Priority { get; init; } = 0.5;
}