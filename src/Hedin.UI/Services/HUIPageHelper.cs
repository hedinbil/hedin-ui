using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace Hedin.UI.Services;

public class HUIPageHelper : IHUIPageHelper
{
    public async Task<List<HUIMenuItem>> GetMenuItems(Assembly[] assemblies)
    {
        var allMenuItems = new List<HUIMenuItem>();
        foreach (var assembly in assemblies)
        {
            var assemblyMenuItems = await GetMenuItemsFromAssembly(assembly);
            allMenuItems.AddRange(assemblyMenuItems);
        }
        var result = BuildMenuHierarchy(allMenuItems);
        return result;
    }

    private async Task<List<HUIMenuItem>> GetMenuItemsFromAssembly(Assembly assembly)
    {
        var components = assembly.ExportedTypes.Where(t => t.IsSubclassOf(typeof(ComponentBase)));
        var flatMenu = new List<HUIMenuItem>();

        foreach (var component in components)
        {
            var menuItem = GetRouteFromComponent(component);
            if (menuItem != null)
            {
                flatMenu.Add(menuItem);
            }
        }

        return flatMenu;
    }

    private List<HUIMenuItem> BuildMenuHierarchy(List<HUIMenuItem> flatMenu)
    {
        var rootItems = flatMenu.Where(x => string.IsNullOrEmpty(x.ParentUrl)).OrderBy(x => x.Order).ThenBy(x => x.DisplayName).ToList();

        foreach (var item in rootItems)
        {
            PopulateSubMenuItems(item, flatMenu);
        }

        // Set the Parent object for each menu item
        SetParentObjects(rootItems);

        return rootItems;
    }

    private void SetParentObjects(List<HUIMenuItem> items)
    {
        foreach (var item in items)
        {
            SetParentObject(item, items);
        }
    }

    private void SetParentObject(HUIMenuItem item, List<HUIMenuItem> flatMenu)
    {
        if (item.ParentUrl != null)
        {
            // Find the parent item in the flat menu by matching its URL
            var parentItem = FindItemByUrl(flatMenu, item.ParentUrl);
            if (parentItem != null)
            {
                item.Parent = parentItem;
            }
        }

        // Recursively set parent objects for sub-items
        foreach (var subItem in item.SubItems)
        {
            SetParentObject(subItem, flatMenu);
        }
    }

    private HUIMenuItem? FindItemByUrl(List<HUIMenuItem> items, string url)
    {
        foreach (var item in items)
        {
            if (item.Url == url)
            {
                return item;
            }

            var foundItem = FindItemByUrl(item.SubItems, url);
            if (foundItem != null)
            {
                return foundItem;
            }
        }

        return null;
    }

    private void PopulateSubMenuItems(HUIMenuItem parentItem, List<HUIMenuItem> flatMenu)
    {
        var subItems = flatMenu.Where(x => x.ParentUrl == parentItem.Url)
                               .OrderBy(x => x.Order)
                               .ThenBy(x => x.DisplayName)
                               .ToList();

        foreach (var subItem in subItems)
        {
            PopulateSubMenuItems(subItem, flatMenu);
            parentItem.SubItems.Add(subItem);
        }
    }

    private HUIMenuItem? GetRouteFromComponent(Type component)
    {
        var attributes = component.GetCustomAttributes(true);
        var routeAttribute = attributes.OfType<RouteAttribute>().FirstOrDefault();
        var settingsAttribute = attributes.OfType<HUIPageSettingsAttribute>().FirstOrDefault();
        var authorizeAttribute = attributes.OfType<AuthorizeAttribute>().FirstOrDefault();

        // Early exit if essential attributes are missing
        if (routeAttribute == null || settingsAttribute == null)
            return null;

        var route = routeAttribute.Template;
        if (string.IsNullOrEmpty(route))
            throw new Exception($"RouteAttribute in component '{component.Name}' has empty route template");

        // Get the parent route from the route template
        var parentRoute = GetParentRoute(route);

        var menuItem = new HUIMenuItem(
            displayName: settingsAttribute.DisplayName,
            url: route,
            parentUrl: parentRoute, // Set the parent URL
            policy: authorizeAttribute?.Policy ?? "",
            order: settingsAttribute.Order,
            icon: settingsAttribute.Icon,
            disabled: settingsAttribute.Disabled,
            id: settingsAttribute.Id
        );
        return menuItem;
    }

    private string? GetParentRoute(string route)
    {
        // Remove the last segment to get the parent route
        int lastIndex = route.LastIndexOf('/');
        if (lastIndex != -1)
        {
            return route.Substring(0, lastIndex);
        }

        return null; // If no parent route found
    }
}