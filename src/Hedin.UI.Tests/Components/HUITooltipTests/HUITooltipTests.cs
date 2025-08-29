using Bunit;
using Shouldly;
using Hedin.UI.Tests.Base;
using MudBlazor;
using Microsoft.AspNetCore.Components;

namespace Hedin.UI.Tests.Components.HUITooltipTests;

public class HUITooltipTests : UiTestBase
{
    private IRenderedComponent<HUITooltip> RenderComponentWithParams(
        RenderFragment? childContent = null,
        RenderFragment? tooltipContent = null,
        Placement placement = Placement.Top,
        string @class = "",
        string style = "",
        string pointerEvents = "auto",
        string text = "")
    {
        return RenderComponent<HUITooltip>(parameters => parameters
            .Add(p => p.ChildContent, childContent)
            .Add(p => p.TooltipContent, tooltipContent)
            .Add(p => p.Placement, placement)
            .Add(p => p.Class, @class)
            .Add(p => p.Style, style)
            .Add(p => p.PointerEvents, pointerEvents)
            .Add(p => p.Text, text)
        );
    }

    [Fact]
    public void Renders_MudTooltip()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        cut.FindComponent<MudTooltip>().ShouldNotBeNull();
    }

    [Fact]
    public void Sets_Default_Placement()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var tooltip = cut.FindComponent<MudTooltip>();
        tooltip.Instance.Placement.ShouldBe(Placement.Top);
    }

    [Fact]
    public void Sets_Custom_Placement()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(placement: Placement.Bottom);

        // Assert
        var tooltip = cut.FindComponent<MudTooltip>();
        tooltip.Instance.Placement.ShouldBe(Placement.Bottom);
    }

    [Fact]
    public void Sets_Default_Class()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var tooltip = cut.FindComponent<MudTooltip>();
        tooltip.Instance.Class.ShouldBe("");
    }

    [Fact]
    public void Sets_Custom_Class()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(@class: "custom-tooltip");

        // Assert
        var tooltip = cut.FindComponent<MudTooltip>();
        tooltip.Instance.Class.ShouldBe("custom-tooltip");
    }

    [Fact]
    public void Sets_Default_Style()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var tooltip = cut.FindComponent<MudTooltip>();
        tooltip.Instance.Style.ShouldBe("pointer-events:auto; ");
    }

    [Fact]
    public void Sets_Custom_Style()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(style: "color: red;");

        // Assert
        var tooltip = cut.FindComponent<MudTooltip>();
        tooltip.Instance.Style.ShouldBe("pointer-events:auto; color: red;");
    }

    [Fact]
    public void Sets_Default_Pointer_Events()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var tooltip = cut.FindComponent<MudTooltip>();
        tooltip.Instance.Style.ShouldContain("pointer-events:auto");
    }

    [Fact]
    public void Sets_Custom_Pointer_Events()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(pointerEvents: "none");

        // Assert
        var tooltip = cut.FindComponent<MudTooltip>();
        tooltip.Instance.Style.ShouldContain("pointer-events:none");
    }

    [Fact]
    public void Sets_Default_Text()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var tooltip = cut.FindComponent<MudTooltip>();
        tooltip.Instance.Text.ShouldBe("");
    }

    [Fact]
    public void Sets_Arrow_To_True()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var tooltip = cut.FindComponent<MudTooltip>();
        tooltip.Instance.Arrow.ShouldBeTrue();
    }

    [Fact]
    public void Renders_Child_Content()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(
            childContent: builder => builder.AddContent(0, "Tooltip Trigger")
        );

        // Assert
        cut.Markup.ShouldContain("Tooltip Trigger");
    }

    [Theory]
    [InlineData(Placement.Top)]
    [InlineData(Placement.Bottom)]
    [InlineData(Placement.Left)]
    [InlineData(Placement.Right)]
    public void Renders_All_Placement_Types(Placement placement)
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(placement: placement);

        // Assert
        var tooltip = cut.FindComponent<MudTooltip>();
        tooltip.Instance.Placement.ShouldBe(placement);
    }

    [Theory]
    [InlineData("none")]
    [InlineData("auto")]
    [InlineData("visible")]
    [InlineData("visibleFill")]
    [InlineData("visiblePainted")]
    [InlineData("visibleStroke")]
    public void Renders_All_Pointer_Event_Types(string pointerEvents)
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(pointerEvents: pointerEvents);

        // Assert
        var tooltip = cut.FindComponent<MudTooltip>();
        tooltip.Instance.Style.ShouldContain($"pointer-events:{pointerEvents}");
    }

    [Fact]
    public void Combines_Style_And_Pointer_Events_Correctly()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(
            style: "background-color: blue; font-size: 14px;",
            pointerEvents: "none"
        );

        // Assert
        var tooltip = cut.FindComponent<MudTooltip>();
        tooltip.Instance.Style.ShouldBe("pointer-events:none; background-color: blue; font-size: 14px;");
    }

    [Fact]
    public void Handles_Empty_Class()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(@class: "");

        // Assert
        var tooltip = cut.FindComponent<MudTooltip>();
        tooltip.Instance.Class.ShouldBe("");
    }

    [Fact]
    public void Handles_Empty_Style()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(style: "");

        // Assert
        var tooltip = cut.FindComponent<MudTooltip>();
        tooltip.Instance.Style.ShouldBe("pointer-events:auto; ");
    }

    [Fact]
    public void Handles_Empty_Text()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(text: "");

        // Assert
        var tooltip = cut.FindComponent<MudTooltip>();
        tooltip.Instance.Text.ShouldBe("");
    }

    [Fact]
    public void Handles_Null_Child_Content()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(childContent: null);

        // Assert
        cut.FindComponent<MudTooltip>().ShouldNotBeNull();
    }

    [Fact]
    public void Handles_Null_Tooltip_Content()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(tooltipContent: null);

        // Assert
        cut.FindComponent<MudTooltip>().ShouldNotBeNull();
    }

    [Fact]
    public void Renders_Complex_Child_Content()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(
            childContent: builder => builder.AddMarkupContent(0, "<button class='btn'>Click me</button>")
        );

        // Assert
        cut.Find("button.btn").ShouldNotBeNull();
        cut.Markup.ShouldContain("Click me");
    }

    [Fact]
    public void Combines_Multiple_Classes()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(@class: "tooltip-class another-class");

        // Assert
        var tooltip = cut.FindComponent<MudTooltip>();
        tooltip.Instance.Class.ShouldBe("tooltip-class another-class");
    }

    [Fact]
    public void Combines_Multiple_Styles()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(
            style: "color: red; background-color: yellow; border: 1px solid black;"
        );

        // Assert
        var tooltip = cut.FindComponent<MudTooltip>();
        tooltip.Instance.Style.ShouldBe("pointer-events:auto; color: red; background-color: yellow; border: 1px solid black;");
    }
}
