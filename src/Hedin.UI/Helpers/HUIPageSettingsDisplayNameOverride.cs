namespace Hedin.UI.Helpers
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class HUIPageSettingsDisplayNameOverride : Attribute
    {
        public string DisplayName { get; }
        public List<string> Brands { get; }

        public HUIPageSettingsDisplayNameOverride(string displayName, string brands)
        {
            DisplayName = displayName;
            Brands = brands.Split(",", StringSplitOptions.TrimEntries).ToList();
        }
    }
}