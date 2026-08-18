using Bunit;
using Hedin.UI.Tests.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;
using Shouldly;

namespace Hedin.UI.Tests.Components.HUIDataGridTests;

/// <summary>
///     Two guards on the table-settings popup: a sticky column must never be movable, and building the column
///     order must survive two columns sharing a title.
/// </summary>
public class TableContextMenuGuardTests : UiTestBase
{
    private sealed record Row(string Name, int Count);

    // MudBlazor pins a sticky column with a flat left:0 / right:0, so one moved into the middle of the table
    // renders on top of its neighbours. It stays locked even though it is titled and drag-enabled.
    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    public void StickyColumns_AreNeverMovable(bool stickyLeft, bool stickyRight)
    {
        var cut = RenderGrid(builder =>
        {
            AddColumn(builder, "Name", stickyLeft: stickyLeft, stickyRight: stickyRight, dragAndDrop: true);
            AddColumn(builder, "Count");
        });

        var menu = cut.FindComponent<Hedin.UI.Internal.TableContextMenu<Row>>();
        var sticky = Columns(menu).Single(c => c.Title == "Name");

        IsMovable(menu, sticky).ShouldBeFalse();
    }

    [Fact]
    public void NonStickyTitledColumns_StayMovable()
    {
        var cut = RenderGrid(builder =>
        {
            AddColumn(builder, "Name", stickyLeft: true, dragAndDrop: true);
            AddColumn(builder, "Count", dragAndDrop: true);
        });

        var menu = cut.FindComponent<Hedin.UI.Internal.TableContextMenu<Row>>();

        IsMovable(menu, Columns(menu).Single(c => c.Title == "Count")).ShouldBeTrue();
    }

    // Titles are localized at runtime, so two different resource keys can resolve to the same string. The old
    // ToDictionary threw ArgumentException here, which surfaced as the grid dying on an arrow press.
    [Fact]
    public void GetColumnOrder_SurvivesDuplicateTitles()
    {
        var cut = RenderGrid(builder =>
        {
            AddColumn(builder, "Delivery");
            AddColumn(builder, "Delivery");
            AddColumn(builder, "Count");
        });

        var menu = cut.FindComponent<Hedin.UI.Internal.TableContextMenu<Row>>();

        var order = Should.NotThrow(() => menu.Instance.GetColumnOrder());

        order.ShouldContainKey("Delivery");
        order.ShouldContainKey("Count");
        // Last occurrence wins, matching how TableStateService reads the order back.
        order["Delivery"].ShouldBe(1);
    }

    [Fact]
    public void GetColumnOrder_GivesUntitledColumnsDistinctKeys()
    {
        var cut = RenderGrid(builder =>
        {
            AddColumn(builder, title: null);
            AddColumn(builder, title: null);
            AddColumn(builder, "Count");
        });

        var menu = cut.FindComponent<Hedin.UI.Internal.TableContextMenu<Row>>();

        menu.Instance.GetColumnOrder().Count.ShouldBe(3);
    }

    private static IReadOnlyList<Column<Row>> Columns(
        IRenderedComponent<Hedin.UI.Internal.TableContextMenu<Row>> menu) =>
        menu.Instance.DataGrid.InnerGridRef.RenderedColumns;

    private static bool IsMovable(
        IRenderedComponent<Hedin.UI.Internal.TableContextMenu<Row>> menu,
        Column<Row> column)
    {
        var method = typeof(Hedin.UI.Internal.TableContextMenu<Row>)
            .GetMethod("IsMoveDisabled", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!;
        return !(bool)method.Invoke(menu.Instance, [column])!;
    }

    private static void AddColumn(
        RenderTreeBuilder builder,
        string? title,
        bool stickyLeft = false,
        bool stickyRight = false,
        bool? dragAndDrop = null)
    {
        builder.OpenComponent<TemplateColumn<Row>>(builder.GetHashCode());
        builder.AddComponentParameter(1, nameof(TemplateColumn<Row>.Title), title);
        builder.AddComponentParameter(2, nameof(TemplateColumn<Row>.StickyLeft), stickyLeft);
        builder.AddComponentParameter(3, nameof(TemplateColumn<Row>.StickyRight), stickyRight);
        builder.AddComponentParameter(4, nameof(TemplateColumn<Row>.DragAndDropEnabled), dragAndDrop);
        builder.AddComponentParameter(5, nameof(TemplateColumn<Row>.CellTemplate),
            (RenderFragment<CellContext<Row>>)(context => cell => cell.AddContent(0, context.Item.Name)));
        builder.CloseComponent();
    }

    // The wrench lives on HUIPageTable and only renders once the grid can save its state (it needs an Id), so
    // the menu has to be reached through the page table rather than the grid alone.
    private IRenderedComponent<HUIPageTable<Row>> RenderGrid(Action<RenderTreeBuilder> columns)
    {
        Row[] items = [new("first", 1), new("second", 2)];

        return RenderComponentWithMudProviders<HUIPageTable<Row>>(parameters => parameters
            .Add(p => p.Header, "Rows")
            .Add(p => p.ChildContent, gridBuilder =>
            {
                gridBuilder.OpenComponent<HUIDataGrid<Row>>(0);
                gridBuilder.AddComponentParameter(1, nameof(HUIDataGrid<Row>.Id), "context-menu-guard-tests");
                gridBuilder.AddComponentParameter(2, nameof(HUIDataGrid<Row>.EnableSettingsMenu), true);
                gridBuilder.AddComponentParameter(3, nameof(HUIDataGrid<Row>.Items), items);
                gridBuilder.AddComponentParameter(4, nameof(HUIDataGrid<Row>.Columns), (RenderFragment)(b => columns(b)));
                gridBuilder.CloseComponent();
            }));
    }
}
