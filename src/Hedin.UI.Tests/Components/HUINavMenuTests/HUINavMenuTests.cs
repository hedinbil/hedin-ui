using Bunit;
using Shouldly;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Hedin.UI.Tests.Base;
using MudBlazor;
using Microsoft.Extensions.DependencyInjection;

namespace Hedin.UI.Tests.Components.HUINavMenuTests;

public class HUINavMenuTests : UiTestBase
{
    private IRenderedComponent<HUINavMenu> RenderComponentWithParams(
        List<HUIMenuItem>? items = null,
        RenderFragment? childContent = null,
        bool expanded = true,
        bool navigateOnGroupClick = false,
        bool requireAuthorization = true,
        bool mergeSubItemWithGroup = true,
        int levelsToRender = -1)
    {
        Services.AddSingleton<NavigationManager>(new TestNavigationManager());

        return RenderComponentWithMudProviders<HUINavMenu>(parameters => parameters
            .Add(p => p.Items, items ?? new List<HUIMenuItem>())
            .Add(p => p.ChildContent, childContent)
            .Add(p => p.Expanded, expanded)
            .Add(p => p.NavigateOnGroupClick, navigateOnGroupClick)
            .Add(p => p.RequireAuthorization, requireAuthorization)
            .Add(p => p.MergeSubItemWithGroup, mergeSubItemWithGroup)
            .Add(p => p.LevelsToRender, levelsToRender)
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
    public void Renders_ChildContent_When_Provided()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null)
        };

        // Act
        var cut = RenderComponentWithParams(
            items: items,
            childContent: builder => builder.AddContent(0, "Custom Content")
        );

        // Assert
        cut.Markup.ShouldContain("Custom Content");
    }

    [Fact]
    public void Sets_Expanded_When_True()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null)
        };

        // Act
        var cut = RenderComponentWithParams(items: items, expanded: true);

        // Assert
        cut.Instance.Expanded.ShouldBeTrue();
    }

    [Fact]
    public void Sets_Expanded_When_False()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null)
        };

        // Act
        var cut = RenderComponentWithParams(items: items, expanded: false);

        // Assert
        cut.Instance.Expanded.ShouldBeFalse();
    }

    [Fact]
    public void Sets_NavigateOnGroupClick_When_True()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null)
        };

        // Act
        var cut = RenderComponentWithParams(items: items, navigateOnGroupClick: true);

        // Assert
        cut.Instance.NavigateOnGroupClick.ShouldBeTrue();
    }

    [Fact]
    public void Sets_NavigateOnGroupClick_When_False()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null)
        };

        // Act
        var cut = RenderComponentWithParams(items: items, navigateOnGroupClick: false);

        // Assert
        cut.Instance.NavigateOnGroupClick.ShouldBeFalse();
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
    public void Sets_MergeSubItemWithGroup_When_True()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null)
        };

        // Act
        var cut = RenderComponentWithParams(items: items, mergeSubItemWithGroup: true);

        // Assert
        cut.Instance.MergeSubItemWithGroup.ShouldBeTrue();
    }

    [Fact]
    public void Sets_MergeSubItemWithGroup_When_False()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null)
        };

        // Act
        var cut = RenderComponentWithParams(items: items, mergeSubItemWithGroup: false);

        // Assert
        cut.Instance.MergeSubItemWithGroup.ShouldBeFalse();
    }

    [Fact]
    public void Sets_LevelsToRender_When_Provided()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null)
        };

        // Act
        var cut = RenderComponentWithParams(items: items, levelsToRender: 2);

        // Assert
        cut.Instance.LevelsToRender.ShouldBe(2);
    }

    [Fact]
    public void Uses_Default_LevelsToRender_When_Not_Specified()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null)
        };

        // Act
        var cut = RenderComponentWithParams(items: items);

        // Assert
        cut.Instance.LevelsToRender.ShouldBe(-1);
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
        cut.Find("a[href='/test']").ShouldNotBeNull();
    }

    [Fact]
    public void Renders_Menu_Group_With_SubItems()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Group", "/group", null)
            {
                SubItems = new List<HUIMenuItem>
                {
                    new HUIMenuItem("Sub Item", "/group/sub", "/group")
                }
            }
        };

        // Act
        var cut = RenderComponentWithParams(items: items);

        // Assert
        cut.Markup.ShouldContain("Group");
        cut.Markup.ShouldContain("Sub Item");
    }

    [Fact]
    public void Renders_Merged_SubItem_When_Only_One_SubItem()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Group", "/group", null)
            {
                SubItems = new List<HUIMenuItem>
                {
                    new HUIMenuItem("Sub Item", "/group/sub", "/group")
                }
            }
        };

        // Act
        var cut = RenderComponentWithParams(items: items, mergeSubItemWithGroup: true);

        // Assert
        cut.Markup.ShouldContain("Group Sub Item");
    }

    [Fact]
    public void Does_Not_Merge_SubItem_When_MergeSubItemWithGroup_False()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Group", "/group", null)
            {
                SubItems = new List<HUIMenuItem>
                {
                    new HUIMenuItem("Sub Item", "/group/sub", "/group")
                }
            }
        };

        // Act
        var cut = RenderComponentWithParams(items: items, mergeSubItemWithGroup: false);

        // Assert
        cut.Markup.ShouldContain("Group");
        cut.Markup.ShouldContain("Sub Item");
        cut.Markup.ShouldNotContain("Group Sub Item");
    }

    [Fact]
    public void Renders_All_Levels_When_LevelsToRender_Is_Negative()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Level 1", "/level1", null)
            {
                SubItems = new List<HUIMenuItem>
                {
                    new HUIMenuItem("Level 2", "/level1/level2", "/level1")
                    {
                        SubItems = new List<HUIMenuItem>
                        {
                            new HUIMenuItem("Level 3", "/level1/level2/level3", "/level1/level2")
                        }
                    }
                }
            }
        };

        // Act
        var cut = RenderComponentWithParams(items: items, levelsToRender: -1);

        // Assert
        cut.Markup.ShouldContain("Level 1");
        cut.Markup.ShouldContain("Level 2");
        cut.Markup.ShouldContain("Level 3");
    }

    [Fact]
    public void NavMenu_Has_Correct_Properties()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null)
        };

        // Act
        var cut = RenderComponentWithParams(items: items);

        // Assert
        var navMenu = cut.FindComponent<MudNavMenu>();
        navMenu.Instance.Dense.ShouldBeTrue();
        navMenu.Instance.Color.ShouldBe(Color.Default);
    }

    [Fact]
    public void NavMenu_Wrapper_Has_Correct_Classes()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null)
        };

        // Act
        var cut = RenderComponentWithParams(items: items);

        // Assert
        var wrapper = cut.Find(".nav-menu-wrapper");
        wrapper.GetAttribute("class").ShouldContain("pt-2");
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
    public void Handles_Empty_SubItems()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Group", "/group", null)
            {
                SubItems = new List<HUIMenuItem>()
            }
        };

        // Act
        var cut = RenderComponentWithParams(items: items);

        // Assert
        cut.Markup.ShouldContain("Group");
    }

    [Fact]
    public void Renders_HTML_Content_In_ChildContent()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null)
        };

        // Act
        var cut = RenderComponentWithParams(
            items: items,
            childContent: builder => builder.AddMarkupContent(0, "<div class='custom-content'>HTML Content</div>")
        );

        // Assert
        cut.Find(".custom-content").ShouldNotBeNull();
        cut.Markup.ShouldContain("HTML Content");
    }

    [Fact]
    public void Renders_Component_Content_In_ChildContent()
    {
        // Arrange
        var items = new List<HUIMenuItem>
        {
            new HUIMenuItem("Test Item", "/test", null)
        };

        // Act
        var cut = RenderComponentWithParams(
            items: items,
            childContent: builder => builder.AddContent(0, "Component Content")
        );

        // Assert
        cut.Markup.ShouldContain("Component Content");
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
