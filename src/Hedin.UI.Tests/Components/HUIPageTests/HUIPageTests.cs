using Bunit;
using Hedin.UI.Services;
using Hedin.UI.Tests.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MudBlazor;

namespace Hedin.UI.Tests.Components.HUIPageTests;

public class HUIPageTests : UiTestBase
{
    private IRenderedComponent<HUIPage> RenderComponentWithParams(
        string header = "Test Header",
        string? pageDescription = null,
        string? pageInfo = null,
        List<BreadcrumbItem>? breadcrumbs = null,
        RenderFragment? buttons = null,
        RenderFragment? titleSuffixContent = null,
        bool alignButtonsCenter = true,
        string? addButtonText = null,
        EventCallback? onAddClick = null,
        RenderFragment? childContent = null,
        string? appTitle = null)
    {
        Services.AddSingleton<IOptions<TitleService>>(Options.Create(new TitleService { AppTitle = appTitle ?? "" }));

        return RenderComponent<HUIPage>(parameters => parameters
            .Add(p => p.Header, header)
            .Add(p => p.PageDescription, pageDescription)
            .Add(p => p.PageInfo, pageInfo)
            .Add(p => p.Breadcrumbs, breadcrumbs)
            .Add(p => p.Buttons, buttons)
            .Add(p => p.TitleSuffixContent, titleSuffixContent)
            .Add(p => p.AlignButtonsCenter, alignButtonsCenter)
            .Add(p => p.AddButtonText, addButtonText)
            .Add(p => p.OnAddClick, onAddClick ?? EventCallback.Factory.Create(this, () => { }))
            .Add(p => p.ChildContent, childContent)
        );
    }

    [Fact]
    public void Renders_Header_And_PageTitle()
    {
        // Arrange & Act
        var comp = RenderComponentWithParams(header: "My Page", appTitle: "App");

        // Assert
        comp.Markup.Contains("My Page");
        comp.Markup.Contains("My Page – App");
    }

    [Fact]
    public void Renders_PageDescription_When_Provided()
    {
        var comp = RenderComponentWithParams(pageDescription: "This is a description");
        Assert.Contains("This is a description", comp.Markup);
    }

    [Fact]
    public void Does_Not_Render_PageDescription_When_Null()
    {
        var comp = RenderComponentWithParams(pageDescription: null);
        Assert.DoesNotContain("<p>", comp.Markup);
    }

    //[Fact]
    //public void Renders_PageInfo_Tooltip_When_Provided()
    //{
    //    var comp = RenderComponentWithParams(pageInfo: "Info text");
    //    Assert.Contains("Info text", comp.Markup);
    //    Assert.Contains("HUITooltip", comp.Markup);
    //}

    [Fact]
    public void Does_Not_Render_PageInfo_Tooltip_When_Null()
    {
        var comp = RenderComponentWithParams(pageInfo: null);
        Assert.DoesNotContain("HUITooltip", comp.Markup);
    }

    [Fact]
    public void Renders_Breadcrumbs_When_Provided()
    {
        var breadcrumbs = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: "/"),
            new BreadcrumbItem("Page", href: "/page")
        };
        var comp = RenderComponentWithParams(breadcrumbs: breadcrumbs);
        Assert.Contains("breadcrumbs", comp.Markup);
    }

    [Fact]
    public void Renders_Buttons_And_TitleSuffixContent()
    {
        var comp = RenderComponentWithParams(
            buttons: builder => builder.AddContent(0, "ButtonContent"),
            titleSuffixContent: builder => builder.AddContent(0, "SuffixContent")
        );
        Assert.Contains("ButtonContent", comp.Markup);
        Assert.Contains("SuffixContent", comp.Markup);
    }

    [Fact]
    public void Renders_ChildContent()
    {
        var comp = RenderComponentWithParams(
            childContent: builder => builder.AddContent(0, "ChildContent")
        );
        Assert.Contains("ChildContent", comp.Markup);
    }

    [Fact]
    public void Renders_AddButton_When_AddButtonText_Provided()
    {
        var comp = RenderComponentWithParams(addButtonText: "Add Something");
        Assert.Contains("Add Something", comp.Markup);
        Assert.Contains("<button", comp.Markup);
    }

    [Fact]
    public async Task Invokes_OnAddClick_When_AddButton_Clicked()
    {
        bool clicked = false;
        var comp = RenderComponentWithParams(
            addButtonText: "Add",
            onAddClick: EventCallback.Factory.Create(this, () => clicked = true)
        );

        var button = comp.FindComponent<HUIButton>();
        await button.InvokeAsync(() => button.Instance.OnClick.InvokeAsync());

        Assert.True(clicked);
    }

    [Fact]
    public void Buttons_Are_Centered_When_AlignButtonsCenter_True()
    {
        var comp = RenderComponentWithParams(alignButtonsCenter: true);
        Assert.Contains("align-self-center", comp.Markup);
    }

    [Fact]
    public void Buttons_Are_Not_Centered_When_AlignButtonsCenter_False()
    {
        var comp = RenderComponentWithParams(alignButtonsCenter: false);
        Assert.DoesNotContain("align-self-center", comp.Markup);
    }
}
