using Bunit;
using Hedin.UI.Tests.Base;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shouldly;

namespace Hedin.UI.Tests.Components.HUIDataGridTests;

/// <summary>
///     The table-settings popup's up/down arrows reorder columns through <see cref="ITableStateService" />,
///     not through MudBlazor's drag-and-drop, so their availability must not depend on the drag-and-drop
///     flags. It used to: the old predicate read <c>!column.DragAndDropEnabled ?? !grid.DragDropColumnReordering</c>,
///     and because <c>TemplateColumn</c> hard-overrides <c>DragAndDropEnabled</c> to <c>false</c>, the first
///     operand was always non-null and padlocked every template column whatever the grid flag said — leaving a
///     template-column grid with no way to reorder at all, drag included (MudBlazor#9504).
/// </summary>
public class TableContextMenuColumnMoveTests : UiTestBase
{
    private sealed record Row(string Name, int Count);

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void TitledColumns_AreMovable_WhateverTheDragDropFlagSays(bool dragDropColumnReordering)
    {
        var cut = RenderGrid(dragDropColumnReordering);

        var menu = cut.FindComponent<Hedin.UI.Internal.TableContextMenu<Row>>();
        var movableColumns = menu.Instance.DataGrid.InnerGridRef.RenderedColumns
            .Where(column => !string.IsNullOrWhiteSpace(column.Title))
            .ToList();

        movableColumns.Count.ShouldBe(2);
        movableColumns.ShouldAllBe(column => IsMovable(menu, column));
    }

    // Pins the mechanism the old predicate tripped over, so this suite fails loudly if a MudBlazor upgrade
    // changes it: TemplateColumn opts itself out of drag-and-drop, which is why keying movability to that flag
    // locked every arrow. If this ever becomes null or true, revisit why IsMoveDisabled ignores it.
    [Fact]
    public void TemplateColumn_OptsOutOfDragAndDrop()
    {
        new TemplateColumn<Row>().DragAndDropEnabled.ShouldBe(false);
    }

    // ApplyColumnOrder keeps an untitled column at its own index, so there is nothing a move could write —
    // and it is what pins a sticky action column in place.
    [Fact]
    public void UntitledColumns_StayLocked()
    {
        var cut = RenderGrid(dragDropColumnReordering: false);

        var menu = cut.FindComponent<Hedin.UI.Internal.TableContextMenu<Row>>();
        var untitled = menu.Instance.DataGrid.InnerGridRef.RenderedColumns
            .Single(column => string.IsNullOrWhiteSpace(column.Title));

        IsMovable(menu, untitled).ShouldBeFalse();
    }

    private static bool IsMovable(IRenderedComponent<Hedin.UI.Internal.TableContextMenu<Row>> menu, Column<Row> column)
    {
        var method = typeof(Hedin.UI.Internal.TableContextMenu<Row>)
            .GetMethod("IsMoveDisabled", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!;
        return !(bool)method.Invoke(menu.Instance, [column])!;
    }

    // The wrench lives on HUIPageTable and only renders once the grid can save its state (it needs an Id), so
    // the menu has to be reached through the page table rather than the grid alone.
    private IRenderedComponent<HUIPageTable<Row>> RenderGrid(bool dragDropColumnReordering)
    {
        Row[] items = [new("first", 1), new("second", 2)];

        return RenderComponentWithMudProviders<HUIPageTable<Row>>(parameters => parameters
            .Add(p => p.Header, "Rows")
            .Add(p => p.ChildContent, gridBuilder =>
            {
                var sequence = 0;
                gridBuilder.OpenComponent<HUIDataGrid<Row>>(sequence++);
                gridBuilder.AddComponentParameter(sequence++, nameof(HUIDataGrid<Row>.Id), "column-move-tests");
                gridBuilder.AddComponentParameter(sequence++, nameof(HUIDataGrid<Row>.EnableSettingsMenu), true);
                gridBuilder.AddComponentParameter(sequence++, nameof(HUIDataGrid<Row>.Items), items);
                gridBuilder.AddComponentParameter(sequence++, nameof(HUIDataGrid<Row>.DragDropColumnReordering), dragDropColumnReordering);
                gridBuilder.AddComponentParameter(sequence++, nameof(HUIDataGrid<Row>.Columns), BuildColumns());
                gridBuilder.CloseComponent();
            }));
    }

    // Template columns on purpose: they are the case MudBlazor's own drag-reorder cannot handle, and the one
    // the popup arrows exist for. The untitled column stands in for a sticky action column.
    private static RenderFragment BuildColumns() => builder =>
    {
        var sequence = 0;

        builder.OpenComponent<TemplateColumn<Row>>(sequence++);
        builder.AddComponentParameter(sequence++, nameof(TemplateColumn<Row>.Title), "Name");
        builder.AddComponentParameter(sequence++, nameof(TemplateColumn<Row>.CellTemplate),
            (RenderFragment<CellContext<Row>>)(context => cell => cell.AddContent(0, context.Item.Name)));
        builder.CloseComponent();

        builder.OpenComponent<TemplateColumn<Row>>(sequence++);
        builder.AddComponentParameter(sequence++, nameof(TemplateColumn<Row>.Title), "Count");
        builder.AddComponentParameter(sequence++, nameof(TemplateColumn<Row>.CellTemplate),
            (RenderFragment<CellContext<Row>>)(context => cell => cell.AddContent(0, context.Item.Count)));
        builder.CloseComponent();

        builder.OpenComponent<TemplateColumn<Row>>(sequence++);
        builder.AddComponentParameter(sequence++, nameof(TemplateColumn<Row>.CellTemplate),
            (RenderFragment<CellContext<Row>>)(_ => cell => cell.AddContent(0, "…")));
        builder.CloseComponent();
    };
}
