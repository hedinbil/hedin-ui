# Hedin UI Cursor Templates

This directory contains templates and rules for generating new Hedin UI components using Cursor.

## Files Overview

### `.cursor/rules/` Directory
The rules directory contains organized guidelines for different component types:

- **`component-generation.md`** - Main rules for all component generation
- **`button-components.md`** - Specific rules for button components
- **`general-components.md`** - Rules for general components (cards, alerts, etc.)

### `templates/` Directory
The templates directory contains ready-to-use component templates:

- **`component.md`** - Generic template for basic components
- **`button-component.md`** - Specialized template for button components

## How to Use

### 1. Using the Rules Files
The rules files in `.cursor/rules/` will automatically guide Cursor when you ask it to create new components. They ensure:
- Proper file structure and naming conventions
- Correct namespaces and inheritance
- All necessary files are created
- Consistent patterns across the project

### 2. Using the Templates
When creating a new component, you can reference these templates:

#### For Basic Components
Use `templates/component.md` when creating components like:
- Cards
- Alerts
- Badges
- Containers
- Layout components

#### For Button Components
Use `templates/button-component.md` when creating components like:
- Submit buttons
- Action buttons
- Icon buttons
- Any component that should behave like a button

### 3. Example Usage

When chatting with Cursor, you can say:

> "Create a new HUICard component using the component template"

Or:

> "Create a new HUISubmitButton component using the button template"

Cursor will then:
1. Read the appropriate rules and templates
2. Generate all necessary files
3. Follow the project conventions
4. Create proper folder structures
5. Add routes to PageRoute.cs
6. Generate comprehensive tests

## Template Variables

The templates use these placeholder variables:

- `{{ComponentName}}` - The component name (e.g., "Card", "SubmitButton")
- `{{component-name}}` - Kebab-case version (e.g., "card", "submit-button")
- `{{Category}}` - The category folder (e.g., "Cards", "Buttons")

## File Structure Generated

Each component will create:

```
src/Hedin.UI/Components/{ComponentName}/
├── HUI{ComponentName}.razor
└── HUI{ComponentName}.razor.scss

src/Hedin.UI.Demo/Pages/Components/{Category}/{ComponentName}/
├── {ComponentName}.razor
└── Examples/
    ├── Primary.razor
    ├── Secondary.razor
    └── Loading.razor (for buttons)

src/Hedin.UI.Tests/Components/HUI{ComponentName}Tests/
└── HUI{ComponentName}Tests.cs
```

## Best Practices

1. **Always use the HUI prefix** for component names
2. **Follow existing naming conventions** for files and folders
3. **Test thoroughly** before committing
4. **Update documentation** if needed
5. **Use appropriate base classes** (MudButton, MudComponentBase, etc.)
6. **Include comprehensive tests** for all functionality

## Customization

You can modify these templates to:
- Add new component types
- Change inheritance patterns
- Modify file structures
- Add new example types
- Customize test patterns

## Troubleshooting

If Cursor doesn't follow the rules:
1. Ensure rules are in `.cursor/rules/` directory
2. Check that templates are in `.cursor/templates/`
3. Restart Cursor to reload rules
4. Verify file paths are correct

## Need Help?

If you need to create a component type not covered by these templates, you can:
1. Create a new template file
2. Modify existing templates
3. Ask Cursor to follow the rules manually
4. Reference existing components as examples

## Modern Cursor Standards

This setup follows the current Cursor best practices:
- Rules are stored in `.cursor/rules/` directory
- Templates are stored in `.cursor/templates/` directory
- No deprecated `.cursorrules` files
- Compatible with latest Cursor versions
- Organized rules for different component types
