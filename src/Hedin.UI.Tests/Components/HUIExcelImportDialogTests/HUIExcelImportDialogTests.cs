using Microsoft.Extensions.DependencyInjection;
using Bunit;
using Shouldly;
using Hedin.UI.Tests.Base;
using MudBlazor;
using Microsoft.AspNetCore.Components.Forms;

namespace Hedin.UI.Tests.Components.HUIExcelImportDialogTests;

public class HUIExcelImportDialogTests : UiTestBase
{
    private IRenderedComponent<HUIExcelImportDialog> RenderComponentWithParams(
        string cancelText = "Cancel",
        string okText = "Confirm",
        string clearText = "Clear",
        string dragDropText = "Drag and drop files here or click to open",
        bool multipleFiles = false)
    {
        return Render<HUIExcelImportDialog>(parameters => parameters
            .Add(p => p.CancelText, cancelText)
            .Add(p => p.OkText, okText)
            .Add(p => p.ClearText, clearText)
            .Add(p => p.DragDropText, dragDropText)
            .Add(p => p.MultipleFiles, multipleFiles)
        );
    }

    [Fact]
    public void Renders_Dialog()
    {
        // Arrange & Act
        var cut = RenderComponentWithParams();

        // Assert
        cut.FindComponent<MudDialog>().ShouldNotBeNull();
    }

    [Fact]
    public async Task Renders_DropZone_With_DragDropText()
    {
        // Arrange
        var provider = Render<MudDialogProvider>();
        var dialogService = Services.GetRequiredService<IDialogService>();

        // Act
        await provider.InvokeAsync(() => dialogService.ShowAsync<HUIExcelImportDialog>("Import",
            new DialogParameters<HUIExcelImportDialog> { { p => p.DragDropText, "Drop it here" } }));

        // Assert: the drop zone must be rendered inside the file upload, not swallowed as an unknown attribute
        var upload = provider.FindComponent<MudFileUpload<IReadOnlyList<IBrowserFile>>>();
        upload.FindComponent<MudPaper>().Markup.ShouldContain("Drop it here");
    }
}
