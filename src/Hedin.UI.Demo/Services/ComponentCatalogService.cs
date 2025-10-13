using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace Hedin.UI.Demo.Services;

public class ComponentCatalogService
{
    private readonly Assembly _demoAsm = typeof(ComponentCatalogService).Assembly;

    public IReadOnlyList<ComponentSummary> GetSummaries()
    {
        // 1) Find all example types
        var exampleTypes = _demoAsm.GetTypes()
            .Where(t =>
                typeof(ComponentBase).IsAssignableFrom(t) &&
                t.Namespace is not null &&
                t.Namespace.Split('.').Contains("Examples")
            );

        // 2) Group by the folder segment before “Examples”
        var groups = exampleTypes
            .GroupBy(t =>
            {
                var parts = t.Namespace!.Split('.');
                var idx = Array.IndexOf(parts, "Examples");
                return idx > 0 ? parts[idx - 1] : "(unknown)";
            });

        var summaries = new List<ComponentSummary>();

        foreach (var g in groups)
        {
            var compName = g.Key;                         // e.g. "Button"
            var exampleType = g.OrderBy(t => t.Name).First();

            // 3) Find the demo *page* type for that component
            //    by looking for [Route] under the same folder namespace
            var pageType = _demoAsm.GetTypes().FirstOrDefault(t =>
                t.GetCustomAttributes<RouteAttribute>().Any() &&
                t.Namespace is not null &&
                t.Namespace.Split('.').Contains(compName) &&
                !t.Namespace.Split('.').Contains("Examples")
            );

            // 4) Grab its route template, or fall back to "/{name}"
            var routeAttrs = pageType?.GetCustomAttributes<RouteAttribute>();
            var routeAttr = routeAttrs?.FirstOrDefault();
            var url = routeAttr?.Template.Replace("~", "").Trim()
                        ?? $"/{compName.ToLower()}";

            summaries.Add(new ComponentSummary(compName, exampleType, url));
        }

        return summaries;
    }
}

public record ComponentSummary(string ParentComponentName, Type ExampleType, string Url);

