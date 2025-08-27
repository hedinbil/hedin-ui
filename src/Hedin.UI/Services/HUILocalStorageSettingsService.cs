using Blazored.LocalStorage;

namespace Hedin.UI.Services
{

    internal class HUILocalStorageSetting
    {
        public List<HUILocalStorageTableSetting> TableSettings { get; set; } = new();
    }

    internal class HUILocalStorageTableSetting
    {
        public string TableId { get; set; }
        public Dictionary<string, int> ColumnOrder { get; set; }
        public Dictionary<string, bool> ColumnVisibility { get; set; }
    }


    internal interface ILocalStorageSettingsService
    {
        public Task<HUILocalStorageSetting> GetSettings();
        public Task SetSettings(HUILocalStorageSetting settings);
        public Task ClearSettings();
    }

    internal class LocalStorageSettingsService : ILocalStorageSettingsService
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
