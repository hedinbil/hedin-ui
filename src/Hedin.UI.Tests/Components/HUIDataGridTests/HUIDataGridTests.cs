//using Bunit;
//using Shouldly;
//using Microsoft.AspNetCore.Components;
//using Microsoft.Extensions.DependencyInjection;
//using Hedin.UI.Tests.Base;
//using Hedin.UI.Services;
//using MudBlazor;
//using System.Globalization;

//namespace Hedin.UI.Tests.Components.HUIDataGridTests;

//public class HUIDataGridTests : UiTestBase
//{
//    private IRenderedComponent<HUIDataGrid<TestData>> RenderComponentWithParams(
//        IEnumerable<TestData>? items = null,
//        Func<GridState<TestData>, Task<GridData<TestData>>>? serverData = null,
//        bool loading = false,
//        bool filterable = false,
//        SortMode sortMode = SortMode.Multiple,
//        bool groupable = false,
//        bool hover = true,
//        int elevation = 1,
//        bool striped = true,
//        Func<TestData, int, string>? rowStyleFunc = null,
//        Func<TestData, int, string>? rowClassFunc = null,
//        bool dragDropColumnReordering = true,
//        ResizeMode columnResizeMode = ResizeMode.None,
//        bool showColumnOptions = false,
//        RenderFragment? columns = null,
//        string? rowClass = null,
//        string? rowStyle = null,
//        string? @class = null,
//        string? headerClass = null,
//        bool hideable = true,
//        string? height = null,
//        bool fixedHeader = true,
//        bool dense = false,
//        bool horizontalScrollbar = false,
//        string? footerClass = null,
//        RenderFragment? pagerContent = null,
//        RenderFragment? noRecordsContent = null,
//        RenderFragment? toolBarContent = null,
//        bool multiSelection = false,
//        HashSet<TestData>? selectedItems = null,
//        EventCallback<HashSet<TestData>>? selectedItemsChanged = null,
//        RenderFragment<CellContext<TestData>>? childRowContent = null,
//        bool selectOnRowClick = true,
//        TestData? selectedItem = null,
//        EventCallback<TestData>? selectedItemChanged = null,
//        DataGridEditMode editMode = DataGridEditMode.Cell,
//        EventCallback<TestData>? startedEditingItem = null,
//        EventCallback<TestData>? canceledEditingItem = null,
//        EventCallback<TestData>? committedItemChanges = null,
//        DataGridEditTrigger editTrigger = DataGridEditTrigger.Manual,
//        bool readOnly = true,
//        Breakpoint breakpoint = Breakpoint.Xs,
//        CultureInfo? culture = null,
//        bool virtualize = false,
//        Func<GridStateVirtualize<TestData>, CancellationToken, Task<GridData<TestData>>>? virtualizeServerData = null,
//        EventCallback<DataGridRowClickEventArgs<TestData>>? rowClick = null,
//        bool wrapCellContent = false,
//        bool enableSettingsMenu = false,
//        string? id = null,
//        EventCallback<TestData>? onRowClick = null,
//        bool showPager = true,
//        int rowsPerPage = 10,
//        EventCallback<int>? rowsPerPageChanged = null,
//        DialogOptions? editDialogOptions = null,
//        int currentPage = 0,
//        EventCallback<int>? currentPageChanged = null)
//    {
//        Services.AddSingleton<ITableStateService>(new TestTableStateService());
//        Services.AddSingleton<ILocalStorageSettingsService>(new TestLocalStorageSettingsService());

//        return RenderComponent<HUIDataGrid<TestData>>(parameters => parameters
//            .Add(p => p.Items, items)
//            .Add(p => p.ServerData, serverData)
//            .Add(p => p.Loading, loading)
//            .Add(p => p.Filterable, filterable)
//            .Add(p => p.SortMode, sortMode)
//            .Add(p => p.Groupable, groupable)
//            .Add(p => p.Hover, hover)
//            .Add(p => p.Elevation, elevation)
//            .Add(p => p.Striped, striped)
//            .Add(p => p.RowStyleFunc, rowStyleFunc)
//            .Add(p => p.RowClassFunc, rowClassFunc)
//            .Add(p => p.DragDropColumnReordering, dragDropColumnReordering)
//            .Add(p => p.ColumnResizeMode, columnResizeMode)
//            .Add(p => p.ShowColumnOptions, showColumnOptions)
//            .Add(p => p.Columns, columns ?? (builder => builder.AddContent(0, "Test Column")))
//            .Add(p => p.RowClass, rowClass)
//            .Add(p => p.RowStyle, rowStyle)
//            .Add(p => p.Class, @class)
//            .Add(p => p.HeaderClass, headerClass)
//            .Add(p => p.Hideable, hideable)
//            .Add(p => p.Height, height)
//            .Add(p => p.FixedHeader, fixedHeader)
//            .Add(p => p.Dense, dense)
//            .Add(p => p.HorizontalScrollbar, horizontalScrollbar)
//            .Add(p => p.FooterClass, footerClass)
//            .Add(p => p.PagerContent, pagerContent)
//            .Add(p => p.NoRecordsContent, noRecordsContent)
//            .Add(p => p.ToolBarContent, toolBarContent)
//            .Add(p => p.MultiSelection, multiSelection)
//            .Add(p => p.SelectedItems, selectedItems ?? new HashSet<TestData>())
//            .Add(p => p.SelectedItemsChanged, selectedItemsChanged ?? EventCallback.Factory.Create<HashSet<TestData>>(this, _ => { }))
//            .Add(p => p.ChildRowContent, childRowContent)
//            .Add(p => p.SelectOnRowClick, selectOnRowClick)
//            .Add(p => p.SelectedItem, selectedItem)
//            .Add(p => p.SelectedItemChanged, selectedItemChanged ?? EventCallback.Factory.Create<TestData>(this, _ => { }))
//            .Add(p => p.EditMode, editMode)
//            .Add(p => p.StartedEditingItem, startedEditingItem ?? EventCallback.Factory.Create<TestData>(this, _ => { }))
//            .Add(p => p.CanceledEditingItem, canceledEditingItem ?? EventCallback.Factory.Create<TestData>(this, _ => { }))
//            .Add(p => p.CommittedItemChanges, committedItemChanges ?? EventCallback.Factory.Create<TestData>(this, _ => { }))
//            .Add(p => p.EditTrigger, editTrigger)
//            .Add(p => p.ReadOnly, readOnly)
//            .Add(p => p.Breakpoint, breakpoint)
//            .Add(p => p.Culture, culture)
//            .Add(p => p.Virtualize, virtualize)
//            .Add(p => p.VirtualizeServerData, virtualizeServerData)
//            .Add(p => p.RowClick, rowClick ?? EventCallback.Factory.Create<DataGridRowClickEventArgs<TestData>>(this, _ => { }))
//            .Add(p => p.WrapCellContent, wrapCellContent)
//            .Add(p => p.EnableSettingsMenu, enableSettingsMenu)
//            .Add(p => p.Id, id)
//            .Add(p => p.OnRowClick, onRowClick ?? EventCallback.Factory.Create<TestData>(this, _ => { }))
//            .Add(p => p.ShowPager, showPager)
//            .Add(p => p.RowsPerPage, rowsPerPage)
//            .Add(p => p.RowsPerPageChanged, rowsPerPageChanged ?? EventCallback.Factory.Create<int>(this, _ => { }))
//            .Add(p => p.EditDialogOptions, editDialogOptions)
//            .Add(p => p.CurrentPage, currentPage)
//            .Add(p => p.CurrentPageChanged, currentPageChanged ?? EventCallback.Factory.Create<int>(this, _ => { }))
//        );
//    }

//    [Fact]
//    public void Renders_DataGrid_With_Default_Properties()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams();

//        // Assert
//        cut.Find(".hui-datagrid").ShouldNotBeNull();
//        cut.Find(".mud-data-grid").ShouldNotBeNull();
//    }

//    [Fact]
//    public void Renders_Columns_When_Provided()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(
//            columns: builder => builder.AddContent(0, "Custom Column")
//        );

//        // Assert
//        cut.Markup.ShouldContain("Custom Column");
//    }

//    [Fact]
//    public void Renders_Items_When_Provided()
//    {
//        // Arrange
//        var items = new List<TestData>
//        {
//            new TestData { Id = 1, Name = "Item 1" },
//            new TestData { Id = 2, Name = "Item 2" }
//        };

//        // Act
//        var cut = RenderComponentWithParams(items: items);

//        // Assert
//        cut.Markup.ShouldContain("Item 1");
//        cut.Markup.ShouldContain("Item 2");
//    }

//    [Fact]
//    public void Shows_Loading_State_When_Loading_True()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(loading: true);

//        // Assert
//        cut.Instance.Loading.ShouldBeTrue();
//    }

//    [Fact]
//    public void Applies_Custom_Class_When_Provided()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(@class: "custom-class");

//        // Assert
//        var dataGrid = cut.Find(".mud-data-grid");
//        dataGrid.GetAttribute("class").ShouldContain("custom-class");
//    }

//    [Fact]
//    public void Applies_Custom_HeaderClass_When_Provided()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(headerClass: "custom-header");

//        // Assert
//        var dataGrid = cut.Find(".mud-data-grid");
//        dataGrid.GetAttribute("class").ShouldContain("hui-datagrid-header");
//        dataGrid.GetAttribute("class").ShouldContain("custom-header");
//    }

//    [Fact]
//    public void Shows_Pager_When_ShowPager_True()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(showPager: true);

//        // Assert
//        cut.Instance.ShowPager.ShouldBeTrue();
//    }

//    [Fact]
//    public void Hides_Pager_When_ShowPager_False()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(showPager: false);

//        // Assert
//        cut.Instance.ShowPager.ShouldBeFalse();
//    }

//    [Fact]
//    public void Sets_Correct_RowsPerPage()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(rowsPerPage: 25);

//        // Assert
//        cut.Instance.RowsPerPage.ShouldBe(25);
//    }

//    [Fact]
//    public void Sets_Correct_CurrentPage()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(currentPage: 2);

//        // Assert
//        cut.Instance.CurrentPage.ShouldBe(2);
//    }

//    [Fact]
//    public void Enables_MultiSelection_When_True()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(multiSelection: true);

//        // Assert
//        cut.Instance.MultiSelection.ShouldBeTrue();
//    }

//    [Fact]
//    public void Disables_MultiSelection_When_False()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(multiSelection: false);

//        // Assert
//        cut.Instance.MultiSelection.ShouldBeFalse();
//    }

//    [Fact]
//    public void Sets_Correct_Elevation()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(elevation: 3);

//        // Assert
//        cut.Instance.Elevation.ShouldBe(3);
//    }

//    [Fact]
//    public void Enables_Striped_Rows_When_True()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(striped: true);

//        // Assert
//        cut.Instance.Striped.ShouldBeTrue();
//    }

//    [Fact]
//    public void Disables_Striped_Rows_When_False()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(striped: false);

//        // Assert
//        cut.Instance.Striped.ShouldBeFalse();
//    }

//    [Fact]
//    public void Enables_Hover_When_True()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(hover: true);

//        // Assert
//        cut.Instance.Hover.ShouldBeTrue();
//    }

//    [Fact]
//    public void Disables_Hover_When_False()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(hover: false);

//        // Assert
//        cut.Instance.Hover.ShouldBeFalse();
//    }

//    [Fact]
//    public void Sets_Correct_SortMode()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(sortMode: SortMode.Single);

//        // Assert
//        cut.Instance.SortMode.ShouldBe(SortMode.Single);
//    }

//    [Fact]
//    public void Enables_Filterable_When_True()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(filterable: true);

//        // Assert
//        cut.Instance.Filterable.ShouldBeTrue();
//    }

//    [Fact]
//    public void Disables_Filterable_When_False()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(filterable: false);

//        // Assert
//        cut.Instance.Filterable.ShouldBeFalse();
//    }

//    [Fact]
//    public void Enables_Groupable_When_True()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(groupable: true);

//        // Assert
//        cut.Instance.Groupable.ShouldBeTrue();
//    }

//    [Fact]
//    public void Disables_Groupable_When_False()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(groupable: false);

//        // Assert
//        cut.Instance.Groupable.ShouldBeFalse();
//    }

//    [Fact]
//    public void Sets_Correct_ColumnResizeMode()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(columnResizeMode: ResizeMode.Column);

//        // Assert
//        cut.Instance.ColumnResizeMode.ShouldBe(ResizeMode.Column);
//    }

//    [Fact]
//    public void Enables_DragDropColumnReordering_When_True()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(dragDropColumnReordering: true);

//        // Assert
//        cut.Instance.DragDropColumnReordering.ShouldBeTrue();
//    }

//    [Fact]
//    public void Disables_DragDropColumnReordering_When_False()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(dragDropColumnReordering: false);

//        // Assert
//        cut.Instance.DragDropColumnReordering.ShouldBeFalse();
//    }

//    [Fact]
//    public void Sets_Correct_Breakpoint()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(breakpoint: Breakpoint.Md);

//        // Assert
//        cut.Instance.Breakpoint.ShouldBe(Breakpoint.Md);
//    }

//    [Fact]
//    public void Sets_Correct_EditMode()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(editMode: DataGridEditMode.EditForm);

//        // Assert
//        cut.Instance.EditMode.ShouldBe(DataGridEditMode.EditForm);
//    }

//    [Fact]
//    public void Sets_Correct_EditTrigger()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(editTrigger: DataGridEditTrigger.DoubleClick);

//        // Assert
//        cut.Instance.EditTrigger.ShouldBe(DataGridEditTrigger.DoubleClick);
//    }

//    [Fact]
//    public void Enables_ReadOnly_When_True()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(readOnly: true);

//        // Assert
//        cut.Instance.ReadOnly.ShouldBeTrue();
//    }

//    [Fact]
//    public void Disables_ReadOnly_When_False()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(readOnly: false);

//        // Assert
//        cut.Instance.ReadOnly.ShouldBeFalse();
//    }

//    [Fact]
//    public void Enables_Virtualize_When_True()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(virtualize: true);

//        // Assert
//        cut.Instance.Virtualize.ShouldBeTrue();
//    }

//    [Fact]
//    public void Disables_Virtualize_When_False()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(virtualize: false);

//        // Assert
//        cut.Instance.Virtualize.ShouldBeFalse();
//    }

//    [Fact]
//    public void Enables_WrapCellContent_When_True()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(wrapCellContent: true);

//        // Assert
//        cut.Instance.WrapCellContent.ShouldBeTrue();
//    }

//    [Fact]
//    public void Disables_WrapCellContent_When_False()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(wrapCellContent: false);

//        // Assert
//        cut.Instance.WrapCellContent.ShouldBeFalse();
//    }

//    [Fact]
//    public void Enables_SettingsMenu_When_True()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(enableSettingsMenu: true, id: "test-grid");

//        // Assert
//        cut.Instance.EnableSettingsMenu.ShouldBeTrue();
//        cut.Instance.CanSaveAndLoad.ShouldBeTrue();
//    }

//    [Fact]
//    public void Disables_SettingsMenu_When_False()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(enableSettingsMenu: false);

//        // Assert
//        cut.Instance.EnableSettingsMenu.ShouldBeFalse();
//        cut.Instance.CanSaveAndLoad.ShouldBeFalse();
//    }

//    [Fact]
//    public void Sets_Correct_Height()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(height: "500px");

//        // Assert
//        cut.Instance.Height.ShouldBe("500px");
//    }

//    [Fact]
//    public void Enables_FixedHeader_When_True()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(fixedHeader: true);

//        // Assert
//        cut.Instance.FixedHeader.ShouldBeTrue();
//    }

//    [Fact]
//    public void Disables_FixedHeader_When_False()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(fixedHeader: false);

//        // Assert
//        cut.Instance.FixedHeader.ShouldBeFalse();
//    }

//    [Fact]
//    public void Enables_Dense_When_True()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(dense: true);

//        // Assert
//        cut.Instance.Dense.ShouldBeTrue();
//    }

//    [Fact]
//    public void Disables_Dense_When_False()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(dense: false);

//        // Assert
//        cut.Instance.Dense.ShouldBeFalse();
//    }

//    [Fact]
//    public void Enables_HorizontalScrollbar_When_True()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(horizontalScrollbar: true);

//        // Assert
//        cut.Instance.HorizontalScrollbar.ShouldBeTrue();
//    }

//    [Fact]
//    public void Disables_HorizontalScrollbar_When_False()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(horizontalScrollbar: false);

//        // Assert
//        cut.Instance.HorizontalScrollbar.ShouldBeFalse();
//    }

//    [Fact]
//    public void Sets_Correct_Culture()
//    {
//        // Arrange
//        var culture = new CultureInfo("sv-SE");

//        // Act
//        var cut = RenderComponentWithParams(culture: culture);

//        // Assert
//        cut.Instance.Culture.ShouldBe(culture);
//    }

//    [Fact]
//    public void Renders_ToolBarContent_When_Provided()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(
//            toolBarContent: builder => builder.AddContent(0, "Toolbar Content")
//        );

//        // Assert
//        cut.Markup.ShouldContain("Toolbar Content");
//    }

//    [Fact]
//    public void Renders_NoRecordsContent_When_Provided()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(
//            noRecordsContent: builder => builder.AddContent(0, "No Records")
//        );

//        // Assert
//        cut.Markup.ShouldContain("No Records");
//    }

//    [Fact]
//    public void Renders_PagerContent_When_Provided()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(
//            pagerContent: builder => builder.AddContent(0, "Custom Pager")
//        );

//        // Assert
//        cut.Markup.ShouldContain("Custom Pager");
//    }

//    [Fact]
//    public void Renders_ChildRowContent_When_Provided()
//    {
//        // Arrange
//        var childRowContent = new RenderFragment<CellContext<TestData>>(context => 
//            builder => builder.AddContent(0, $"Child: {context.Item.Name}"));

//        // Act
//        var cut = RenderComponentWithParams(childRowContent: childRowContent);

//        // Assert
//        // Note: Child row content is only rendered when there are items and the template is used
//        cut.Instance.ChildRowContent.ShouldNotBeNull();
//    }

//    [Fact]
//    public void Sets_SelectedItems_When_Provided()
//    {
//        // Arrange
//        var selectedItems = new HashSet<TestData>
//        {
//            new TestData { Id = 1, Name = "Selected Item" }
//        };

//        // Act
//        var cut = RenderComponentWithParams(selectedItems: selectedItems);

//        // Assert
//        cut.Instance.SelectedItems.ShouldBe(selectedItems);
//    }

//    [Fact]
//    public void Sets_SelectedItem_When_Provided()
//    {
//        // Arrange
//        var selectedItem = new TestData { Id = 1, Name = "Selected Item" };

//        // Act
//        var cut = RenderComponentWithParams(selectedItem: selectedItem);

//        // Assert
//        cut.Instance.SelectedItem.ShouldBe(selectedItem);
//    }

//    [Fact]
//    public void Sets_FooterClass_When_Provided()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(footerClass: "custom-footer");

//        // Assert
//        cut.Instance.FooterClass.ShouldBe("custom-footer");
//    }

//    [Fact]
//    public void Sets_RowClass_When_Provided()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(rowClass: "custom-row");

//        // Assert
//        cut.Instance.RowClass.ShouldBe("custom-row");
//    }

//    [Fact]
//    public void Sets_RowStyle_When_Provided()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(rowStyle: "background-color: red");

//        // Assert
//        cut.Instance.RowStyle.ShouldBe("background-color: red");
//    }

//    [Fact]
//    public void Sets_EditDialogOptions_When_Provided()
//    {
//        // Arrange
//        var editDialogOptions = new DialogOptions { MaxWidth = MaxWidth.Medium };

//        // Act
//        var cut = RenderComponentWithParams(editDialogOptions: editDialogOptions);

//        // Assert
//        cut.Instance.EditDialogOptions.ShouldBe(editDialogOptions);
//    }

//    [Fact]
//    public void CanSaveAndLoad_Returns_False_When_No_Id()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(enableSettingsMenu: true, id: null);

//        // Assert
//        cut.Instance.CanSaveAndLoad.ShouldBeFalse();
//    }

//    [Fact]
//    public void CanSaveAndLoad_Returns_False_When_SettingsMenu_Disabled()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(enableSettingsMenu: false, id: "test-grid");

//        // Assert
//        cut.Instance.CanSaveAndLoad.ShouldBeFalse();
//    }

//    [Fact]
//    public void CanSaveAndLoad_Returns_True_When_Both_Conditions_Met()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(enableSettingsMenu: true, id: "test-grid");

//        // Assert
//        cut.Instance.CanSaveAndLoad.ShouldBeTrue();
//    }
//}

//// Test data class
//public class TestData : IHUIGridItem
//{
//    public int Id { get; set; }
//    public string Name { get; set; } = "";
//    public Severity? Severity { get; set; }
//}

//// Test services
//public class TestTableStateService : ITableStateService
//{
//    public void ApplyColumnOrder<T>(MudDataGrid<T> grid, Dictionary<string, int> newOrder) { }
//    public Dictionary<string, int> GetColumnOrder<T>(MudDataGrid<T> grid) => new();
//    public void ApplyColumnVisibility<T>(MudDataGrid<T> grid, Dictionary<string, bool> visibility) { }
//    public Dictionary<string, bool> GetColumnVisibility<T>(MudDataGrid<T> grid) => new();
//    public Task UpdateState<T>(string? id, MudDataGrid<T> grid) => Task.CompletedTask;
//    public Task<bool> LoadStateAsync<T>(string id, MudDataGrid<T> grid) => Task.FromResult(true);
//}

//public class TestLocalStorageSettingsService : ILocalStorageSettingsService
//{
//    public Task<Settings> GetSettings() => Task.FromResult(new Settings());
//    public Task SetSettings(Settings settings) => Task.CompletedTask;
//}
