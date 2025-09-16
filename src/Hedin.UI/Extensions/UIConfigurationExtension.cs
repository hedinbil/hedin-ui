using Blazored.LocalStorage;
using Hedin.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MudBlazor;
using MudBlazor.Services;

namespace Hedin.UI
{
    public static class UIConfigurationExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddUIConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.SnackbarVariant = MudBlazor.Variant.Outlined;
                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.VisibleStateDuration = 10000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.MaximumOpacity = 100;
            });
            ApplyDefaultVariants();
            services.AddScoped<IHUIPageHelper, HUIPageHelper>();
            services.Configure<TitleService>(configuration.GetSection("HedinUI"));
            services.AddTransient<ILocalStorageSettingsService, LocalStorageSettingsService>();
            services.TryAddTransient<InternaHUIlLocalizer>();
            services.AddBlazoredLocalStorage();
            services.AddTransient<ITableStateService, TableStateService>();
            services.AddTransient<IHUISettingsService, HUISettingsService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<ISEOService, SEOService>();
            services.AddScoped<ISitemapService, SitemapService>();

        }

        private static void ApplyDefaultVariants()
        {
            MudGlobal.InputDefaults.Variant = Variant.Outlined;
            MudGlobal.InputDefaults.Margin = Margin.Dense;
            MudGlobal.TooltipDefaults.Duration = TimeSpan.FromMilliseconds(0);
            //MudGlobal.ButtonDefaults.Variant = Variant.Outlined;
        }
    }
}
