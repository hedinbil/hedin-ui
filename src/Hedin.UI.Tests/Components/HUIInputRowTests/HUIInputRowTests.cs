using Bunit;
using Shouldly;
using Microsoft.AspNetCore.Components;
using Hedin.UI.Tests.Base;
using MudBlazor;

namespace Hedin.UI.Tests.Components.HUIInputRowTests;

public class HUIInputRowTests : UiTestBase
{
    private IRenderedComponent<HUIInputRow<TestItem>> RenderComponentWithParams(
        TestItem? item = null,
        RenderFragment? childContent = null,
        EventCallback<TestItem>? onAddClick = null,
        EventCallback<TestItem>? onDeleteClick = null)
    {
        return RenderComponent<HUIInputRow<TestItem>>(parameters => parameters
            .Add(p => p.Item, item ?? new TestItem { Id = 1, Name = "Test Item" })
            .Add(p => p.ChildContent, childContent ?? (builder => builder.AddContent(0, "Default Content")))
            .Add(p => p.OnAddClick, onAddClick ?? EventCallback.Factory.Create<TestItem>(this, _ => { }))
            .Add(p => p.OnDeleteClick, onDeleteClick ?? EventCallback.Factory.Create<TestItem>(this, _ => { }))
        );
    }

    [Fact]
    public void Renders_InputRow_With_Default_Properties()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        cut.Find(".hui-input-table").ShouldNotBeNull();
        cut.Find(".hui-input-table-grid").ShouldNotBeNull();
        cut.Find(".hui-input-table-buttons").ShouldNotBeNull();
        cut.Markup.ShouldContain("Default Content");
    }

    [Fact]
    public void Renders_ChildContent_When_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(
            childContent: builder => builder.AddContent(0, "Custom Content")
        );

        // Assert
        cut.Markup.ShouldContain("Custom Content");
    }

    [Fact]
    public void Renders_Add_Button()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var addButton = cut.Find("button[class*='mud-icon-button']");
        addButton.ShouldNotBeNull();
        addButton.GetAttribute("class").ShouldContain("mud-icon-button");
    }

    [Fact]
    public void Renders_Delete_Button()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var deleteButton = cut.FindAll("button[class*='mud-icon-button']")[1];
        deleteButton.ShouldNotBeNull();
        deleteButton.GetAttribute("class").ShouldContain("mud-icon-button");
    }

    [Fact]
    public void Add_Button_Has_Correct_Size()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var addButton = cut.Find("button[class*='mud-icon-button']");
        addButton.GetAttribute("class").ShouldContain("mud-icon-button-size-small");
    }

    [Fact]
    public void Delete_Button_Has_Correct_Size()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var deleteButton = cut.FindAll("button[class*='mud-icon-button']")[1];
        deleteButton.GetAttribute("class").ShouldContain("mud-icon-button-size-small");
    }

    [Fact]
    public void Sets_Item_When_Provided()
    {
        // Arrange
        var testItem = new TestItem { Id = 2, Name = "Custom Item" };

        // Act
        var cut = RenderComponentWithParams(item: testItem);

        // Assert
        cut.Instance.Item.ShouldBe(testItem);
    }

    [Fact]
    public void Add_Button_Click_Invokes_OnAddClick_Callback()
    {
        // Arrange
        var addClicked = false;
        var testItem = new TestItem { Id = 1, Name = "Test Item" };
        var cut = RenderComponentWithParams(
            item: testItem,
            onAddClick: EventCallback.Factory.Create<TestItem>(this, _ => addClicked = true)
        );

        // Act
        cut.Find("button[class*='mud-icon-button']").Click();

        // Assert
        addClicked.ShouldBeTrue();
    }

    [Fact]
    public void Delete_Button_Click_Invokes_OnDeleteClick_Callback()
    {
        // Arrange
        var deleteClicked = false;
        var testItem = new TestItem { Id = 1, Name = "Test Item" };
        var cut = RenderComponentWithParams(
            item: testItem,
            onDeleteClick: EventCallback.Factory.Create<TestItem>(this, _ => deleteClicked = true)
        );

        // Act
        var deleteButton = cut.FindAll("button[class*='mud-icon-button']")[1];
        deleteButton.Click();

        // Assert
        deleteClicked.ShouldBeTrue();
    }

    [Fact]
    public void Add_Button_Click_Passes_Correct_Item()
    {
        // Arrange
        TestItem? passedItem = null;
        var testItem = new TestItem { Id = 1, Name = "Test Item" };
        var cut = RenderComponentWithParams(
            item: testItem,
            onAddClick: EventCallback.Factory.Create<TestItem>(this, item => passedItem = item)
        );

        // Act
        cut.Find("button[class*='mud-icon-button']").Click();

        // Assert
        passedItem.ShouldBe(testItem);
    }

    [Fact]
    public void Delete_Button_Click_Passes_Correct_Item()
    {
        // Arrange
        TestItem? passedItem = null;
        var testItem = new TestItem { Id = 1, Name = "Test Item" };
        var cut = RenderComponentWithParams(
            item: testItem,
            onDeleteClick: EventCallback.Factory.Create<TestItem>(this, item => passedItem = item)
        );

        // Act
        var deleteButton = cut.FindAll("button[class*='mud-icon-button']")[1];
        deleteButton.Click();

        // Assert
        passedItem.ShouldBe(testItem);
    }

    [Fact]
    public void Container_Has_Correct_Classes()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var container = cut.Find(".hui-input-table");
        container.ShouldNotBeNull();
    }

    [Fact]
    public void Grid_Container_Has_Correct_Classes()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var gridContainer = cut.Find(".hui-input-table-grid");
        gridContainer.ShouldNotBeNull();
    }

    [Fact]
    public void Buttons_Container_Has_Correct_Classes()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var buttonsContainer = cut.Find(".hui-input-table-buttons");
        buttonsContainer.ShouldNotBeNull();
    }

    [Fact]
    public void Renders_HTML_Content_In_ChildContent()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(
            childContent: builder => builder.AddMarkupContent(0, "<div class='inner-content'>HTML Content</div>")
        );

        // Assert
        cut.Find(".inner-content").ShouldNotBeNull();
        cut.Markup.ShouldContain("HTML Content");
    }

    [Fact]
    public void Renders_Component_Content_In_ChildContent()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(
            childContent: builder => builder.AddContent(0, "Component Content")
        );

        // Assert
        cut.Markup.ShouldContain("Component Content");
    }
}

// Test data class
public class TestItem
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}
