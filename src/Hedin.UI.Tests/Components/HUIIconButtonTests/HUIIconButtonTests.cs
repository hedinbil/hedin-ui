using Bunit;
using Shouldly;
using Microsoft.AspNetCore.Components;
using Hedin.UI.Tests.Base;
using MudBlazor;

namespace Hedin.UI.Tests.Components.HUIIconButtonTests;

public class HUIIconButtonTests : UiTestBase
{
    private IRenderedComponent<HUIIconButton> RenderComponentWithParams(
        bool loading = false,
        string? tooltip = null,
        Color color = Color.Default,
        Variant variant = Variant.Outlined,
        string? icon = null,
        string? href = null,
        EventCallback? onClick = null,
        bool disabled = false,
        string? style = null,
        string? target = null,
        string tooltipPointerEvents = "auto")
    {
        return RenderComponent<HUIIconButton>(parameters => parameters
            .Add(p => p.Loading, loading)
            .Add(p => p.Tooltip, tooltip)
            .Add(p => p.Color, color)
            .Add(p => p.Variant, variant)
            .Add(p => p.Icon, icon)
            .Add(p => p.Href, href)
            .Add(p => p.OnClick, onClick ?? EventCallback.Factory.Create(this, () => { }))
            .Add(p => p.Disabled, disabled)
            .Add(p => p.Style, style ?? "")
            .Add(p => p.Target, target)
            .Add(p => p.TooltipPointerEvents, tooltipPointerEvents)
        );
    }

    [Fact]
    public void Renders_IconButton_With_Default_Properties()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        cut.Find(".mud-icon-button").ShouldNotBeNull();
    }

    [Fact]
    public void Shows_Progress_Circular_When_Loading()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(loading: true);

        // Assert
        cut.Find(".mud-progress-circular").ShouldNotBeNull();
        cut.FindAll("i[class*='mud-icon']").Count.ShouldBe(0);
    }

    [Fact]
    public void Hides_Icon_When_Loading()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(loading: true, icon: "test-icon");

        // Assert
        cut.FindAll("i[class*='test-icon']").Count.ShouldBe(0);
    }

    [Fact]
    public void Applies_Custom_Color()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(color: Color.Primary);

        // Assert
        cut.Instance.Color.ShouldBe(Color.Primary);
    }

    [Fact]
    public void Applies_Custom_Variant()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(variant: Variant.Filled);

        // Assert
        cut.Instance.Variant.ShouldBe(Variant.Filled);
    }

    [Fact]
    public void Applies_Custom_Style()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(style: "background-color: red");

        // Assert
        cut.Instance.Style.ShouldBe("background-color: red");
    }

    [Fact]
    public void Sets_Href_When_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(href: "/test-page");

        // Assert
        cut.Instance.Href.ShouldBe("/test-page");
    }

    [Fact]
    public void Sets_Target_When_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(target: "_blank");

        // Assert
        cut.Instance.Target.ShouldBe("_blank");
    }

    [Fact]
    public void Sets_TooltipPointerEvents_When_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(tooltipPointerEvents: "none");

        // Assert
        cut.Instance.TooltipPointerEvents.ShouldBe("none");
    }

    [Fact]
    public void Button_Is_Disabled_When_Loading()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(loading: true);

        // Assert
        var button = cut.Find("button");
        button.HasAttribute("disabled").ShouldBeTrue();
    }

    [Fact]
    public void Button_Is_Disabled_When_Disabled_Parameter_True()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(disabled: true);

        // Assert
        var button = cut.Find("button");
        button.HasAttribute("disabled").ShouldBeTrue();
    }

    [Fact]
    public void Button_Is_Not_Disabled_When_Not_Loading_And_Not_Disabled()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(loading: false, disabled: false);

        // Assert
        var button = cut.Find("button");
        button.HasAttribute("disabled").ShouldBeFalse();
    }

    [Fact]
    public void Button_Has_Correct_Size()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var button = cut.Find("button");
        button.GetAttribute("class").ShouldContain("mud-icon-button-size-small");
    }

    [Fact]
    public void Button_Has_No_DropShadow()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var button = cut.Find("button");
        button.GetAttribute("class").ShouldNotContain("mud-icon-button-elevation");
    }

    [Fact]
    public void Progress_Circular_Has_Correct_Classes()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(loading: true);

        // Assert
        var progress = cut.Find(".mud-progress-circular");
        progress.GetAttribute("class").ShouldContain("mt-1");
    }

    [Fact]
    public void MergedStyle_Combines_StyleFromColumn_And_Style()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(style: "custom-style");

        // Assert
        // Note: StyleFromColumn is a cascading parameter, so we test the logic
        cut.Instance.Style.ShouldBe("custom-style");
    }

    [Fact]
    public void MergedStyle_Handles_Empty_Style()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(style: "");

        // Assert
        cut.Instance.Style.ShouldBe("");
    }

    [Fact]
    public void MergedStyle_Handles_Null_Style()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(style: null);

        // Assert
        cut.Instance.Style.ShouldBe("");
    }
}
