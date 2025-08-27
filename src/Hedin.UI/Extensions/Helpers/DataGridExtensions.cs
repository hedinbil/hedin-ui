using MudBlazor;
using System.Linq.Expressions;
using System.Reflection;

namespace Hedin.UI.Extensions.Helpers
{
    public static class DataGridExtensions
    {
        private static IQueryable<TSource> OrderByDescendingDynamic<TSource>(this IQueryable<TSource> source, string? propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                propertyName = typeof(TSource).GetProperties()
                    .FirstOrDefault()
                    ?.Name;
                if (string.IsNullOrEmpty(propertyName)) return source;
            }

            var parameter = Expression.Parameter(typeof(TSource), "x");
            var selector = Expression.PropertyOrField(parameter, propertyName);
            var lambda = Expression.Lambda(selector, parameter);
            var orderByDescendingMethod = typeof(Queryable).GetMethods()
                .FirstOrDefault(m => m.Name == "OrderByDescending" && m.GetParameters()
                    .Length == 2)
                ?.MakeGenericMethod(typeof(TSource), selector.Type);
            if (orderByDescendingMethod == null) return source;
            try
            {
                return (IQueryable<TSource>)orderByDescendingMethod.Invoke(null, new object[] { source, lambda })!;
            }
            catch
            {
                return source;
            }
        }
        public static IQueryable<TSource> ApplyPaging<TSource>(this IQueryable<TSource> query, GridState<TSource> gridQuery)
        {
            return query.Skip(gridQuery.Page * gridQuery.PageSize).Take(gridQuery.PageSize);
        }

        public static IEnumerable<TSource> ApplyPaging<TSource>(this IEnumerable<TSource> list, GridState<TSource> gridQuery)
        {
            return list.Skip(gridQuery.Page * gridQuery.PageSize).Take(gridQuery.PageSize);
        }

        public static IQueryable<TSource> ApplySorting<TSource>(this IQueryable<TSource> query, GridState<TSource> gridState)
        {
            if (gridState.SortDefinitions is { Count: > 0 })
            {
                var sortedDefinitions = gridState.SortDefinitions
                    .Where(sortDefinition => !string.IsNullOrEmpty(sortDefinition.SortBy))
                    .OrderBy(sd => sd.Index)
                    .ToList();

                return sortedDefinitions.Aggregate(query, (current, sortDefinition) => sortDefinition.Descending ? current.OrderByDescendingDynamic(sortDefinition.SortBy) : current.OrderByDynamic(sortDefinition.SortBy));
            }
            return query;
        }

        private static IQueryable<TSource> OrderByDynamic<TSource>(this IQueryable<TSource> source, string? propertyName, bool isDescending = false)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                propertyName = typeof(TSource).GetProperties()
                    .FirstOrDefault()
                    ?.Name;
                if (string.IsNullOrEmpty(propertyName)) return source;
            }

            var parameterExpression = Expression.Parameter(typeof(TSource), "x");
            var propertyInfo = typeof(TSource).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null)
            {
                return source;
            }
            var propertyAccess = Expression.MakeMemberAccess(parameterExpression, propertyInfo);
            var lambda = Expression.Lambda(propertyAccess, parameterExpression);
            var orderByMethodName = isDescending ? "OrderByDescending" : "OrderBy";
            var orderByMethod = typeof(Queryable).GetMethods()
                .FirstOrDefault(m => m.Name == orderByMethodName && m.GetParameters()
                    .Length == 2);
            orderByMethod = orderByMethod?.MakeGenericMethod(typeof(TSource), propertyInfo.PropertyType);
            if (orderByMethod == null) throw new InvalidOperationException($"Unable to find method '{orderByMethodName}' for type IQueryable<TSource>.");
            try
            {
                return (IQueryable<TSource>)orderByMethod.Invoke(null, new object[] { source, lambda })!;
            }
            catch
            {
                return source;
            }
        }
    }
}
