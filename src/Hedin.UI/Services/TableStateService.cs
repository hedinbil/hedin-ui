using Hedin.UI.Services;
using MudBlazor;

internal interface ITableStateService
{
    Task<bool> LoadStateAsync<T>(string id, MudDataGrid<T> grid);
    Dictionary<string, int> GetColumnOrder<T>(MudDataGrid<T> grid);
    void ApplyColumnOrder<T>(MudDataGrid<T> grid, Dictionary<string, int> newOrder);
    Dictionary<string, bool> GetColumnVisibility<T>(MudDataGrid<T> grid);
    void ApplyColumnVisibility<T>(MudDataGrid<T> grid, Dictionary<string, bool> visibility);
    Task UpdateState<T>(string id, MudDataGrid<T> grid);
}

internal class TableStateService : ITableStateService
{
    private readonly ILocalStorageSettingsService localStorage;

    public TableStateService(ILocalStorageSettingsService localStorage)
    {
        this.localStorage = localStorage;
    }

    public Dictionary<string, int> GetColumnOrder<T>(MudDataGrid<T> grid)
    {
        var columnOrder = new Dictionary<string, int>();
        for (int i = 0; i < grid.RenderedColumns.Count; i++)
        {
            var title = grid.RenderedColumns[i].Title;
            if (string.IsNullOrWhiteSpace(title))
            {
                title = $"Column_{i}";
            }
            columnOrder[title] = i;
        }
        return columnOrder;
    }

    public void ApplyColumnOrder<T>(MudDataGrid<T> grid, Dictionary<string, int> newOrder)
    {
        try
        {
            var columnsWithTitle = grid.RenderedColumns
            .Where(c => !string.IsNullOrWhiteSpace(c.Title))
            .ToList();

            var columnsWithoutTitle = grid.RenderedColumns
                .Where(c => string.IsNullOrWhiteSpace(c.Title))
                .ToList();

            var reorderedColumnsWithTitle = (from sourceColumn in columnsWithTitle
                                             join order in newOrder on sourceColumn.Title equals order.Key
                                             orderby order.Value
                                             select sourceColumn).ToList();

            var finalColumns = new List<Column<T>>();

            int indexWithTitle = 0;
            int indexWithoutTitle = 0;

            for (int i = 0; i < grid.RenderedColumns.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(grid.RenderedColumns[i].Title))
                {
                    finalColumns.Add(columnsWithoutTitle[indexWithoutTitle]);
                    indexWithoutTitle++;
                }
                else
                {
                    finalColumns.Add(reorderedColumnsWithTitle[indexWithTitle]);
                    indexWithTitle++;
                }
            }

            grid.RenderedColumns.Clear();
            grid.RenderedColumns.AddRange(finalColumns);
        }
        catch (Exception e)
        {

        }
    }

    public Dictionary<string, bool> GetColumnVisibility<T>(MudDataGrid<T> grid)
    {
        var visibility = new Dictionary<string, bool>();
        for (int i = 0; i < grid.RenderedColumns.Count; i++)
        {
            var title = grid.RenderedColumns[i].Title;
            if (string.IsNullOrWhiteSpace(title))
            {
                title = $"Column_{i}";
            }
            visibility[title] = grid.RenderedColumns[i].Hidden;
        }
        return visibility;
    }

    public void ApplyColumnVisibility<T>(MudDataGrid<T> grid, Dictionary<string, bool> visibility)
    {
        for (int i = 0; i < grid.RenderedColumns.Count; i++)
        {
            var column = grid.RenderedColumns[i];
            var title = column.Title;
            if (string.IsNullOrWhiteSpace(title))
            {
                title = $"Column_{i}";
            }

            if (visibility.TryGetValue(title, out var hidden))
            {
                if (hidden)
                    column.HideAsync();
                else
                    column.ShowAsync();
                column.Hidden = hidden;
                column.HiddenChanged.InvokeAsync(hidden);
            }
            else
            {
                column.Hidden = false;
            }
        }
    }

    public async Task<bool> LoadStateAsync<T>(string id, MudDataGrid<T> grid)
    {
        try
        {
            var settings = await localStorage.GetSettings();
            var currentSettings = settings?.TableSettings?.FirstOrDefault(x => x.TableId == id);

            if (currentSettings != null)
            {
                var columnOrder = currentSettings.ColumnOrder;
                var columnVisibility = currentSettings.ColumnVisibility;

                if (columnOrder?.Any() ?? false)
                {
                    // Detect and handle changes in columns
                    var currentOrder = GetColumnOrder(grid);
                    var newOrder = HandleColumnChanges(currentOrder, columnOrder);

                    ApplyColumnOrder(grid, newOrder);
                    ApplyColumnVisibility(grid, columnVisibility ?? new Dictionary<string, bool>());
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    private Dictionary<string, int> HandleColumnChanges(Dictionary<string, int> currentOrder, Dictionary<string, int> savedOrder)
    {
        // Handle newly added columns
        foreach (var kvp in currentOrder)
        {
            if (!savedOrder.ContainsKey(kvp.Key))
            {
                savedOrder[kvp.Key] = kvp.Value;
            }
        }

        // Handle removed columns
        var keysToRemove = savedOrder.Keys.Except(currentOrder.Keys).ToList();
        foreach (var key in keysToRemove)
        {
            savedOrder.Remove(key);
        }

        // Ensure savedOrder values are continuous and unique
        int index = 0;
        foreach (var key in savedOrder.Keys.ToList())
        {
            savedOrder[key] = index++;
        }

        return savedOrder;
    }

    public async Task UpdateState<T>(string id, MudDataGrid<T> grid)
    {
        try
        {
            var columnOrder = GetColumnOrder(grid);
            var columnVisibility = GetColumnVisibility(grid);

            var settings = await localStorage.GetSettings();

            if (settings == null)
            {
                settings = new();
            }

            var tableSettings = settings.TableSettings?.FirstOrDefault(x => x.TableId == id);

            if (tableSettings != null)
            {
                tableSettings.ColumnOrder = columnOrder;
                tableSettings.ColumnVisibility = columnVisibility;
            }
            else
            {
                tableSettings = new HUILocalStorageTableSetting
                {
                    TableId = id!,
                    ColumnOrder = columnOrder,
                    ColumnVisibility = columnVisibility
                };
                settings.TableSettings.Add(tableSettings);
            }

            await localStorage.SetSettings(settings);
        }
        catch (Exception e)
        {
            //In some scenarios, specially when debugging, writing to local storage does not work.
        }
    }
}
