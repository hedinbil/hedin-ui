using Blazored.LocalStorage;

namespace Hedin.UI.Services
{
    public enum ThemeMode
    {
        Light,
        Dark
    }

    public class HUILocalStorageSetting
    {
        public List<HUILocalStorageTableSetting> TableSettings { get; set; } = new();
        public ThemeMode Theme { get; set; } = ThemeMode.Dark;
    }

    public class HUILocalStorageTableSetting
    {
        public string TableId { get; set; }
        public Dictionary<string, int> ColumnOrder { get; set; }
        public Dictionary<string, bool> ColumnVisibility { get; set; }
    }


    public interface ILocalStorageSettingsService
    {
        public Task<HUILocalStorageSetting> GetSettings();
        public Task SetSettings(HUILocalStorageSetting settings);
        public Task ClearSettings();
    }

    public class LocalStorageSettingsService : ILocalStorageSettingsService
    {
        private const string _settingsName = "hui"; 
        private ILocalStorageService _localStorageService;

        public LocalStorageSettingsService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        /// <summary>
        /// Get current local storage user settings.
        /// If settings does not exist, they will be initialized with empty values. This is to reduce null-check errors and is somewhat comparable to fetch data from a non-existing database table.
        /// </summary>
        /// <returns></returns>
        public async Task<HUILocalStorageSetting> GetSettings()
        {
            var settings = await _localStorageService.GetItemAsync<HUILocalStorageSetting>(_settingsName);
            if (settings == null)
                settings = await InitializeSettings();
            return settings;
        }

        public async Task SetSettings(HUILocalStorageSetting settings)
        {
            await _localStorageService.SetItemAsync(_settingsName, settings);
        }

        public async Task ClearSettings()
        {
            await InitializeSettings();
        }

        private async Task<HUILocalStorageSetting> InitializeSettings()
        {
            var settings = new HUILocalStorageSetting();
            await SetSettings(settings);
            return settings;
        }
    }
}
