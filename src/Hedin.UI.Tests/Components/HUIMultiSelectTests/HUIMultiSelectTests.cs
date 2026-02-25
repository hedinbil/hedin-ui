using Bunit;
using Shouldly;
using Microsoft.AspNetCore.Components;
using Hedin.UI.Tests.Base;
using MudBlazor;

namespace Hedin.UI.Tests.Components.HUIMultiSelectTests;

public class HUIMultiSelectTests : UiTestBase
{
    private IRenderedComponent<HUIMultiSelect<TestItem>> RenderComponentWithParams(
        IEnumerable<TestItem>? values = null,
        EventCallback<IEnumerable<TestItem>>? valuesChanged = null,
        string label = "Test Label",
        Func<string?, Task<IEnumerable<TestItem>>>? searchFunc = null,
        Func<TestItem, string>? toStringFunc = null,
        Func<TestItem, string>? chipToStringFunc = null,
        Func<TestItem, string>? chipTooltipStringFunc = null,
        Func<TestItem, bool>? itemDisabledFunc = null,
        string? @class = null,
        string? style = null,
        Color color = Color.Default,
        bool required = false,
        RenderFragment<TestItem>? itemTemplate = null,
        RenderFragment<TestItem>? itemDisabledTemplate = null,
        bool clearable = true,
        bool disabled = false,
        bool selectAll = false,
        bool selectAllValue = false,
        string allItemsSelectedText = "All items selected",
        EventCallback<bool>? selectAllValueChanged = null,
        string selectAllText = "Select All",
        RenderFragment? beforeItemsTemplate = null,
        RenderFragment? noItemsTemplate = null,
        bool dense = true,
        int maxHeight = 300)
    {
        return RenderComponentWithMudProviders<HUIMultiSelect<TestItem>>(parameters => parameters
            .Add(p => p.Values, values)
            .Add(p => p.ValuesChanged, valuesChanged ?? EventCallback.Factory.Create<IEnumerable<TestItem>>(this, _ => { }))
            .Add(p => p.Label, label)
            .Add(p => p.SearchFunc, searchFunc ?? ((_) => Task.FromResult<IEnumerable<TestItem>>(new List<TestItem>())))
            .Add(p => p.ToStringFunc, toStringFunc ?? (item => item?.Name))
            .Add(p => p.ChipToStringFunc, chipToStringFunc ?? (item => item?.Name))
            .Add(p => p.ChipTooltipStringFunc, chipTooltipStringFunc)
            .Add(p => p.ItemDisabledFunc, itemDisabledFunc)
            .Add(p => p.Class, @class ?? "")
            .Add(p => p.Style, style ?? "")
            .Add(p => p.Color, color)
            .Add(p => p.Required, required)
            .Add(p => p.ItemTemplate, itemTemplate)
            .Add(p => p.ItemDisabledTemplate, itemDisabledTemplate)
            .Add(p => p.Clearable, clearable)
            .Add(p => p.Disabled, disabled)
            .Add(p => p.SelectAll, selectAll)
            .Add(p => p.SelectAllValue, selectAllValue)
            .Add(p => p.AllItemsSelectedText, allItemsSelectedText)
            .Add(p => p.SelectAllValueChanged, selectAllValueChanged ?? EventCallback.Factory.Create<bool>(this, _ => { }))
            .Add(p => p.SelectAllText, selectAllText)
            .Add(p => p.BeforeItemsTemplate, beforeItemsTemplate)
            .Add(p => p.NoItemsTemplate, noItemsTemplate)
            .Add(p => p.Dense, dense)
            .Add(p => p.MaxHeight, maxHeight)
        );
    }

    [Fact]
    public void Renders_Label_When_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(label: "Custom Label");

        // Assert
        cut.Instance.Label.ShouldBe("Custom Label");
    }

    [Fact]
    public void Applies_Custom_Class_When_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(@class: "custom-class");

        // Assert
        var container = cut.Find("div");
        container.GetAttribute("class").ShouldContain("custom-class");
    }

    [Fact]
    public void Applies_Custom_Style_When_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(style: "background-color: red");

        // Assert
        var container = cut.Find("div");
        container.GetAttribute("style").ShouldContain("background-color: red");
    }

    [Fact]
    public void Sets_Custom_Color_When_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(color: Color.Primary);

        // Assert
        cut.Instance.Color.ShouldBe(Color.Primary);
    }

    [Fact]
    public void Sets_Required_When_True()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(required: true);

        // Assert
        cut.Instance.Required.ShouldBeTrue();
    }

    [Fact]
    public void Sets_Required_When_False()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(required: false);

        // Assert
        cut.Instance.Required.ShouldBeFalse();
    }

    [Fact]
    public void Sets_Clearable_When_True()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(clearable: true);

        // Assert
        cut.Instance.Clearable.ShouldBeTrue();
    }

    [Fact]
    public void Sets_Clearable_When_False()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(clearable: false);

        // Assert
        cut.Instance.Clearable.ShouldBeFalse();
    }

    [Fact]
    public void Sets_Disabled_When_True()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(disabled: true);

        // Assert
        cut.Instance.Disabled.ShouldBeTrue();
    }

    [Fact]
    public void Sets_Disabled_When_False()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(disabled: false);

        // Assert
        cut.Instance.Disabled.ShouldBeFalse();
    }

    [Fact]
    public void Sets_SelectAll_When_True()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(selectAll: true);

        // Assert
        cut.Instance.SelectAll.ShouldBeTrue();
    }

    [Fact]
    public void Sets_SelectAll_When_False()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(selectAll: false);

        // Assert
        cut.Instance.SelectAll.ShouldBeFalse();
    }

    [Fact]
    public void Sets_SelectAllValue_When_True()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(selectAllValue: true);

        // Assert
        cut.Instance.SelectAllValue.ShouldBeTrue();
    }

    [Fact]
    public void Sets_SelectAllValue_When_False()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(selectAllValue: false);

        // Assert
        cut.Instance.SelectAllValue.ShouldBeFalse();
    }

    [Fact]
    public void Sets_Custom_AllItemsSelectedText()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(allItemsSelectedText: "Custom Text");

        // Assert
        cut.Instance.AllItemsSelectedText.ShouldBe("Custom Text");
    }

    [Fact]
    public void Sets_Custom_SelectAllText()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(selectAllText: "Select Everything");

        // Assert
        cut.Instance.SelectAllText.ShouldBe("Select Everything");
    }

    [Fact]
    public void Sets_Custom_MaxHeight()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(maxHeight: 500);

        // Assert
        cut.Instance.MaxHeight.ShouldBe(500);
    }

    [Fact]
    public void Uses_Default_MaxHeight_When_Not_Specified()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        cut.Instance.MaxHeight.ShouldBe(300);
    }

    [Fact]
    public void Sets_Dense_When_True()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(dense: true);

        // Assert
        cut.Instance.Dense.ShouldBeTrue();
    }

    [Fact]
    public void Sets_Dense_When_False()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(dense: false);

        // Assert
        cut.Instance.Dense.ShouldBeFalse();
    }

    [Fact]
    public void Uses_Default_Dense_When_Not_Specified()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        cut.Instance.Dense.ShouldBeTrue();
    }

    [Fact]
    public void Autocomplete_Has_Correct_Properties()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var autocomplete = cut.FindComponent<MudAutocomplete<TestItem>>();
        autocomplete.Instance.Variant.ShouldBe(Variant.Outlined);
        autocomplete.Instance.Dense.ShouldBeTrue();
        autocomplete.Instance.Margin.ShouldBe(Margin.Dense);
        autocomplete.Instance.MaxHeight.ShouldBe(300);
        autocomplete.Instance.DebounceInterval.ShouldBe(250);
        autocomplete.Instance.ShowProgressIndicator.ShouldBeTrue();
        autocomplete.Instance.Clearable.ShouldBeTrue();
        autocomplete.Instance.CoerceValue.ShouldBeTrue();
    }

    [Fact]
    public void Autocomplete_Has_Correct_Icons()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var autocomplete = cut.FindComponent<MudAutocomplete<TestItem>>();
        autocomplete.Instance.OpenIcon.ShouldBe(Icons.Material.Filled.Search);
        autocomplete.Instance.CloseIcon.ShouldBe(Icons.Material.Filled.Search);
    }

    [Fact]
    public void ChipSet_Has_Correct_Properties()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var chipSet = cut.FindComponent<MudChipSet<TestItem>>();
        chipSet.Instance.AllClosable.ShouldBeTrue();
        chipSet.Instance.Color.ShouldBe(Color.Default);
        chipSet.Instance.Variant.ShouldBe(Variant.Outlined);
        chipSet.Instance.CloseIcon.ShouldBe(Icons.Material.Filled.Close);
    }

    [Fact]
    public void Does_Not_Render_SelectAll_Checkbox_When_SelectAll_False()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(selectAll: false);

        // Assert
        cut.FindAll("input[type='checkbox']").Count.ShouldBe(0);
    }

    [Fact]
    public void Renders_AllItemsSelected_Chip_When_SelectAllValue_True()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(selectAllValue: true);

        // Assert
        cut.Markup.ShouldContain("All items selected");
    }

    [Fact]
    public void Renders_Individual_Chips_When_SelectAllValue_False()
    {
        // Arrange
        var values = new List<TestItem>
        {
            new TestItem { Id = 1, Name = "Item 1" },
            new TestItem { Id = 2, Name = "Item 2" }
        };

        // Act
        var cut = RenderComponentWithParams(values: values, selectAllValue: false);

        // Assert
        cut.Markup.ShouldContain("Item 1");
        cut.Markup.ShouldContain("Item 2");
    }

    [Fact]
    public void Sets_Values_When_Provided()
    {
        // Arrange
        var values = new List<TestItem>
        {
            new TestItem { Id = 1, Name = "Item 1" },
            new TestItem { Id = 2, Name = "Item 2" }
        };

        // Act
        var cut = RenderComponentWithParams(values: values);

        // Assert
        cut.Instance.Values.ShouldBe(values);
    }

    [Fact]
    public void Handles_Null_Values()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(values: null);

        // Assert
        cut.Instance.Values.ShouldBeNull();
    }

    [Fact]
    public void Handles_Empty_Values()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(values: new List<TestItem>());

        // Assert
        cut.Instance.Values.ShouldNotBeNull();
        cut.Instance.Values.Count().ShouldBe(0);
    }

    [Fact]
    public void Handles_Null_Class()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(@class: null);

        // Assert
        cut.Instance.Class.ShouldBe("");
    }

    [Fact]
    public void Handles_Null_Style()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(style: null);

        // Assert
        cut.Instance.Style.ShouldBe("");
    }

    [Fact]
    public void Handles_Empty_Class()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(@class: "");

        // Assert
        cut.Instance.Class.ShouldBe("");
    }

    [Fact]
    public void Handles_Empty_Style()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(style: "");

        // Assert
        cut.Instance.Style.ShouldBe("");
    }

    [Fact]
    public void Autocomplete_Is_Properly_Configured()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var autocomplete = cut.FindComponent<MudAutocomplete<TestItem>>();
        autocomplete.ShouldNotBeNull();
    }

    [Fact]
    public void ChipSet_Is_Properly_Configured()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var chipSet = cut.FindComponent<MudChipSet<TestItem>>();
        chipSet.ShouldNotBeNull();
    }
}

// Test data class
public class TestItem
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}
