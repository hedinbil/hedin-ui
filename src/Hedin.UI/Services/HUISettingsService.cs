using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hedin.UI.Services
{
    public interface IHUISettingsService
    {
        /// <summary>
        /// Resets all settings to default values
        /// </summary>
        /// <returns></returns>
        public Task ResetAsync();
    }
    internal class HUISettingsService(ILocalStorageSettingsService _huiLocalStorageService) : IHUISettingsService
    {
        public async Task ResetAsync()
        {
            await _huiLocalStorageService.ClearSettings();
        }
    }
}
