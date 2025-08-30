# Hedin UI Component Template

## Component: {{ComponentName}}

### 1. Main Component File
**File:** `src/Hedin.UI/Components/{{ComponentName}}/HUI{{ComponentName}}.razor`

```razor
@namespace Hedin.UI
@inherits MudComponentBase

<div class="hui-{{component-name}} @Class" @attributes="UserAttributes">
    @ChildContent
</div>

@code {
    /// <summary>
    /// Additional CSS classes to apply to the component
    /// </summary>
    [Parameter]
    public string Class { get; set; } = string.Empty;

    /// <summary>
    /// Additional attributes to apply to the component
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> UserAttributes { get; set; } = new();

    /// <summary>
    /// Content to render inside the component
    /// </summary>
    [Parameter]
    public RenderFragment ChildContent { get; set; } = default!;
}
```

### 2. Component Styles
**File:** `src/Hedin.UI/Components/{{ComponentName}}/HUI{{ComponentName}}.razor.scss`

```scss
.hui-{{component-name}} {
    // Add your custom styles here
}
```

### 3. Demo Page
**File:** `src/Hedin.UI.Demo/Pages/Components/{{Category}}/{{ComponentName}}/{{ComponentName}}.razor`

```razor
@attribute [Route(PageRoute.{{ComponentName}})]
@attribute [HUIPageSettings("{{ComponentName}}")]

<DemoPageLayout TItem="{{ComponentName}}" HUIComponent="typeof(HUI{{ComponentName}})">
    <DemoExample ComponentType="typeof(Hedin.UI.Demo.Pages.Components.{{Category}}.{{ComponentName}}.Examples.Primary)">
        <MudText>Primary {{component-name}}</MudText>
    </DemoExample>
    <DemoExample ComponentType="typeof(Hedin.UI.Demo.Pages.Components.{{Category}}.{{ComponentName}}.Examples.Secondary)">
        <MudText>Secondary {{component-name}}</MudText>
    </DemoExample>
</DemoPageLayout>
```

### 4. Demo Examples
**File:** `src/Hedin.UI.Demo/Pages/Components/{{Category}}/{{ComponentName}}/Examples/Primary.razor`

```razor
<HUI{{ComponentName}} Class="primary-{{component-name}}">
    Primary {{ComponentName}} Content
</HUI{{ComponentName}}>
```

**File:** `src/Hedin.UI.Demo/Pages/Components/{{Category}}/{{ComponentName}}/Examples/Secondary.razor`

```razor
<HUI{{ComponentName}} Class="secondary-{{component-name}}">
    Secondary {{ComponentName}} Content
</HUI{{ComponentName}}>
```

### 5. Test File
**File:** `src/Hedin.UI.Tests/Components/HUI{{ComponentName}}Tests/HUI{{ComponentName}}Tests.cs`

```csharp
using Bunit;
using Shouldly;
using Microsoft.AspNetCore.Components;
using Hedin.UI.Tests.Base;

namespace Hedin.UI.Tests.Components.HUI{{ComponentName}}Tests;

public class HUI{{ComponentName}}Tests : UiTestBase
{
    [Fact]
    public void Renders_ChildContent_Correctly()
    {
        // Arrange & Act
        var cut = RenderComponent<HUI{{ComponentName}}>(parameters => parameters
            .AddChildContent("<span>Test content</span>")
        );

        // Assert
        cut.Markup.ShouldContain("Test content");
    }

    [Fact]
    public void Applies_Custom_Class_When_Provided()
    {
        // Arrange & Act
        var cut = RenderComponent<HUI{{ComponentName}}>(parameters => parameters
            .Add(p => p.Class, "custom-class")
            .AddChildContent("Content")
        );

        // Assert
        var element = cut.Find(".hui-{{component-name}}");
        element.ClassList.ShouldContain("custom-class");
    }

    [Fact]
    public void Renders_With_Default_Classes()
    {
        // Arrange & Act
        var cut = RenderComponent<HUI{{ComponentName}}>(parameters => parameters
            .AddChildContent("Content")
        );

        // Assert
        var element = cut.Find(".hui-{{component-name}}");
        element.ShouldNotBeNull();
    }
}
```

### 6. PageRoute Addition
**File:** `src/Hedin.UI.Demo/PageRoute.cs`

Add this line in the appropriate section:

```csharp
public const string {{ComponentName}} = $"{Category}/{{component-name}}";
```

## Usage Instructions

1. Replace `{{ComponentName}}` with the actual component name (e.g., "Card", "Alert", "Badge")
2. Replace `{{component-name}}` with the kebab-case version (e.g., "card", "alert", "badge")
3. Replace `{{Category}}` with the appropriate category (e.g., "Cards", "Alerts", "Badges")
4. Create all the files in the specified locations
5. Update the PageRoute.cs file with the new route
6. Build and test the component

## Notes

- Always use the HUI prefix for component names
- Follow the existing naming conventions
- Ensure all files are created in the correct locations
- Test the component thoroughly before committing
- Update documentation if needed
