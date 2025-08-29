using Bunit;
using Shouldly;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Hedin.UI.Tests.Base;
using MudBlazor;

namespace Hedin.UI.Tests.Components.HUIChatPanelTests;

public class HUIChatPanelTests : UiTestBase
{
    private IRenderedComponent<HUIChatPanel> RenderComponentWithParams(
        List<HUIChatPanel.HUIChatLine>? conversation = null,
        Func<string, CancellationToken, Task>? onSendMessage = null,
        string title = "Chat",
        string emptyStateText = "Use AI to find out anything we know",
        RenderFragment? buttons = null,
        RenderFragment<HUIChatPanel.HUIChatLine>? assistantTemplate = null)
    {
        return RenderComponentWithMudProviders<HUIChatPanel>(parameters => parameters
            .Add(p => p.Conversation, conversation ?? new List<HUIChatPanel.HUIChatLine>())
            .Add(p => p.OnSendMessage, onSendMessage ?? ((_, _) => Task.CompletedTask))
            .Add(p => p.Title, title)
            .Add(p => p.EmptyStateText, emptyStateText)
            .Add(p => p.Buttons, buttons)
            .Add(p => p.AssistantTemplate, assistantTemplate)
        );
    }

    [Fact]
    public void Renders_Custom_Title()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(title: "Custom Chat Title");

        // Assert
        cut.Markup.ShouldContain("Custom Chat Title");
    }

    [Fact]
    public void Renders_Custom_EmptyStateText()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(emptyStateText: "No messages yet");

        // Assert
        cut.Markup.ShouldContain("No messages yet");
    }

    [Fact]
    public void Renders_Empty_State_When_No_Conversation()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        cut.Markup.ShouldContain("Use AI to find out anything we know");
        cut.FindAll(".mud-card").Count.ShouldBe(0);
    }

    [Fact]
    public void Renders_User_Message_When_Conversation_Has_User_Line()
    {
        // Arrange
        var conversation = new List<HUIChatPanel.HUIChatLine>
        {
            new HUIChatPanel.HUIChatLine(HUIChatPanel.HUIChatRole.User, "Hello, how are you?")
        };

        // Act
        var cut = RenderComponentWithParams(conversation: conversation);

        // Assert
        cut.Markup.ShouldContain("Hello, how are you?");
        cut.FindAll(".mud-card").Count.ShouldBe(1);
    }


    [Fact]
    public void Renders_Custom_Assistant_Template_When_Provided()
    {
        // Arrange
        var conversation = new List<HUIChatPanel.HUIChatLine>
        {
            new HUIChatPanel.HUIChatLine(HUIChatPanel.HUIChatRole.Assistant, "Custom message")
        };

        var assistantTemplate = new RenderFragment<HUIChatPanel.HUIChatLine>(line => 
            builder => builder.AddContent(0, $"Custom: {line.Text}"));

        // Act
        var cut = RenderComponentWithParams(
            conversation: conversation,
            assistantTemplate: assistantTemplate
        );

        // Assert
        cut.Markup.ShouldContain("Custom: Custom message");
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
    public void Send_Button_Is_Disabled_When_Input_Empty()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var sendButton = cut.Find("button");
        sendButton.HasAttribute("disabled").ShouldBeTrue();
    }


    [Fact]
    public void User_Messages_Are_Right_Aligned()
    {
        // Arrange
        var conversation = new List<HUIChatPanel.HUIChatLine>
        {
            new HUIChatPanel.HUIChatLine(HUIChatPanel.HUIChatRole.User, "User message")
        };

        // Act
        var cut = RenderComponentWithParams(conversation: conversation);

        // Assert
        var userMessageContainer = cut.Find(".d-flex.justify-end");
        userMessageContainer.ShouldNotBeNull();
    }

    [Fact]
    public void Assistant_Messages_Are_Left_Aligned()
    {
        // Arrange
        var conversation = new List<HUIChatPanel.HUIChatLine>
        {
            new HUIChatPanel.HUIChatLine(HUIChatPanel.HUIChatRole.Assistant, "Assistant message")
        };

        // Act
        var cut = RenderComponentWithParams(conversation: conversation);

        // Assert
        var assistantMessageContainer = cut.Find(".d-flex.justify-start");
        assistantMessageContainer.ShouldNotBeNull();
    }

    [Fact]
    public void User_Message_Cards_Have_Correct_Styling()
    {
        // Arrange
        var conversation = new List<HUIChatPanel.HUIChatLine>
        {
            new HUIChatPanel.HUIChatLine(HUIChatPanel.HUIChatRole.User, "User message")
        };

        // Act
        var cut = RenderComponentWithParams(conversation: conversation);

        // Assert
        var userCard = cut.Find(".mud-card");
        userCard.GetAttribute("style").ShouldContain("max-width:80%");
        userCard.GetAttribute("style").ShouldContain("background-color:var(--mud-palette-gray-lighter)");
    }

    [Fact]
    public void Conversation_Container_Has_Correct_Classes()
    {
        // Arrange
        var conversation = new List<HUIChatPanel.HUIChatLine>
        {
            new HUIChatPanel.HUIChatLine(HUIChatPanel.HUIChatRole.User, "Test message")
        };

        // Act
        var cut = RenderComponentWithParams(conversation: conversation);

        // Assert
        var container = cut.Find("section");
        container.GetAttribute("class").ShouldContain("d-flex");
        container.GetAttribute("class").ShouldContain("flex-column");
        container.GetAttribute("class").ShouldContain("gap-2");
        container.GetAttribute("class").ShouldContain("px-3");
    }


    [Fact]
    public void Header_Container_Has_Correct_Classes()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        var headerContainer = cut.Find("div").QuerySelector("div:first-child");
        headerContainer.GetAttribute("class").ShouldContain("d-flex");
        headerContainer.GetAttribute("class").ShouldContain("align-center");
        headerContainer.GetAttribute("class").ShouldContain("justify-space-between");
        headerContainer.GetAttribute("class").ShouldContain("pa-3");
    }
}
