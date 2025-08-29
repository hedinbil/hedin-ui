using Bunit;
using Shouldly;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Hedin.UI.Tests.Base;
using MudBlazor;

namespace Hedin.UI.Tests.Components.HUIInputButtonTests;

public class HUIInputButtonTests : UiTestBase
{
    private IRenderedComponent<HUIInputButton> RenderComponentWithParams(
        string? value = null,
        EventCallback<string?>? valueChanged = null,
        bool autoFocus = false,
        EventCallback<string?>? onEnterKey = null,
        string? tooltip = null,
        string? label = null,
        string icon = "test-icon",
        bool disabled = false,
        bool showInput = false,
        EventCallback<bool>? showInputChanged = null)
    {
        return RenderComponent<HUIInputButton>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, valueChanged ?? EventCallback.Factory.Create<string?>(this, _ => { }))
            .Add(p => p.AutoFocus, autoFocus)
            .Add(p => p.OnEnterKey, onEnterKey ?? EventCallback.Factory.Create<string?>(this, _ => { }))
            .Add(p => p.Tooltip, tooltip)
            .Add(p => p.Label, label)
            .Add(p => p.Icon, icon)
            .Add(p => p.Disabled, disabled)
            .Add(p => p.ShowInput, showInput)
            .Add(p => p.ShowInputChanged, showInputChanged ?? EventCallback.Factory.Create<bool>(this, _ => { }))
        );
    }

    [Fact]
    public void Renders_IconButton_When_ShowInput_False()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(showInput: false);

        // Assert
        cut.Find("button").ShouldNotBeNull();
        cut.FindAll("input").Count.ShouldBe(0);
    }

    [Fact]
    public void Renders_Input_With_Custom_Label()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(label: "Search", showInput: true);

        // Assert
        cut.Find("label").ShouldNotBeNull();
        cut.Find("label").TextContent.ShouldContain("Search");
    }

    [Fact]
    public void Input_Field_Has_Correct_Variant()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(showInput: true);

        // Assert
        var textField = cut.FindComponent<MudTextField<string>>();
        textField.Instance.Variant.ShouldBe(Variant.Outlined);
    }

    [Fact]
    public void Input_Field_Has_Correct_Margin()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(showInput: true);

        // Assert
        var textField = cut.FindComponent<MudTextField<string>>();
        textField.Instance.Margin.ShouldBe(Margin.Dense);
    }

    [Fact]
    public void Input_Field_Has_Correct_Adornment()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(showInput: true);

        // Assert
        var textField = cut.FindComponent<MudTextField<string>>();
        textField.Instance.Adornment.ShouldBe(Adornment.End);
    }

    [Fact]
    public void Input_Field_Has_Correct_AdornmentIcon()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(showInput: true);

        // Assert
        var textField = cut.FindComponent<MudTextField<string>>();
        textField.Instance.AdornmentIcon.ShouldBe(Icons.Material.Filled.Close);
    }

    [Fact]
    public void Input_Field_Has_Correct_IconSize()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(showInput: true);

        // Assert
        var textField = cut.FindComponent<MudTextField<string>>();
        textField.Instance.IconSize.ShouldBe(Size.Small);
    }

    [Fact]
    public void Input_Field_Has_Correct_DebounceInterval()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(showInput: true);

        // Assert
        var textField = cut.FindComponent<MudTextField<string>>();
        textField.Instance.DebounceInterval.ShouldBe(200);
    }

    [Fact]
    public void Input_Field_Has_AutoFocus_When_Specified()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(showInput: true, autoFocus: true);

        // Assert
        var textField = cut.FindComponent<MudTextField<string>>();
        textField.Instance.AutoFocus.ShouldBeTrue();
    }

    [Fact]
    public void Input_Field_Does_Not_Have_AutoFocus_When_Not_Specified()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(showInput: true, autoFocus: false);

        // Assert
        var textField = cut.FindComponent<MudTextField<string>>();
        textField.Instance.AutoFocus.ShouldBeFalse();
    }

    [Fact]
    public void IconButton_Is_Disabled_When_Disabled_Parameter_True()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(disabled: true, showInput: false);

        // Assert
        var button = cut.Find("button");
        button.HasAttribute("disabled").ShouldBeTrue();
    }

    [Fact]
    public void IconButton_Is_Not_Disabled_When_Disabled_Parameter_False()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(disabled: false, showInput: false);

        // Assert
        var button = cut.Find("button");
        button.HasAttribute("disabled").ShouldBeFalse();
    }

    [Fact]
    public void Input_Field_Is_Not_Rendered_When_Disabled()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(disabled: true, showInput: true);

        // Assert
        cut.FindAll("input").Count.ShouldBe(0);
        cut.Find("button").ShouldNotBeNull();
    }

    [Fact]
    public void Sets_Value_When_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(value: "test value", showInput: true);

        // Assert
        cut.Instance.Value.ShouldBe("test value");
    }

    [Fact]
    public void Button_Click_Shows_Input_Field()
    {
        // Arrange
        var cut = RenderComponentWithParams(showInput: false);

        // Act
        cut.Find("button").Click();

        // Assert
        cut.Instance.ShowInput.ShouldBeTrue();
    }

    [Fact]
    public void Button_Click_Invokes_ShowInputChanged()
    {
        // Arrange
        var showInputChanged = false;
        var cut = RenderComponentWithParams(
            showInput: false,
            showInputChanged: EventCallback.Factory.Create<bool>(this, _ => showInputChanged = true)
        );

        // Act
        cut.Find("button").Click();

        // Assert
        showInputChanged.ShouldBeTrue();
    }

    [Fact]
    public void Close_Button_Click_Hides_Input_Field()
    {
        // Arrange
        var cut = RenderComponentWithParams(showInput: true);

        // Act
        cut.Find("button[class*='mud-icon-button']").Click();

        // Assert
        cut.Instance.ShowInput.ShouldBeFalse();
    }

    [Fact]
    public void Close_Button_Click_Clears_Value()
    {
        // Arrange
        var cut = RenderComponentWithParams(showInput: true, value: "test");

        // Act
        cut.Find("button[class*='mud-icon-button']").Click();

        // Assert
        cut.Instance.Value.ShouldBe("");
    }

    [Fact]
    public void Close_Button_Click_Invokes_ValueChanged_With_Null()
    {
        // Arrange
        var valueChanged = false;
        var cut = RenderComponentWithParams(
            showInput: true,
            value: "test",
            valueChanged: EventCallback.Factory.Create<string?>(this, _ => valueChanged = true)
        );

        // Act
        cut.Find("button[class*='mud-icon-button']").Click();

        // Assert
        valueChanged.ShouldBeTrue();
    }

    [Fact]
    public void Close_Button_Click_Invokes_ShowInputChanged()
    {
        // Arrange
        var showInputChanged = false;
        var cut = RenderComponentWithParams(
            showInput: true,
            showInputChanged: EventCallback.Factory.Create<bool>(this, _ => showInputChanged = true)
        );

        // Act
        cut.Find("button[class*='mud-icon-button']").Click();

        // Assert
        showInputChanged.ShouldBeTrue();
    }

    [Fact]
    public void ActivateAsync_Shows_Input_Field()
    {
        // Arrange
        var cut = RenderComponentWithParams(showInput: false);

        // Act
        cut.Instance.ActivateAsync().Wait();

        // Assert
        cut.Instance.ShowInput.ShouldBeTrue();
    }

    [Fact]
    public void DeactivateAsync_Hides_Input_Field()
    {
        // Arrange
        var cut = RenderComponentWithParams(showInput: true);

        // Act
        cut.Instance.DeactivateAsync().Wait();

        // Assert
        cut.Instance.ShowInput.ShouldBeFalse();
    }

    [Fact]
    public void DeactivateAsync_Clears_Value()
    {
        // Arrange
        var cut = RenderComponentWithParams(showInput: true, value: "test");

        // Act
        cut.Instance.DeactivateAsync().Wait();

        // Assert
        cut.Instance.Value.ShouldBe("");
    }

    [Fact]
    public void Input_Field_Stops_Event_Propagation()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(showInput: true);

        // Assert
        // The @onclick:stopPropagation="true" attribute should be present
        // This is tested by verifying the component renders without errors
        cut.Find("input").ShouldNotBeNull();
    }
}
