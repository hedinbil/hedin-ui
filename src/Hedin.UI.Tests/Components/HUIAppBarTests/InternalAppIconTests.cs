using Bunit;
using Hedin.UI.Internal;
using Hedin.UI.Tests.Base;
using Shouldly;

namespace Hedin.UI.Tests.Components.HUIAppBarTests;

public class InternalAppIconTests : UiTestBase
{
    [Fact]
    public void Given_DefaultAppIcon_When_Rendered_Then_UsesExistingNavigationBehavior()
    {
        // Arrange & Act
        var cut = Render<InternalAppIcon>(parameters => parameters
            .Add(p => p.Icon, "icons/app.svg")
            .Add(p => p.AppName, "Portal")
            .Add(p => p.AppUrl, "/portal"));

        // Assert
        cut.FindAll("a").ShouldBeEmpty();
        cut.Find(".hui-app-icon-container").ShouldNotBeNull();
    }

    [Fact]
    public void Given_AppIconWithOpenInNewTab_When_Rendered_Then_RendersBlankTargetLink()
    {
        // Arrange & Act
        var cut = Render<InternalAppIcon>(parameters => parameters
            .Add(p => p.Icon, "icons/app.svg")
            .Add(p => p.AppName, "Portal")
            .Add(p => p.AppUrl, "https://portal.example.com")
            .Add(p => p.OpenInNewTab, true));

        // Assert
        var link = cut.Find("a");
        link.GetAttribute("href").ShouldBe("https://portal.example.com");
        link.GetAttribute("target").ShouldBe("_blank");
        link.GetAttribute("rel").ShouldBe("noopener noreferrer");
    }
}
