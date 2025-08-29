//using Bunit;
//using Shouldly;
//using Microsoft.AspNetCore.Components;
//using Hedin.UI.Tests.Base;
//using Hedin.UI.Internal;
//using MudBlazor;
//using Microsoft.Extensions.DependencyInjection;

//namespace Hedin.UI.Tests.Components.HUIInformationMessageTests;

//public class HUIInformationMessageTests : UiTestBase
//{
//    private IRenderedComponent<HUIInformationMessage> RenderComponentWithParams(
//        HUIInformationMessageModel? model = null,
//        EventCallback<HUIInformationMessageModel>? onReadClick = null,
//        EventCallback<HUIInformationMessageModel>? onArchiveClick = null,
//        EventCallback<HUIInformationMessageModel>? onEditClick = null,
//        bool disabled = false,
//        bool showEdit = false)
//    {


//        return RenderComponentWithMudProviders<HUIInformationMessage>(parameters => parameters
//            .Add(p => p.Model, model)
//            .Add(p => p.OnReadClick, onReadClick ?? EventCallback.Factory.Create<HUIInformationMessageModel>(this, _ => { }))
//            .Add(p => p.OnArchiveClick, onArchiveClick ?? EventCallback.Factory.Create<HUIInformationMessageModel>(this, _ => { }))
//            .Add(p => p.OnEditClick, onEditClick ?? EventCallback.Factory.Create<HUIInformationMessageModel>(this, _ => { }))
//            .Add(p => p.Disabled, disabled)
//            .Add(p => p.ShowEdit, showEdit)
//        );
//    }

//    [Fact]
//    public void Renders_Nothing_When_Model_Is_Null()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(model: null);

//        // Assert
//        cut.Markup.ShouldBeEmpty();
//    }

//    [Fact]
//    public void Renders_InformationMessage_When_Model_Provided()
//    {
//        // Arrange
//        var model = new HUIInformationMessageModel
//        {
//            Header = "Test Header",
//            Message = "Test Message",
//            Date = DateTime.Now,
//            Severity = Severity.Info
//        };

//        // Act
//        var cut = RenderComponentWithParams(model: model);

//        // Assert
//        cut.Find(".hui-information-message").ShouldNotBeNull();
//        cut.Markup.ShouldContain("Test Header");
//        cut.Markup.ShouldContain("Test Message");
//    }

//    [Fact]
//    public void Renders_Date_When_Model_Provided()
//    {
//        // Arrange
//        var date = new DateTime(2023, 1, 15);
//        var model = new HUIInformationMessageModel
//        {
//            Header = "Test",
//            Message = "Test",
//            Date = date,
//            Severity = Severity.Info
//        };

//        // Act
//        var cut = RenderComponentWithParams(model: model);

//        // Assert
//        cut.Markup.ShouldContain(date.ToShortDateString());
//    }

//    [Fact]
//    public void Renders_Pin_Icon_When_Model_Is_Pinned()
//    {
//        // Arrange
//        var model = new HUIInformationMessageModel
//        {
//            Header = "Test",
//            Message = "Test",
//            Date = DateTime.Now,
//            Severity = Severity.Info,
//            Pinned = true
//        };

//        // Act
//        var cut = RenderComponentWithParams(model: model);

//        // Assert
//        cut.FindAll("i[class*='mud-icon']").Count.ShouldBeGreaterThan(0);
//    }

//    [Fact]
//    public void Does_Not_Render_Pin_Icon_When_Model_Is_Not_Pinned()
//    {
//        // Arrange
//        var model = new HUIInformationMessageModel
//        {
//            Header = "Test",
//            Message = "Test",
//            Date = DateTime.Now,
//            Severity = Severity.Info,
//            Pinned = false
//        };

//        // Act
//        var cut = RenderComponentWithParams(model: model);

//        // Assert
//        cut.FindAll("i[class*='mud-icon']").Count.ShouldBe(0);
//    }

//    [Fact]
//    public void Renders_Edit_Link_When_ShowEdit_True()
//    {
//        // Arrange
//        var model = new HUIInformationMessageModel
//        {
//            Header = "Test",
//            Message = "Test",
//            Date = DateTime.Now,
//            Severity = Severity.Info
//        };

//        // Act
//        var cut = RenderComponentWithParams(model: model, showEdit: true);

//        // Assert
//        cut.Find("a").ShouldNotBeNull();
//    }

//    [Fact]
//    public void Does_Not_Render_Edit_Link_When_ShowEdit_False()
//    {
//        // Arrange
//        var model = new HUIInformationMessageModel
//        {
//            Header = "Test",
//            Message = "Test",
//            Date = DateTime.Now,
//            Severity = Severity.Info
//        };

//        // Act
//        var cut = RenderComponentWithParams(model: model, showEdit: false);

//        // Assert
//        cut.FindAll("a").Count.ShouldBe(0);
//    }

//    [Fact]
//    public void Applies_Correct_Severity_Class_For_Warning()
//    {
//        // Arrange
//        var model = new HUIInformationMessageModel
//        {
//            Header = "Test",
//            Message = "Test",
//            Date = DateTime.Now,
//            Severity = Severity.Warning
//        };

//        // Act
//        var cut = RenderComponentWithParams(model: model);

//        // Assert
//        var messageDiv = cut.Find(".hui-information-message");
//        messageDiv.GetAttribute("class").ShouldContain("hui-border-severity-warning");
//    }

//    [Fact]
//    public void Applies_Correct_Severity_Class_For_Error()
//    {
//        // Arrange
//        var model = new HUIInformationMessageModel
//        {
//            Header = "Test",
//            Message = "Test",
//            Date = DateTime.Now,
//            Severity = Severity.Error
//        };

//        // Act
//        var cut = RenderComponentWithParams(model: model);

//        // Assert
//        var messageDiv = cut.Find(".hui-information-message");
//        messageDiv.GetAttribute("class").ShouldContain("hui-border-severity-error");
//    }

//    [Fact]
//    public void Applies_Correct_Severity_Class_For_Info()
//    {
//        // Arrange
//        var model = new HUIInformationMessageModel
//        {
//            Header = "Test",
//            Message = "Test",
//            Date = DateTime.Now,
//            Severity = Severity.Info
//        };

//        // Act
//        var cut = RenderComponentWithParams(model: model);

//        // Assert
//        var messageDiv = cut.Find(".hui-information-message");
//        messageDiv.GetAttribute("class").ShouldContain("hui-border-severity-info");
//    }

//    [Fact]
//    public void Applies_Correct_Severity_Class_For_Success()
//    {
//        // Arrange
//        var model = new HUIInformationMessageModel
//        {
//            Header = "Test",
//            Message = "Test",
//            Date = DateTime.Now,
//            Severity = Severity.Success
//        };

//        // Act
//        var cut = RenderComponentWithParams(model: model);

//        // Assert
//        var messageDiv = cut.Find(".hui-information-message");
//        messageDiv.GetAttribute("class").ShouldContain("hui-border-severity-success");
//    }

//    [Fact]
//    public void Shows_Read_Verification_When_Required_And_Not_Read()
//    {
//        // Arrange
//        var model = new HUIInformationMessageModel
//        {
//            Header = "Test",
//            Message = "Test",
//            Date = DateTime.Now,
//            Severity = Severity.Info,
//            RequireReadVerification = true,
//            Read = false
//        };

//        // Act
//        var cut = RenderComponentWithParams(model: model);

//        // Assert
//        cut.Markup.ShouldContain("I have read this notification");
//    }

//    [Fact]
//    public void Shows_Marked_As_Read_When_Required_And_Read()
//    {
//        // Arrange
//        var model = new HUIInformationMessageModel
//        {
//            Header = "Test",
//            Message = "Test",
//            Date = DateTime.Now,
//            Severity = Severity.Info,
//            RequireReadVerification = true,
//            Read = true
//        };

//        // Act
//        var cut = RenderComponentWithParams(model: model);

//        // Assert
//        cut.Markup.ShouldContain("Marked as read");
//    }

//    [Fact]
//    public void Shows_Archive_Link_When_Not_Archived()
//    {
//        // Arrange
//        var model = new HUIInformationMessageModel
//        {
//            Header = "Test",
//            Message = "Test",
//            Date = DateTime.Now,
//            Severity = Severity.Info,
//            Archived = false
//        };

//        // Act
//        var cut = RenderComponentWithParams(model: model);

//        // Assert
//        cut.Markup.ShouldContain("Move to archive");
//    }

//    [Fact]
//    public void Does_Not_Show_Archive_Link_When_Archived()
//    {
//        // Arrange
//        var model = new HUIInformationMessageModel
//        {
//            Header = "Test",
//            Message = "Test",
//            Date = DateTime.Now,
//            Severity = Severity.Info,
//            Archived = true
//        };

//        // Act
//        var cut = RenderComponentWithParams(model: model);

//        // Assert
//        cut.Markup.ShouldNotContain("Move to archive");
//    }

//    [Fact]
//    public void Applies_Background_Color_When_RequireReadVerification_And_Not_Read()
//    {
//        // Arrange
//        var model = new HUIInformationMessageModel
//        {
//            Header = "Test",
//            Message = "Test",
//            Date = DateTime.Now,
//            Severity = Severity.Info,
//            RequireReadVerification = true,
//            Read = false
//        };

//        // Act
//        var cut = RenderComponentWithParams(model: model);

//        // Assert
//        var messageDiv = cut.Find(".hui-information-message");
//        messageDiv.GetAttribute("style").ShouldContain("background-color: var(--mud-palette-gray-default);");
//    }

//    [Fact]
//    public void Does_Not_Apply_Background_Color_When_RequireReadVerification_And_Read()
//    {
//        // Arrange
//        var model = new HUIInformationMessageModel
//        {
//            Header = "Test",
//            Message = "Test",
//            Date = DateTime.Now,
//            Severity = Severity.Info,
//            RequireReadVerification = true,
//            Read = true
//        };

//        // Act
//        var cut = RenderComponentWithParams(model: model);

//        // Assert
//        var messageDiv = cut.Find(".hui-information-message");
//        messageDiv.GetAttribute("style").ShouldNotContain("background-color: var(--mud-palette-gray-default);");
//    }

//    [Fact]
//    public void Does_Not_Show_Actions_When_Disabled()
//    {
//        // Arrange
//        var model = new HUIInformationMessageModel
//        {
//            Header = "Test",
//            Message = "Test",
//            Date = DateTime.Now,
//            Severity = Severity.Info,
//            RequireReadVerification = true,
//            Read = false
//        };

//        // Act
//        var cut = RenderComponentWithParams(model: model, disabled: true);

//        // Assert
//        cut.FindAll("a").Count.ShouldBe(0);
//    }

//    [Fact]
//    public void Header_Has_Correct_Typography()
//    {
//        // Arrange
//        var model = new HUIInformationMessageModel
//        {
//            Header = "Test Header",
//            Message = "Test",
//            Date = DateTime.Now,
//            Severity = Severity.Info
//        };

//        // Act
//        var cut = RenderComponentWithParams(model: model);

//        // Assert
//        var header = cut.Find("h2");
//        header.ShouldNotBeNull();
//        header.GetAttribute("class").ShouldContain("hui-text-semibold");
//        header.GetAttribute("class").ShouldContain("hui-text-s");
//    }

//    [Fact]
//    public void Message_Has_Correct_Typography()
//    {
//        // Arrange
//        var model = new HUIInformationMessageModel
//        {
//            Header = "Test",
//            Message = "Test Message",
//            Date = DateTime.Now,
//            Severity = Severity.Info
//        };

//        // Act
//        var cut = RenderComponentWithParams(model: model);

//        // Assert
//        var message = cut.Find("p");
//        message.ShouldNotBeNull();
//        message.GetAttribute("style").ShouldContain("white-space: pre-line");
//        message.GetAttribute("class").ShouldContain("hui-text-s");
//    }

//    [Fact]
//    public void Date_Has_Correct_Typography()
//    {
//        // Arrange
//        var model = new HUIInformationMessageModel
//        {
//            Header = "Test",
//            Message = "Test",
//            Date = DateTime.Now,
//            Severity = Severity.Info
//        };

//        // Act
//        var cut = RenderComponentWithParams(model: model);

//        // Assert
//        var date = cut.Find("p[class*='mud-text-secondary']");
//        date.ShouldNotBeNull();
//        date.GetAttribute("class").ShouldContain("hui-text-xs");
//    }

//    [Fact]
//    public void Container_Has_Correct_Classes()
//    {
//        // Arrange
//        var model = new HUIInformationMessageModel
//        {
//            Header = "Test",
//            Message = "Test",
//            Date = DateTime.Now,
//            Severity = Severity.Info
//        };

//        // Act
//        var cut = RenderComponentWithParams(model: model);

//        // Assert
//        var container = cut.Find(".hui-information-message");
//        container.GetAttribute("class").ShouldContain("pl-3");
//        container.GetAttribute("class").ShouldContain("pr-1");
//        container.GetAttribute("class").ShouldContain("py-1");
//    }
//}
