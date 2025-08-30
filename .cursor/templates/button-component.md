# Hedin UI Button Component Template

## Component: {{ComponentName}} (Button Type)

### 1. Main Component File
**File:** `src/Hedin.UI/Components/{{ComponentName}}/HUI{{ComponentName}}.razor`

```razor
@namespace Hedin.UI
@inherits MudButton

<MudButton Disabled="Loading || Disabled"
           OnClick="base.OnClick"
           Variant="base.Variant"
           Color="base.Color"
           StartIcon="@base.StartIcon"
           EndIcon="@base.EndIcon"
           ButtonType="base.ButtonType"
           Class="@base.Class"
           DropShadow="false"
           Style="@base.Style"
           Size="@base.Size"
           HtmlTag="@base.HtmlTag"
           Href="@base.Href"
           UserAttributes="@base.UserAttributes">
    @if (Loading)
    {
        <MudProgressCircular Class="mr-3" Color="Color.Info" Size="Size.Small" Indeterminate="true" />
    }
    @ChildContent
</MudButton>

@code {
    /// <summary>
    /// Shows a progress icon when Loading is true
    /// </summary>
    [Parameter]
    public bool Loading { get; set; }

    /// <summary>
    /// Whether the button is disabled
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }
}
```

### 2. Component Styles
**File:** `src/Hedin.UI/Components/{{ComponentName}}/HUI{{ComponentName}}.razor.scss`

```scss
// Add custom button styles here if needed
```

### 3. Demo Page
**File:** `src/Hedin.UI.Demo/Pages/Components/Buttons/{{ComponentName}}/{{ComponentName}}.razor`

```razor
@attribute [Route(PageRoute.{{ComponentName}})]
@attribute [HUIPageSettings("{{ComponentName}}")]

<DemoPageLayout TItem="{{ComponentName}}" HUIComponent="typeof(HUI{{ComponentName}})">
    <DemoExample ComponentType="typeof(Hedin.UI.Demo.Pages.Components.Buttons.{{ComponentName}}.Examples.Primary)">
        <MudText>Primary {{component-name}}</MudText>
    </DemoExample>
    <DemoExample ComponentType="typeof(Hedin.UI.Demo.Pages.Components.Buttons.{{ComponentName}}.Examples.Secondary)">
        <MudText>Secondary {{component-name}}</MudText>
    </DemoExample>
    <DemoExample ComponentType="typeof(Hedin.UI.Demo.Pages.Components.Buttons.{{ComponentName}}.Examples.Loading)">
        <MudText>Loading {{component-name}}</MudText>
    </DemoExample>
</DemoPageLayout>
```

### 4. Demo Examples
**File:** `src/Hedin.UI.Demo/Pages/Components/Buttons/{{ComponentName}}/Examples/Primary.razor`

```razor
<HUI{{ComponentName}} Color="Color.Primary" Variant="Variant.Filled">
    Primary {{ComponentName}}
</HUI{{ComponentName}}>
```

**File:** `src/Hedin.UI.Demo/Pages/Components/Buttons/{{ComponentName}}/Examples/Secondary.razor`

```razor
<HUI{{ComponentName}} Color="Color.Secondary" Variant="Variant.Outlined">
    Secondary {{ComponentName}}
</HUI{{ComponentName}}>
```

**File:** `src/Hedin.UI.Demo/Pages/Components/Buttons/{{ComponentName}}/Examples/Loading.razor`

```razor
<HUI{{ComponentName}} Loading="true" Color="Color.Info">
    Loading {{ComponentName}}
</HUI{{ComponentName}}>
```

### 5. Test File
**File:** `src/Hedin.UI.Tests/Components/HUI{{ComponentName}}Tests/HUI{{ComponentName}}Tests.cs`

```csharp
using Bunit;
using Shouldly;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using Hedin.UI.Tests.Base;

namespace Hedin.UI.Tests.Components.HUI{{ComponentName}}Tests;

public class HUI{{ComponentName}}Tests : UiTestBase
{
    [Fact]
    public void Renders_ChildContent_When_NotLoading()
    {
        // Arrange & Act
        var cut = RenderComponent<HUI{{ComponentName}}>(parameters => parameters
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
        var cut = RenderComponent<HUI{{ComponentName}}>(parameters => parameters
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

        var cut = RenderComponent<HUI{{ComponentName}}>(parameters => parameters
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

        var cut = RenderComponent<HUI{{ComponentName}}>(parameters => parameters
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
        var cut = RenderComponent<HUI{{ComponentName}}>(parameters => parameters
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
```

### 6. PageRoute Addition
**File:** `src/Hedin.UI.Demo/PageRoute.cs`

Add this line in the Buttons section:

```csharp
public const string {{ComponentName}} = $"{Buttons}/{{component-name}}";
```

## Usage Instructions

1. Replace `{{ComponentName}}` with the actual button name (e.g., "SubmitButton", "ActionButton", "IconButton")
2. Replace `{{component-name}}` with the kebab-case version (e.g., "submit-button", "action-button", "icon-button")
3. Create all the files in the specified locations
4. Update the PageRoute.cs file with the new route
5. Build and test the component

## Button-Specific Notes

- Inherits from `MudButton` to get all MudBlazor button functionality
- Includes loading state with spinner
- Forwards all base button parameters
- Disables button when loading
- Follows existing button patterns in the project
