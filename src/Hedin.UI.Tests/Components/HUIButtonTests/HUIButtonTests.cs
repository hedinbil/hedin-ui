using Bunit;
using Shouldly;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using Hedin.UI.Tests.Base;

namespace Hedin.UI.Tests.Components.HUIButtonTests;

public class HUIButtonTests : UiTestBase
{
    [Fact]
    public void Renders_ChildContent_When_NotLoading()
    {
        // Arrange & Act
        var cut = RenderComponent<HUIButton>(parameters => parameters
            .Add(p => p.Loading, false)
            .AddChildContent("<span>Click me</span>")
        );

        // Assert
        cut.Markup.ShouldContain("Click me");
        cut.FindAll(".mud-progress-circular").Count.ShouldBe(0);
    }

    [Fact]
    public void Shows_Spinner_And_Disables_Button_When_Loading()
    {
        var cut = RenderComponent<HUIButton>(parameters => parameters
            .Add(p => p.Loading, true)
            .AddChildContent("Doing work")
        );

        // Spinner present
        cut.FindAll(".mud-progress-circular").Count.ShouldBe(1);

        // Button disabled
        var button = cut.Find("button");
        button.HasAttribute("disabled").ShouldBeTrue();

        // Child content rendered
        cut.Markup.ShouldContain("Doing work");
    }

    [Fact]
    public void Click_Invokes_OnClick_When_NotLoading()
    {
        var clicked = false;

        var cut = RenderComponent<HUIButton>(parameters => parameters
            .Add(p => p.Loading, false)
            .Add(p => p.OnClick, EventCallback.Factory.Create<MouseEventArgs>(this, () => clicked = true))
            .AddChildContent("Go")
        );

        cut.Find("button").Click();

        clicked.ShouldBeTrue();
    }

    [Fact]
    public void Click_Does_Not_Invoke_OnClick_When_Loading()
    {
        var clicked = false;

        var cut = RenderComponent<HUIButton>(parameters => parameters
            .Add(p => p.Loading, true)
            .Add(p => p.OnClick, EventCallback.Factory.Create<MouseEventArgs>(this, () => clicked = true))
            .AddChildContent("Wait…")
        );

        cut.Find("button").Click();

        clicked.ShouldBeFalse("button should be disabled while loading");
    }

    [Fact]
    public void Forwards_Common_MudButton_Parameters()
    {
        var cut = RenderComponent<HUIButton>(parameters => parameters
            .Add(p => p.Loading, false)
            .Add(p => p.Color, Color.Secondary)
            .Add(p => p.Variant, Variant.Filled)
            .Add(p => p.Class, "extra-class")
            .AddChildContent("Proxy")
        );

        var root = cut.Find("button");

        // Our extra class should be there
        root.ClassList.ShouldContain("extra-class");
    }
}
