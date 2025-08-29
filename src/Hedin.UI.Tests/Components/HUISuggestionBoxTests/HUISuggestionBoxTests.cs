using Bunit;
using Shouldly;
using Microsoft.AspNetCore.Components;
using Hedin.UI.Tests.Base;
using MudBlazor;
using Microsoft.Extensions.DependencyInjection;

namespace Hedin.UI.Tests.Components.HUISuggestionBoxTests;

public class HUISuggestionBoxTests : UiTestBase
{
    private IRenderedComponent<HUISuggestionBox> RenderComponentWithParams(
        RenderFragment? activatorContent = null,
        RenderFragment? suggestionFooter = null,
        RenderFragment? suggestionBody = null,
        string header = "",
        Origin anchorOrigin = Origin.BottomRight,
        Origin transformOrigin = Origin.TopRight,
        bool togglePopover = false,
        EventCallback<bool>? togglePopoverChanged = default,
        EventCallback<string>? onSubmit = default)
    {
        return RenderComponentWithMudProviders<HUISuggestionBox>(parameters => parameters
            .Add(p => p.ActivatorContent, activatorContent)
            .Add(p => p.HUISuggestionFooter, suggestionFooter)
            .Add(p => p.HUISuggestionBody, suggestionBody)
            .Add(p => p.Header, header)
            .Add(p => p.AnchorOrigin, anchorOrigin)
            .Add(p => p.TransformOrigin, transformOrigin)
            .Add(p => p.TogglePopover, togglePopover)
            .Add(p => p.TogglePopoverChanged, togglePopoverChanged ?? EventCallback.Factory.Create<bool>(this, _ => { }))
            .Add(p => p.OnSubmit, onSubmit ?? EventCallback.Factory.Create<string>(this, _ => { }))
        );
    }

    [Fact]
    public void Renders_Default_Icon_Button_When_No_Activator_Content()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        cut.FindComponent<HUIIconButton>().ShouldNotBeNull();
    }

    [Fact]
    public void Renders_Custom_Activator_Content_When_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(
            activatorContent: builder => builder.AddContent(0, "Custom Activator")
        );

        // Assert
        cut.Markup.ShouldContain("Custom Activator");
    }

    [Fact]
    public void Renders_Default_Icon_Button_With_Correct_Properties()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var iconButton = cut.FindComponent<HUIIconButton>();
        iconButton.Instance.Icon.ShouldBe(Icons.Material.Outlined.Chat);
        iconButton.Instance.Variant.ShouldBe(Variant.Text);
    }

    [Fact]
    public void Sets_Anchor_Origin_When_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(anchorOrigin: Origin.TopLeft);

        // Assert
        cut.Instance.AnchorOrigin.ShouldBe(Origin.TopLeft);
    }

    [Fact]
    public void Sets_Transform_Origin_When_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(transformOrigin: Origin.BottomLeft);

        // Assert
        cut.Instance.TransformOrigin.ShouldBe(Origin.BottomLeft);
    }

    [Fact]
    public void Sets_Toggle_Popover_When_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(togglePopover: true);

        // Assert
        cut.Instance.TogglePopover.ShouldBeTrue();
    }

    [Fact]
    public void Renders_Popover_When_Toggle_Popover_Is_True()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(togglePopover: true);

        // Assert
        cut.FindComponent<MudPopover>().ShouldNotBeNull();
    }

    [Fact]
    public void Popover_Has_Correct_Properties()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(togglePopover: true);

        // Assert
        var popover = cut.FindComponent<MudPopover>();
        popover.Instance.Open.ShouldBeTrue();
        popover.Instance.AnchorOrigin.ShouldBe(Origin.BottomRight);
        popover.Instance.TransformOrigin.ShouldBe(Origin.TopRight);
    }

    [Fact]
    public void Tooltip_Has_Correct_Properties()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var tooltip = cut.FindComponent<HUITooltip>();
        tooltip.Instance.PointerEvents.ShouldBe("none");
        tooltip.Instance.Text.ShouldBe("Open Suggestion Box");
    }

    [Fact]
    public void Handles_Null_Activator_Content()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(activatorContent: null);

        // Assert
        cut.FindComponent<HUIIconButton>().ShouldNotBeNull();
    }
}


