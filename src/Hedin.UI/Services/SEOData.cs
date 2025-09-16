using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hedin.UI.Services
{
    /// <summary>
    /// Represents SEO metadata for a page
    /// </summary>
    public class SEOData
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Keywords { get; set; } = string.Empty;
        public string? Author { get; set; }
        public string? Canonical { get; set; }
        public string? Image { get; set; }
        public string? SiteName { get; set; }
        public string? Type { get; set; } = "website";
        
        // Open Graph specific
        public string? OGTitle { get; set; }
        public string? OGDescription { get; set; }
        public string? OGImage { get; set; }
        public string? OGUrl { get; set; }
        public string? OGType { get; set; } = "website";
        public string? OGSiteName { get; set; }
        
        // Twitter Card specific
        public string? TwitterCard { get; set; } = "summary_large_image";
        public string? TwitterSite { get; set; }
        public string? TwitterCreator { get; set; }
        public string? TwitterTitle { get; set; }
        public string? TwitterDescription { get; set; }
        public string? TwitterImage { get; set; }
        
        // Additional meta tags
        public Dictionary<string, string> AdditionalMetaTags { get; set; } = new();
        
        /// <summary>
        /// Gets the effective title (uses OGTitle if available, otherwise Title)
        /// </summary>
        public string GetEffectiveTitle() => !string.IsNullOrEmpty(OGTitle) ? OGTitle : Title;
        
        /// <summary>
        /// Gets the effective description (uses OGDescription if available, otherwise Description)
        /// </summary>
        public string GetEffectiveDescription() => !string.IsNullOrEmpty(OGDescription) ? OGDescription : Description;
        
        /// <summary>
        /// Gets the effective image (uses OGImage if available, otherwise Image)
        /// </summary>
        public string? GetEffectiveImage() => !string.IsNullOrEmpty(OGImage) ? OGImage : Image;
        
        /// <summary>
        /// Creates SEO data from basic parameters
        /// </summary>
        public static SEOData Create(string title, string description, string? keywords = null, string? image = null)
        {
            return new SEOData
            {
                Title = title,
                Description = description,
                Keywords = keywords ?? string.Empty,
                Image = image,
                OGTitle = title,
                OGDescription = description,
                OGImage = image,
                TwitterTitle = title,
                TwitterDescription = description,
                TwitterImage = image
            };
        }
        
        /// <summary>
        /// Creates a copy of the SEO data
        /// </summary>
        public SEOData Clone()
        {
            return new SEOData
            {
                Title = Title,
                Description = Description,
                Keywords = Keywords,
                Author = Author,
                Canonical = Canonical,
                Image = Image,
                SiteName = SiteName,
                Type = Type,
                OGTitle = OGTitle,
                OGDescription = OGDescription,
                OGImage = OGImage,
                OGUrl = OGUrl,
                OGType = OGType,
                OGSiteName = OGSiteName,
                TwitterCard = TwitterCard,
                TwitterSite = TwitterSite,
                TwitterCreator = TwitterCreator,
                TwitterTitle = TwitterTitle,
                TwitterDescription = TwitterDescription,
                TwitterImage = TwitterImage,
                AdditionalMetaTags = new Dictionary<string, string>(AdditionalMetaTags)
            };
        }
    }
}