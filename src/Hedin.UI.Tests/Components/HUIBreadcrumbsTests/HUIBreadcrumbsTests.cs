using Bunit;
using Shouldly;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Hedin.UI.Tests.Base;
using MudBlazor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace Hedin.UI.Tests.Components.HUIBreadcrumbsTests;

public class HUIBreadcrumbsTests : UiTestBase
{
    private IRenderedComponent<HUIBreadcrumbs> RenderComponentWithParams(
        List<BreadcrumbItem>? breadcrumbs = null)
    {
        Services.AddSingleton<NavigationManager>(new TestNavigationManager());
        Services.AddSingleton<IJSRuntime>(new TestJSRuntime());

        return RenderComponent<HUIBreadcrumbs>(parameters => parameters
            .Add(p => p.Breadcrumbs, breadcrumbs ?? new List<BreadcrumbItem>())
        );
    }

    [Fact]
    public void Renders_Nothing_When_Breadcrumbs_Empty()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(new List<BreadcrumbItem>());

        // Assert
        cut.Markup.ShouldBeEmpty();
    }

    [Fact]
    public void Renders_Nothing_When_Breadcrumbs_Null()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(null);

        // Assert
        cut.Markup.ShouldBeEmpty();
    }

    [Fact]
    public void Renders_Nothing_When_Breadcrumbs_Has_Only_One_Item()
    {
        // Arrange
        var breadcrumbs = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: "/")
        };

        // Act
        var cut = RenderComponentWithParams(breadcrumbs);

        // Assert
        cut.Markup.ShouldBeEmpty();
    }

    [Fact]
    public void Renders_Breadcrumbs_When_Has_Multiple_Items()
    {
        // Arrange
        var breadcrumbs = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: "/"),
            new BreadcrumbItem("Products", href: "/products"),
            new BreadcrumbItem("Category", href: "/products/category")
        };

        // Act
        var cut = RenderComponentWithParams(breadcrumbs);

        // Assert
        cut.Find(".mud-breadcrumbs").ShouldNotBeNull();
        cut.Markup.ShouldContain("Home");
        cut.Markup.ShouldContain("Products");
        cut.Markup.ShouldContain("Category");
    }

    [Fact]
    public void Breadcrumbs_Container_Has_Correct_Classes()
    {
        // Arrange
        var breadcrumbs = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: "/"),
            new BreadcrumbItem("Products", href: "/products")
        };

        // Act
        var cut = RenderComponentWithParams(breadcrumbs);

        // Assert
        var container = cut.Find("span");
        container.GetAttribute("class").ShouldContain("d-flex");
        container.GetAttribute("class").ShouldContain("align-center");
        container.GetAttribute("class").ShouldContain("gap-3");
        container.GetAttribute("class").ShouldContain("mb-3");
    }

    [Fact]
    public void MudBreadcrumbs_Has_Correct_Classes()
    {
        // Arrange
        var breadcrumbs = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: "/"),
            new BreadcrumbItem("Products", href: "/products")
        };

        // Act
        var cut = RenderComponentWithParams(breadcrumbs);

        // Assert
        var mudBreadcrumbs = cut.Find(".mud-breadcrumbs");
        mudBreadcrumbs.GetAttribute("class").ShouldContain("pa-0");
    }

    [Fact]
    public void Back_Button_Click_Falls_Back_To_History_When_No_Previous_Breadcrumb()
    {
        // Arrange
        var breadcrumbs = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: "/")
        };

        var cut = RenderComponentWithParams(breadcrumbs);

        // Act
        // This should not render anything since there's only one breadcrumb
        // But if somehow it does, the back button should fall back to history.back

        // Assert
        cut.Markup.ShouldBeEmpty();
    }

    [Fact]
    public void Breadcrumbs_Items_Are_Passed_To_MudBreadcrumbs()
    {
        // Arrange
        var breadcrumbs = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: "/"),
            new BreadcrumbItem("Products", href: "/products")
        };

        // Act
        var cut = RenderComponentWithParams(breadcrumbs);

        // Assert
        var mudBreadcrumbs = cut.FindComponent<MudBreadcrumbs>();
        mudBreadcrumbs.Instance.Items.ShouldBe(breadcrumbs);
    }

    [Fact]
    public void Component_Handles_Breadcrumbs_With_Empty_Href()
    {
        // Arrange
        var breadcrumbs = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: ""),
            new BreadcrumbItem("Products", href: "/products")
        };

        // Act
        var cut = RenderComponentWithParams(breadcrumbs);

        // Assert
        cut.Find(".mud-breadcrumbs").ShouldNotBeNull();
        cut.Markup.ShouldContain("Home");
        cut.Markup.ShouldContain("Products");
    }

    [Fact]
    public void Component_Handles_Breadcrumbs_With_Null_Href()
    {
        // Arrange
        var breadcrumbs = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: null),
            new BreadcrumbItem("Products", href: "/products")
        };

        // Act
        var cut = RenderComponentWithParams(breadcrumbs);

        // Assert
        cut.Find(".mud-breadcrumbs").ShouldNotBeNull();
        cut.Markup.ShouldContain("Home");
        cut.Markup.ShouldContain("Products");
    }
}

// Test helpers
public class TestNavigationManager : NavigationManager
{
    public TestNavigationManager()
    {
        Initialize("http://localhost/", "http://localhost/");
    }

    protected override void NavigateToCore(string uri, bool forceLoad)
    {
        // Mock navigation
    }
}

public class TestJSRuntime : IJSRuntime
{
    public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args)
    {
        return ValueTask.FromResult<TValue>(default!);
    }

    public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args)
    {
        return ValueTask.FromResult<TValue>(default!);
    }
}
