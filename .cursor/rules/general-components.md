# Hedin UI General Component Rules

## General Component Guidelines

### Base Class Inheritance
- **For containers/layout**: Inherit from `MudComponentBase`
- **For inputs**: Inherit from `MudTextField<T>`, `MudSelect<T>`, etc.
- **For dialogs**: Inherit from `MudDialog`
- **For cards/panels**: Inherit from `MudComponentBase`

### Required Parameters
- Always include `ChildContent` parameter for content
- Include `Class` parameter for custom styling
- Use `UserAttributes` parameter with `CaptureUnmatchedValues = true`

### Parameter Implementation
Use this standard pattern:
```razor
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

### CSS Class Naming
- Use `hui-{component-name}` as the base class
- Apply custom classes: `class="hui-{component-name} @Class"`
- Use kebab-case for component names in CSS

### Demo Examples
General components should include these example types:
1. **Primary** - Main component style
2. **Secondary** - Alternative component style
3. **Variants** - Different visual variations if applicable

### Testing Requirements
Test these general behaviors:
- Child content renders correctly
- Custom classes are applied
- User attributes are forwarded
- Default styling is present

### File Structure
General components follow this structure:
```
src/Hedin.UI/Components/{ComponentName}/HUI{ComponentName}.razor
src/Hedin.UI/Components/{ComponentName}/HUI{ComponentName}.razor.scss
src/Hedin.UI.Demo/Pages/Components/{Category}/{ComponentName}/{ComponentName}.razor
src/Hedin.UI.Demo/Pages/Components/{Category}/{ComponentName}/Examples/
src/Hedin.UI.Tests/Components/HUI{ComponentName}Tests/HUI{ComponentName}Tests.cs
```

### Route Structure
Component routes should be added to the appropriate category section in PageRoute.cs:
```csharp
public const string {ComponentName} = $"{Category}/{component-name}";
```

### Category Guidelines
Choose appropriate categories based on component type:
- **Cards**: For card-like components
- **Alerts**: For notification/message components
- **Layout**: For container/layout components
- **Inputs**: For form input components
- **Navigation**: For menu/navigation components
- **Data**: For data display components
