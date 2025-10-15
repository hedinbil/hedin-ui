namespace Hedin.UI;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class HUIPageSettingsAttribute : Attribute
{
    public string DisplayName { get; }
    public int Order { get; }
    public string? Icon { get; }
    public bool Disabled { get; }
    public string? Id { get; }

    public HUIPageSettingsAttribute(string displayName, int order = 99999, string? icon = "", bool disabled = false, string id = "")
    {
        DisplayName = displayName;
        Order = order;
        Icon = icon;
        Id = id;
    }
}