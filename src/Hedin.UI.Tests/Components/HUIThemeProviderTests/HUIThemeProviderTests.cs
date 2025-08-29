//using Bunit;
//using Shouldly;
//using Hedin.UI.Tests.Base;
//using MudBlazor;

//namespace Hedin.UI.Tests.Components.HUIThemeProviderTests;

//public class HUIThemeProviderTests : UiTestBase
//{
//    private IRenderedComponent<HUIThemeProvider> RenderComponentWithParams(MudTheme? theme = null)
//    {
//        return RenderComponentWithMudProviders<HUIThemeProvider>(parameters => parameters
//            .Add(p => p.Theme, theme ?? new MudTheme())
//        );
//    }

//    [Fact]
//    public void Renders_MudThemeProvider()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams();

//        // Assert
//        cut.FindComponent<MudThemeProvider>().ShouldNotBeNull();
//    }

//    [Fact]
//    public void Sets_Theme_When_Provided()
//    {
//        // Arrange
//        var theme = new MudTheme { PaletteDark = new PaletteDark { Primary = "#FF0000" } };

//        // Act
//        var cut = RenderComponentWithParams(theme: theme);

//        // Assert
//        var themeProvider = cut.FindComponent<MudThemeProvider>();
//        themeProvider.Instance.Theme.ShouldBe(theme);
//    }

//    [Fact]
//    public void Sets_IsDarkMode_To_True()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams();

//        // Assert
//        var themeProvider = cut.FindComponent<MudThemeProvider>();
//        themeProvider.Instance.IsDarkMode.ShouldBeTrue();
//    }

//    [Fact]
//    public void Handles_Default_Theme()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams();

//        // Assert
//        var themeProvider = cut.FindComponent<MudThemeProvider>();
//        themeProvider.Instance.Theme.ShouldNotBeNull();
//    }

//    [Fact]
//    public void Handles_Custom_Theme_With_Palette()
//    {
//        // Arrange
//        var theme = new MudTheme
//        {
//            PaletteDark = new PaletteDark
//            {
//                Primary = "#FF0000",
//                Secondary = "#00FF00",
//                Background = "#000000",
//                Surface = "#FFFFFF"
//            }
//        };

//        // Act
//        var cut = RenderComponentWithParams(theme: theme);

//        // Assert
//        var themeProvider = cut.FindComponent<MudThemeProvider>();
//        themeProvider.Instance.Theme.ShouldBe(theme);
//                 themeProvider.Instance.Theme.PaletteDark.Primary.ShouldBe("#FF0000");
//         themeProvider.Instance.Theme.PaletteDark.Secondary.ShouldBe("#00FF00");
//    }

//    [Fact]
//    public void Handles_Custom_Theme_With_Typography()
//    {
//        // Arrange
//        var theme = new MudTheme
//        {
//            Typography = new Typography
//            {
//                H1 = new DefaultFontStyle { FontSize = "2.5rem", FontWeight = 300 },
//                H2 = new DefaultFontStyle { FontSize = "2rem", FontWeight = 400 }
//            }
//        };

//        // Act
//        var cut = RenderComponentWithParams(theme: theme);

//        // Assert
//        var themeProvider = cut.FindComponent<MudThemeProvider>();
//        themeProvider.Instance.Theme.ShouldBe(theme);
//        themeProvider.Instance.Theme.Typography.H1.FontSize.ShouldBe("2.5rem");
//        themeProvider.Instance.Theme.Typography.H2.FontWeight.ShouldBe(400);
//    }

//    [Fact]
//    public void Handles_Custom_Theme_With_Shadows()
//    {
//        // Arrange
//        var theme = new MudTheme
//        {
//            Shadows = new string[]
//            {
//                "0px 2px 4px rgba(0,0,0,0.1)",
//                "0px 4px 8px rgba(0,0,0,0.15)"
//            }
//        };

//        // Act
//        var cut = RenderComponentWithParams(theme: theme);

//        // Assert
//        var themeProvider = cut.FindComponent<MudThemeProvider>();
//        themeProvider.Instance.Theme.ShouldBe(theme);
//        themeProvider.Instance.Theme.Shadows.ShouldContain("0px 2px 4px rgba(0,0,0,0.1)");
//        themeProvider.Instance.Theme.Shadows.ShouldContain("0px 4px 8px rgba(0,0,0,0.15)");
//    }

//    [Fact]
//    public void Handles_Custom_Theme_With_BorderRadius()
//    {
//        // Arrange
//        var theme = new MudTheme
//        {
//            BorderRadius = 8
//        };

//        // Act
//        var cut = RenderComponentWithParams(theme: theme);

//        // Assert
//        var themeProvider = cut.FindComponent<MudThemeProvider>();
//        themeProvider.Instance.Theme.ShouldBe(theme);
//        themeProvider.Instance.Theme.BorderRadius.ShouldBe(8);
//    }

//    [Fact]
//    public void Handles_Custom_Theme_With_Spacing()
//    {
//        // Arrange
//        var theme = new MudTheme
//        {
//            Spacing = 8
//        };

//        // Act
//        var cut = RenderComponentWithParams(theme: theme);

//        // Assert
//        var themeProvider = cut.FindComponent<MudThemeProvider>();
//        themeProvider.Instance.Theme.ShouldBe(theme);
//        themeProvider.Instance.Theme.Spacing.ShouldBe(8);
//    }

//    [Fact]
//    public void Handles_Custom_Theme_With_Breakpoints()
//    {
//        // Arrange
//        var theme = new MudTheme
//        {
//            Breakpoints = new Breakpoints
//            {
//                Xs = 0,
//                Sm = 600,
//                Md = 960,
//                Lg = 1280,
//                Xl = 1920
//            }
//        };

//        // Act
//        var cut = RenderComponentWithParams(theme: theme);

//        // Assert
//        var themeProvider = cut.FindComponent<MudThemeProvider>();
//        themeProvider.Instance.Theme.ShouldBe(theme);
//        themeProvider.Instance.Theme.Breakpoints.Sm.ShouldBe(600);
//        themeProvider.Instance.Theme.Breakpoints.Md.ShouldBe(960);
//    }

//    [Fact]
//    public void Handles_Custom_Theme_With_ZIndex()
//    {
//        // Arrange
//        var theme = new MudTheme
//        {
//            ZIndex = new ZIndex
//            {
//                Drawer = 1100,
//                AppBar = 1200,
//                Dialog = 1300,
//                Tooltip = 1400
//            }
//        };

//        // Act
//        var cut = RenderComponentWithParams(theme: theme);

//        // Assert
//        var themeProvider = cut.FindComponent<MudThemeProvider>();
//        themeProvider.Instance.Theme.ShouldBe(theme);
//        themeProvider.Instance.Theme.ZIndex.Drawer.ShouldBe(1100);
//        themeProvider.Instance.Theme.ZIndex.AppBar.ShouldBe(1200);
//    }

//    [Fact]
//    public void Handles_Complex_Theme_Combination()
//    {
//        // Arrange
//        var theme = new MudTheme
//        {
//            PaletteDark = new PaletteDark
//            {
//                Primary = "#1976D2",
//                Secondary = "#424242",
//                Background = "#FAFAFA",
//                Surface = "#FFFFFF"
//            },
//            Typography = new Typography
//            {
//                H1 = new DefaultFontStyle { FontSize = "3rem", FontWeight = 300 },
//                Body1 = new DefaultFontStyle { FontSize = "1rem", FontWeight = 400 }
//            },
//            BorderRadius = 4,
//            Spacing = 8
//        };

//        // Act
//        var cut = RenderComponentWithParams(theme: theme);

//        // Assert
//        var themeProvider = cut.FindComponent<MudThemeProvider>();
//        themeProvider.Instance.Theme.ShouldBe(theme);
//                 themeProvider.Instance.Theme.PaletteDark.Primary.ShouldBe("#1976D2");
//        themeProvider.Instance.Theme.Typography.H1.FontSize.ShouldBe("3rem");
//        themeProvider.Instance.Theme.BorderRadius.ShouldBe(4);
//        themeProvider.Instance.Theme.Spacing.ShouldBe(8);
//    }
//}
