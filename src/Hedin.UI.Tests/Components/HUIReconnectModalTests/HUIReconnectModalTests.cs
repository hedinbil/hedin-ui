using Bunit;
using Shouldly;
using Hedin.UI.Tests.Base;
using MudBlazor;
using Microsoft.AspNetCore.Components;

namespace Hedin.UI.Tests.Components.HUIReconnectModalTests;

public class HUIReconnectModalTests : UiTestBase
{
    private IRenderedComponent<HUIReconnectModal> RenderComponentWithParams(
        RenderFragment? childContent = null)
    {
        return RenderComponentWithMudProviders<HUIReconnectModal>(parameters => parameters
            .Add(p => p.ChildContent, childContent)
        );
    }

    [Fact]
    public void Renders_Container_With_Correct_ID()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        cut.Find("#components-reconnect-modal").ShouldNotBeNull();
    }



    [Fact]
    public void Renders_Default_Content_When_No_Child_Content()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        cut.FindComponent<MudProgressCircular>().ShouldNotBeNull();
        cut.Markup.ShouldContain("There was a problem with the connection!");
        cut.Markup.ShouldContain("Current reconnect attempt:");
        cut.Markup.ShouldContain("components-reconnect-current-attempt");
        cut.Markup.ShouldContain("components-reconnect-max-retries");
    }

    [Fact]
    public void Renders_Custom_Child_Content_When_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(
            childContent: builder => builder.AddContent(0, "Custom Reconnect Message")
        );

        // Assert
        cut.Markup.ShouldContain("Custom Reconnect Message");
        cut.FindAll(".mud-progress-circular").Count.ShouldBe(0);
        cut.Markup.ShouldNotContain("There was a problem with the connection!");
    }

    [Fact]
    public void Progress_Circular_Has_Correct_Properties()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var progress = cut.FindComponent<MudProgressCircular>();
        progress.Instance.Color.ShouldBe(Color.Primary);
        progress.Instance.Indeterminate.ShouldBeTrue();
    }

    [Fact]
    public void Renders_Connection_Problem_Text()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        cut.Markup.ShouldContain("There was a problem with the connection!");
    }

    [Fact]
    public void Renders_Reconnect_Attempt_Text()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        cut.Markup.ShouldContain("Current reconnect attempt:");
    }

    [Fact]
    public void Renders_Current_Attempt_Span()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        cut.Find("#components-reconnect-current-attempt").ShouldNotBeNull();
    }

    [Fact]
    public void Renders_Max_Retries_Span()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        cut.Find("#components-reconnect-max-retries").ShouldNotBeNull();
    }

    [Fact]
    public void Renders_Forward_Slash_Separator()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        cut.Markup.ShouldContain("/");
    }

    [Fact]
    public void Renders_Parentheses_Around_Attempt_Info()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        cut.Markup.ShouldContain("(Current reconnect attempt:");
        cut.Markup.ShouldContain(")");
    }

    [Fact]
    public void Handles_Null_Child_Content()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(childContent: null);

        // Assert
        cut.FindComponent<MudProgressCircular>().ShouldNotBeNull();
        cut.Markup.ShouldContain("There was a problem with the connection!");
    }

    [Fact]
    public void Renders_HTML_Content_In_Child_Content()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(
            childContent: builder => builder.AddMarkupContent(0, "<div class='custom-content'>HTML Content</div>")
        );

        // Assert
        cut.Find(".custom-content").ShouldNotBeNull();
        cut.Markup.ShouldContain("HTML Content");
    }

    [Fact]
    public void Renders_Component_Content_In_Child_Content()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(
            childContent: builder => builder.AddContent(0, "Component Content")
        );

        // Assert
        cut.Markup.ShouldContain("Component Content");
    }

    [Fact]
    public void Renders_Multiple_Child_Content_Items()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(
            childContent: builder =>
            {
                builder.AddContent(0, "First Item");
                builder.AddContent(1, "Second Item");
                builder.AddContent(2, "Third Item");
            }
        );

        // Assert
        cut.Markup.ShouldContain("First Item");
        cut.Markup.ShouldContain("Second Item");
        cut.Markup.ShouldContain("Third Item");
    }

    [Fact]
    public void Renders_Complex_Child_Content_Structure()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(
            childContent: builder => builder.AddMarkupContent(0, 
                "<div class='reconnect-header'>" +
                "<h3>Connection Lost</h3>" +
                "<p>Attempting to reconnect...</p>" +
                "</div>")
        );

        // Assert
        cut.Find(".reconnect-header").ShouldNotBeNull();
        cut.Find("h3").ShouldNotBeNull();
        cut.Find("p").ShouldNotBeNull();
        cut.Markup.ShouldContain("Connection Lost");
        cut.Markup.ShouldContain("Attempting to reconnect...");
    }



    [Fact]
    public void Progress_Circular_Is_Indeterminate()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var progress = cut.FindComponent<MudProgressCircular>();
        progress.Instance.Indeterminate.ShouldBeTrue();
    }

    [Fact]
    public void Progress_Circular_Has_Primary_Color()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var progress = cut.FindComponent<MudProgressCircular>();
        progress.Instance.Color.ShouldBe(Color.Primary);
    }
}
