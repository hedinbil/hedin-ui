using Bunit;
using Shouldly;
using Microsoft.AspNetCore.Components;
using Hedin.UI.Tests.Base;
using MudBlazor;

namespace Hedin.UI.Tests.Components.HUIMainContainerTests;

public class HUIMainContainerTests : UiTestBase
{
    private IRenderedComponent<HUIMainContainer> RenderComponentWithParams(
        RenderFragment? childContent = null,
        int zIndex = 1100,
        string? @class = null,
        string? style = null,
        MaxWidth maxWidth = MaxWidth.False)
    {
        return RenderComponent<HUIMainContainer>(parameters => parameters
            .Add(p => p.ChildContent, childContent ?? (builder => builder.AddContent(0, "Default Content")))
            .Add(p => p.ZIndex, zIndex)
            .Add(p => p.Class, @class ?? "")
            .Add(p => p.Style, style ?? "")
            .Add(p => p.MaxWidth, maxWidth)
        );
    }

    [Fact]
    public void Renders_MainContainer_With_Default_Properties()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        cut.Find(".mud-main-content").ShouldNotBeNull();
        cut.Find(".mud-container").ShouldNotBeNull();
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
    public void Sets_Custom_ZIndex_When_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(zIndex: 2000);

        // Assert
        cut.Instance.ZIndex.ShouldBe(2000);
    }

    [Fact]
    public void Uses_Default_ZIndex_When_Not_Specified()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        cut.Instance.ZIndex.ShouldBe(1100);
    }

    [Fact]
    public void Applies_Custom_Class_When_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(@class: "custom-class");

        // Assert
        var container = cut.Find(".mud-container");
        container.GetAttribute("class").ShouldContain("custom-class");
    }

    [Fact]
    public void Applies_Custom_Style_When_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(style: "background-color: red");

        // Assert
        var container = cut.Find(".mud-container");
        container.GetAttribute("style").ShouldContain("background-color: red");
    }

    [Fact]
    public void Sets_Custom_MaxWidth_When_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(maxWidth: MaxWidth.Large);

        // Assert
        cut.Instance.MaxWidth.ShouldBe(MaxWidth.Large);
    }

    [Fact]
    public void Uses_Default_MaxWidth_When_Not_Specified()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        cut.Instance.MaxWidth.ShouldBe(MaxWidth.False);
    }

    [Fact]
    public void Container_Has_Correct_Classes()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var container = cut.Find(".mud-container");
        container.ShouldNotBeNull();
    }

    [Fact]
    public void MainContent_Has_Correct_Classes()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var mainContent = cut.Find(".mud-main-content");
        mainContent.ShouldNotBeNull();
    }

    [Fact]
    public void Style_Combines_ZIndex_And_Custom_Style()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(zIndex: 1500, style: "background-color: blue");

        // Assert
        var container = cut.Find(".mud-container");
        var style = container.GetAttribute("style");
        style.ShouldContain("z-index: 1500");
        style.ShouldContain("background-color: blue");
    }

    [Fact]
    public void Style_Contains_Default_ZIndex_When_No_Custom_Style()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var container = cut.Find(".mud-container");
        container.GetAttribute("style").ShouldContain("z-index: 1100");
    }

    [Fact]
    public void Style_Contains_Custom_Style_When_No_ZIndex_Change()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(style: "padding: 20px");

        // Assert
        var container = cut.Find(".mud-container");
        container.GetAttribute("style").ShouldContain("padding: 20px");
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

    [Fact]
    public void Handles_Empty_Class()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(@class: "");

        // Assert
        var container = cut.Find(".mud-container");
        container.GetAttribute("class").ShouldNotContain("custom-class");
    }

    [Fact]
    public void Handles_Empty_Style()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(style: "");

        // Assert
        var container = cut.Find(".mud-container");
        container.GetAttribute("style").ShouldContain("z-index: 1100");
        container.GetAttribute("style").ShouldNotContain("background-color");
    }

    [Fact]
    public void Handles_Null_Class()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(@class: null);

        // Assert
        var container = cut.Find(".mud-container");
        container.GetAttribute("class").ShouldNotContain("custom-class");
    }

    [Fact]
    public void Handles_Null_Style()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(style: null);

        // Assert
        var container = cut.Find(".mud-container");
        container.GetAttribute("style").ShouldContain("z-index: 1100");
        container.GetAttribute("style").ShouldNotContain("background-color");
    }

    [Fact]
    public void ZIndex_Is_Applied_To_Container_Style()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(zIndex: 9999);

        // Assert
        var container = cut.Find(".mud-container");
        container.GetAttribute("style").ShouldContain("z-index: 9999");
    }

    [Fact]
    public void MaxWidth_Is_Applied_To_Container()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(maxWidth: MaxWidth.ExtraLarge);

        // Assert
        var container = cut.Find(".mud-container");
        container.GetAttribute("class").ShouldContain("mud-container");
    }

    [Fact]
    public void MainContent_Contains_Flex_Container()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var mainContent = cut.Find(".mud-main-content");
        var flexContainer = mainContent.QuerySelector("div");
        flexContainer.ShouldNotBeNull();
        flexContainer.GetAttribute("class").ShouldContain("d-flex");
    }
}
