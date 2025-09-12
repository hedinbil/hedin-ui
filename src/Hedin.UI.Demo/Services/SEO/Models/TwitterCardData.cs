namespace Hedin.UI.Demo.Services.SEO.Models;

/// <summary>
/// Twitter Card meta data for Twitter sharing
/// </summary>
public record TwitterCardData
{
    /// <summary>
    /// The card type (summary, summary_large_image, app, player)
    /// </summary>
    public string Card { get; init; } = "summary_large_image";

    /// <summary>
    /// Title of content (max 70 characters)
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// Description of content (max 200 characters)
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// URL of image to show in the card
    /// </summary>
    public string? Image { get; init; }

    /// <summary>
    /// Alt text for the image
    /// </summary>
    public string? ImageAlt { get; init; }

    /// <summary>
    /// Twitter username of website/content creator
    /// </summary>
    public string? Site { get; init; }

    /// <summary>
    /// Twitter username of content creator
    /// </summary>
    public string? Creator { get; init; }
}