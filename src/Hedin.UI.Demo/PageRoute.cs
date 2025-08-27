namespace Hedin.UI.Demo
{
    public record PageRoute
    {
        public const string Translations = "/translations";
        
        public const string Demo = "/demo";
        public const string DemoPage = $"{Demo}/demo-page";
        public const string DemoSubPage = $"{DemoPage}/demo-sub-page";
        public const string DemoSubPage2 = $"{DemoPage}/demo-sub-page2";

        public const string DemoStateManagement = $"/state-management";
        public const string DemoStateManagementSubPage = $"{DemoStateManagement}/sub-page";

        public const string Guidelines = "/guidelines";

        public const string AIChatBot = $"albot";


        public const string Components = "/components";
        
        public const string AttachmentCard = $"{Components}/attachment-card";
        public const string AppBar = $"{Components}/app-bar";
        public const string BlogPost = $"{Components}/blog-post";
        public const string InformationMessage = $"{Components}/info-msg";
        public const string DataGrid = $"{Components}/data-grid";
        public const string Page = $"{Components}/page";
        public const string Dialog = $"{Components}/dialog";
        public const string Frame = $"{Components}/frame";
        public const string Maps = $"{Components}/maps";
        public const string MainContainer = $"{Components}/main-container";
        public const string Tooltip = $"{Components}/tooltip";
        public const string Charts = $"{Components}/charts";
        public const string ChatPanel = $"{Components}/chat-panel";
        public const string StatusChip = $"{Components}/status-chip";
        public const string SkeletonList = $"{Components}/skeleton-list";
        public const string ReconnectModal = $"{Components}/reconnect";

        public const string Module = $"{Components}/module";
        public const string ModulePage = $"{Module}/module-comp";
        public const string Links = $"{Module}/links";
        public const string Contacts = $"{Module}/contacts";
        public const string Documents = $"{Module}/documents";


        public const string Buttons = $"{Components}/buttons";
        public const string Button = $"{Buttons}/button";
        public const string IconButton = $"{Buttons}/icon-button";
        public const string BackButton = $"{Buttons}/back-button";

        public const string Inputs = $"{Components}/inputs";
        public const string InputButton = $"{Inputs}/input-button";
        public const string InputRow = $"{Inputs}/input-row";
        public const string MultiSelect = $"{Inputs}/multi-select";

        public const string Menus = $"{Components}/menus";
        public const string NavMenu = $"{Menus}/nav-menu";
        public const string NavMenuDrawer = $"{Menus}/nav-menu-drawer";
        public const string NavMenuHorizontal = $"{Menus}/nav-menu-horizontal";
        
        public const string Breadcrumbs = $"{Components}/breadcrumbs";
        public const string SuggestionBox = $"{Components}/suggestionbox";
        public const string GettingStarted = $"getting-started";


    }
}
