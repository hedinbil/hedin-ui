using MudBlazor;

namespace Hedin.UI;

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
        
    public bool Disabled { get; set; }
        
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
        Severity? dot = null,
        bool disabled = false)
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
        Disabled = disabled;
    }
}