# Installation Guide

This library is wrapping MudBlazor. Original installation guides can be found **[here](https://mudblazor.com/getting-started/installation)**. However, using Hedin.UI this guide needs to be followed.

## Install the package

Install the latest version of the NuGet **Hedin.UI**.

## Register Services

Add the following in `Program.cs`
```
using Hedin.UI;
builder.Services.AddUIConfiguration(builder.Configuration);
```

## Add configuration

Add the following in `appsettings.json`
```
  "HedinUI": {
    "AppTitle": "Hedin UI"          // Applies to PageTitle using HUIPage, AppSwitcher and more.
    "ShowDevEnvWarning":  true,     //Will show a top banner for dev/stage/test, should not be true in prod
    "GoogleMapsApiKey": ""          //Required with HUIMaps component
  }
```

## **Add font and style references**

Add the following to your HTML head section, it's either `index.html` or `_Layout.cshtml`/`_Host.cshtml` depending on whether you're running WebAssembly or Server.
```
<link href="https://fonts.googleapis.com/css2?family=IBM+Plex+Sans:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;1,100;1,200;1,300;1,400;1,500;1,600;1,700&display=swap" rel="stylesheet">
<link href="_content/MudBlazor/MudBlazor.min.css" rel="stylesheet" />
<link href="_content/Hedin.UI/Hedin.UI.bundle.scp.css" rel="stylesheet" />
<link href="_content/Hedin.UI/css/hedin-ui.css" rel="stylesheet" />
```

## Add script reference
Add the following to your HTML beside the default blazor script
```
<script src="_content/MudBlazor/MudBlazor.min.js"></script>
<script src="_content/Hedin.UI/js/hedin-ui.js"></script>
```

## **Add Imports**
Add the following in your `_Imports.razor`
```
@using MudBlazor
@using Hedin.UI  //Or use the full namespace when using the components. e.g. HUI.HUIButton
@using Hedin.UI.Services
```

## **Register theme**
Add the following to the top of MainLayout.razor.
```
<HUIThemeProvider Theme="HUITheme.Dark" />
<MudPopoverProvider/>
<MudDialogProvider CloseOnEscapeKey="false" BackdropClick="true" FullWidth="true" CloseButton="false" />
<MudSnackbarProvider />
```

You may override theme parameters and colors and utilize the Light theme. Please read more about how to configure your theme in our [Guidelines](/guidelines) section.

# Usage
This library has its own custom components as well as exposing the original MudBlazor components. Both can be used.

## HUI
Try out the HUIButton button component!
```
<HUIButton Color="Color.Primary" Variant="Variant.Filled">I'm a HUI button</HUIButton>
```

## MudBlazor
Try out the MudBlazor button component!
```
<MudButton Color="Color.Primary" Variant="Variant.Filled">I'm a MudBlazor button</MudButton>
```
