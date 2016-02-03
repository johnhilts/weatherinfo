using Infrastructure.Core;

namespace WeatherInfo.Application.Services
{
    public class BaseService
    {
        protected SettingsService SettingsService { get; private set; }
        protected Settings Settings { get; private set; }

        public BaseService(SettingsService settingsService)
        {
            SettingsService = settingsService;
            Settings = new Settings { KeyPath = settingsService.GetKeyPath(), };
        }
    }
}
