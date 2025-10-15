namespace Hedin.UI.Demo.Helpers;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class SeoMetadataAttribute : Attribute
{
    public string Title { get; set; } = "Hedin UI - Build Blazor Apps in Minutes";
    public string Description { get; set; } = "Hedin UI gives you a polished suite of MudBlazor-based components, themes, and patterns so you can ship UIs faster.";
    public string Keywords { get; set; } = "Blazor, MudBlazor, UI Components, C#, .NET, Web Development, Hedin UI";
    public string Type { get; set; } = "website";
}

