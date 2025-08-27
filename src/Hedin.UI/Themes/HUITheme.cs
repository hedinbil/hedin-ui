using MudBlazor;

namespace Hedin.UI;

//Any changes made to this class. Such as adding or removing properties, will need to be made in the clone functions in the ThemeProvider class.
public static class HUITheme
{
    #region General theme properties

    private const string BoxShadowLight = "0px 0px 0px 1px #D9D9D9, 0px 0.8px 3.6px 0px rgba(0, 0, 0, 0.02), 0px 1.7px 6.4px 0px rgba(0, 0, 0, 0.02), 0px 3px 10.4px 0px rgba(0, 0, 0, 0.02), 0px 5.3px 16.2px 0px rgba(0, 0, 0, 0.03), 0px 13px 31px 0px rgba(0, 0, 0, 0.05);";
    private const string BoxShadowDark = "0px 0px 0px 1px #2E2E2E, 0px 0.4px 1.5px 0px rgba(0, 0, 0, 0.01), 0px 0.9px 3.6px 0px rgba(0, 0, 0, 0.02), 0px 1.7px 6.4px 0px rgba(0, 0, 0, 0.02), 0px 3px 10.4px 0px rgba(0, 0, 0, 0.02), 0px 5.3px 16.2px 0px rgba(0, 0, 0, 0.03), 0px 13px 31px 0px rgba(0, 0, 0, 0.05);";

    private static readonly ZIndex ZIndex = new()
    {
        AppBar = 1100,
        Popover = 1500,
    };
    private static readonly LayoutProperties LayoutProperties = new()
    {
        DrawerWidthLeft = "212px",
        DrawerWidthRight = "400px",
    };
    private static readonly Typography Typo = new()
    {
        Default = new DefaultTypography
        {
            FontFamily = ["IBM Plex Sans", "sans-serif"],
        },
        H1 = new H1Typography
        {
            FontWeight = "600",
            FontSize = "1.5rem",
            LineHeight = "1.25",
            LetterSpacing = ".02em"
        },
        H2 = new H2Typography
        {
            FontWeight = "400",
            FontSize = "1.25rem",
            LineHeight = "1.25",
            LetterSpacing = ".02em"
        },
        H3 = new H3Typography
        {
            FontWeight = "400",
            FontSize = "1.125rem",
            LineHeight = "1.25",
            LetterSpacing = ".02em"
        },
        Body1 = new Body1Typography
        {
            FontWeight = "400",
            FontSize = "1rem",
            LineHeight = "1.25",
            LetterSpacing = ".02em"
        },
        Body2 = new Body2Typography
        {
            FontWeight = "400",
            FontSize = ".875rem",
            LineHeight = "1.25",
            LetterSpacing = ".02em"
        },
        Button = new ButtonTypography
        {
            FontWeight = "400",
            FontSize = "1rem",
            LineHeight = "1.45",
            LetterSpacing = ".02em",
            TextTransform = "initial"
        }
    };
#endregion

    /// <summary>
    /// Light theme for the HUI
    /// </summary>
    public static readonly MudTheme Light = new()
    {
        Typography = Typo,
        PaletteDark = new PaletteDark
        {
            Black = "rgba(39,44,52,1)",
            White = "rgba(255,255,255,1)",
            Primary = "#1E7194",
            PrimaryContrastText = "#ffffffff",
            Secondary = "#1FA6E0",
            SecondaryContrastText = "rgba(255,255,255,1)",
            Tertiary = "rgba(30,200,165,1)",
            TertiaryContrastText = "rgba(255,255,255,1)",
            Info = "#43C6F5",
            InfoContrastText = "#000000",
            Success = "#7ED157",
            SuccessContrastText = "#000000",
            Warning = "#FBB33E",
            WarningContrastText = "#000000",
            Error = "#F65757",
            ErrorContrastText = "#000000",
            Dark = "#f0f0f0",
            DarkContrastText = "#000000",
            TextPrimary = "#424242",
            TextSecondary = "#42424299",
            TextDisabled = "#00000033",
            ActionDefault = "rgba(0,0,0,0.5372549019607843)",
            ActionDisabled = "rgba(0,0,0,0.25882352941176473)",
            ActionDisabledBackground = "rgba(0,0,0,0.11764705882352941)",
            Background = "#f7f7f7",
            BackgroundGray = "#D6D6D6",
            Surface = "#f7f7f7",
            DrawerBackground = "#f7f7f7",
            DrawerText = "#424242e6",
            DrawerIcon = "rgba(97,97,97,1)",
            AppbarBackground = "#f7f7f7",
            AppbarText = "#000000b2",
            LinesDefault = "rgba(0,0,0,0.11764705882352941)",
            LinesInputs = "rgba(189,189,189,1)",
            TableLines = "rgba(224,224,224,1)",
            TableStriped = "#F2F2F2",
            TableHover = "#1E7194",
            Divider = "00000033",
            DividerLight = "rgba(0,0,0,0.8)",
            PrimaryDarken = "#005174",
            PrimaryLighten = "#3e9ec6",
            SecondaryDarken = "rgb(255,31,105)",
            SecondaryLighten = "rgb(255,102,153)",
            TertiaryDarken = "rgb(25,169,140)",
            TertiaryLighten = "rgb(42,223,187)",
            InfoDarken = "#0091CF",
            InfoLighten = "#38bbf4",
            SuccessDarken = "#329C00",
            SuccessLighten = "#52ff00",
            WarningDarken = "#C17700",
            WarningLighten = "#ffd234",
            ErrorDarken = "#F94229",
            ErrorLighten = "#ff6651",
            DarkDarken = "#F4F4F4",
            DarkLighten = "#CECECE",
            HoverOpacity = 0.1,
            GrayDefault = "#EBEBEB",
            GrayLight = "#E5E5E5",
            GrayLighter = "#E0E0E0",
            GrayDark = "#F0F0F0",
            GrayDarker = "#FDFDFD",
            OverlayDark = "rgba(33,33,33,0.4980392156862745)",
            OverlayLight = "rgba(255,255,255,0.4980392156862745)",
            Skeleton = "rgba(0,0,0,0.10980392156862745)"
        },
        Shadows = new Shadow
        {
            Elevation =
            [
                "",
                "0px 0px 0px 1px #d9d9d9",
                BoxShadowLight,
                BoxShadowLight,
                "0px 0px 0px 1px #d9d9d9, 0px 0.8px 3.6px 0px rgba(0, 0, 0, 0.02), 0px 1.7px 6.4px 0px rgba(0, 0, 0, 0.02), 0px 3px 10.4px 0px rgba(0, 0, 0, 0.02), 0px 5.3px 16.2px 0px rgba(0, 0, 0, 0.03), 0px 13px 31px 0px rgba(0, 0, 0, 0.05), 0px -12px 12px -12px rgba(0, 0, 0, 0.12) inset;",
                BoxShadowLight,
                BoxShadowLight,
                BoxShadowLight,
                "0px -0.6px 2.5px rgba(0, 0, 0, 0.2),  0px 0.8px 6.6px rgba(0, 0, 0, 0.018),  0px 11.7px 6.4px rgba(0, 0, 0, 0.019),  0px 3px 10.4px rgba(0, 0, 0, 0.022),  0px 5.3px 16.2px rgba(0, 0, 0, 0.028),  0px 13px 34px rgba(0, 0, 0, 0.1), 0 0 0 999em rgba(0,0,0,0.08);",
                BoxShadowLight,
                BoxShadowLight,
                BoxShadowLight,
                BoxShadowLight,
                BoxShadowLight,
                BoxShadowLight,
                BoxShadowLight,
                BoxShadowLight,
                BoxShadowLight,
                BoxShadowLight,
                BoxShadowLight,
                BoxShadowLight,
                BoxShadowLight,
                BoxShadowLight,
                BoxShadowLight,
                BoxShadowLight,
                BoxShadowLight
            ]
        },
        LayoutProperties = LayoutProperties,
        ZIndex = ZIndex
    };

    /// <summary>
    /// Dark theme for the HUI
    /// </summary>
    public static readonly MudTheme Dark = new()
    {
        Typography = Typo,
        PaletteDark = new PaletteDark
        {
            Black = "#000000",
            White = "#ffffffff",
            Primary = "#1E7194",
            PrimaryContrastText = "#ffffffff",
            Secondary = "#1FA6E0",
            SecondaryContrastText = "rgba(255,255,255,1)",
            Tertiary = "#1ec8a5ff",
            TertiaryContrastText = "#ffffffff",
            Info = "#1FA6E0",
            InfoContrastText = "#000000",
            Success = "#52FF00",
            SuccessContrastText = "#000000",
            Warning = "#FFC700",
            WarningContrastText = "#000000",
            Error = "#F94229",
            ErrorContrastText = "#000000",
            Dark = "#181818",
            DarkContrastText = "#ffffffff",
            TextPrimary = "#EFEFEF",
            TextSecondary = "#EFEFEF99",
            TextDisabled = "#ffffff33",
            ActionDefault = "#adadb1ff",
            ActionDisabled = "#ffffff42",
            ActionDisabledBackground = "#ffffff1e",
            Background = "#101010",
            BackgroundGray = "#353535",
            Surface = "#1F1F1F",
            DrawerBackground = "#101010",
            DrawerText = "#EFEFEFe6",
            DrawerIcon = "#EFEFEFb2",
            AppbarBackground = "#101010",
            AppbarText = "#ffffffb2",
            LinesDefault = "#ffffff1e",
            LinesInputs = "#505050",
            TableLines = "#ffffff1e",
            TableStriped = "#272727",
            TableHover = "#1E7194",
            Divider = "#ffffff33",
            DividerLight = "#ffffff",
            PrimaryDarken = "#005174",
            PrimaryLighten = "#3e9ec6",
            SecondaryDarken = "rgb(255,31,105)",
            SecondaryLighten = "rgb(255,102,153)",
            TertiaryDarken = "rgb(25,169,140)",
            TertiaryLighten = "rgb(42,223,187)",
            InfoDarken = "#0787be",
            InfoLighten = "#38bbf4",
            SuccessDarken = "#40c800",
            SuccessLighten = "#52ff00",
            WarningDarken = "#cea100",
            WarningLighten = "#ffd234",
            ErrorDarken = "#cc2711",
            ErrorLighten = "#ff6651",
            DarkDarken = "#141414",
            DarkLighten = "#434343",
            HoverOpacity = 0.1,
            GrayDefault = "#1F1F1F",
            GrayLight = "#272727",
            GrayLighter = "#2F2F2F",
            GrayDark = "#181818",
            GrayDarker = "#0A0A0A",
            OverlayDark = "rgba(33,33,33,0.4980392156862745)",
            OverlayLight = "rgba(255,255,255,0.4980392156862745)",
            Skeleton = "rgba(255,255,255,0.10980392156862745)"
        },
        Shadows = new Shadow
        {
            Elevation =
            [
                "",
                "0px 0px 0px 1px #2E2E2E",
                BoxShadowDark,
                BoxShadowDark,
                "0px 0px 0px 1px #2E2E2E, 0px 0.8px 3.6px 0px rgba(0, 0, 0, 0.02), 0px 1.7px 6.4px 0px rgba(0, 0, 0, 0.02), 0px 3px 10.4px 0px rgba(0, 0, 0, 0.02), 0px 5.3px 16.2px 0px rgba(0, 0, 0, 0.03), 0px 13px 31px 0px rgba(0, 0, 0, 0.05), 0px -12px 12px -12px rgba(0, 0, 0, 0.12) inset;",
                BoxShadowDark,
                BoxShadowDark,
                BoxShadowDark,
                "0px 0px 12px 6px rgba(0, 0, 0, 0.15), 0 0 0 999em rgba(0,0,0,0.4);",
                BoxShadowDark,
                BoxShadowDark,
                BoxShadowDark,
                BoxShadowDark,
                BoxShadowDark,
                BoxShadowDark,
                BoxShadowDark,
                BoxShadowDark,
                BoxShadowDark,
                BoxShadowDark,
                BoxShadowDark,
                BoxShadowDark,
                BoxShadowDark,
                BoxShadowDark,
                BoxShadowDark,
                BoxShadowDark,
                BoxShadowDark
            ]
        },
        LayoutProperties = LayoutProperties,
        ZIndex = ZIndex
    };
}
