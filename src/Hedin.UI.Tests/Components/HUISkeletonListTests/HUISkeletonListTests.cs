//using Bunit;
//using Shouldly;
//using Hedin.UI.Tests.Base;
//using MudBlazor;

//namespace Hedin.UI.Tests.Components.HUISkeletonListTests;

//public class HUISkeletonListTests : UiTestBase
//{
//    private IRenderedComponent<HUISkeletonList> RenderComponentWithParams(
//        List<HUISkeletonList.SkeletonItem>? items = null,
//        SkeletonType skeletonType = SkeletonType.Text,
//        int count = 3,
//        string? @class = null)
//    {
//        return RenderComponentWithMudProviders<HUISkeletonList>(parameters => parameters
//            .Add(p => p.Items, items ?? new List<HUISkeletonList.SkeletonItem>())
//            .Add(p => p.SkeletonType, skeletonType)
//            .Add(p => p.Count, count)
//            .Add(p => p.Class, @class)
//        );
//    }

//    [Fact]
//    public void Renders_Default_Count_Of_Skeletons()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams();

//        // Assert
//        cut.FindAll(".mud-skeleton").Count.ShouldBe(8);
//    }

//    [Fact]
//    public void Renders_Custom_Count_Of_Skeletons()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(count: 5);

//        // Assert
//        cut.FindAll(".mud-skeleton").Count.ShouldBe(5);
//    }

//    [Fact]
//    public void Renders_Zero_Skeletons_When_Count_Is_Zero()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(count: 0);

//        // Assert
//        cut.FindAll(".mud-skeleton").Count.ShouldBe(0);
//    }

//    [Fact]
//    public void Renders_Negative_Count_As_Zero_Skeletons()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(count: -3);

//        // Assert
//        cut.FindAll(".mud-skeleton").Count.ShouldBe(0);
//    }

//    [Fact]
//    public void Sets_Default_Height_On_Skeletons()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams();

//        // Assert
//        var skeleton = cut.Find(".mud-skeleton");
//        skeleton.GetAttribute("style").ShouldContain("height: 40px");
//    }

//    [Fact]
//    public void Sets_Custom_Height_On_Skeletons()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(height: "60px");

//        // Assert
//        var skeleton = cut.Find(".mud-skeleton");
//        skeleton.GetAttribute("style").ShouldContain("height: 60px");
//    }

//    [Fact]
//    public void Sets_Empty_Width_On_Skeletons_By_Default()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams();

//        // Assert
//        var skeleton = cut.Find(".mud-skeleton");
//        skeleton.GetAttribute("style").ShouldNotContain("width:");
//    }

//    [Fact]
//    public void Sets_Custom_Width_On_Skeletons()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(width: "200px");

//        // Assert
//        var skeleton = cut.Find(".mud-skeleton");
//        skeleton.GetAttribute("style").ShouldContain("width: 200px");
//    }

//    [Fact]
//    public void Sets_Default_Class_On_Skeletons()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams();

//        // Assert
//        var skeleton = cut.Find(".mud-skeleton");
//        skeleton.GetAttribute("class").ShouldBe("mud-skeleton");
//    }

//    [Fact]
//    public void Sets_Custom_Class_On_Skeletons()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(@class: "custom-class");

//        // Assert
//        var skeleton = cut.Find(".mud-skeleton");
//        skeleton.GetAttribute("class").ShouldBe("mud-skeleton custom-class");
//    }

//    [Fact]
//    public void Sets_Default_SkeletonType_On_Skeletons()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams();

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.Rectangle);
//    }

//    [Fact]
//    public void Sets_Custom_SkeletonType_On_Skeletons()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.Circular);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.Circular);
//    }

//    [Fact]
//    public void Renders_Multiple_Skeletons_With_Same_Properties()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(count: 3, height: "50px", width: "100px", @class: "test-class");

//        // Assert
//        var skeletons = cut.FindAll(".mud-skeleton");
//        skeletons.Count.ShouldBe(3);

//        foreach (var skeleton in skeletons)
//        {
//            skeleton.GetAttribute("style").ShouldContain("height: 50px");
//            skeleton.GetAttribute("style").ShouldContain("width: 100px");
//            skeleton.GetAttribute("class").ShouldBe("mud-skeleton test-class");
//        }
//    }

//    [Fact]
//    public void Renders_Skeletons_With_Text_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.Text);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.Text);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_Avatar_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.Avatar);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.Avatar);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_Table_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.Table);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.Table);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_Form_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.Form);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.Form);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_Article_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.Article);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.Article);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_Button_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.Button);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.Button);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_Card_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.Card);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.Card);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_ListItem_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.ListItem);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.ListItem);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_H1_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.H1);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.H1);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_H2_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.H2);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.H2);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_H3_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.H3);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.H3);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_H4_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.H4);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.H4);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_H5_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.H5);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.H5);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_H6_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.H6);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.H6);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_Paragraph_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.Paragraph);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.Paragraph);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_Image_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.Image);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.Image);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_Input_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.Input);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.Input);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_Video_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.Video);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.Video);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_Radio_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.Radio);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.Radio);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_Checkbox_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.Checkbox);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.Checkbox);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_Switch_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.Switch);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.Switch);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_Select_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.Select);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.Select);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_DatePicker_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.DatePicker);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.DatePicker);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_TimePicker_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.TimePicker);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.TimePicker);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_DateTimePicker_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.DateTimePicker);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.DateTimePicker);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_Autocomplete_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.Autocomplete);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.Autocomplete);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_Slider_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.Slider);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.Slider);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_Rating_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.Rating);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.Rating);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_Progress_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.Progress);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.Progress);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_LinearProgress_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.LinearProgress);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.LinearProgress);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_CircularProgress_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.CircularProgress);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.CircularProgress);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_Spinner_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.Spinner);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.Spinner);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_Icon_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.Icon);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.Icon);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_AvatarText_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.AvatarText);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.AvatarText);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_AvatarIcon_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.AvatarIcon);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.AvatarIcon);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_AvatarImage_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.AvatarImage);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.AvatarImage);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_AvatarVariant_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.AvatarVariant);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.AvatarVariant);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_AvatarGroup_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.AvatarGroup);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.AvatarGroup);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_AvatarGroupItem_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.AvatarGroupItem);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.AvatarGroupItem);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_AvatarGroupItemVariant_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.AvatarGroupItemVariant);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.AvatarGroupItemVariant);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_AvatarGroupItemIcon_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.AvatarGroupItemIcon);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.AvatarGroupItemIcon);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_AvatarGroupItemText_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.AvatarGroupItemText);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.AvatarGroupItemText);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_AvatarGroupItemImage_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.AvatarGroupItemImage);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.AvatarGroupItemImage);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_AvatarGroupItemVariantIcon_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.AvatarGroupItemVariantIcon);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.AvatarGroupItemVariantIcon);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_AvatarGroupItemVariantText_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.AvatarGroupItemVariantText);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.AvatarGroupItemVariantText);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_AvatarGroupItemVariantImage_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.AvatarGroupItemVariantImage);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.AvatarGroupItemVariantImage);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_AvatarGroupItemIconText_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.AvatarGroupItemIconText);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.AvatarGroupItemIconText);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_AvatarGroupItemIconImage_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.AvatarGroupItemIconImage);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.AvatarGroupItemIconImage);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_AvatarGroupItemTextImage_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.AvatarGroupItemTextImage);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.AvatarGroupItemTextImage);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_AvatarGroupItemVariantIconText_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.AvatarGroupItemVariantIconText);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.AvatarGroupItemVariantIconText);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_AvatarGroupItemVariantIconImage_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.AvatarGroupItemVariantIconImage);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.AvatarGroupItemVariantIconImage);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_AvatarGroupItemVariantTextImage_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.AvatarGroupItemVariantTextImage);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.AvatarGroupItemVariantTextImage);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_AvatarGroupItemIconTextImage_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.AvatarGroupItemIconTextImage);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.AvatarGroupItemIconTextImage);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_AvatarGroupItemVariantIconTextImage_SkeletonType()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(skeletonType: SkeletonType.AvatarGroupItemVariantIconTextImage);

//        // Assert
//        var skeleton = cut.FindComponent<MudSkeleton>();
//        skeleton.Instance.SkeletonType.ShouldBe(SkeletonType.AvatarGroupItemVariantIconTextImage);
//    }

//    [Fact]
//    public void Renders_Skeletons_With_AvatarGroupItemVariantIconTextImage_SkeletonType_And_Custom_Properties()
//    {
//        // Arrange & Act
//        var cut = RenderComponentWithParams(
//            count: 10,
//            height: "80px",
//            width: "300px",
//            @class: "custom-skeleton",
//            skeletonType: SkeletonType.AvatarGroupItemVariantIconTextImage
//        );

//        // Assert
//        var skeletons = cut.FindAll(".mud-skeleton");
//        skeletons.Count.ShouldBe(10);

//        foreach (var skeleton in skeletons)
//        {
//            skeleton.GetAttribute("style").ShouldContain("height: 80px");
//            skeleton.GetAttribute("style").ShouldContain("width: 300px");
//            skeleton.GetAttribute("class").ShouldBe("mud-skeleton custom-skeleton");
//        }

//        var skeletonComponent = cut.FindComponent<MudSkeleton>();
//        skeletonComponent.Instance.SkeletonType.ShouldBe(SkeletonType.AvatarGroupItemVariantIconTextImage);
//    }
//}
