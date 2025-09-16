using MudBlazor;

namespace Hedin.UI
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class HUIPageSettingsAttribute : Attribute
    {
        public string DisplayName { get; }
        public int Order { get; }
        public string? Icon { get; }
        
        // SEO properties
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Keywords { get; set; }
        public string? Image { get; set; }
        
        public HUIPageSettingsAttribute(string displayName, int order = 99999, string? icon = "")
        {
            DisplayName = displayName;
            Order = order;
            Icon = icon;
        }
        
        /// <summary>
        /// Creates SEO data from this attribute
        /// </summary>
        public SEOData? ToSEOData()
        {
            if (string.IsNullOrEmpty(Title) && string.IsNullOrEmpty(Description))
                return null;
                
            return Services.SEOData.Create(
                Title ?? DisplayName,
                Description ?? $"Learn about the {DisplayName} component in Hedin UI - examples, usage, and API documentation.",
                Keywords,
                Image
            );
        }
    }

    public class HUIMenuItem
    {
        public string DisplayName { get; set; }
        public string Url { get; set; }
        public HUIMenuItem? Parent { get; set; } // Change type to HUIMenuItem?
        public string? ParentUrl { get; set; }
        public List<HUIMenuItem> SubItems { get; set; } = new List<HUIMenuItem>();
        public bool IsExpanded { get; set; }
        public string Policy { get; set; }
        public int Order { get; set; }
        public bool HasExpandedChild { get; set; }

        public string? Tooltip { get; set; }
        public string? BadgeData { get; set; }
        public Color BadgeColor { get; set; }

        public Severity? Dot { get; set; }

        public string? Icon { get; set; }
        
        public HUIMenuItem(
            string displayName, 
            string url, 
            string? parentUrl, 
            string policy = "", 
            int order = 0,
            string? icon = null,
            string? tooltip = null,
            string? badgeData = null, 
            Color badgeColor = Color.Default,
            Severity? dot = null)
        {
            DisplayName = displayName;
            Url = url;
            ParentUrl = parentUrl;
            Policy = policy;
            Order = order;
            Icon = icon;
            Tooltip = tooltip;
            BadgeData = badgeData;
            BadgeColor = badgeColor;
            Dot = dot;
        }
    }
}
