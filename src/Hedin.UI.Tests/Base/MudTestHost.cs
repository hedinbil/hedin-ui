using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Microsoft.AspNetCore.Components.Authorization;

namespace Hedin.UI.Tests.Base;

public sealed class MudTestHost : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder b)
    {
        var s = 0;

        // Providers as siblings (self-closing / no ChildContent)
        b.OpenComponent(s++, typeof(MudThemeProvider));
        b.CloseComponent();

        b.OpenComponent(s++, typeof(MudDialogProvider));
        b.CloseComponent();

        b.OpenComponent(s++, typeof(MudSnackbarProvider));
        b.CloseComponent();

        b.OpenComponent(s++, typeof(MudPopoverProvider));
        b.CloseComponent();

        // Authentication state provider that wraps all child content
        b.OpenComponent(s++, typeof(CascadingAuthenticationState));
        b.AddAttribute(s++, "ChildContent", ChildContent);
        b.CloseComponent();
    }
}