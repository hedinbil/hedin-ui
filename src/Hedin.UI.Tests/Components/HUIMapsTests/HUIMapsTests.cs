//using Bunit;
//using Shouldly;
//using Microsoft.Extensions.Configuration;
//using Microsoft.JSInterop;
//using Hedin.UI.Tests.Base;
//using MudBlazor;
//using Microsoft.AspNetCore.Components;
//using Microsoft.Extensions.DependencyInjection;

//namespace Hedin.UI.Tests.Components.HUIMapsTests;

//public class HUIMapsTests : UiTestBase
//{
//    private IRenderedComponent<HUIMaps> RenderComponentWithParams(
//        List<HUIMaps.HUIMapPoint>? points = null,
//        bool autoFit = true,
//        bool darkMode = false,
//        HUIMaps.HUIMapsControlOptions? controlOptions = null,
//        EventCallback<HUIMaps.HUIMapPoint>? onPointClick = default,
//        string width = "100%",
//        string height = "500px",
//        string? @class = null,
//        double? defaultLat = null,
//        double? defaultLng = null,
//        int? defaultZoom = null)
//    {
//        //Services.AddSingleton<IConfiguration>(new TestConfiguration());
//        Services.AddSingleton<IJSRuntime>(new TestJSRuntime());

//        return RenderComponentWithMudProviders<HUIMaps>(parameters => parameters
//            .Add(p => p.Points, points ?? new List<HUIMaps.HUIMapPoint>())
//            .Add(p => p.AutoFit, autoFit)
//            .Add(p => p.DarkMode, darkMode)
//            .Add(p => p.ControlOptions, controlOptions ?? new HUIMaps.HUIMapsControlOptions())
//            .Add(p => p.OnPointClick, onPointClick ?? EventCallback.Factory.Create<HUIMaps.HUIMapPoint>(this, _ => { }))
//            .Add(p => p.Width, width)
//            .Add(p => p.Height, height)
//            .Add(p => p.Class, @class)
//            .Add(p => p.DefaultLat, defaultLat)
//            .Add(p => p.DefaultLng, defaultLng)
//            .Add(p => p.DefaultZoom, defaultZoom)
//        );
//    }

//    [Fact]
//    public void Renders_Error_Message_When_No_API_Key()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams();

//        // Assert
//        cut.FindComponent<MudText>().ShouldNotBeNull();
//        cut.Markup.ShouldContain("No API key found");
//        cut.Markup.ShouldContain("HedinUI:GoogleMapsApiKey");
//    }

//    [Fact]
//    public void Renders_Map_Container_With_Correct_ID()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams();

//        // Assert
//        var mapContainer = cut.Find("div[id^='map-']");
//        mapContainer.ShouldNotBeNull();
//    }

//    [Fact]
//    public void Sets_Default_Width_And_Height()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams();

//        // Assert
//        var mapContainer = cut.Find("div[id^='map-']");
//        mapContainer.GetAttribute("style").ShouldContain("width:100%");
//        mapContainer.GetAttribute("style").ShouldContain("height:500px");
//    }

//    [Fact]
//    public void Sets_Custom_Width_And_Height()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(width: "800px", height: "600px");

//        // Assert
//        var mapContainer = cut.Find("div[id^='map-']");
//        mapContainer.GetAttribute("style").ShouldContain("width:800px");
//        mapContainer.GetAttribute("style").ShouldContain("height:600px");
//    }

//    [Fact]
//    public void Sets_Custom_Class()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(@class: "custom-map");

//        // Assert
//        var mapContainer = cut.Find("div[id^='map-']");
//        mapContainer.GetAttribute("class").ShouldBe("custom-map");
//    }

//    [Fact]
//    public void Sets_Default_AutoFit()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams();

//        // Assert
//        cut.Instance.AutoFit.ShouldBeTrue();
//    }

//    [Fact]
//    public void Sets_Custom_AutoFit()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(autoFit: false);

//        // Assert
//        cut.Instance.AutoFit.ShouldBeFalse();
//    }

//    [Fact]
//    public void Sets_Default_DarkMode()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams();

//        // Assert
//        cut.Instance.DarkMode.ShouldBeFalse();
//    }

//    [Fact]
//    public void Sets_Custom_DarkMode()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(darkMode: true);

//        // Assert
//        cut.Instance.DarkMode.ShouldBeTrue();
//    }

//    [Fact]
//    public void Sets_Default_Lat_Lng_Zoom()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams();

//        // Assert
//        cut.Instance.DefaultLat.ShouldBeNull();
//        cut.Instance.DefaultLng.ShouldBeNull();
//        cut.Instance.DefaultZoom.ShouldBeNull();
//    }

//    [Fact]
//    public void Sets_Custom_Lat_Lng_Zoom()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(
//            defaultLat: 59.3293,
//            defaultLng: 18.0686,
//            defaultZoom: 10
//        );

//        // Assert
//        cut.Instance.DefaultLat.ShouldBe(59.3293);
//        cut.Instance.DefaultLng.ShouldBe(18.0686);
//        cut.Instance.DefaultZoom.ShouldBe(10);
//    }

//    [Fact]
//    public void Sets_Default_Control_Options()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams();

//        // Assert
//        cut.Instance.ControlOptions.ShouldNotBeNull();
//        cut.Instance.ControlOptions.DisableDefaultUI.ShouldBeTrue();
//    }

//    [Fact]
//    public void Sets_Custom_Control_Options()
//    {
//        // Arrange
//        var controlOptions = new HUIMaps.HUIMapsControlOptions
//        {
//            DisableDefaultUI = false,
//            MapTypeControl = true,
//            FullscreenControl = true,
//            StreetViewControl = false,
//            RotateControl = true,
//            ZoomControl = true,
//            ClickableIcons = false,
//            MinZoom = 5,
//            MaxZoom = 15
//        };

//        // Act
//        var cut = RenderComponentWithParams(controlOptions: controlOptions);

//        // Assert
//        cut.Instance.ControlOptions.ShouldBe(controlOptions);
//        cut.Instance.ControlOptions.DisableDefaultUI.ShouldBeFalse();
//                 cut.Instance.ControlOptions.MapTypeControl.ShouldBe(true);
//         cut.Instance.ControlOptions.FullscreenControl.ShouldBe(true);
//         cut.Instance.ControlOptions.StreetViewControl.ShouldBe(false);
//         cut.Instance.ControlOptions.RotateControl.ShouldBe(true);
//         cut.Instance.ControlOptions.ZoomControl.ShouldBe(true);
//         cut.Instance.ControlOptions.ClickableIcons.ShouldBe(false);
//        cut.Instance.ControlOptions.MinZoom.ShouldBe(5);
//        cut.Instance.ControlOptions.MaxZoom.ShouldBe(15);
//    }

//    [Fact]
//    public void Renders_Empty_Points_List()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(points: new List<HUIMaps.HUIMapPoint>());

//        // Assert
//        cut.Instance.Points.ShouldBeEmpty();
//    }

//    [Fact]
//    public void Renders_Points_With_Default_Values()
//    {
//        // Arrange
//        var points = new List<HUIMaps.HUIMapPoint>
//        {
//            new HUIMaps.HUIMapPoint()
//        };

//        // Act
//        var cut = RenderComponentWithParams(points: points);

//        // Assert
//        cut.Instance.Points.ShouldHaveSingleItem();
//        var point = cut.Instance.Points[0];
//        point.Id.ShouldNotBeEmpty();
//        point.Lat.ShouldBe(0);
//        point.Lng.ShouldBe(0);
//        point.Heading.ShouldBe(0);
//        point.Icon.ShouldBeNull();
//        point.IconSize.ShouldBeNull();
//        point.IconColor.ShouldBeNull();
//        point.Badge.ShouldBeNull();
//        point.BadgeSize.ShouldBeNull();
//        point.BadgeColor.ShouldBeNull();
//        point.Data.ShouldBeNull();
//    }

//    [Fact]
//    public void Renders_Points_With_Custom_Values()
//    {
//        // Arrange
//        var customData = new { Name = "Test Point", Value = 42 };
//        var points = new List<HUIMaps.HUIMapPoint>
//        {
//            new HUIMaps.HUIMapPoint
//            {
//                Id = "custom-id",
//                Lat = 59.3293,
//                Lng = 18.0686,
//                Heading = 45.0,
//                Icon = "custom-icon.svg",
//                IconSize = 32,
//                IconColor = "#FF0000",
//                Badge = "badge.svg",
//                BadgeSize = 16,
//                BadgeColor = "#00FF00",
//                Data = customData
//            }
//        };

//        // Act
//        var cut = RenderComponentWithParams(points: points);

//        // Assert
//        cut.Instance.Points.ShouldHaveSingleItem();
//        var point = cut.Instance.Points[0];
//        point.Id.ShouldBe("custom-id");
//        point.Lat.ShouldBe(59.3293);
//        point.Lng.ShouldBe(18.0686);
//        point.Heading.ShouldBe(45.0);
//        point.Icon.ShouldBe("custom-icon.svg");
//        point.IconSize.ShouldBe(32);
//        point.IconColor.ShouldBe("#FF0000");
//        point.Badge.ShouldBe("badge.svg");
//        point.BadgeSize.ShouldBe(16);
//        point.BadgeColor.ShouldBe("#00FF00");
//        point.Data.ShouldBe(customData);
//    }

//    [Fact]
//    public void Renders_Multiple_Points()
//    {
//        // Arrange
//        var points = new List<HUIMaps.HUIMapPoint>
//        {
//            new HUIMaps.HUIMapPoint { Id = "point1", Lat = 59.3293, Lng = 18.0686 },
//            new HUIMaps.HUIMapPoint { Id = "point2", Lat = 59.3294, Lng = 18.0687 },
//            new HUIMaps.HUIMapPoint { Id = "point3", Lat = 59.3295, Lng = 18.0688 }
//        };

//        // Act
//        var cut = RenderComponentWithParams(points: points);

//        // Assert
//        cut.Instance.Points.Count.ShouldBe(3);
//        cut.Instance.Points[0].Id.ShouldBe("point1");
//        cut.Instance.Points[1].Id.ShouldBe("point2");
//        cut.Instance.Points[2].Id.ShouldBe("point3");
//    }

//    [Fact]
//    public void Generates_Unique_Map_ID()
//    {
//        // Arrange & Act
//        var cut1 = RenderComponentWithParams();
//        var cut2 = RenderComponentWithParams();

//        // Assert
//        var mapId1 = cut1.Find("div[id^='map-']").GetAttribute("id");
//        var mapId2 = cut2.Find("div[id^='map-']").GetAttribute("id");
//        mapId1.ShouldNotBe(mapId2);
//        mapId1.ShouldStartWith("map-");
//        mapId2.ShouldStartWith("map-");
//    }

//    [Fact]
//    public void Handles_Null_Points()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(points: null);

//        // Assert
//        cut.Instance.Points.ShouldNotBeNull();
//        cut.Instance.Points.ShouldBeEmpty();
//    }

//    [Fact]
//    public void Handles_Null_Control_Options()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(controlOptions: null);

//        // Assert
//        cut.Instance.ControlOptions.ShouldNotBeNull();
//    }

//    [Fact]
//    public void Handles_Empty_Class()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(@class: "");

//        // Assert
//        var mapContainer = cut.Find("div[id^='map-']");
//        mapContainer.GetAttribute("class").ShouldBe("");
//    }

//    [Fact]
//    public void Handles_Null_Class()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(@class: null);

//        // Assert
//        var mapContainer = cut.Find("div[id^='map-']");
//        mapContainer.GetAttribute("class").ShouldBeNull();
//    }

//    [Fact]
//    public void Handles_Zero_Values_For_Lat_Lng_Zoom()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(
//            defaultLat: 0.0,
//            defaultLng: 0.0,
//            defaultZoom: 0
//        );

//        // Assert
//        cut.Instance.DefaultLat.ShouldBe(0.0);
//        cut.Instance.DefaultLng.ShouldBe(0.0);
//        cut.Instance.DefaultZoom.ShouldBe(0);
//    }

//    [Fact]
//    public void Handles_Negative_Values_For_Lat_Lng()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(
//            defaultLat: -90.0,
//            defaultLng: -180.0
//        );

//        // Assert
//        cut.Instance.DefaultLat.ShouldBe(-90.0);
//        cut.Instance.DefaultLng.ShouldBe(-180.0);
//    }

//    [Fact]
//    public void Handles_Extreme_Values_For_Zoom()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(
//            defaultZoom: 21
//        );

//        // Assert
//        cut.Instance.DefaultZoom.ShouldBe(21);
//    }
//}

//// Test helpers
// public class TestConfiguration : IConfiguration
// {
//     public string? this[string key] => null;

//     public IEnumerable<IConfigurationSection> GetChildren() => Enumerable.Empty<IConfigurationSection>();

//     public IConfigurationSection GetSection(string key) => throw new NotImplementedException();
// }

//public class TestJSRuntime : IJSRuntime
//{
//    public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args) => throw new NotImplementedException();

//    public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args) => throw new NotImplementedException();
//}
