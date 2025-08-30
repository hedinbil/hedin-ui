# Hedin UI Button Component Rules

## Button Component Specific Guidelines

### Base Class Inheritance
- **Always inherit from `MudButton`** for button components
- This provides all MudBlazor button functionality automatically

### Required Parameters
- Include `Loading` parameter for loading states
- Include `Disabled` parameter for disabled states
- Forward all base `MudButton` parameters using `base.PropertyName`

### Loading State Implementation
- Show `MudProgressCircular` when `Loading = true`
- Disable button when `Loading = true` using `Disabled="Loading || Disabled"`
- Position spinner with `Class="mr-3"` for proper spacing

### Parameter Forwarding
Use this pattern for all MudButton parameters:
```razor
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
```

### Demo Examples
Button components should include these example types:
1. **Primary** - Main button style
2. **Secondary** - Alternative button style  
3. **Loading** - Button in loading state

### Testing Requirements
Test these specific button behaviors:
- Loading state shows spinner and disables button
- Click events work when not loading
- Click events are blocked when loading
- All MudButton parameters are properly forwarded
- Custom classes are applied correctly

### File Structure
Button components follow this structure:
```
src/Hedin.UI/Components/{ComponentName}/HUI{ComponentName}.razor
src/Hedin.UI/Components/{ComponentName}/HUI{ComponentName}.razor.scss
src/Hedin.UI.Demo/Pages/Components/Buttons/{ComponentName}/{ComponentName}.razor
src/Hedin.UI.Demo/Pages/Components/Buttons/{ComponentName}/Examples/
src/Hedin.UI.Tests/Components/HUI{ComponentName}Tests/HUI{ComponentName}Tests.cs
```

### Route Structure
Button routes should be added to the Buttons section in PageRoute.cs:
```csharp
public const string {ComponentName} = $"{Buttons}/{component-name}";
```
