using Bunit;
using Shouldly;
using Hedin.UI.Tests.Base;
using MudBlazor;
using Microsoft.AspNetCore.Components;

namespace Hedin.UI.Tests.Components.HUIStatusChipTests;

public class HUIStatusChipTests : UiTestBase
{
    private IRenderedComponent<HUIStatusChip> RenderComponentWithParams(
        Severity severity = Severity.Normal,
        RenderFragment? childContent = null)
    {
        return RenderComponent<HUIStatusChip>(parameters => parameters
            .Add(p => p.Severity, severity)
            .Add(p => p.ChildContent, childContent)
        );
    }

    [Fact]
    public void Renders_Child_Content()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(
            childContent: builder => builder.AddContent(0, "Status Text")
        );

        // Assert
        cut.Markup.ShouldContain("Status Text");
    }

    [Fact]
    public void Icon_Has_Correct_Properties()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var icon = cut.FindComponent<MudIcon>();
        icon.Instance.Size.ShouldBe(Size.Small);
        icon.Instance.Icon.ShouldBe(Icons.Material.Filled.Circle);
        icon.Instance.Style.ShouldContain("width: 0.5rem");
        icon.Instance.Style.ShouldContain("height: 0.5rem");
    }

    [Fact]
    public void Container_Has_Correct_Classes()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var container = cut.Find("span");
        container.GetAttribute("class").ShouldContain("d-flex");
        container.GetAttribute("class").ShouldContain("gap-2");
        container.GetAttribute("class").ShouldContain("align-center");
    }
}
