namespace Hedin.UI;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class HUIPageSettingsAttribute : Attribute
{
    public string DisplayName { get; }
    public int Order { get; }
    public string? Icon { get; }

    public HUIPageSettingsAttribute(string displayName, int order = 99999, string? icon = "")
    {
        DisplayName = displayName;
        Order = order;
        Icon = icon;
    }
}