using Bunit;
using Shouldly;
using Microsoft.AspNetCore.Components;
using Hedin.UI.Tests.Base;
using Hedin.UI.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Hedin.UI.Tests.Components.HUIBlogPostTests;

public class HUIBlogPostTests : UiTestBase
{
    private IRenderedComponent<HUIBlogPost> RenderComponentWithParams(
        string? header = "Test Header",
        string? subHeader = "Test SubHeader",
        string bodyText = "Test body text",
        RenderFragment? headerContent = null,
        string? imgSrc = null,
        string? imgAlt = null,
        bool imgLazyLoad = false,
        int maxBodyLength = 300)
    {
        return RenderComponent<HUIBlogPost>(parameters => parameters
            .Add(p => p.Header, header)
            .Add(p => p.SubHeader, subHeader)
            .Add(p => p.BodyText, bodyText)
            .Add(p => p.HeaderContent, headerContent)
            .Add(p => p.ImgSrc, imgSrc)
            .Add(p => p.ImgAlt, imgAlt)
            .Add(p => p.ImgLazyLoad, imgLazyLoad)
            .Add(p => p.MaxBodyLength, maxBodyLength)
        );
    }

    [Fact]
    public void Renders_BlogPost_With_Required_Parameters()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        cut.Markup.ShouldContain("Test Header");
        cut.Markup.ShouldContain("Test SubHeader");
        cut.Markup.ShouldContain("Test body text");
    }

    [Fact]
    public void Renders_Header_With_H1_Typography()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var header = cut.Find("h1");
        header.ShouldNotBeNull();
        header.TextContent.ShouldContain("Test Header");
    }

    [Fact]
    public void Renders_SubHeader_With_H2_Typography()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var subHeader = cut.Find("h2");
        subHeader.ShouldNotBeNull();
        subHeader.TextContent.ShouldContain("Test SubHeader");
        subHeader.GetAttribute("class").ShouldContain("hui-text-semibold");
    }

    [Fact]
    public void Renders_HeaderContent_When_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(
            headerContent: builder => builder.AddContent(0, "Custom Header Content")
        );

        // Assert
        cut.Markup.ShouldContain("Custom Header Content");
    }

    [Fact]
    public void Renders_Image_When_ImgSrc_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(imgSrc: "/test-image.jpg", imgAlt: "Test Image");

        // Assert
        var image = cut.Find("img[src='/test-image.jpg']");
        image.ShouldNotBeNull();
        image.GetAttribute("alt").ShouldBe("Test Image");
        image.GetAttribute("style").ShouldContain("min-width: 100%");
        image.GetAttribute("style").ShouldContain("max-height:350px");
    }

    [Fact]
    public void Renders_Skeleton_When_ImgLazyLoad_True_And_No_ImgSrc()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(imgLazyLoad: true);

        // Assert
        cut.Find(".mud-skeleton").ShouldNotBeNull();
        cut.FindAll("img").Count.ShouldBe(0);
    }

    [Fact]
    public void Does_Not_Render_Image_Or_Skeleton_When_No_ImgSrc_And_ImgLazyLoad_False()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(imgLazyLoad: false);

        // Assert
        cut.FindAll("img").Count.ShouldBe(0);
        cut.FindAll(".mud-skeleton").Count.ShouldBe(0);
    }

    [Fact]
    public void Renders_BodyText_With_PreLine_Style()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var bodyText = cut.Find("p");
        bodyText.ShouldNotBeNull();
        bodyText.GetAttribute("style").ShouldContain("white-space: pre-line");
    }

    [Fact]
    public void Shows_ShowMore_Link_When_BodyText_Exceeds_MaxLength()
    {
        // Arrange
        var longBodyText = new string('a', 400); // 400 characters, exceeds default 300

        // Act
        var cut = RenderComponentWithParams(bodyText: longBodyText);

        // Assert
        cut.Markup.ShouldContain("Show More");
        cut.Find("a").ShouldNotBeNull();
    }

    [Fact]
    public void Does_Not_Show_ShowMore_Link_When_BodyText_Within_MaxLength()
    {
        // Arrange
        var shortBodyText = "Short text"; // Well within 300 character limit

        // Act
        var cut = RenderComponentWithParams(bodyText: shortBodyText);

        // Assert
        cut.Markup.ShouldNotContain("Show More");
        cut.FindAll("a").Count.ShouldBe(0);
    }

    [Fact]
    public void Truncates_BodyText_When_Exceeds_MaxLength()
    {
        // Arrange
        var longBodyText = new string('a', 400);
        var cut = RenderComponentWithParams(bodyText: longBodyText);

        // Act
        var bodyTextElement = cut.Find("p");

        // Assert
        bodyTextElement.TextContent.ShouldContain("...");
        bodyTextElement.TextContent.Length.ShouldBe(303); // 300 + "..."
    }

    [Fact]
    public void ShowMore_Click_Toggles_Between_Show_More_And_Less()
    {
        // Arrange
        var longBodyText = new string('a', 400);
        var cut = RenderComponentWithParams(bodyText: longBodyText);

        // Act & Assert - Initially shows "Show More"
        cut.Markup.ShouldContain("Show More");

        // Click to expand
        cut.Find("a").Click();
        cut.Markup.ShouldContain("Show Less");

        // Click to collapse
        cut.Find("a").Click();
        cut.Markup.ShouldContain("Show More");
    }

    [Fact]
    public void ShowMore_Click_Expands_And_Collapses_BodyText()
    {
        // Arrange
        var longBodyText = new string('a', 400);
        var cut = RenderComponentWithParams(bodyText: longBodyText);

        // Act & Assert - Initially truncated
        var bodyTextElement = cut.Find("p");
        bodyTextElement.TextContent.Length.ShouldBe(303); // 300 + "..."

        // Click to expand
        cut.Find("a").Click();
        bodyTextElement = cut.Find("p");
        bodyTextElement.TextContent.Length.ShouldBe(400); // Full text

        // Click to collapse
        cut.Find("a").Click();
        bodyTextElement = cut.Find("p");
        bodyTextElement.TextContent.Length.ShouldBe(303); // Truncated again
    }

    [Fact]
    public void Respects_Custom_MaxBodyLength()
    {
        // Arrange
        var customMaxLength = 100;
        var bodyText = new string('a', 150); // Exceeds custom max length

        // Act
        var cut = RenderComponentWithParams(bodyText: bodyText, maxBodyLength: customMaxLength);

        // Assert
        cut.Markup.ShouldContain("Show More");
        var bodyTextElement = cut.Find("p");
        bodyTextElement.TextContent.Length.ShouldBe(103); // 100 + "..."
    }

    [Fact]
    public void Renders_Divider_At_Bottom()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        cut.Find(".mud-divider").ShouldNotBeNull();
    }

    [Fact]
    public void Container_Has_Correct_Classes()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var container = cut.Find(".mud-container");
        container.GetAttribute("class").ShouldContain("pa-0");
        container.GetAttribute("class").ShouldContain("ma-0");
    }

    [Fact]
    public void Header_And_SubHeader_Have_Correct_Margins()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var header = cut.Find("h1");
        var subHeader = cut.Find("h2");
        
        header.GetAttribute("class").ShouldContain("mb-3");
        subHeader.GetAttribute("class").ShouldContain("mb-3");
    }

    [Fact]
    public void BodyText_Has_Correct_Margin()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var bodyText = cut.Find("p");
        bodyText.GetAttribute("class").ShouldContain("mb-3");
    }

    [Fact]
    public void ShowMore_Link_Has_Correct_Styling()
    {
        // Arrange
        var longBodyText = new string('a', 400);
        var cut = RenderComponentWithParams(bodyText: longBodyText);

        // Act
        var showMoreLink = cut.Find("a");

        // Assert
        showMoreLink.GetAttribute("class").ShouldContain("mb-3");
        showMoreLink.GetAttribute("class").ShouldContain("mud-link");
    }
}
