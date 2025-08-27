# Contributing to Hedin.UI

First of all, thank you for considering contributing! 🎉  

This library is used by multiple products and **must remain generic**.  
➡️ **Do not add domain-specific logic, naming, or branding.**

---

## Contribution Workflow

1. **Create an Issue**
   - Before starting work, please create an issue in this repository describing the change or feature you intend to add.
   - This helps discuss scope and approach before implementation.

2. **Fork / Branch**
   - Fork or clone the repository.
   - Create a new branch for your change:
     ```
     git checkout -b feature/{short-description}
     ```

3. **Make Changes**
   - Keep code **generic and reusable**.
   - Follow existing **naming conventions** and **project style**.
   - If adding a new component, also add:
     - A demo page in **Hedin.UI.Demo**
     - A code example file, following the structure of existing demo pages.

4. **Versioning**
   - In Visual Studio, right-click the **Hedin.UI** project and update the **Package Version**.  
   - Builds will fail if this is skipped.

5. **Commit & Push**
   - Write clear, descriptive commit messages.
   - Push your branch to remote.

6. **Pull Request**
   - Open a PR against the `main` branch.
   - Link the PR to the related issue (e.g., `Closes #123`).
   - Request at least one reviewer.
   - Ensure your branch is up to date with `main`.

---

## Code Style & Standards

- Use **C# coding conventions** (see `.editorconfig`).
- All UI components should:
  - Be **themed** consistently with `HUITheme`.
  - Support **accessibility** where applicable.
  - Avoid product/brand references.
- Keep components **small, composable, and testable**.

---

## Testing

- Run and verify **Hedin.UI.Demo** before submitting.
- Add **unit tests** for logic where possible.
- Manually test new components in different themes/states.

---

## Questions / Discussions

If you’re unsure about scope, naming, or implementation, please open an **Issue**, **Discussion**, or a **draft PR** before proceeding.

---
