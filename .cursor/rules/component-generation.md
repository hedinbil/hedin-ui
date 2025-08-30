# Hedin UI Component Generation Rules

When creating a new Hedin UI component, follow these rules to ensure consistency and generate all necessary files:

## Component Structure

### 1. Main Component File (Hedin.UI/Components/{ComponentName}/HUI{ComponentName}.razor)
- Place in: `src/Hedin.UI/Components/{ComponentName}/HUI{ComponentName}.razor`
- Namespace: `Hedin.UI`
- Inherit from appropriate base class (usually MudBlazor component)
- Use HUI prefix for component name
- Include proper XML documentation for parameters

### 2. Component Styles (Hedin.UI/Components/{ComponentName}/HUI{ComponentName}.razor.scss)
- Place in: `src/Hedin.UI/Components/{ComponentName}/HUI{ComponentName}.razor.scss`
- Keep empty if no custom styles needed initially

### 3. Demo Page (Hedin.UI.Demo/Pages/Components/{Category}/{ComponentName}/{ComponentName}.razor)
- Place in: `src/Hedin.UI.Demo/Pages/Components/{Category}/{ComponentName}/{ComponentName}.razor`
- Use `DemoPageLayout` with proper component type
- Include multiple examples showing different use cases
- Add route attribute with PageRoute constant

### 4. Demo Examples (Hedin.UI.Demo/Pages/Components/{Category}/{ComponentName}/Examples/)
- Place in: `src/Hedin.UI.Demo/Pages/Components/{Category}/{ComponentName}/Examples/`
- Create separate .razor files for each example (Primary.razor, Secondary.razor, etc.)
- Keep examples simple and focused

### 5. Test File (Hedin.UI.Tests/Components/HUI{ComponentName}Tests/HUI{ComponentName}Tests.cs)
- Place in: `src/Hedin.UI.Tests/Components/HUI{ComponentName}Tests/HUI{ComponentName}Tests.cs`
- Inherit from `UiTestBase`
- Test all parameters and behaviors
- Use Bunit for testing
- Follow existing test patterns

### 6. PageRoute Addition
- Add new route constant to `src/Hedin.UI.Demo/PageRoute.cs`
- Follow naming convention: `{ComponentName} = $"{Category}/{component-name}"`

## File Naming Conventions

- Component files: `HUI{ComponentName}.razor`
- Demo pages: `{ComponentName}.razor`
- Test files: `HUI{ComponentName}Tests.cs`
- Folders: Use PascalCase for component names, kebab-case for routes

## Required Attributes

### Demo Pages
- `@attribute [Route(PageRoute.{ComponentName})]`
- `@attribute [HUIPageSettings("{ComponentName}")]`

### Components
- `@namespace Hedin.UI`
- Proper inheritance from base component
- XML documentation for all public parameters

## Example Component Generation

When asked to create a new component, generate ALL of these files:

1. **Main component** with proper namespace and inheritance
2. **SCSS file** (even if empty)
3. **Demo page** with examples
4. **Example files** showing different use cases
5. **Test file** with comprehensive tests
6. **PageRoute addition** for navigation

## Common Base Classes

- For buttons: inherit from `MudButton`
- For inputs: inherit from `MudTextField<T>` or similar
- For containers: inherit from `MudComponentBase`
- For dialogs: inherit from `MudDialog`

## Parameter Guidelines

- Always include `ChildContent` parameter for content
- Use proper MudBlazor types (Color, Variant, Size, etc.)
- Include loading states where appropriate
- Forward common base component parameters

## Testing Guidelines

- Test all parameters render correctly
- Test loading states and disabled states
- Test event callbacks
- Test parameter forwarding to base components
- Use descriptive test names following pattern: `MethodName_ExpectedBehavior_When_Condition`
