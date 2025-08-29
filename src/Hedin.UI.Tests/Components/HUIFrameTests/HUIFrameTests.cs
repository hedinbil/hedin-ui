using Bunit;
using Shouldly;
using Microsoft.AspNetCore.Components;
using Hedin.UI.Tests.Base;

namespace Hedin.UI.Tests.Components.Frame;

public class HUIFrameTests : UiTestBase
{
    private IRenderedComponent<HUIFrame> RenderComponentWithParams(
        RenderFragment? childContent = null,
        string? @class = null,
        string? style = null)
    {
        return RenderComponent<HUIFrame>(parameters => parameters
            .Add(p => p.ChildContent, childContent ?? (builder => builder.AddContent(0, "Default Content")))
            .Add(p => p.Class, @class ?? "")
            .Add(p => p.Style, style ?? "")
        );
    }

    [Fact]
    public void Renders_Frame_With_Default_Properties()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        cut.Find(".panel-container").ShouldNotBeNull();
        cut.Find(".hui-frame").ShouldNotBeNull();
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
    public void Applies_Custom_Class_When_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(@class: "custom-class");

        // Assert
        var frame = cut.Find(".hui-frame");
        frame.GetAttribute("class").ShouldContain("custom-class");
        frame.GetAttribute("class").ShouldContain("hui-frame");
    }

    [Fact]
    public void Applies_Custom_Style_When_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(style: "background-color: red;");

        // Assert
        var frame = cut.Find(".hui-frame");
        frame.GetAttribute("style").ShouldContain("background-color: red;");
    }

    [Fact]
    public void Frame_Container_Has_Correct_Classes()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var container = cut.Find(".panel-container");
        container.ShouldNotBeNull();
    }

    [Fact]
    public void Frame_Element_Has_Correct_Base_Class()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var frame = cut.Find(".hui-frame");
        frame.GetAttribute("class").ShouldContain("hui-frame");
    }

    [Fact]
    public void Combines_Custom_Class_With_Base_Class()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(@class: "extra-class");

        // Assert
        var frame = cut.Find(".hui-frame");
        var classes = frame.GetAttribute("class");
        classes.ShouldContain("hui-frame");
        classes.ShouldContain("extra-class");
    }

    [Fact]
    public void Handles_Multiple_Custom_Classes()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(@class: "class1 class2 class3");

        // Assert
        var frame = cut.Find(".hui-frame");
        var classes = frame.GetAttribute("class");
        classes.ShouldContain("hui-frame");
        classes.ShouldContain("class1");
        classes.ShouldContain("class2");
        classes.ShouldContain("class3");
    }

    [Fact]
    public void Handles_Complex_Styles()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(style: "width: 100%; height: 200px; border: 1px solid black;");

        // Assert
        var frame = cut.Find(".hui-frame");
        frame.GetAttribute("style").ShouldContain("width: 100%");
        frame.GetAttribute("style").ShouldContain("height: 200px");
        frame.GetAttribute("style").ShouldContain("border: 1px solid black");
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
