namespace Hedin.UI.Demo.Services.SEO.Models;

/// <summary>
/// Open Graph meta data for social media sharing
/// </summary>
public record OpenGraphData
{
    /// <summary>
    /// The title of your object as it should appear within the graph
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// A one to two sentence description of your object
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// The type of your object (website, article, etc.)
    /// </summary>
    public string? Type { get; init; } = "website";

    /// <summary>
    /// An image URL which should represent your object within the graph
    /// </summary>
    public string? Image { get; init; }

    /// <summary>
    /// The canonical URL of your object that will be used as its permanent ID
    /// </summary>
    public string? Url { get; init; }

    /// <summary>
    /// The name of your site (not the title of your page)
    /// </summary>
    public string? SiteName { get; init; }

    /// <summary>
    /// The locale these tags are marked up in
    /// </summary>
    public string? Locale { get; init; } = "en_US";

    /// <summary>
    /// Width of the image in pixels
    /// </summary>
    public int? ImageWidth { get; init; }

    /// <summary>
    /// Height of the image in pixels
    /// </summary>
    public int? ImageHeight { get; init; }

    /// <summary>
    /// MIME type of the image
    /// </summary>
    public string? ImageType { get; init; }

    /// <summary>
    /// Alt text for the image
    /// </summary>
    public string? ImageAlt { get; init; }
}