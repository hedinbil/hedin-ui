using Bunit;
using Shouldly;
using Microsoft.AspNetCore.Components;
using Hedin.UI.Tests.Base;
using MudBlazor;

namespace Hedin.UI.Tests.Components.HUIDialogTests;

public class HUIDialogTests : UiTestBase
{
    private IRenderedComponent<HUIDialog> RenderComponentWithParams(
        EventCallback? onCancelClick = null,
        bool showUnsavedChanges = false,
        EventCallback<bool>? showUnsavedChangesChanged = null,
        RenderFragment? titleButtons = null)
    {
        return RenderComponent<HUIDialog>(parameters => parameters
            .Add(p => p.OnCancelClick, onCancelClick ?? EventCallback.Factory.Create(this, () => { }))
            .Add(p => p.ShowUnsavedChanges, showUnsavedChanges)
            .Add(p => p.ShowUnsavedChangesChanged, showUnsavedChangesChanged ?? EventCallback.Factory.Create<bool>(this, _ => { }))
            .Add(p => p.TitleButtons, titleButtons)
        );
    }


    [Fact]
    public void Shows_UnsavedChanges_Dialog_When_ShowUnsavedChanges_True()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(showUnsavedChanges: true);

        // Assert
        cut.Instance.ShowUnsavedChanges.ShouldBeTrue();
    }

    [Fact]
    public void Hides_UnsavedChanges_Dialog_When_ShowUnsavedChanges_False()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams(showUnsavedChanges: false);

        // Assert
        cut.Instance.ShowUnsavedChanges.ShouldBeFalse();
    }
}
