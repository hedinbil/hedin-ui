using Bunit;
using Hedin.UI.Tests.Base;
using Shouldly;

namespace Hedin.UI.Tests.Components.HUIDataGridTests;

public class StickyHorizontalScrollTests : UiTestBase
{
    private const string ModulePath = "./_content/Hedin.UI/Components/DataGrid/HUIDataGrid.razor.js";

    private sealed record Row(string Name);

    private IRenderedComponent<HUIDataGrid<Row>> Render(bool sticky) =>
        RenderComponentWithMudProviders<HUIDataGrid<Row>>(p => p
            .Add(g => g.Items, new[] { new Row("a") })
            .Add(g => g.StickyHorizontalScroll, sticky));

    [Fact]
    public void Default_RendersNoProxyBar_AndLoadsNoModule()
    {
        var cut = Render(sticky: false);

        cut.FindAll(".hui-datagrid-hscroll-bar").ShouldBeEmpty();
        JSInterop.Invocations.Any(i => i.Identifier == "import").ShouldBeFalse();
    }

    [Fact]
    public void Enabled_RendersHiddenFromAssistiveTech_AndImportsModule()
    {
        var cut = Render(sticky: true);

        var bar = cut.Find(".hui-datagrid-hscroll-bar");
        bar.GetAttribute("aria-hidden").ShouldBe("true");
        bar.GetAttribute("tabindex").ShouldBe("-1");
        JSInterop.Invocations.Any(i => i.Identifier == "import" && (string?)i.Arguments[0] == ModulePath).ShouldBeTrue();
    }

    [Fact]
    public void ProxyBar_SitsAbovePager_OrAfterGridWithoutPager()
    {
        var cut = Render(sticky: true);
        cut.FindAll(".hui-datagrid > .mud-table > .mud-table-pagination > .hui-datagrid-hscroll-bar:first-child").Count.ShouldBe(1);

        cut.Render(p => p.Add(g => g.ShowPager, false));
        cut.FindAll(".hui-datagrid > .hui-datagrid-hscroll-bar").Count.ShouldBe(1);
        cut.FindAll(".hui-datagrid-hscroll-bar").Count.ShouldBe(1);
    }

    [Fact]
    public void SwitchedOffAtRuntime_RemovesProxyBar()
    {
        var cut = Render(sticky: true);

        cut.Render(p => p.Add(g => g.StickyHorizontalScroll, false));

        cut.FindAll(".hui-datagrid-hscroll-bar").ShouldBeEmpty();
    }
}
