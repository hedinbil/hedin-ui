using Bunit;
using Shouldly;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Hedin.UI.Tests.Base;
using MudBlazor;
using Microsoft.Extensions.DependencyInjection;

namespace Hedin.UI.Tests.Components.HUINavMenuHorizontalTests;

public class HUINavMenuHorizontalTests : UiTestBase
{
    private IRenderedComponent<HUINavMenuHorizontal> RenderComponentWithParams(
        List<HUIMenuItem>? items = null,
        HUIMenuItem? activeItem = null,
        bool requireAuthorization = true)
    {

        Services.AddSingleton<NavigationManager>(new TestNavigationManager());

        return RenderComponentWithMudProviders<HUINavMenuHorizontal>(parameters => parameters
            .Add(p => p.Items, items ?? new List<HUIMenuItem>())
            .Add(p => p.ActiveItem, activeItem)
            .Add(p => p.RequireAuthorization, requireAuthorization)
        );
    }

    [Fact]
    public void Renders_Nothing_When_No_Items()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(items: new List<HUIMenuItem>());

        // Assert
        cut.Markup.ShouldBeEmpty();
    }

    [Fact]
    public void Renders_Tabs_When_Items_Provided()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null)
        };

        // Act
        var cut = RenderComponentWithParams(items: items);

        // Assert
        cut.Find(".tabs-wrapper").ShouldNotBeNull();
        cut.Find(".mud-tabs").ShouldNotBeNull();
    }

    [Fact]
    public void Renders_Single_Menu_Item()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null)
        };

        // Act
        var cut = RenderComponentWithParams(items: items);

        // Assert
        cut.Markup.ShouldContain("Test Item");
    }

    [Fact]
    public void Renders_Multiple_Menu_Items()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Item 1", "/item1", null),
            new HUIMenuItem("Item 2", "/item2", null)
        };

        // Act
        var cut = RenderComponentWithParams(items: items);

        // Assert
        cut.Markup.ShouldContain("Item 1");
        cut.Markup.ShouldContain("Item 2");
    }

    [Fact]
    public void Sets_Active_Tab_Index_When_ActiveItem_Provided()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Item 1", "/item1", null),
            new HUIMenuItem("Item 2", "/item2", null)
        };
        var activeItem = items[1];

        // Act
        var cut = RenderComponentWithParams(items: items, activeItem: activeItem);

        // Assert
        var tabs = cut.FindComponent<MudTabs>();
        tabs.Instance.ActivePanelIndex.ShouldBe(1);
    }

    [Fact]
    public void Sets_Active_Tab_Index_To_Negative_One_When_ActiveItem_Not_Provided()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Item 1", "/item1", null)
        };

        // Act
        var cut = RenderComponentWithParams(items: items, activeItem: null);

        // Assert
        var tabs = cut.FindComponent<MudTabs>();
        tabs.Instance.ActivePanelIndex.ShouldBe(-1);
    }

    [Fact]
    public void Sets_Active_Tab_Index_To_Negative_One_When_ActiveItem_Not_In_Items()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Item 1", "/item1", null)
        };
        var activeItem = new HUIMenuItem("Different Item", "/different", null);

        // Act
        var cut = RenderComponentWithParams(items: items, activeItem: activeItem);

        // Assert
        var tabs = cut.FindComponent<MudTabs>();
        tabs.Instance.ActivePanelIndex.ShouldBe(-1);
    }

    [Fact]
    public void Sets_RequireAuthorization_When_True()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null)
        };

        // Act
        var cut = RenderComponentWithParams(items: items, requireAuthorization: true);

        // Assert
        cut.Instance.RequireAuthorization.ShouldBeTrue();
    }

    [Fact]
    public void Sets_RequireAuthorization_When_False()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null)
        };

        // Act
        var cut = RenderComponentWithParams(items: items, requireAuthorization: false);

        // Assert
        cut.Instance.RequireAuthorization.ShouldBeFalse();
    }

    [Fact]
    public void MudTabs_Has_Correct_Properties()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null)
        };

        // Act
        var cut = RenderComponentWithParams(items: items);

        // Assert
        var tabs = cut.FindComponent<MudTabs>();
        tabs.Instance.Elevation.ShouldBe(0);
        tabs.Instance.Color.ShouldBe(Color.Transparent);
        tabs.Instance.Rounded.ShouldBeTrue();
        tabs.Instance.MinimumTabWidth.ShouldBe("32px");
        tabs.Instance.HideSlider.ShouldBeTrue();
    }

    [Fact]
    public void Renders_Menu_Item_Without_Icon()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null)
        };

        // Act
        var cut = RenderComponentWithParams(items: items);

        // Assert
        cut.FindAll(".mud-icon").Count.ShouldBe(0);
    }

    [Fact]
    public void Renders_Menu_Item_Without_Tooltip()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null)
        };

        // Act
        var cut = RenderComponentWithParams(items: items);

        // Assert
        cut.Markup.ShouldNotContain("tooltip");
    }

    [Fact]
    public void Renders_StatusChip_When_Dot_Is_Provided()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null, "", 0, null, null, null, Color.Primary, Severity.Info)
        };

        // Act
        var cut = RenderComponentWithParams(items: items);

        // Assert
        cut.FindComponent<HUIStatusChip>().ShouldNotBeNull();
    }

    [Fact]
    public void Renders_MudChip_When_BadgeData_Is_Provided()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null, "", 0, null, null, "5", Color.Primary)
        };

        // Act
        var cut = RenderComponentWithParams(items: items);

        // Assert
        cut.FindComponent<MudChip<string>>().ShouldNotBeNull();
        cut.Markup.ShouldContain("5");
    }

    [Fact]
    public void Renders_Nothing_When_No_Badge_Or_Dot()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null)
        };

        // Act
        var cut = RenderComponentWithParams(items: items);

        // Assert
        cut.FindAll(".mud-chip").Count.ShouldBe(0);
        cut.FindAll("hui-status-chip").Count.ShouldBe(0);
    }

    [Fact]
    public void MudTabPanel_Has_Correct_Properties()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null)
        };

        // Act
        var cut = RenderComponentWithParams(items: items);

        // Assert
        var tabPanel = cut.FindComponent<MudTabPanel>();
        tabPanel.Instance.Class.ShouldBe("px-6");
    }

    [Fact]
    public void MudStack_Has_Correct_Properties()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null)
        };

        // Act
        var cut = RenderComponentWithParams(items: items);

        // Assert
        var stack = cut.FindComponent<MudStack>();
        stack.Instance.Spacing.ShouldBe(2);
        stack.Instance.Row.ShouldBeTrue();
        stack.Instance.Justify.ShouldBe(Justify.Center);
        stack.Instance.AlignItems.ShouldBe(AlignItems.Center);
    }

    [Fact]
    public void Renders_Menu_Item_With_Policy()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null, "TestPolicy")
        };

        // Act
        var cut = RenderComponentWithParams(items: items);

        // Assert
        cut.Markup.ShouldContain("Test Item");
        // Note: Policy checking is handled by AuthorizeView in the actual component
    }

    [Fact]
    public void Renders_Menu_Item_Without_Policy()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null)
        };

        // Act
        var cut = RenderComponentWithParams(items: items);

        // Assert
        cut.Markup.ShouldContain("Test Item");
    }

    [Fact]
    public void Handles_Empty_Items_List()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(items: new List<HUIMenuItem>());

        // Assert
        cut.Markup.ShouldBeEmpty();
    }

    [Fact]
    public void Handles_Null_Items()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(items: null);

        // Assert
        cut.Markup.ShouldBeEmpty();
    }
}



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
