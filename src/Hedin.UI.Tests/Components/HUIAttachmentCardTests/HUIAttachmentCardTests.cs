using Bunit;
using Shouldly;
using Microsoft.AspNetCore.Components;
using Hedin.UI.Tests.Base;

namespace Hedin.UI.Tests.Components.HUIAttachmentCardTests;

public class HUIAttachmentCardTests : UiTestBase
{
    private IRenderedComponent<HUIAttachmentCard> RenderComponentWithParams(
        RenderFragment? buttons = null,
        RenderFragment? fileInformation = null,
        string? imgSrc = null,
        EventCallback? onViewClick = null,
        EventCallback? onRemoveClick = null)
    {
        return RenderComponent<HUIAttachmentCard>(parameters => parameters
            .Add(p => p.Buttons, buttons)
            .Add(p => p.FileInformation, fileInformation)
            .Add(p => p.ImgSrc, imgSrc)
            .Add(p => p.OnViewClick, onViewClick ?? EventCallback.Factory.Create(this, () => { }))
            .Add(p => p.OnRemoveClick, onRemoveClick ?? EventCallback.Factory.Create(this, () => { }))
        );
    }

    [Fact]
    public void Renders_AttachmentCard_With_Default_Properties()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        cut.Find(".hui-attachment-card").ShouldNotBeNull();
        cut.Find(".mud-card").ShouldNotBeNull();
        cut.Markup.ShouldContain("Attached file");
        cut.Markup.ShouldContain("File information");
    }

    [Fact]
    public void Renders_Image_When_Image_Source_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(imgSrc: "/test-image.jpg");

        // Assert
        var image = cut.Find("img[src='/test-image.jpg']");
        image.ShouldNotBeNull();
        image.GetAttribute("width").ShouldBe("196");
        image.GetAttribute("class").ShouldContain("file-paper");
    }

    [Fact]
    public void Renders_Custom_Buttons_When_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(
            buttons: builder => builder.AddContent(0, "Custom Button")
        );

        // Assert
        cut.Markup.ShouldContain("Custom Button");
    }

    [Fact]
    public void Renders_Custom_File_Information_When_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(
            fileInformation: builder => builder.AddContent(0, "File size: 1MB")
        );

        // Assert
        cut.Markup.ShouldContain("File size: 1MB");
    }

    [Fact]
    public void Shows_View_Button_When_OnViewClick_Has_Delegate()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(
            onViewClick: EventCallback.Factory.Create(this, () => { })
        );

        // Assert
        cut.Markup.ShouldContain("View content");
        cut.FindAll("button").Count.ShouldBeGreaterThan(0);
    }

    [Fact]
    public void Shows_Remove_Button_When_OnRemoveClick_Has_Delegate()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(
            onRemoveClick: EventCallback.Factory.Create(this, () => { })
        );

        // Assert
        cut.Markup.ShouldContain("Remove");
        cut.FindAll("button").Count.ShouldBeGreaterThan(0);
    }

    [Fact]
    public void Shows_Both_Buttons_When_Both_Delegates_Provided()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(
            onViewClick: EventCallback.Factory.Create(this, () => { }),
            onRemoveClick: EventCallback.Factory.Create(this, () => { })
        );

        // Assert
        cut.Markup.ShouldContain("View content");
        cut.Markup.ShouldContain("Remove");
        cut.FindAll("button").Count.ShouldBe(2);
    }

    [Fact]
    public void View_Button_Click_Invokes_OnViewClick_Callback()
    {
        // Arrange
        var viewClicked = false;
        var cut = RenderComponentWithParams(
            onViewClick: EventCallback.Factory.Create(this, () => viewClicked = true)
        );

        // Act
        var viewButton = cut.Find("button:contains('View content')");
        viewButton.Click();

        // Assert
        viewClicked.ShouldBeTrue();
    }

    [Fact]
    public void Remove_Button_Click_Invokes_OnRemoveClick_Callback()
    {
        // Arrange
        var removeClicked = false;
        var cut = RenderComponentWithParams(
            onRemoveClick: EventCallback.Factory.Create(this, () => removeClicked = true)
        );

        // Act
        var removeButton = cut.Find("button:contains('Remove')");
        removeButton.Click();

        // Assert
        removeClicked.ShouldBeTrue();
    }

    [Fact]
    public void File_Paper_Click_Invokes_OnViewClick_Callback()
    {
        // Arrange
        var viewClicked = false;
        var cut = RenderComponentWithParams(
            onViewClick: EventCallback.Factory.Create(this, () => viewClicked = true)
        );

        // Act
        cut.Find(".file-paper").Click();

        // Assert
        viewClicked.ShouldBeTrue();
    }

    [Fact]
    public void Image_Click_Invokes_OnViewClick_Callback()
    {
        // Arrange
        var viewClicked = false;
        var cut = RenderComponentWithParams(
            imgSrc: "/test-image.jpg",
            onViewClick: EventCallback.Factory.Create(this, () => viewClicked = true)
        );

        // Act
        cut.Find("img").Click();

        // Assert
        viewClicked.ShouldBeTrue();
    }

    [Fact]
    public void Renders_File_Info_Section_With_Correct_Classes()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        cut.Find(".file-info").ShouldNotBeNull();
        cut.Find(".mud-text-secondary").ShouldNotBeNull();
    }

    [Fact]
    public void Renders_Buttons_Section_With_Correct_Layout()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(
            onViewClick: EventCallback.Factory.Create(this, () => { }),
            onRemoveClick: EventCallback.Factory.Create(this, () => { })
        );

        // Assert
        cut.Find(".card-actions").ShouldNotBeNull();
        cut.FindAll("button").Count.ShouldBe(2);
    }
}
