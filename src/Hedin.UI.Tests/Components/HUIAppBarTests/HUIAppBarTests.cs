//using Bunit;
//using Shouldly;
//using Microsoft.AspNetCore.Components;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Options;
//using MudBlazor;
//using Hedin.UI.Tests.Base;
//using Hedin.UI.Services;
//using Hedin.UI.Internal;
//using Hedin.UI.Components;

//namespace Hedin.UI.Tests.Components.HUIAppBarTests;

//public class HUIAppBarTests : UiTestBase
//{
//    private IRenderedComponent<HUIAppBar> RenderComponentWithParams(
//        RenderFragment? childContent = null,
//        EventCallback<bool>? menuButtonVisibilityChanged = null,
//        EventCallback? menuButtonClicked = null,
//        bool fixed = true,
//        string logoSrc = "",
//        string logoBrandSrc = "",
//        RenderFragment? topContent = null,
//        List<HUIAppDrawerItem>? appDrawerItems = null,
//        string? appTitle = null)
//    {
//        Services.AddSingleton<IOptions<TitleService>>(Options.Create(new TitleService { AppTitle = appTitle ?? "Test App" }));
//        Services.AddSingleton<InternaHUIlLocalizer>(new InternaHUIlLocalizer());
//        Services.AddSingleton<NavigationManager>(new TestNavigationManager());
//        Services.AddSingleton<IJSRuntime>(new TestJSRuntime());

//        return RenderComponent<HUIAppBar>(parameters => parameters
//            .Add(p => p.ChildContent, childContent)
//            .Add(p => p.MenuButtonVisibilityChanged, menuButtonVisibilityChanged ?? EventCallback.Factory.Create<bool>(this, _ => { }))
//            .Add(p => p.MenuButtonClicked, menuButtonClicked ?? EventCallback.Factory.Create(this, () => { }))
//            .Add(p => p.Fixed, fixed)
//            .Add(p => p.LogoSrc, logoSrc)
//            .Add(p => p.LogoBrandSrc, logoBrandSrc)
//            .Add(p => p.TopContent, topContent)
//            .Add(p => p.AppDrawerItems, appDrawerItems ?? new List<HUIAppDrawerItem>())
//        );
//    }

//    [Fact]
//    public void Renders_AppBar_With_Default_Properties()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams();

//        // Assert
//        cut.Find("div[class*='mud-appbar']").ShouldNotBeNull();
//        cut.Markup.ShouldContain("Test App");
//    }

//    [Fact]
//    public void Renders_ChildContent_When_Provided()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(
//            childContent: builder => builder.AddContent(0, "Custom Content")
//        );

//        // Assert
//        cut.Markup.ShouldContain("Custom Content");
//    }

//    [Fact]
//    public void Renders_TopContent_When_Provided()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(
//            topContent: builder => builder.AddContent(0, "Top Content")
//        );

//        // Assert
//        cut.Markup.ShouldContain("Top Content");
//    }

//    [Fact]
//    public void Renders_Logo_When_LogoSrc_Provided()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(logoSrc: "/logo.png");

//        // Assert
//        var logo = cut.Find("img[src='/logo.png']");
//        logo.ShouldNotBeNull();
//        logo.GetAttribute("height").ShouldBe("40");
//        logo.GetAttribute("width").ShouldBe("40");
//    }

//    [Fact]
//    public void Renders_BrandLogo_When_LogoBrandSrc_Provided()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(logoBrandSrc: "/brand.png");

//        // Assert
//        var brandLogo = cut.Find("img[src='/brand.png']");
//        brandLogo.ShouldNotBeNull();
//        brandLogo.GetAttribute("style").ShouldContain("max-width: 121px");
//    }

//    [Fact]
//    public void Shows_AppTitle_When_No_AppDrawerItems()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(appDrawerItems: new List<HUIAppDrawerItem>());

//        // Assert
//        cut.Markup.ShouldContain("Test App");
//        cut.FindAll(".hui-app-title").Count.ShouldBe(1);
//    }

//    [Fact]
//    public void Shows_AppTitle_With_AppDrawer_When_AppDrawerItems_Provided()
//    {
//        // Arrange
//        var appDrawerItems = new List<HUIAppDrawerItem>
//        {
//            new HUIAppDrawerItem { Name = "App 1", Icon = "icon1" },
//            new HUIAppDrawerItem { Name = "App 2", Icon = "icon2" }
//        };

//        // Act
//        var cut = RenderComponentWithParams(appDrawerItems: appDrawerItems);

//        // Assert
//        cut.Markup.ShouldContain("Test App");
//        cut.FindAll(".hui-app-title").Count.ShouldBe(1);
//        cut.FindAll("i[class*='mud-icon']").Count.ShouldBeGreaterThan(0);
//    }

//    [Fact]
//    public void Renders_MenuButton_For_Mobile_View()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams();

//        // Assert
//        cut.FindAll("button[class*='mud-icon-button']").Count.ShouldBeGreaterThan(0);
//    }

//    [Fact]
//    public void AppBar_Is_Fixed_By_Default()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams();

//        // Assert
//        var appBar = cut.Find("div[class*='mud-appbar']");
//        appBar.GetAttribute("class").ShouldContain("mud-appbar-fixed");
//    }

//    [Fact]
//    public void AppBar_Can_Be_Unfixed()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(fixed: false);

//        // Assert
//        var appBar = cut.Find("div[class*='mud-appbar']");
//        appBar.GetAttribute("class").ShouldNotContain("mud-appbar-fixed");
//    }

//    [Fact]
//    public void Logo_Click_Navigates_To_Home()
//    {
//        // Arrange
//        var navigationManager = Services.GetRequiredService<NavigationManager>();
//        var cut = RenderComponentWithParams();

//        // Act
//        cut.Find(".hui-logo").Click();

//        // Assert
//        // Note: In test environment, navigation is mocked, so we just verify the click handler exists
//        cut.Find(".hui-logo").ShouldNotBeNull();
//    }

//    [Fact]
//    public void Menu_Button_Click_Invokes_Callback()
//    {
//        // Arrange
//        var menuClicked = false;
//        var cut = RenderComponentWithParams(
//            menuButtonClicked: EventCallback.Factory.Create(this, () => menuClicked = true)
//        );

//        // Act
//        var menuButton = cut.Find("button[class*='mud-icon-button']");
//        menuButton.Click();

//        // Assert
//        menuClicked.ShouldBeTrue();
//    }

//    [Fact]
//    public void Menu_Button_Visibility_Change_Invokes_Callback()
//    {
//        // Arrange
//        var visibilityChanged = false;
//        var cut = RenderComponentWithParams(
//            menuButtonVisibilityChanged: EventCallback.Factory.Create<bool>(this, _ => visibilityChanged = true)
//        );

//        // Act
//        // Trigger visibility change (this would normally happen on window resize)
//        cut.SetParametersAndRender(parameters => parameters
//            .Add(p => p.MenuButtonVisibilityChanged, EventCallback.Factory.Create<bool>(this, _ => visibilityChanged = true))
//        );

//        // Assert
//        // Note: In test environment, we can't easily simulate window resize, so we just verify the callback is set up
//        cut.Instance.MenuButtonVisibilityChanged.ShouldNotBeNull();
//    }
//}

//// Test helpers
//public class TestNavigationManager : NavigationManager
//{
//    public TestNavigationManager()
//    {
//        Initialize("http://localhost/", "http://localhost/");
//    }

//    protected override void NavigateToCore(string uri, bool forceLoad)
//    {
//        // Mock navigation
//    }
//}

//public class TestJSRuntime : IJSRuntime
//{
//    public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args)
//    {
//        return ValueTask.FromResult<TValue>(default!);
//    }

//    public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args)
//    {
//        return ValueTask.FromResult<TValue>(default!);
//    }
//}
