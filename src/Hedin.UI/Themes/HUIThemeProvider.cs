using MudBlazor;

namespace Hedin.UI;

public static class HUIThemeExtensions
{
    // Method to create a customized theme based on the base theme
    private static MudTheme CreateCustomTheme(MudTheme baseTheme, Action<Palette>? customizePalette, Action<LayoutProperties>? customizeLayout)
    {
        var customTheme = baseTheme.Clone();
        customizePalette?.Invoke(customTheme.PaletteDark);
        customizeLayout?.Invoke(customTheme.LayoutProperties);
        return customTheme;
    }

    // Example clone method for MudTheme
    private static MudTheme Clone(this MudTheme theme)
    {
        return new MudTheme
        {
            Typography = theme.Typography,
            PaletteLight = theme.PaletteLight.CloneLight(),
            PaletteDark = theme.PaletteDark.Clone(),
            Shadows = new Shadow
            {
                Elevation = (string[])theme.Shadows.Elevation.Clone(),
            },
            LayoutProperties = new LayoutProperties
            {
                DrawerWidthLeft = theme.LayoutProperties.DrawerWidthLeft,
                DrawerWidthRight = theme.LayoutProperties.DrawerWidthRight,
                AppbarHeight = theme.LayoutProperties.AppbarHeight,
                DefaultBorderRadius = theme.LayoutProperties.DefaultBorderRadius,
                DrawerMiniWidthLeft = theme.LayoutProperties.DrawerMiniWidthLeft,
                DrawerMiniWidthRight = theme.LayoutProperties.DrawerMiniWidthRight,
            },
            ZIndex = new ZIndex
            {
                AppBar = theme.ZIndex.AppBar,
                Drawer = theme.ZIndex.Drawer,
                Dialog = theme.ZIndex.Dialog,
                Popover = theme.ZIndex.Popover,
                Snackbar = theme.ZIndex.Snackbar,
                Tooltip = theme.ZIndex.Tooltip
            }
        };
    }

    //Example clone method for Palette
    private static PaletteDark Clone(this Palette palette)
    {
        return new PaletteDark
        {
            Primary = palette.Primary,
            Secondary = palette.Secondary,
            Tertiary = palette.Tertiary,
            Info = palette.Info,
            Success = palette.Success,
            Warning = palette.Warning,
            Error = palette.Error,
            Dark = palette.Dark,
            TextPrimary = palette.TextPrimary,
            TextSecondary = palette.TextSecondary,
            TextDisabled = palette.TextDisabled,
            ActionDefault = palette.ActionDefault,
            ActionDisabled = palette.ActionDisabled,
            ActionDisabledBackground = palette.ActionDisabledBackground,
            Background = palette.Background,
            BackgroundGray = palette.BackgroundGray,
            Surface = palette.Surface,
            DrawerBackground = palette.DrawerBackground,
            DrawerText = palette.DrawerText,
            DrawerIcon = palette.DrawerIcon,
            AppbarBackground = palette.AppbarBackground,
            AppbarText = palette.AppbarText,
            LinesDefault = palette.LinesDefault,
            LinesInputs = palette.LinesInputs,
            TableLines = palette.TableLines,
            TableStriped = palette.TableStriped,
            TableHover = palette.TableHover,
            Divider = palette.Divider,
            DividerLight = palette.DividerLight,
            PrimaryDarken = palette.PrimaryDarken,
            PrimaryLighten = palette.PrimaryLighten,
            SecondaryDarken = palette.SecondaryDarken,
            SecondaryLighten = palette.SecondaryLighten,
            TertiaryDarken = palette.TertiaryDarken,
            TertiaryLighten = palette.TertiaryLighten,
            InfoDarken = palette.InfoDarken,
            InfoLighten = palette.InfoLighten,
            SuccessDarken = palette.SuccessDarken,
            SuccessLighten = palette.SuccessLighten,
            WarningDarken = palette.WarningDarken,
            WarningLighten = palette.WarningLighten,
            ErrorDarken = palette.ErrorDarken,
            ErrorLighten = palette.ErrorLighten,
            DarkDarken = palette.DarkDarken,
            DarkLighten = palette.DarkLighten,
            HoverOpacity = palette.HoverOpacity,
            GrayDefault = palette.GrayDefault,
            GrayLight = palette.GrayLight,
            GrayLighter = palette.GrayLighter,
            GrayDark = palette.GrayDark,
            GrayDarker = palette.GrayDarker,
            OverlayDark = palette.OverlayDark,
            OverlayLight = palette.OverlayLight,
            Skeleton = palette.Skeleton
        };
    }


    //Example clone method for Palette
    private static PaletteLight CloneLight(this Palette palette)
    {
        return new PaletteLight
        {
            Primary = palette.Primary,
            Secondary = palette.Secondary,
            Tertiary = palette.Tertiary,
            Info = palette.Info,
            Success = palette.Success,
            Warning = palette.Warning,
            Error = palette.Error,
            Dark = palette.Dark,
            TextPrimary = palette.TextPrimary,
            TextSecondary = palette.TextSecondary,
            TextDisabled = palette.TextDisabled,
            ActionDefault = palette.ActionDefault,
            ActionDisabled = palette.ActionDisabled,
            ActionDisabledBackground = palette.ActionDisabledBackground,
            Background = palette.Background,
            BackgroundGray = palette.BackgroundGray,
            Surface = palette.Surface,
            DrawerBackground = palette.DrawerBackground,
            DrawerText = palette.DrawerText,
            DrawerIcon = palette.DrawerIcon,
            AppbarBackground = palette.AppbarBackground,
            AppbarText = palette.AppbarText,
            LinesDefault = palette.LinesDefault,
            LinesInputs = palette.LinesInputs,
            TableLines = palette.TableLines,
            TableStriped = palette.TableStriped,
            TableHover = palette.TableHover,
            Divider = palette.Divider,
            DividerLight = palette.DividerLight,
            PrimaryDarken = palette.PrimaryDarken,
            PrimaryLighten = palette.PrimaryLighten,
            SecondaryDarken = palette.SecondaryDarken,
            SecondaryLighten = palette.SecondaryLighten,
            TertiaryDarken = palette.TertiaryDarken,
            TertiaryLighten = palette.TertiaryLighten,
            InfoDarken = palette.InfoDarken,
            InfoLighten = palette.InfoLighten,
            SuccessDarken = palette.SuccessDarken,
            SuccessLighten = palette.SuccessLighten,
            WarningDarken = palette.WarningDarken,
            WarningLighten = palette.WarningLighten,
            ErrorDarken = palette.ErrorDarken,
            ErrorLighten = palette.ErrorLighten,
            DarkDarken = palette.DarkDarken,
            DarkLighten = palette.DarkLighten,
            HoverOpacity = palette.HoverOpacity,
            GrayDefault = palette.GrayDefault,
            GrayLight = palette.GrayLight,
            GrayLighter = palette.GrayLighter,
            GrayDark = palette.GrayDark,
            GrayDarker = palette.GrayDarker,
            OverlayDark = palette.OverlayDark,
            OverlayLight = palette.OverlayLight,
            Skeleton = palette.Skeleton
        };
    }


#pragma warning disable CS1570 // XML comment has badly formed XML
    /// <summary>
    /// Wrap the provided customizePalette action to ensure light/ darken colors are set
    /// Action<Palette> enhancedCustomizePalette = (palette) =>
    /// {
    ///     // Apply the provided customization
    ///     customizePalette(palette);
    ///     palette.PrimaryLighten = palette.Primary.ColorLighten(0.125).Value;
    ///     palette.PrimaryDarken = palette.Primary.ColorDarken(0.125).Value;
    ///     palette.TableHover = palette.Primary; // Example of using primary color
    /// };
    /// </summary>
    /// <param name="theme"></param>
    /// <param name="customizePalette"></param>
    /// <returns></returns>
    public static MudTheme Override(this MudTheme theme, Action<Palette>? customizePalette = null, Action<LayoutProperties>? customizeLayout = null)
    {
        return CreateCustomTheme(theme, customizePalette, customizeLayout);
    }
#pragma warning restore CS1570 // XML comment has badly formed XML
}
